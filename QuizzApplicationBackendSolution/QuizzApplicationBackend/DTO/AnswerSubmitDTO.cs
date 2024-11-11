namespace QuizzApplicationBackend.DTO
{
    public class AnswerSubmitDTO
    {
        public int UserId { get; set; }

        public int QuizId { get; set; }

        public IEnumerable<QuestionDTO> Questions { get; set; }

    }
}
