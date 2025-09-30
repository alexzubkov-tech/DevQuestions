using FluentValidation.Results;
using Shared;

namespace DevQuestions.Application.Exstensions;

public static class ValidationExstensions
{
    public static Error[] ToErrors(this ValidationResult validationResult) =>
        validationResult.Errors.Select(e => Error.Validation(
            e.ErrorCode, e.ErrorMessage, e.PropertyName)).ToArray();
}