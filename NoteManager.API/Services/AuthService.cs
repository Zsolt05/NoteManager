using NoteManager.Shared;

namespace NoteManager.API.Services
{
    public interface IAuthService
    {
        Task<AuthResponseDto> Login(LoginDto loginDto);
    }
    public class AuthService : IAuthService
    {
        public Task<AuthResponseDto> Login(LoginDto loginDto)
        {
            throw new NotImplementedException();
        }
    }
}
