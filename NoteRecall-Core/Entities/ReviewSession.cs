using System;
using System.Collections.Generic;
using System.Text;

namespace NoteRecall_Core.Entities
{
    public class ReviewSession
    {
        public int Id { get; set; }
        public int NoteId { get; set; }
        public int UserId { get; set; }
        public DateTime SessionDate { get; set; } = DateTime.UtcNow;

        public ICollection<ReviewResult> Results { get; set; } = new HashSet<ReviewResult>();
    }
}
