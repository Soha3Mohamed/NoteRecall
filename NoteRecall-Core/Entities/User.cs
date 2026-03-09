using System;
using System.Collections.Generic;
using System.Text;

namespace NoteRecall_Core.Entities
{
    internal class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime LastUpdatedAt { get; set; } = DateTime.UtcNow;

        public List<Note> Notes { get; set; } = new();

        public List<ReviewSession> ReviewSessions { get; set; } = new();
    }
}
