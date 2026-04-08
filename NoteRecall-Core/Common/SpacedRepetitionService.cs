using NoteRecall_Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NoteRecall_Core.Common
{
    public class SpacedRepetitionService
    {
        public void UpdateSchedule(Question question, int quality)//quality from 0 to 5, where 5 is perfect recall and 0 is complete failure
        {
            if (quality < 3)
            {
                question.RepetitionCount = 0;
                question.IntervalDays = 1;
            }
            else
            {
                question.RepetitionCount++;

                if (question.RepetitionCount == 1)
                    question.IntervalDays = 1;
                else if (question.RepetitionCount == 2)
                    question.IntervalDays = 3;
                else
                    question.IntervalDays = (int)(question.IntervalDays * question.EaseFactor);

                question.EaseFactor += (0.1 - (5 - quality) * (0.08 + (5 - quality) * 0.02));

                if (question.EaseFactor < 1.3)
                    question.EaseFactor = 1.3;
            }

            question.NextReviewDate = DateTime.UtcNow.AddDays(question.IntervalDays);
        }
    }
}
