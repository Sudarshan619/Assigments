namespace Day15_PatientLogin.Models
{
    public class Patient
    { 
            public Patient(string userName, string password)
            {
                UserName = userName;
                Password = password;
            }

            public Patient()
            {

            }
            public string UserName { get; set; }

            public string Password { get; set; }
        
    }
}
