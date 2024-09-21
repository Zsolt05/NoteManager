using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NoteManager.API.Entities;
using NoteManager.API.Services;
using NoteManager.Shared;

namespace NoteManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NotesController : ControllerBase
    {
        private readonly INoteService _noteService;
        private readonly IMapper _mapper;

        public NotesController(INoteService noteService,
            IMapper mapper)
        {
            _noteService = noteService;
            _mapper = mapper;
        }

        // GET: api/Notes
        [HttpGet]
        [ProducesResponseType(typeof(List<NoteReadDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll([FromServices] IAuthService _authService)
        {
            var userId = _authService.GetUserId();
            if (userId == null)
            {
                return Unauthorized();
            }

            var notes = await _noteService.GetAll(userId);
            var mappedNotes = _mapper.Map<List<NoteReadDto>>(notes);
            return Ok(mappedNotes);
        }

        // GET api/Notes/5
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(NoteReadDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(string id)
        {
            var note = await _noteService.Get(id);
            var mapped = _mapper.Map<NoteReadDto>(note);
            return Ok(mapped);
        }

        // POST api/Notes
        [HttpPost]
        [ProducesResponseType(typeof(NoteReadDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Post([FromBody] NoteCreateDto createDto)
        {
            var note = _mapper.Map<Note>(createDto);
            var entity = await _noteService.Create(note);
            var mapped = _mapper.Map<NoteReadDto>(entity);
            return Ok(mapped);
        }

        // PUT api/Notes/5
        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(typeof(NoteReadDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Put(string id, [FromBody] NoteUpdateDto updateDto)
        {
            var note = _mapper.Map<Note>(updateDto);
            var entity = await _noteService.Update(id, note);
            var mapped = _mapper.Map<NoteReadDto>(entity);
            return Ok(mapped);
        }

        // DELETE api/Notes/5
        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(string id)
        {
            await _noteService.Delete(id);
            return Ok();
        }
    }
}
