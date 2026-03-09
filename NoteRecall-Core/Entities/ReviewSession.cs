using System;
using System.Collections.Generic;
using System.Text;

namespace NoteRecall_Core.Entities
{
    internal class ReviewSession
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime SessionDate { get; set; }

        public List<ReviewResult> Results { get; set; } = new();
    }
}
