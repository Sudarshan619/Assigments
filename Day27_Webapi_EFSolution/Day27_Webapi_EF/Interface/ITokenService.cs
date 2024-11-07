using Day27_Webapi_EF.DTO;

namespace Day27_Webapi_EF.Interface
{
    public interface ITokenService
    {
        public Task<string> GenerateToken(UserTokenDTO user);
    }
}
