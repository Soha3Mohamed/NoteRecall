using Microsoft.EntityFrameworkCore;
using NoteRecall_Core.Entities;
using NoteRecall_Core.Interfaces;
using NoteRecall_Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Text;

namespace NoteRecall_Infrastructure.Repositories
{
    internal class QuestionRepository : IQuestionRepository
    {
        private readonly ApplicationDbContext _context;
        public QuestionRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Question> GetQuestionByIdAsync(int questionId)
        {
            return await _context.Questions.AsNoTracking().FirstOrDefaultAsync(q => q.Id == questionId);
        }
        public Task<List<Question>> GetQuestionsByNoteIdAsync(int noteId)
        {
            return _context.Questions.AsNoTracking().Where(q => q.NoteId == noteId).ToListAsync();
        }
        public async Task AddQuestionAsync(Question question)
        {
            await _context.Questions.AddAsync(question);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteQuestionAsync(int questionId)
        {
           var question = await _context.Questions.FirstOrDefaultAsync(q=>q.Id == questionId);
            if (question != null)
            {
                _context.Questions.Remove(question);
               await _context.SaveChangesAsync();
            }
           
        }
        public async Task UpdateQuestionAsync(Question question)
        {
            _context.Questions.Update(question);
            await _context.SaveChangesAsync();
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
