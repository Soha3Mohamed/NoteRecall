using AutoMapper;
using Microsoft.Extensions.Logging;
using NoteRecall_Application.DTOs.QuestionDTOs;
using NoteRecall_Application.ServiceInterfaces;
using NoteRecall_Core.Common;
using NoteRecall_Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace NoteRecall_Application.ServiceImplementation
{
    internal class QuestionService : IQuestionService
    {
        private readonly ILogger<UserService> _logger;
        private readonly IQuestionRepository _questionRepository;
        private readonly IMapper _mapper;
        private readonly INoteRepository _noteRepository;
        public QuestionService(ILogger<UserService> logger, IQuestionRepository questionRepository, IMapper mapper)
        {
            _logger = logger;
            _questionRepository = questionRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResult<QuestionResponseDTO>> GetQuestionByIdAsync(int questionId)
        {
            var question = await _questionRepository.GetQuestionByIdAsync(questionId);
            if (question == null)
            {
                _logger.LogWarning("Question with ID {QuestionId} not found.", questionId);
                return ServiceResult<QuestionResponseDTO>.Fail("Question not found.");
            }
            _logger.LogInformation("Question with ID {QuestionId} retrieved successfully.", questionId);
            var questionDto = _mapper.Map<QuestionResponseDTO>(question);
            return ServiceResult<QuestionResponseDTO>.Ok(questionDto);
        }

        public async Task<ServiceResult<IEnumerable<QuestionResponseDTO>>> GetQuestionsByNoteIdAsync(int noteId)
        {
            var questions = await _questionRepository.GetQuestionsByNoteIdAsync(noteId);
            if (questions == null || !questions.Any())
            {
                _logger.LogWarning("No questions found for Note ID {NoteId}.", noteId);
                return ServiceResult<IEnumerable<QuestionResponseDTO>>.Fail("No questions found for this note.");
            }
            _logger.LogInformation("{QuestionCount} questions retrieved for Note ID {NoteId}.", questions.Count(), noteId);
            var questionDtos = _mapper.Map<IEnumerable<QuestionResponseDTO>>(questions);
            return ServiceResult<IEnumerable<QuestionResponseDTO>>.Ok(questionDtos);
        }

        //public async Task<ServiceResult<QuestionResponseDTO>> AddQuestionAsync(int noteId, QuestionRequestDTO questionRequest)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<ServiceResult<QuestionResponseDTO>> UpdateQuestionAsync(int noteId, QuestionUpdateDTO questionUpdate)
        //{
        //    throw new NotImplementedException();
        //}
        //public Task<ServiceResult<bool>> DeleteQuestionAsync(int noteId, int questionId)
        //{
        //    throw new NotImplementedException();
        //}

    }
}
