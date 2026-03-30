using NoteRecall_Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NoteRecall_Core.Common
{
    public interface IQuestionGenerator
    {
        List<Question> Generate(Note note);
    }
}
