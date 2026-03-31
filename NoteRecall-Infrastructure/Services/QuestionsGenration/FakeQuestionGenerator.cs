using NoteRecall_Core.Common;
using NoteRecall_Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NoteRecall_Infrastructure.Services.QuestionsGenration
{
    //Rule-based NLP
    public class FakeQuestionGenerator : IQuestionGenerator
    {

        ////////////////////////first version of question generator. it just asks simple general questions about the note.///////////////////////
        //public  List<Question> Generate(Note note)
        //{
        //    return new List<Question>
        //    {
        //         new Question{QuestionText = "What is the main idea of this note?"},
        //         new Question{QuestionText ="Explain this concept simply"}
        //    };
        //}


        private readonly SentenceSplitter _sentenceSplitter;
        private readonly ImportantSentencePicker _sentencePicker;

        public FakeQuestionGenerator(
            SentenceSplitter sentenceSplitter,
            ImportantSentencePicker sentencePicker)
        {
            _sentenceSplitter = sentenceSplitter;
            _sentencePicker = sentencePicker;
        }

        public List<Question> Generate(string noteContent)
        {
            // 1. Split into sentences
            var sentences = _sentenceSplitter.Split(noteContent);

            // 2. Pick important ones
            var importantSentences = _sentencePicker.Pick(sentences);

            // 3. Generate questions
            var questions = new List<Question>();

            foreach (var sentence in importantSentences)
            {
                var question = GenerateQuestionFromSentence(sentence);
                questions.Add(question);
            }

            return questions;
        }

        //private Question GenerateQuestionFromSentence(string sentence)
        //{
        //    return new Question
        //    {
        //        QuestionText = $"Explain: {sentence}?",
        //        ExpectedAnswerText = sentence
        //    };
        //}


        private Question GenerateQuestionFromSentence(string sentence)
        {
            var lower = sentence.ToLower();

            if (lower.Contains("is") || lower.Contains("are"))
                return GenerateDefinitionQuestion(sentence);

            if (lower.Contains("because") || lower.Contains("cause"))
                return GenerateWhyQuestion(sentence);

            if (lower.Contains("process"))
                return GenerateHowQuestion(sentence);

            return GenerateGenericQuestion(sentence);
        }

        private Question GenerateDefinitionQuestion(string sentence)
        {
            var parts = sentence.Split(" is ", StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length >= 2)
            {
                return new Question
                {
                    QuestionText = $"What is {parts[0]}?",
                    ExpectedAnswerText = sentence
                };
            }

            return GenerateGenericQuestion(sentence);
        }

        private Question GenerateWhyQuestion(string sentence)
        {
            return new Question
            {
                QuestionText = "Why does this happen?",
                ExpectedAnswerText = sentence
            };
        }

        private Question GenerateHowQuestion(string sentence)
        {
            return new Question
            {
                QuestionText = "How does this process work?",
                ExpectedAnswerText = sentence
            };
        }
        private Question GenerateGenericQuestion(string sentence)
        {
            return new Question
            {
                QuestionText = $"What does the following mean: \"{sentence}\"?",
                ExpectedAnswerText = sentence
            };
        }
    }
}
