using AutoMapper;
using NoteRecall_Application.DTOs.QuestionDTOs;
using NoteRecall_Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NoteRecall_Application.Mapping
{
    internal class QuestionProfile : Profile
    {
        public QuestionProfile()
        {
            // CreateMap<Source, Destination>();
            // Example:
             CreateMap<Question, QuestionResponseDTO>();
            CreateMap<QuestionRequestDTO, Question>();
             CreateMap<QuestionUpdateDTO, Question>();

        }
    }
}
