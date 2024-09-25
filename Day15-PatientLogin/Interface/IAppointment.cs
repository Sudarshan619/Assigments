using Day15_PatientLogin.Models;

namespace Day15_PatientLogin.Interface
{
    public interface IAppointment
    {
        List<Doctor> GetAllDoctors();  // Retrieve all doctors
        List<Appointment> GetAppointmentsByDoctor(int doctorId);
        bool BookAppointment(Appointment appointment); 

        List<Appointment> GetAppointmentsByPatient(int patientId);
        bool CancelAppointment(int appointmentId); 
        bool IsDoctorAvailable(int doctorId, DateTime appointmentDate); 
    }
}

