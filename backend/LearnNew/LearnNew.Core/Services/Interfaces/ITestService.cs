using LearnNew.Models.Core;
using LearnNew.Models.Dto;

namespace LearnNew.Core.Services.Interfaces;
public interface ITestService
{
    Task CheckTestAsync(IEnumerable<UserAnswer> userAnswers, int testId);
}
