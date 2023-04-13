using LearnNew.Models.Authentication;

namespace LearnNew.Models.Core;
public class Answer
{
    public int Id { get; set; }
    public required int QuestionId { get; set; }
    public Question? Question { get; set; }
    public required string Text { get; set; }
    public required int CreatorId { get; set; }
    public User? Creator { get; set; }
    public required bool IsCorrect { get; set; }
}
