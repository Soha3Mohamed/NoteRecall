using AutoMapper;
using Microsoft.Extensions.Logging;
using NoteRecall_Application.DTOs.ReviewResultDTOs;
using NoteRecall_Application.DTOs.ReviewSessionDTOs;
using NoteRecall_Application.ServiceInterfaces;
using NoteRecall_Core.Common;
using NoteRecall_Core.Entities;
using NoteRecall_Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace NoteRecall_Application.ServiceImplementation
{
    internal class ReviewSessionService : IReviewSessionService
    {
        private readonly ILogger<UserService> _logger;
        private readonly IReviewSessionRepository _reviewSessionRepository;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IQuestionRepository _questionRepo;
        private readonly SpacedRepetitionService _spacedRepetitionService;
        private readonly IReviewResultRepository _reviewResultRepo;

        public ReviewSessionService(ILogger<UserService> logger, IReviewSessionRepository reviewSessionRepository, IMapper mapper, IUserRepository userRepository, IQuestionRepository questionRepo, SpacedRepetitionService spacedRepetitionService, IReviewResultRepository reviewResultRepo)
        {
            _logger = logger;
            _reviewSessionRepository = reviewSessionRepository;
            _mapper = mapper;
            _userRepository = userRepository;
            _questionRepo = questionRepo;
            _spacedRepetitionService = spacedRepetitionService;
            _reviewResultRepo = reviewResultRepo;
        }

        public async Task<ServiceResult<ReviewSessionResponseDTO>> GetReviewSessionByIdAsync(int reviewSessionId)
        {
            var reviewSession = await _reviewSessionRepository.GetByIdAsync(reviewSessionId);
            if (reviewSession == null)
            {
                _logger.LogWarning("Review session with ID {ReviewSessionId} not found.", reviewSessionId);
                return ServiceResult<ReviewSessionResponseDTO>.Fail("Review session not found.");
            }
            var reviewSessionDto = _mapper.Map<ReviewSessionResponseDTO>(reviewSession);
            return ServiceResult<ReviewSessionResponseDTO>.Ok(reviewSessionDto);

        }

        public async Task<ServiceResult<IEnumerable<ReviewSessionResponseDTO>>> GetReviewSessionsByUserIdAsync(int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                _logger.LogWarning("User with ID {UserId} not found.", userId);
                return ServiceResult<IEnumerable<ReviewSessionResponseDTO>>.Fail("User not found.");
            }
            var reviewSessions = await _reviewSessionRepository.GetByUserIdAsync(userId);
            var reviewSessionDtos = _mapper.Map<IEnumerable<ReviewSessionResponseDTO>>(reviewSessions);
            return ServiceResult<IEnumerable<ReviewSessionResponseDTO>>.Ok(reviewSessionDtos);

        }

        //public Task<ServiceResult<ReviewSessionResponseDTO>> AddReviewSessionAsync(ReviewSessionRequestDTO reviewSessionRequest)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<ServiceResult<ReviewSessionResponseDTO>> UpdateReviewSessionRequestAsync(ReviewSessionRequestDTO reviewSessionRequest)
        //{
        //    throw new NotImplementedException();
        //}


        //public Task<ServiceResult<bool>> DeleteReviewSessionRequestAsync(int reviewSessionId)
        //{
        //    throw new NotImplementedException();
        //}

        public async Task<ServiceResult<ReviewResultResponseDTO>> SubmitAnswer(int sessionId, int questionId, int score)
        {
            var question = await _questionRepo.GetQuestionByIdAsync(questionId);

            // 1. Save result
            var result = new ReviewResult
            {
                QuestionId = questionId,
                ReviewSessionId = sessionId,
                SelfScore = score
            };

            await _reviewResultRepo.AddAsync(result);

            // 2. Update spaced repetition
            _spacedRepetitionService.UpdateSchedule(question, score);

            // 3. Save updated question
            await _questionRepo.UpdateQuestionAsync(question);

            var reviewSessionDto = _mapper.Map<ReviewResultResponseDTO>(result);
            return ServiceResult<ReviewResultResponseDTO>.Ok(reviewSessionDto);
        }
    }
}
