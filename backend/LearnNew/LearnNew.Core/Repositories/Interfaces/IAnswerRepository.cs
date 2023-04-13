using LearnNew.Models.Core;

namespace LearnNew.Core.Repositories.Interfaces;
public interface IAnswerRepository
{
    Task<IEnumerable<Answer>?> GetAllByQuestionIdAsync(int questionId);
    Task<IEnumerable<Answer>?> GetAllByTestIdAsync(int testId); 
    Task<IEnumerable<Answer>?> GetCorrectByTestIdAsync(int testId); 
    Task<Answer?> GetCorrectByQuestionIdAsync(int questionId);
    Task<Answer?> GetByIdAsync(int id);
    Task<Answer> CreateAsync(Answer answer);
    Task DeleteAsync(int id);
    Task UpdateAsync(Answer answer);
}
