namespace QuizzApplicationBackend.DTO
{
    public class SubmittedOptionDTO
    {

        public int QuizId { get; set; }

        public int UserId { get; set; }

        public string SubmittedTime {get ;set;}

        public IEnumerable<SelectedOptionDTO> Options { get; set; } = new List<SelectedOptionDTO>();
    }
}
