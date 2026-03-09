using NoteRecall_Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NoteRecall_Core.Interfaces
{
    internal interface IQuestionRepository
    {
        Task<Question> GetQuestionByIdAsync(int questionId);
        Task<List<Question>> GetQuestionsByNoteIdAsync(int noteId);
        Task AddQuestionAsync(Question question);
        Task UpdateQuestionAsync(Question question);
        Task DeleteQuestionAsync(int questionId);
        Task SaveChangesAsync();

    }
}
