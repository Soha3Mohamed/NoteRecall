using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NoteRecall_Application.DTOs.NoteDTOs;
using NoteRecall_Application.DTOs.UserDTOs;
using NoteRecall_Application.ServiceInterfaces;
using NoteRecall_Core.Common;

namespace NoteRecall_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly ILogger<NoteController> _logger;
        private readonly INoteService _noteService;

        public NoteController(ILogger<NoteController> logger, INoteService noteService)
        {
            _logger = logger;
            this._noteService = noteService;
        }

        // GET: api/notes
        [HttpGet("userId/{userId:int}")]
        public async Task<ActionResult<ServiceResult<IEnumerable<NoteResponseDTO>>>> GetByUserId(int userId)
        {
            var result = await _noteService.GetNotesByUserIdAsync(userId);
            if (!result.Success)
                return NotFound(result);

            return Ok(result);
        }

        // GET: api/notes/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ServiceResult<NoteResponseDTO>>> GetById(int id)
        {
            var result = await _noteService.GetNoteByIdAsync(id);
            if (!result.Success)
                return NotFound(result);

            return Ok(result);
        }
      
        [HttpPost("add")]
        public async Task<IActionResult> Register(int userId, NoteRequestDTO noteRequestDTO)
        {
            var result = await _noteService.AddNoteAsync(userId, noteRequestDTO);
            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Data);
        }

        [HttpPost("update")]
        [Authorize]
        public async Task<IActionResult> Update(int userId, NoteUpdateDTO noteUpdateDTO)
        {
            var result = await _noteService.UpdateNoteAsync(userId, noteUpdateDTO);
            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }
            return Ok(result.Data);
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> Delete(int userId,int id)
        {
            var result = await _noteService.DeleteNoteAsync(userId, id);
            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(result.Data);
        }
    }
}
