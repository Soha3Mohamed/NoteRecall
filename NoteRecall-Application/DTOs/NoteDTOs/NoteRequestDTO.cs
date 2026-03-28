using System;
using System.Collections.Generic;
using System.Text;

namespace NoteRecall_Application.DTOs.NoteDTOs
{
    public class NoteRequestDTO
    {
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
