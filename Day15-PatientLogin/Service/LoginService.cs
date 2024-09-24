using Day15_PatientLogin.Models;
using Day15_PatientLogin.Interface;

namespace Day15_PatientLogin.Service
{
    public class LoginService
    {
        private readonly ILogin<string, string, Patient> _userRepository;

        public LoginService(ILogin<string, string, Patient> userRepo)
        {
            _userRepository = userRepo;
        }

        public Patient Login(string username, string password)
        {
            var user = _userRepository.Get(username, password);
            return user;
        }

        public Patient ChangePassword(Patient user)
        {
            var newuser = _userRepository.ChangePassword(user);

            return newuser;
        }
    }
}

