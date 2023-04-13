using LearnNew.Models.Core;

namespace LearnNew.Core.Repositories.Interfaces;
public interface IQuestionScoreRepository
{
    Task<IEnumerable<QuestionScore>?> GetAllAsync();
    Task<IEnumerable<QuestionScore>?> GetByTestScoreIdAsync(int testScoreId);
    Task<QuestionScore?> GetByUserAndQuestionIdAsync(int userId, int questionId);
    Task<QuestionScore?> GetByIdAsync(int id);

    Task<QuestionScore> CreateAsync(QuestionScore questionScore);
    Task DeleteAsync(int id);
}
