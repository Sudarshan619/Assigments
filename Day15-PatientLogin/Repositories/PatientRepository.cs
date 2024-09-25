using Day15_PatientLogin.Models;


namespace Day15_PatientLogin.Repositories
{
    public class PatientRepository
    {
        private static List<Patient> patients = new List<Patient>
        {
            new Patient
            {
                Id = 1,
                UserName = "Sudu",
                Age = 32,
                Email = "sudarhan@example.com"
            },
            new Patient
            {
                Id = 2,
                UserName = "desh",
                Age = 28,
                Email = "desh@example.com"
            },
            
        };

        public List<Patient> GetAllPatients()
        {
            return patients;
        }

        public Patient GetPatientByUsername(string username)
        {
            return patients.FirstOrDefault(p => p.UserName== username);
        }
    }
}
