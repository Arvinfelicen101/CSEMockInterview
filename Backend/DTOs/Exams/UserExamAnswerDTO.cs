namespace Backend.DTOs.Exams;

public class UserExamAnswerDTO
{
    public required string UserId { get; set; }
    public required int QuestionId { get; set; }
    public required int AnswerId { get; set; }
    
    //add duration of exam
}