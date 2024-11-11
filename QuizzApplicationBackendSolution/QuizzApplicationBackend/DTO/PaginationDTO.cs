namespace QuizzApplicationBackend.DTO
{
    public class PaginationDTO
    {
        public int PageNo { get; set; }

        public IEnumerable<LeaderBoardResponseDTO> LeaderBoardResponse { get; set; }
    }
}
