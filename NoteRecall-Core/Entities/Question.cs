using System;
using System.Collections.Generic;
using System.Text;

namespace NoteRecall_Core.Entities
{
    public class Question
    {
        //this question is for spaced repetition, so it will have a question text, an expected answer text, a next review date, a repetition count, an ease factor, and an interval in days
        //the next review date will be calculated based on the repetition count, the ease factor, and the interval in days
        //the repetition count will be incremented each time the question is reviewed, and it will be reset to 0 if the question is answered incorrectly
        //the ease factor will be adjusted based on whether the question is answered correctly or not, and it will be used to calculate the next review date
        //the interval in days will be calculated based on the repetition count and the ease factor, and it will be used to calculate the next review date
        
        public int Id { get; set; }
        public int NoteId { get; set; }
        public string QuestionText { get; set; }
        public string ExpectedAnswerText { get; set; }
        public DateTime NextReviewDate { get; set; }

        public int RepetitionCount { get; set; }

        public double EaseFactor { get; set; } = 2.5;

        public int IntervalDays { get; set; }
    }
}
