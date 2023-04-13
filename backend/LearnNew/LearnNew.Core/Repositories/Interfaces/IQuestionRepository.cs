using LearnNew.Models.Core;

namespace LearnNew.Core.Repositories.Interfaces;
public interface IQuestionRepository
{
    Task<IEnumerable<Question>?> GetByTestIdAsync(int  testId);
    Task<Question?> GetByIdAsync(int id);
    Task<Question> CreateAsync(Question question);
    Task DeleteAsync(int id);
    Task UpdateAsync(Question question);
}
