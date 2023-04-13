﻿namespace LearnNew.Models.Requests.Create;

public record CreateQuestionScoreRequest
{
    public required bool IsCorrect { get; set; }
    public required string UserAnswerText { get; set; }
    public required int TestScoreId { get; set; }
    public required string UserId { get; set; }
    public required int QuestionId { get; set; }
}