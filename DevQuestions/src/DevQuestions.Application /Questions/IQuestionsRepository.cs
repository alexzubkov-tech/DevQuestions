using DevQuestions.Domain.Questions;

namespace DevQuestions.Application.Questions;

public interface IQuestionsRepository
{
    Task<Guid> AddAsync(Question question, CancellationToken cancellationToken); 

    Task<Guid> SaveAsync(Question question, CancellationToken cancellationToken);
 
    Task<Guid> DeleteAsync(Guid questionId, CancellationToken cancellationToken);

    Task<Question?> GetIdAsync(Guid questionId, CancellationToken cancellationToken);

    Task<int> GetOpenUserQuestionsAsync(Guid userId, CancellationToken cancellationToken);
}