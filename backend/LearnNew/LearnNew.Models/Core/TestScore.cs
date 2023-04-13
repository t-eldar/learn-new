using LearnNew.Models.Authentication;

namespace LearnNew.Models.Core;

public class TestScore
{
    public int Id { get; set; }
    public required DateTime TestingDate { get; set; }
    public required int UserId { get; set; }
    public User? User { get; set; }
    public required int TestId { get; set; }
    public Test? Test { get; set; }
    public required int Score { get; set; }
    public IEnumerable<QuestionScore>? QuestionScores { get; set; }
}
