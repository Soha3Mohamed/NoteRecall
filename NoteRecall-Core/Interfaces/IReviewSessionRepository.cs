using NoteRecall_Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NoteRecall_Core.Interfaces
{
    internal interface IReviewSessionRepository
    {
        Task<ReviewSession> GetByIdAsync(int id);
        Task<IEnumerable<ReviewSession>> GetByUserIdAsync(int userId);
        Task AddAsync(ReviewSession reviewSession);
        Task UpdateAsync(ReviewSession reviewSession);
        Task DeleteAsync(int id);
        Task SaveChangesAsync();
    }
}
