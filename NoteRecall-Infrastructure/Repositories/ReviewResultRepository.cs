using Microsoft.EntityFrameworkCore;
using NoteRecall_Core.Entities;
using NoteRecall_Core.Interfaces;
using NoteRecall_Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Text;

namespace NoteRecall_Infrastructure.Repositories
{
    internal class ReviewResultRepository : IReviewResultRepository
    {
        private readonly ApplicationDbContext _context;
        public ReviewResultRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ReviewResult> GetByIdAsync(int id)
        {
            return await _context.ReviewResults.AsNoTracking().FirstOrDefaultAsync(rs => rs.Id == id);
        }
        public async Task AddAsync(ReviewResult reviewResult)
        {
            await _context.ReviewResults.AddAsync(reviewResult);
            await _context.SaveChangesAsync();
        }
    }
}
