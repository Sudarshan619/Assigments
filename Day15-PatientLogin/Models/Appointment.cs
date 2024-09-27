namespace Day15_PatientLogin.Models
{
    public class Appointment
    {
        public int AppointmentId { get; set; }  

        public int DoctorId { get; set; } 

        public int PatientId { get; set; } 

        public DateTime AppointmentDate { get; set; } 

        public string Status { get; set; } = "Pending";

        public Appointment()
        {
        }

        public Appointment(int appointmentId, int doctorId, int patientId, DateTime appointmentDate, string status, string notes)
        {
            AppointmentId = appointmentId;
            DoctorId = doctorId;
            PatientId = patientId;
            AppointmentDate = appointmentDate;
            Status = status;
            Notes = notes;
        }
    }
}
