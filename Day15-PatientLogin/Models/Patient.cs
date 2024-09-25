namespace Day15_PatientLogin.Models
{
    public class Patient
    {
        static int count = 0;
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int Age { get; set; }    
        public string Email { get; set; } 
      

        public Patient()
        {
        }

        public Patient(string userName, string password,int age,string email)
        {
            Id = count++;
            Age = age;
            Email = email;
            UserName = userName;
            Password = password;
        }
    }
}
