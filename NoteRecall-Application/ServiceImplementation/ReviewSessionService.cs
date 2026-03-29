using AutoMapper;
using Microsoft.Extensions.Logging;
using NoteRecall_Application.DTOs.ReviewSessionDTOs;
using NoteRecall_Application.ServiceInterfaces;
using NoteRecall_Core.Common;
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

        public ReviewSessionService(ILogger<UserService> logger, IReviewSessionRepository reviewSessionRepository, IMapper mapper, IUserRepository userRepository)
        {
            _logger = logger;
            _reviewSessionRepository = reviewSessionRepository;
            _mapper = mapper;
            _userRepository = userRepository;
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


    }
}
