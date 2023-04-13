using LearnNew.Models.Authentication;

namespace LearnNew.Models.Core;
public class Question
{
    public int Id { get; set; }
    public required string Content { get; set; }
    public required int TestId { get; set; }
    public required bool AreAnswersChoicable { get; set; }
    public Test? Test { get; set; }
    public required int CreatorId { get; set; }
    public User? Creator { get; set; }
    public IEnumerable<Answer>? Answers { get; set; }
}
