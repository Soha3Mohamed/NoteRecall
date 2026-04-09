using Microsoft.EntityFrameworkCore;
using NoteRecall_Core.Entities;
using NoteRecall_Core.Interfaces;
using NoteRecall_Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Text;

namespace NoteRecall_Infrastructure.Repositories
{
    internal class ReviewSessionRepository : IReviewSessionRepository
    {
        private readonly ApplicationDbContext _context;
        public ReviewSessionRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ReviewSession> GetByIdAsync(int id)
        {
            return await _context.ReviewSessions.AsNoTracking().FirstOrDefaultAsync(rs => rs.Id == id);
        }
        public async Task<IEnumerable<ReviewSession>> GetByUserIdAsync(int userId)
        {
            return await _context.ReviewSessions.AsNoTracking().Where(rs => rs.UserId == userId).ToListAsync();
        }

        public async Task AddAsync(ReviewSession reviewSession)
        {
            await _context.ReviewSessions.AddAsync(reviewSession);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(ReviewSession reviewSession)
        {
            _context.ReviewSessions.Update(reviewSession);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var reviewSession = await _context.ReviewSessions.FirstOrDefaultAsync(rs => rs.Id == id);
            if (reviewSession != null)
            {
                _context.ReviewSessions.Remove(reviewSession);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<ReviewSession?> GetLatestByUserIdAsync(int userId)
        {
            return await _context.ReviewSessions
                .Where(r => r.UserId == userId)
                .OrderByDescending(r => r.SessionDate)
                .FirstOrDefaultAsync();
        }
        public async Task SaveChangesAsync()
        {
           await _context.SaveChangesAsync();
        }
    }
}
