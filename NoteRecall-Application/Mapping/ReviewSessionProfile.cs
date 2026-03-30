using AutoMapper;
using NoteRecall_Application.DTOs.ReviewSessionDTOs;
using NoteRecall_Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NoteRecall_Application.Mapping
{
    public class ReviewSessionProfile : Profile
    {
        public ReviewSessionProfile()
        {
            // CreateMap<Source, Destination>();
            CreateMap<ReviewSession, ReviewSessionResponseDTO>();
            CreateMap<ReviewSessionRequestDTO, ReviewSession>();
        }
    }
}
