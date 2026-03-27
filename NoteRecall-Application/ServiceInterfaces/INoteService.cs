using NoteRecall_Application.DTOs.NoteDTOs;
using NoteRecall_Application.DTOs.UserDTOs;
using NoteRecall_Core.Common;
using NoteRecall_Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NoteRecall_Application.ServiceInterfaces
{
    public interface INoteService 
    {
        Task<ServiceResult<NoteResponseDTO>> GetNoteByIdAsync(int id);
        Task<ServiceResult<IEnumerable<NoteResponseDTO>>> GetNoteByUserIdAsync(int userId);
        Task<ServiceResult<NoteResponseDTO>> AddNoteAsync(int userId, NoteRequestDTO noteRequest);
        Task<ServiceResult<NoteResponseDTO>> UpdateNoteAsync(int userId, NoteUpdateDTO noteRequest);
        Task<ServiceResult<bool>> DeleteNoteAsync(int id);

    }
}
