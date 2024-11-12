using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using QuizzApplicationBackend.DTO;
using QuizzApplicationBackend.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Moq;

namespace TestProject1
{
    public class TokenServiceTest
    {
        private readonly TokenService _tokenService;
        private readonly Mock<IConfiguration> _configurationMock;

        public TokenServiceTest()
        {
            _configurationMock = new Mock<IConfiguration>();
            var secretKey = Convert.ToBase64String(Encoding.UTF8.GetBytes("ThisIsA32CharactersLongSecretKey!"));
            _configurationMock.Setup(config => config["JWT:SecretKey"]).Returns(secretKey);


            _tokenService = new TokenService(_configurationMock.Object);
        }

        [Test]
        public async Task GenerateToken_ShouldReturnValidToken_WithCorrectClaims()
        {
            // Arrange
            var userTokenDto = new UserTokenDTO
            {
                Username = "testuser",
                Role = "Admin"
            };

            // Act
            var token = await _tokenService.GenerateToken(userTokenDto);

            // Assert
            Assert.False(string.IsNullOrEmpty(token));
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            Assert.NotNull(jwtToken);
            Assert.AreEqual("testuser", jwtToken.Claims.First(c => c.Type == JwtRegisteredClaimNames.GivenName).Value);
            Assert.AreEqual("Admin", jwtToken.Claims.First(c => c.Type == ClaimTypes.Role).Value);
            Assert.True(jwtToken.ValidTo > DateTime.UtcNow);
        }

        [Test]
        public async Task GenerateToken_ShouldThrowException_WithInvalidKey()
        {
            // Arrange
            _configurationMock.Setup(config => config["JWT:SecretKey"]).Returns((string)null);
            var invalidTokenService = new TokenService(_configurationMock.Object);
            var userTokenDto = new UserTokenDTO { Username = "testuser", Role = "User" };

            // Act & Assert
            Assert.ThrowsAsync<ArgumentNullException>(() => invalidTokenService.GenerateToken(userTokenDto));
        }
    }
}
