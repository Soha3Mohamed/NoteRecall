using AutoMapper;
using NoteRecall_Application.DTOs.UserDTOs;
using NoteRecall_Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NoteRecall_Application.Mapping
{
    internal class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserResponseDTO>(); //for get
            CreateMap<UserUpdateDTO, User>(); //for update
            CreateMap<UserRequestDTO, User>(); //for create
        }
    }
}
