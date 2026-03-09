using System;
using System.Collections.Generic;
using System.Text;

namespace NoteRecall_Core.Entities
{
    internal class ReviewResult
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public string UserAnswer { get; set; }
        public int SelfScore { get; set; }
        public DateTime ReviewedAt { get; set; }
    }
}
