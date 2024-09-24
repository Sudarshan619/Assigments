using Day14_Login.Interface;

namespace Day14_Login.Models
{
    public class User 
    {
        public User(string userName,string password) { 
            UserName = userName;
            Password = password;
        }

        public User()
        {

        }
        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
