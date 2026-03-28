using NoteRecall_Application.DTOs.NoteDTOs;
using NoteRecall_Application.DTOs.ReviewSessionDTOs;
using NoteRecall_Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NoteRecall_Application.DTOs.UserDTOs
{
    public class UserResponseDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }

        public DateTime CreatedAt { get; set; }

        public ICollection<NoteResponseDTO> Notes { get; set; }

        public ICollection<ReviewSessionResponseDTO> ReviewSessions { get; set; } 
    }
}
