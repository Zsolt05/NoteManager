using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Text.Json;
using NoteManager.Shared;
using System.Net.Http.Json;

namespace NoteManager.Client.Services
{
    public interface IAuthService
    {
        Task<bool> Login(LoginDto loginModel);
        Task Logout();
    }

    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly ILocalStorageService _localStorage;
        private readonly HttpInterceptorService _httpInterceptorService;

        public AuthService(HttpClient httpClient,
                           AuthenticationStateProvider authenticationStateProvider,
                           ILocalStorageService localStorage,
                           HttpInterceptorService httpInterceptorService)
        {
            _httpClient = httpClient;
            _authenticationStateProvider = authenticationStateProvider;
            _localStorage = localStorage;
            _httpInterceptorService = httpInterceptorService;
        }

        public async Task<bool> Login(LoginDto loginModel)
        {
            var response = await _httpInterceptorService.PostAsync<LoginDto, AuthResponseDto>("/api/Auth/Login", loginModel);
            if (response == null)
            {
                return false;
            }
            await _localStorage.SetItemAsync("authToken", response.Token);
            ((JwtAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(loginModel.UserName);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", response.Token);

            return true;
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("authToken");
            ((JwtAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsLoggedOut();
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }
    }
}
