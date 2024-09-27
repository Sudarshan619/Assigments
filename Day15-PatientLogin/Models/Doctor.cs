namespace Day15_PatientLogin.Models
{
    public class Doctor : IEquatable<Doctor>
    {
        public int DoctorId { get; set; }

        public string Name { get; set; }
        = string.Empty;

        public string Specialization { get; set; }

        public string Gender { get; set; }

        public string Image { get; set; }

        public long Contact { get; set; }

        public Doctor()
        {

        }
        public Doctor(int id, string name, string specialization, string gender, string image, long contact)
        {
            DoctorId = id;
            Name = name;
            Specialization = specialization;
            Gender = gender;
            Image = image;
            Contact = contact;
        }

        public bool Equals(Doctor? other)
        {

            return this.DoctorId == other.DoctorId;
        }
    }
}
