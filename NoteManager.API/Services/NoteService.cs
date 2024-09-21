using Microsoft.EntityFrameworkCore;
using NoteManager.API.Entities;

namespace NoteManager.API.Services
{
    public interface INoteService
    {
        Task<List<Note>> GetAll(string userId);
        Task<Note> Get(string id);
        Task<Note> Create(Note note);
        Task<Note> Update(string id, Note note);
        Task Delete(string id);
    }

    public class NoteService : INoteService
    {
        private readonly NoteManagerDbContext _dbContext;
        private readonly IAuthService _authService;

        public NoteService(NoteManagerDbContext dbContext, IAuthService authService)
        {
            _dbContext = dbContext;
            _authService = authService;
        }

        public async Task<Note> Create(Note note)
        {
            await _dbContext.Notes!.AddAsync(note);
            note.UserId = _authService.GetUserId() ?? throw new Exception("A felhasználó nem található");
            await _dbContext.SaveChangesAsync();
            return note;
        }

        public async Task Delete(string id)
        {
            var note = await GetNoteById(id);
            _dbContext.Notes.Remove(note);
            await _dbContext.SaveChangesAsync();

        }

        public async Task<Note> Get(string id)
        {
            var note = await GetNoteById(id);
            return note;
        }

        public async Task<List<Note>> GetAll(string userId)
        {
            var notes = await _dbContext.Notes.Where(n => n.UserId == userId).ToListAsync();
            return notes;

        }

        public async Task<Note> Update(string id, Note note)
        {
            var existingNote = await GetNoteById(id);
            existingNote.Title = note.Title;
            existingNote.Content = note.Content;
            await _dbContext.SaveChangesAsync();
            return existingNote;
        }

        private async Task<Note> GetNoteById(string noteId)
        {
            var userId = _authService.GetUserId() ?? throw new Exception("A felhasználó nem található");
            var note = await _dbContext.Notes.FindAsync(noteId) ?? throw new Exception("A jegyzet nem található");
            if (note.UserId != userId)
            {
                throw new Exception("Nincs jogosultság a művelethez");
            }
            return note;
        }
    }
}
