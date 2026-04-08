using System;
using System.Collections.Generic;
using System.Text;

namespace NoteRecall_Core.Entities
{
    public class ReviewResult
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public int ReviewSessionId { get; set; }
        public int SelfScore { get; set; }
        public DateTime ReviewedAt { get; set; } = DateTime.UtcNow;
    }
}
