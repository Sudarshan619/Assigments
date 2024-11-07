using Day29_BackendPlan.DTO;

namespace Day29_BackendPlan.Interface
{
    public interface ITokenService
    {
        public Task<string> GenerateToken(UserTokenDTO user);
    }
}
