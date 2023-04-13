using LearnNew.Core.Repositories.Interfaces;
using LearnNew.Core.Services.Interfaces;
using LearnNew.Models.Authentication;
using LearnNew.Models.Core;
using LearnNew.Models.Dto;

namespace LearnNew.Core.Services.Implementations;
public class TestService : ITestService
{
    private readonly IQuestionRepository _questionRepository;
    private readonly IAnswerRepository _answerRepository;
    private readonly IQuestionScoreRepository _questionScoreRepository;
    private readonly ITestScoreRepository _testScoreRepository;
    private readonly IDateTimeProvider _dateTimeProvider;

    public TestService(
        IQuestionRepository questionRepository,
        ITestScoreRepository testScoreRepository,
        IQuestionScoreRepository questionScoreRepository,
        IDateTimeProvider dateTimeProvider,
        IAnswerRepository answerRepository)
    {
        _questionRepository = questionRepository;
        _testScoreRepository = testScoreRepository;
        _questionScoreRepository = questionScoreRepository;
        _dateTimeProvider = dateTimeProvider;
        _answerRepository = answerRepository;
    }

    public async Task CheckTestAsync(IEnumerable<UserAnswer> userAnswers, int testId)
    {
        var single = userAnswers.Single();
        var incorrect = userAnswers
            .Where(a => a.UserId != single.UserId)
            .ToArray();

        var userId = single.UserId;

        if (incorrect.Length > 0)
        {
            throw new Exception("User answers should have same user id");
        }

        var questions = await _questionRepository.GetByTestIdAsync(testId)
            ?? throw new Exception($"No questions for test id = {testId}");

        await _testScoreRepository.CreateAsync(new TestScore
        {
            Score = 0,
            TestingDate = _dateTimeProvider.GetCurrent(),
            UserId = userId,
            TestId = testId
        });
        var testScore = await _testScoreRepository.GetByUserAndTestIdAsync(userId, testId)
            ?? throw new Exception("Failed to create TestScore entity");

        var score = 0;
        var correctAnswers = await _answerRepository.GetCorrectByTestIdAsync(testId)
            ?? throw new Exception($"No correct answers fot test id = {testId}");
        foreach (var question in questions)
        {
            var userAnswer = userAnswers.FirstOrDefault(a => a.QuestionId == question.Id);
            var correct = correctAnswers.FirstOrDefault(a => a.QuestionId == question.Id)
                ?? throw new Exception($"No correct answers for {question.Id}");
            var questionScore = GetQuestionScore(userAnswer, correct, question, testScore.Id, userId);
            if (questionScore.IsCorrect)
            {
                score++;
            }
            await _questionScoreRepository.CreateAsync(questionScore);
        }

        var updatedTestScore = testScore;
        testScore.Score = score;
        await _testScoreRepository.UpdateAsync(updatedTestScore);
    }

    private QuestionScore GetQuestionScore(
        UserAnswer? userAnswer,
        Answer correctAnswer,
        Question question,
        int testScoreId,
        int userId)
    {
        if (userAnswer is null)
        {
            return new QuestionScore
            {
                TestScoreId = testScoreId,
                UserId = userId,
                QuestionId = question.Id,
                IsCorrect = false,
                UserAnswerText = "",
            };
        }

        var isCorrect = false;
        var answerText = string.Empty;
        if (question.AreAnswersChoicable)
        {
            if (userAnswer.Answer is null)
            {
                throw new Exception("If question has choicable answers, Answer property should not be null");
            }

            if (userAnswer.Answer.Id == correctAnswer.Id)
            {
                isCorrect = true;
            }
            answerText = userAnswer.Answer.Text;
        }
        else
        {
            if (userAnswer.AnswerText is null)
            {
                throw new Exception("AnswerText is null");
            }
            if (userAnswer.AnswerText == correctAnswer.Text)
            {
                isCorrect = true;
            }
            answerText = userAnswer.AnswerText;
        }

        return new QuestionScore
        {
            TestScoreId = testScoreId,
            UserId = userId,
            QuestionId = question.Id,
            IsCorrect = isCorrect,
            UserAnswerText = answerText,
        };
    }
}
