using NoteRecall_Application.DTOs.QuestionDTOs;
using NoteRecall_Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NoteRecall_Application.DTOs.NoteDTOs
{
    public class NoteResponseDTO
    {

        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }

        public ICollection<QuestionResponseDTO> Questions { get; set; }
    }
}
