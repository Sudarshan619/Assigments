using Day15_PatientLogin.Interface;
using Day15_PatientLogin.Models;

namespace Day15_PatientLogin.Repositories

{
    public class LoginRepository:ILogin<string,string,Patient>
    {

        static List<Patient> _users = new List<Patient>()
        {
            new Patient("Sudu","skp123",20,"sudu@gamil.com"),
            new Patient("Desh","desh@123",23,"desh@gmail.com")
        };

        public Patient Delete(string username, string password)
        {
            var user = _users.FirstOrDefault(e => e.UserName == username && e.Password == password);

            return user;
        }

        public Patient Get(string username, string password)
        {
            var user = _users.FirstOrDefault(e => e.UserName == username && e.Password == password);
            return user;
        }

        public List<Patient> GetAll()
        {
            return _users;
        }

        public Patient ChangePassword(Patient Item)
        {
            var user = _users.FirstOrDefault(e => e.UserName == Item.UserName);
            user.Password = Item.Password;

            return user;
        }

        public Patient Login(string key, string pass)
        {
            var user = _users.FirstOrDefault(e => e.UserName == key && e.Password == pass);

            return user;
        }
    }

}
