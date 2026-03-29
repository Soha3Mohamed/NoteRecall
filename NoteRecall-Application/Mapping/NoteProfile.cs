using AutoMapper;
using NoteRecall_Application.DTOs.NoteDTOs;
using NoteRecall_Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NoteRecall_Application.Mapping
{
    internal class NoteProfile : Profile
    {
        public NoteProfile()
        {
            CreateMap<Note, NoteResponseDTO>();
            CreateMap<NoteRequestDTO, Note>();
            CreateMap<NoteUpdateDTO, Note>();
        }
    }
}
