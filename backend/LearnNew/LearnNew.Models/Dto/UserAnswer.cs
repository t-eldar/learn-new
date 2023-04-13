using LearnNew.Models.Core;

namespace LearnNew.Models.Dto;

public class UserAnswer
{
    public required int UserId { get; set; }
    public required int QuestionId { get; set; }
    public Answer? Answer { get; set; }
    public string? AnswerText { get; set; }
}