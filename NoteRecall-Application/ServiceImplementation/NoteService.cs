using AutoMapper;
using Microsoft.Extensions.Logging;
using NoteRecall_Application.DTOs.NoteDTOs;
using NoteRecall_Application.ServiceInterfaces;
using NoteRecall_Core.Common;
using NoteRecall_Core.Entities;
using NoteRecall_Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
namespace NoteRecall_Application.ServiceImplementation
{
    internal class NoteService : INoteService
    {
        private readonly ILogger<NoteService> _logger;
        private readonly INoteRepository _noteRepository;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IQuestionGenerator _questionGenerator;
        public NoteService(ILogger<NoteService> logger, INoteRepository noteRepository, IMapper mapper, IUserService userService)
        {
            _logger = logger;
            _noteRepository = noteRepository;
            _mapper = mapper;
            _userService = userService;
        }

        public async Task<ServiceResult<NoteResponseDTO>> GetNoteByIdAsync(int id)
        {
            var note = await _noteRepository.GetByIdAsync(id);
            if (note == null)
            {
                 _logger.LogWarning("Note with id {Id} not found.", id);
                return ServiceResult<NoteResponseDTO>.Fail("Note not found.");
            }
            var noteDto = _mapper.Map<NoteResponseDTO>(note);
            return ServiceResult<NoteResponseDTO>.Ok(noteDto);
        }

        public async Task<ServiceResult<IEnumerable<NoteResponseDTO>>> GetNotesByUserIdAsync(int userId)
        {
            //should i see first if the user exist or not ? like use the user repository or user service to check if the user exist or not ?
            //or should i search if that userId is tied to any notes in the note repository and if not return not found ?
            // i think i will check if user exists first that seems more rightwish to me .
            var user = await _userService.GetUserByIdAsync(userId);
            if (!user.Success)
            {
                _logger.LogWarning("User with id {UserId} not found.", userId);
                return ServiceResult<IEnumerable<NoteResponseDTO>>.Fail("User not found.");
            }
            var notes = await _noteRepository.GetByUserIdAsync(userId);
            if (notes == null || !notes.Any())
            {
                _logger.LogInformation("No notes found for user with id {UserId}.", userId);
                return ServiceResult<IEnumerable<NoteResponseDTO>>.Ok(new List<NoteResponseDTO>());
            }
            var noteDtos = _mapper.Map<IEnumerable<NoteResponseDTO>>(notes);
            return ServiceResult<IEnumerable<NoteResponseDTO>>.Ok(noteDtos);

        }
        public async Task<ServiceResult<NoteResponseDTO>> AddNoteAsync(int userId, NoteRequestDTO noteRequest)
        {
            var user = await _userService.GetUserByIdAsync(userId);
            if (!user.Success)
            {
                _logger.LogWarning("User with id {UserId} not found.", userId);
                return ServiceResult<NoteResponseDTO>.Fail("User not found.");
            }
            var note = _mapper.Map<Note>(noteRequest);
            note.CreatedAt = DateTime.UtcNow;
            var questions = _questionGenerator.Generate(note);
            note.Questions = questions;
            await _noteRepository.AddAsync(note);
            await _noteRepository.SaveChangesAsync();
            var noteDto = _mapper.Map<NoteResponseDTO>(note);
            return ServiceResult<NoteResponseDTO>.Ok(noteDto);

        }
        public async Task<ServiceResult<NoteResponseDTO>> UpdateNoteAsync(int userId, NoteUpdateDTO noteRequest)
        {
            var user = await _userService.GetUserByIdAsync(userId);
            if (!user.Success)
            {
                _logger.LogWarning("User with id {UserId} not found.", userId);
                return ServiceResult<NoteResponseDTO>.Fail("User not found.");
            }
            var note = _mapper.Map<Note>(noteRequest);
            await _noteRepository.UpdateAsync(note);
            await _noteRepository.SaveChangesAsync();
            var noteDto = _mapper.Map<NoteResponseDTO>(note);
            return ServiceResult<NoteResponseDTO>.Ok(noteDto);
        }
        public async Task<ServiceResult<bool>> DeleteNoteAsync(int id, int userId)//this userId that i will use it to check if the note that i want to delete is tied to that user or not if not i will return not found because the note is not found for that user and if it is i will delete it and return true
        {
            var user = await _userService.GetUserByIdAsync(userId);
            if (!user.Success)
            {
                _logger.LogWarning("User with id {UserId} not found.", userId);
                return ServiceResult<bool>.Fail("User not found.");
            }
            var note = await _noteRepository.GetByIdAsync(id);
            if (note == null || note.UserId != userId)
            {
                _logger.LogWarning("Note with id {Id} not found for user with id {UserId}.", id, userId);
                return ServiceResult<bool>.Fail("Note not found for this user.");
            }
            await _noteRepository.DeleteAsync(note.Id);
            await _noteRepository.SaveChangesAsync();
            return ServiceResult<bool>.Ok(true);

        }
    }
}
