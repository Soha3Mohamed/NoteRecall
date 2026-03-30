using NoteRecall_Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NoteRecall_Infrastructure.Services.QuestionsGenration
{
    public class SentenceSplitter
    {
        public List<string> Split(string text)
        {
            var separators = new[] { '.', '!', '?' };

            var sentences = text
                .Split(separators, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => s.Trim())
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .ToList();

            return sentences;
        }
    }
}
