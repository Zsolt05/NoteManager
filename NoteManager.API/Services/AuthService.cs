using AutoWrapper.Wrappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NoteManager.API.Entities;
using NoteManager.Shared;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace NoteManager.API.Services
{
    public interface IAuthService
    {
        Task Init(int count = 3);
        Task<AuthResponseDto> Login(LoginDto loginDto);
        Task<User?> GetUserByUsername(string username);
        string? GetUserId();
        string? GetUserName();
        Task<bool> UserExists(string userName);
    }

    public class AuthService : IAuthService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly NoteManagerDbContext _dbContext;
        private readonly IConfiguration _configuration;
        public AuthService(IHttpContextAccessor httpContextAccessor,
            NoteManagerDbContext dbContext, IConfiguration configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _dbContext = dbContext;
            _configuration = configuration;
        }

        public async Task Init(int count = 3)
        {
            for (int i = 1; i <= count; i++)
            {
                var userName = $"user{i}";
                if (await UserExists(userName))
                {
                    continue;
                }
                CreatePasswordHash($"password{i}", out byte[] passwordHash, out byte[] passwordSalt);
                var user = new User
                {
                    UserName = userName,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt
                };
                await _dbContext.Users!.AddAsync(user);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<User?> GetUserByUsername(string username)
        {
            var user = await _dbContext.Users!.Where(u => u.UserName.Equals(username))
                .FirstOrDefaultAsync();
            return user;
        }

        public string? GetUserId() => _httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.SerialNumber);
        public string? GetUserName() => _httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier);

        public async Task<AuthResponseDto> Login(LoginDto loginDto)
        {
            var user = await _dbContext.Users!.FirstOrDefaultAsync(x => x.UserName.ToLower().Equals(loginDto.UserName.ToLower()))
                ?? throw new ApiException("Rossz felhasználónév vagy jelszó");
            if (VerifyPasswordHash(loginDto.Password, user.PasswordHash!, user.PasswordSalt!))
            {
                return new AuthResponseDto
                {
                    Token = CreateToken(user),
                    UserName = user.UserName
                };
            }
            else
            {
                throw new ApiException("Nem megfelelő a jelszó");
            }
        }

        public async Task<bool> UserExists(string userName)
        {
            return await _dbContext.Users!.AnyAsync(user => user.UserName.ToLower().Equals(userName.ToLower()));
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.UserName),
                new Claim(ClaimTypes.SerialNumber, user.Id),
                new Claim(JwtRegisteredClaimNames.Iss, _configuration.GetSection("JwtSettings:Issuer").Value),
                new Claim(JwtRegisteredClaimNames.Aud, _configuration.GetSection("JwtSettings:Audience").Value)
            };

            /// <summary>
            /// A JWT kulcsot a konfigurációból olvassuk ki.
            /// </summary>
            /// <remarks>
            /// Amennyiben ezt a hibát kapjuk akkor az appsettings fájlban nincs rögzítve a Token kulcs.
            /// </remarks>
            /// <exception cref="Exception">Sikertelen bejelentkezés.</exception>
            var jwtKeyFromConfig = _configuration.GetSection("JwtSettings:Key").Value ?? throw new ApiException("Sikertelen bejelentkezés");
            bool successParse = int.TryParse(_configuration.GetSection("JwtSettings:DurationInDays").Value, out int jwtExpireFromConfig);
            if (!successParse)
            {
                throw new ApiException("Sikertelen bejelentkezés");
            }

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(jwtKeyFromConfig));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.UtcNow.AddDays(Convert.ToInt32(jwtExpireFromConfig)),
                    signingCredentials: cred
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}
