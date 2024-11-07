namespace QuizzApplicationBackend.DTO
{
    public class OptionDTO
    {
        public int QuestionId { get; set; }

        public string Text { get; set; }

        public Boolean IsCorrect { get; set; }
    }
}
