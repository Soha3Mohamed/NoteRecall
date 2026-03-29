using NoteRecall_Application.DTOs.QuestionDTOs;
using NoteRecall_Core.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace NoteRecall_Application.ServiceInterfaces
{
    public interface IQuestionService
    {
        Task<ServiceResult<QuestionResponseDTO>> GetQuestionByIdAsync(int questionId);
        Task<ServiceResult<IEnumerable<QuestionResponseDTO>>> GetQuestionsByNoteIdAsync(int noteId);
        //Task<ServiceResult<QuestionResponseDTO>> AddQuestionAsync(int noteId, QuestionRequestDTO questionRequest);
        //Task<ServiceResult<QuestionResponseDTO>> UpdateQuestionAsync(int noteId, QuestionUpdateDTO questionUpdate);
        //Task<ServiceResult<bool>> DeleteQuestionAsync(int noteId, int questionId);

    }
}
