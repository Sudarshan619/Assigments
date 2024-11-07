using QuizzApplicationBackend.DTO;

namespace QuizzApplicationBackend.Interfaces
{
    public interface ITokenService
    {
        public Task<string> GenerateToken(UserTokenDTO user);
    }
}
