using NoteRecall_Application.DTOs.ReviewSessionDTOs;
using NoteRecall_Core.Common;
using NoteRecall_Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NoteRecall_Application.ServiceInterfaces
{
    public interface IReviewSessionService
    {
        Task<ServiceResult<ReviewSessionResponseDTO>> GetReviewSessionByIdAsync(int reviewSessionId);
        Task<ServiceResult<IEnumerable<ReviewSessionResponseDTO>>> GetReviewSessionsByUserIdAsync(int userId);
        //Task<ServiceResult<ReviewSessionResponseDTO>> AddReviewSessionAsync(ReviewSessionRequestDTO reviewSessionRequest);
        //Task<ServiceResult<ReviewSessionResponseDTO>> UpdateReviewSessionRequestAsync(ReviewSessionRequestDTO reviewSessionRequest);
        //Task<ServiceResult<bool>> DeleteReviewSessionRequestAsync(int reviewSessionId);
    }
}
