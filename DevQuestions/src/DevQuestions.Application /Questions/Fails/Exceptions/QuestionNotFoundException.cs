﻿using Shared;

namespace DevQuestions.Application.Questions.Exceptions;

public class QuestionNotFoundException: NotFoundException
{
    protected QuestionNotFoundException(Error[] errors)
        : base(errors)
    {
    }

}  