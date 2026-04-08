using NoteRecall_Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NoteRecall_Core.Interfaces
{
    public interface IReviewResultRepository
    {
        Task AddAsync(ReviewResult reviewResult);
    }
}
