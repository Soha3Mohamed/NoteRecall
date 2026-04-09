using Microsoft.EntityFrameworkCore;
using NoteRecall_Core.Entities;
using NoteRecall_Core.Interfaces;
using NoteRecall_Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Text;

namespace NoteRecall_Infrastructure.Repositories
{
    public class QuestionProgressRepository : IQuestionProgressRepository
    {
        private readonly ApplicationDbContext _context;
        public QuestionProgressRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(QuestionProgress progress)
        {
            await _context.QuestionProgresses.AddAsync(progress);
            await _context.SaveChangesAsync();
        }

        public Task DeleteAsync(int questionId)
        {
            throw new NotImplementedException();
        }

        public Task<QuestionProgress> GetByQuestionIdAsync(int questionId)
        {
            throw new NotImplementedException();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public async Task<List<Question>> GetDueQuestionsAsync(int userId)
        {
            return await _context.QuestionProgresses
                .Where(p => p.NextReviewDate <= DateTime.UtcNow &&
                            p.Question.Note.UserId == userId)
                .Select(p => p.Question)
                .ToListAsync();
        }
        public async Task UpdateAsync(QuestionProgress progress)
        {
             _context.QuestionProgresses.Update(progress);
            await _context.SaveChangesAsync();
        }
    }
}
