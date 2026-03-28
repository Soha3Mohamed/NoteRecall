using NoteRecall_Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NoteRecall_Application.DTOs.ReviewSessionDTOs
{
    public class ReviewSessionRequestDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime SessionDate { get; set; }

        public ICollection<ReviewResult> Results { get; set; }
    }
}
