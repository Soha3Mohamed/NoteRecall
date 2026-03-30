using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NoteRecall_Application.DTOs.NoteDTOs;
using NoteRecall_Application.DTOs.QuestionDTOs;
using NoteRecall_Application.ServiceInterfaces;
using NoteRecall_Core.Common;

namespace NoteRecall_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly ILogger<QuestionController> _logger;
        private readonly IQuestionService _questionService;

        public QuestionController(ILogger<QuestionController> logger, IQuestionService questionService)
        {
            _logger = logger;
            this._questionService = questionService;
        }

        // GET: api/questions/userId/{userId}
        [HttpGet("userId/{userId:int}")]
        public async Task<ActionResult<ServiceResult<IEnumerable<QuestionResponseDTO>>>> GetById(int id)
        {
            var result = await _questionService.GetQuestionByIdAsync(id);
            if (!result.Success)
                return NotFound(result);

            return Ok(result);
        }

        // GET: api/questions/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ServiceResult<QuestionResponseDTO>>> GetByNoteId(int NoteId)
        {
            var result = await _questionService.GetQuestionsByNoteIdAsync(NoteId);
            if (!result.Success)
                return NotFound(result);

            return Ok(result);
        }

        
    }
}
