using Day14_Login.Interface;
using Day14_Login.Models;

namespace Day14_Login.Repositories
{
    public class UserRepositories:IUser<string,string,User>
    {

        static List<User> _users = new List<User>()
        {
            new User("Sudu","skp123"),
            new User("Desh","desh@123")
        };
        //public User Add(User item)
        //{
        //    _users.Add(item);
        //    return item;
        //}

        public User Delete(string username,string password)
        {
            var user = _users.FirstOrDefault(e => e.UserName == username && e.Password == password);

            return user;
        }

        public User Get(string username,string password)
        {
            var user = _users.FirstOrDefault(e => e.UserName == username && e.Password == password);
            return user;
        }

        public List<User> GetAll()
        {
            return _users;          
        }

        public User ChangePassword(User Item)
        {
            var user = _users.FirstOrDefault(e=> e.UserName == Item.UserName);
            user.Password = Item.Password;
            
            return user;
        }

        public User Login(string key, string pass)
        {
            var user = _users.FirstOrDefault(e => e.UserName == key && e.Password == pass);

            return user;
        }
    }

}
