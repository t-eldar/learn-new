using LearnNew.Models.Authentication;

namespace LearnNew.Models.Core;

public class QuestionScore
{
    public int Id { get; set; }
    public required int TestScoreId { get; set; }
    public TestScore? TestScore { get; set; }
    public required int UserId { get; set; }
    public User? User { get; set; }
    public required int QuestionId { get; set; }
    public Question? Question { get; set; }
    public required string UserAnswerText { get; set; }
    public required bool IsCorrect { get; set; }
}