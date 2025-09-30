﻿using DevQuestions.Application.Exstensions;
using DevQuestions.Application.FullTextSearch;
using DevQuestions.Application.Questions.Exceptions;
using DevQuestions.Application.Questions.Fails;
using DevQuestions.Application.Questions.Fails.Exceptions;
using DevQuestions.Contracts.Questions;
using DevQuestions.Domain.Questions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Shared;

namespace DevQuestions.Application.Questions;

public class QuestionsService : IQuestionsService
{
    private readonly IQuestionsRepository _questionsRepository;
   // private readonly ISearchProvider _searchProvider;
    private readonly ILogger<QuestionsService> _logger;
    private readonly IValidator<CreateQuestionDto> _validator;

    public QuestionsService(
        IQuestionsRepository questionsRepository,
        IValidator<CreateQuestionDto> validator,
      //  ISearchProvider searchProvider,
        ILogger<QuestionsService> logger)
    {
        _questionsRepository = questionsRepository;
        _validator = validator;
      //  _searchProvider = searchProvider;
        _logger = logger;
    }

    public async Task<Guid> Create(CreateQuestionDto questionDto, CancellationToken cancellationToken)
    {
        // проверка валидности
        // валидация входных данных
        var validationResult = await _validator.ValidateAsync(questionDto, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new QuestionValidationException(validationResult.ToErrors());
        }

        // валидация бизнес логики
        int openUserQuestionsCount = await _questionsRepository
            .GetOpenUserQuestionsAsync(questionDto.UserId, cancellationToken);

        var existedQuestion = await _questionsRepository.GetIdAsync(Guid.Empty, cancellationToken);
        
        if (openUserQuestionsCount > 3)
        {
            throw new ToManyQuestionsException();
        }

        // создание сущности Question
        var questionId = Guid.NewGuid();

        var question = new Question(
            questionId,
            questionDto.Title,
            questionDto.Text,
            questionDto.UserId,
            null,
            questionDto.TagIds);

        // сохранение сущности Question в базе данных
        await _questionsRepository.AddAsync(question,  cancellationToken);
        
       // await _searchProvider.IndexQuestionAsync(question );
        
        // логирование об успешном или неуспешном сохранении
        _logger.LogInformation("Created question with id {questionId}", questionId);

         return questionId;
    }

    // public async Task<IActionResult> Update(
    //     Guid questionId,
    //     UpdateQuestionDto request,
    //     CancellationToken cancellationToken)
    // {
    // }
    //
    // public async Task<IActionResult> Delete(Guid questionId, CancellationToken cancellationToken)
    // {
    // }
    //
    // public async Task<IActionResult> SelectSolution(
    //     Guid questionId,
    //     Guid answerId,
    //     CancellationToken cancellationToken)
    // {
    // }
    //
    // public async Task<IActionResult> AddAnswer(
    //     Guid questionId,
    //     AddAnswerDto request,
    //     CancellationToken cancellationToken)
    // {
    // }
}