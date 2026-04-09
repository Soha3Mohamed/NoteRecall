using NoteRecall_Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NoteRecall_Core.Interfaces
{
    public interface IQuestionProgressRepository
    {
            Task<QuestionProgress> GetByQuestionIdAsync(int questionId);
            Task AddAsync(QuestionProgress progress);
            Task UpdateAsync(QuestionProgress progress);
            Task<List<Question>> GetDueQuestionsAsync(int userId);
            Task DeleteAsync(int questionId);
            Task SaveChangesAsync();
    }
}
