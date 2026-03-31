using System;
using System.Collections.Generic;
using System.Text;

namespace NoteRecall_Infrastructure.Services.QuestionsGenration
{
    public class ImportantSentencePicker
    {
        private readonly string[] _keywords =
            { "is", "are", "was", "were", "because", "cause", "process", "important" };

        public List<string> Pick(List<string> sentences, int count = 3)
        {
            if (sentences == null || sentences.Count == 0)
                return new List<string>();

            var scoredSentences = sentences
                .Select((sentence, index) => new
                {
                    Sentence = sentence,
                    Score = CalculateScore(sentence, index)
                })
                .OrderByDescending(x => x.Score)
                .Take(count)
                .Select(x => x.Sentence)
                .ToList();

            return scoredSentences;
        }

        private int CalculateScore(string sentence, int index)
        {
            int score = 0;

            // 1. Length score (longer = more important)
            if (sentence.Length > 20)
                score += 2;

            if (sentence.Length > 40)
                score += 2;

            // 2. Keyword score
            foreach (var keyword in _keywords)
            {
                if (sentence.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                {
                    score += 3;
                    break;
                }
            }

            // 3. Position bonus (first sentence is important)
            if (index == 0)
                score += 2;

            return score;
        }
    }
}
