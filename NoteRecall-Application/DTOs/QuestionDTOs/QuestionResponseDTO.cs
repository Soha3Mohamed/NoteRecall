using System;
using System.Collections.Generic;
using System.Text;

namespace NoteRecall_Application.DTOs.QuestionDTOs
{
    public class QuestionResponseDTO
    {
        //i realised now that question entity is supposed to be only get and the user should not be able to create or update questions, because they are generated based on the note content, so i will remove the question request and update dtos and only keep the response dto
        public int NoteId { get; set; }
        public string QuestionText { get; set; }
        public string ExpectedAnswerText { get; set; }
        public DateTime NextReviewDate { get; set; }
    }
}
