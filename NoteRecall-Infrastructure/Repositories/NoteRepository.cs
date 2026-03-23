using Microsoft.EntityFrameworkCore;
using NoteRecall_Core.Entities;
using NoteRecall_Core.Interfaces;
using NoteRecall_Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Text;

namespace NoteRecall_Infrastructure.Repositories
{
    internal class NoteRepository : INoteRepository
    {
        private readonly ApplicationDbContext _context;
        public NoteRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Note?> GetByIdAsync(int id)
        {
           return await _context.Notes.AsNoTracking().FirstOrDefaultAsync(N => N.Id == id);
        }

        public async Task<IEnumerable<Note>> GetByUserIdAsync(int userId)
        {
            return await _context.Notes.AsNoTracking().Where(n => n.UserId == userId).ToListAsync();
        }
        public async Task AddAsync(Note note)
        {
            await _context.Notes.AddAsync(note);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Note note)
        {
            _context.Notes.Update(note);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var note = await _context.Notes.FirstOrDefaultAsync(n =>n.Id == id);
            if (note != null)
            {
                _context.Notes.Remove(note);
                await _context.SaveChangesAsync();
            }
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

       
    }
}
