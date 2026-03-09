using NoteRecall_Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NoteRecall_Core.Interfaces
{
    internal interface IUserRepository
    {
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByIdAsync(int id);
        Task<IEnumerable<User>> GetAllAsync();
        Task AddAsync(User user);

        Task UpdateAsync(User user);
        Task DeleteAsync(int id);
        Task SaveChangesAsync();
    }
}
