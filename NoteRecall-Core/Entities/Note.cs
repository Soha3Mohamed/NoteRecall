using System;
using System.Collections.Generic;
using System.Text;

namespace NoteRecall_Core.Entities
{
    internal class Note
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        //public DateTime LastUpdatedAt { get; set; } = DateTime.UtcNow;

        public List<Question> Questions { get; set; } = new();
    }
}
