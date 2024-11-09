using NoteManager.Client.Components.Toast;
using System.Net.Http.Json;
using System.Net;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NoteManager.Client.Services
{
    public class HttpInterceptorService
    {
        private readonly HttpClient _httpClient;
        private readonly ToastService _toastService;

        public HttpInterceptorService(HttpClient httpClient, ToastService toastService)
        {
            _httpClient = httpClient;
            _toastService = toastService;
        }

        public async Task<T?> GetAsync<T>(string url)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(url);
            if (await HandleResponse(response))
            {
                var content = await response.Content.ReadAsStringAsync();
                var jsonObj = JObject.Parse(content);
                var result = jsonObj["result"].ToObject<T>();
                return result;
            }
            return default;
        }

        public async Task<TResult?> PostAsync<T, TResult>(string url, T data)
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync(url, data);
            if (await HandleResponse(response))
            {
                var content = await response.Content.ReadAsStringAsync();
                var jsonObj = JObject.Parse(content);
                var result = jsonObj["result"].ToObject<TResult>();
                return result;
            }
            return default;
        }

        public async Task<TResult?> PutAsync<T, TResult>(string url, T data)
        {
            HttpResponseMessage response = await _httpClient.PutAsJsonAsync(url, data);
            if (await HandleResponse(response))
            {
                var content = await response.Content.ReadAsStringAsync();
                var jsonObj = JObject.Parse(content);
                var result = jsonObj["result"].ToObject<TResult>();
                return result;
            }
            return default;
        }

        public async Task<HttpResponseMessage> DeleteAsync(string url)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync(url);
            await HandleResponse(response);
            return response;
        }

        private async Task<bool> HandleResponse(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {

                var content = await response.Content.ReadAsStringAsync();
                dynamic jsonObject = JsonConvert.DeserializeObject<dynamic>(content);
                string exceptionMessage = jsonObject?.responseException?.exceptionMessage;
                string errorMessage = response.StatusCode switch
                {
                    HttpStatusCode.Unauthorized => "Be kell jelentkezned a művelet végrehajtásához.",
                    HttpStatusCode.Forbidden => "Nincs jogosultságod a művelet végrehajtásához.",
                    HttpStatusCode.InternalServerError => "Szerver hiba történt, kérlek próbáld újra később.",
                    HttpStatusCode.BadRequest => exceptionMessage ?? "Hibás kérés. Kérlek ellenőrizd az adatokat.",
                    _ => $"Ismeretlen hiba: {response.StatusCode}"
                };

                _toastService.ShowError(errorMessage);
                return false;
            }
            else
            {
                return true;
            }
        }
    }

}
