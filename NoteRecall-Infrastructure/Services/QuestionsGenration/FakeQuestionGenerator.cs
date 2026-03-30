using NoteRecall_Core.Common;
using NoteRecall_Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NoteRecall_Infrastructure.Services.QuestionsGenration
{
    internal class FakeQuestionGenerator : IQuestionGenerator
    {
        public  List<Question> Generate(Note note)
        {
            return new List<Question>
            {
                 new Question{QuestionText = "What is the main idea of this note?"},
                 new Question{QuestionText ="Explain this concept simply"}
            };
        }

    }
}
