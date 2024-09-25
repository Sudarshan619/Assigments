
using Day15_PatientLogin.Repositories;
using Day15_PatientLogin.Models;
using Day15_PatientLogin.Interface;

namespace Day15_PatientLogin.Service
{
    public class AppointmentService
    {
        private readonly IAppointment _repository;

        static int count = 100;
        public AppointmentService(AppointmentRepository repository)
        {
            _repository = repository;
        }

        public bool BookAppointment(int doctorId, int patientId, DateTime appointmentDate)
        {
           
            if (_repository.IsDoctorAvailable(doctorId, appointmentDate))
            {
                count++;
                var appointment = new Appointment
                {    
                    AppointmentId = count,
                    DoctorId = doctorId,
                    PatientId = patientId,
                    AppointmentDate = appointmentDate,
                    Status = "Pending",
                };

               
                return _repository.BookAppointment(appointment);
            }
            return false;
        }
        public List<Appointment> GetAppointmentsForPatient(int patientId)
        {
            return _repository.GetAppointmentsByPatient(patientId);
        }

        public List<Doctor> GetAllDoctors()
        {
            return _repository.GetAllDoctors();
        }

    }
}
