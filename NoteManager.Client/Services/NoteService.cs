using NoteManager.Shared;

namespace NoteManager.Client.Services
{
    public interface INoteService
    {
        Task<List<NoteReadDto>> GetNotes();
        Task<NoteReadDto> GetNoteById(string id);
        Task<NoteReadDto> CreateNote(NoteCreateDto note);
        Task<NoteReadDto> UpdateNote(string id, NoteUpdateDto note);
        Task DeleteNote(string id);
    }

    public class NoteService : INoteService
    {
        private readonly HttpInterceptorService _httpInterceptorService;

        public NoteService(HttpInterceptorService httpInterceptorService)
        {
            _httpInterceptorService = httpInterceptorService;
        }

        public async Task<NoteReadDto> CreateNote(NoteCreateDto note)
        {
            return await _httpInterceptorService.PostAsync<NoteCreateDto, NoteReadDto>("/api/Notes", note);
        }

        public async Task DeleteNote(string id)
        {
            await _httpInterceptorService.DeleteAsync($"/api/Notes/{id}");
        }

        public async Task<NoteReadDto> GetNoteById(string id)
        {
            return await _httpInterceptorService.GetAsync<NoteReadDto>($"/api/Notes/{id}");
        }

        public async Task<List<NoteReadDto>> GetNotes()
        {
            return await _httpInterceptorService.GetAsync<List<NoteReadDto>>("/api/Notes");
        }

        public async Task<NoteReadDto> UpdateNote(string id, NoteUpdateDto note)
        {
            return await _httpInterceptorService.PutAsync<NoteUpdateDto, NoteReadDto>($"/api/Notes/{id}", note);
        }
    }
}
