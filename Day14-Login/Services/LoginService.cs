using Day14_Login.Interface;
using Day14_Login.Models;

namespace Day14_Login.Services
{
    public class LoginService
    {
        private readonly IUser<string, string, User> _userRepository;

        public LoginService(IUser<string, string, User> userRepo)
        {
            _userRepository = userRepo;
        }

        public User Login(string username, string password)
        {
            var user = _userRepository.Get(username, password);
            return user;
        }

        public User ChangePassword(User user)
        {
              var newuser = _userRepository.ChangePassword(user);
            
            return newuser;
        }
    }
}
