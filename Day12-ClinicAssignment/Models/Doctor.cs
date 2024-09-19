namespace Day12_ClinicAssignment.Models
{
    public class Doctor
    {
        public int Id { get; set; }

        public string Name { get; set; }
        = string.Empty;

        public string Specialization { get; set; }

        public string Gender { get; set; }

        public string Image { get; set; }

        public long Contact{ get; set; }


        public Doctor(int id,string name,string specialization,string gender,string image,long contact) { 
            Id=id;
            Name=name;
            Specialization=specialization;
            Gender=gender;
            Image=image;
            Contact=contact;
        
        }
    }
}
