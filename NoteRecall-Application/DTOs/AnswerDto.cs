using System;
using System.Collections.Generic;
using System.Text;

namespace NoteRecall_Application.DTOs
{
    public class AnswerDto
    {
        public int QuestionId { get; set; }
        public int Quality { get; set; } // 0 to 5, where 5 is perfect recall and 0 is complete failure
    }
}
