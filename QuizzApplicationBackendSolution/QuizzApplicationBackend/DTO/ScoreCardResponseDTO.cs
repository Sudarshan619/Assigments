namespace QuizzApplicationBackend.DTO
{
    public class ScoreCardResponseDTO
    {
        public int ScoreCardId { get; set; }
        
        public int QuizId { get; set; }

        public string QuizName { get; set; }

        public string Image { get; set; }

        public string Username { get; set; }

        public int Score { get; set; }

        public double Acuuracy { get; set; }
    }
}
