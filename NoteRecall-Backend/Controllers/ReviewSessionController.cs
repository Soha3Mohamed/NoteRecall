using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NoteRecall_Application.DTOs.QuestionDTOs;
using NoteRecall_Application.DTOs.ReviewSessionDTOs;
using NoteRecall_Application.ServiceInterfaces;
using NoteRecall_Core.Common;

namespace NoteRecall_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewSessionController : ControllerBase
    {
        private readonly ILogger<ReviewSessionController> _logger;
        private readonly IReviewSessionService _reviewSessionService;

        public ReviewSessionController(ILogger<ReviewSessionController> logger, IReviewSessionService reviewSessionService)
        {
            _logger = logger;
            this._reviewSessionService = reviewSessionService;
        }

        // GET: api/reviewSessions/userId/{userId}
        [HttpGet("userId/{userId:int}")]
        public async Task<ActionResult<ServiceResult<IEnumerable<ReviewSessionResponseDTO>>>> GetById(int id)
        {
            var result = await _reviewSessionService.GetReviewSessionByIdAsync(id);
            if (!result.Success)
                return NotFound(result);

            return Ok(result);
        }

        // GET: api/reviewSessions/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ServiceResult<ReviewSessionResponseDTO>>> GetByNoteId(int NoteId)
        {
            var result = await _reviewSessionService.GetReviewSessionsByUserIdAsync(NoteId);
            if (!result.Success)
                return NotFound(result);

            return Ok(result);
        }
    }
}
