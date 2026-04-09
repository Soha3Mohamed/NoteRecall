using System;
using System.Collections.Generic;
using System.Text;

namespace NoteRecall_Core.Entities
{
    public class QuestionProgress
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public Question Question { get; set; }  

        public double EaseFactor { get; set; } = 2.5;
        public int Interval { get; set; } = 0;
        public DateTime NextReviewDate { get; set; } = DateTime.UtcNow;
    }
}
