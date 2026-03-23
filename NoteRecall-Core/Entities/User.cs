using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace NoteRecall_Core.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime LastUpdatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Note> Notes { get; set; } = new HashSet<Note>();
       
        public ICollection<ReviewSession> ReviewSessions { get; set; } = new HashSet<ReviewSession>();
    }
}
