using Day15_PatientLogin.Models;
using Day15_PatientLogin.Interface;
using System.Linq;

namespace Day15_PatientLogin.Repositories
{
    public class AppointmentRepository : IAppointment
    {
        static List<Doctor> doctors;
        static List<Appointment> appointments = new List<Appointment>();

        public AppointmentRepository()
        {
           
            doctors = new List<Doctor>()
            {
                new Doctor(1,"Thomas","Gynecologist","Male","Images/d1.jpeg",8129874737),
                new Doctor(2,"Mahesh","Orthopedic","Male","Images/d2.jpeg",8942346734),
                new Doctor(3,"Asha","Pediatrician","Female","Images/d3.jpeg",9387466322),
                new Doctor(4,"Kavya","Surgeon","Female","Images/d4.jpeg",7323797239),
                new Doctor(5,"Pramila","Neurologist","Female","Images/d5.jpeg",6274927927),
                new Doctor(6,"Hema","Surgeon","Female","Images/d6.jpeg",8227428433)
            };

            
        }

        
        public List<Doctor> GetAllDoctors()
        {
            return doctors;
        }

        public List<Appointment> GetAllAppointments() {
            
            return appointments;
        }

       
        public List<Appointment> GetAppointmentsByDoctor(int doctorId)
        {
            return appointments.Where(a => a.DoctorId == doctorId).ToList();
        }

        public bool BookAppointment(Appointment appointment)
        {
            if (IsDoctorAvailable(appointment.DoctorId, appointment.AppointmentDate))
            {
                appointments.Add(appointment);
                Console.WriteLine("line 50 repo" +appointments.Count);
                return true;
            }
            return false;
        }

       
        public bool CancelAppointment(int appointmentId)
        {
            var appointment = appointments.FirstOrDefault(a => a.AppointmentId == appointmentId);
            if (appointment != null)
            {
                appointments.Remove(appointment);
                return true;
            }
            return false;
        }

        public List<Appointment> GetAppointmentsByPatient(int patientId)
        {
            Console.WriteLine(appointments.Count);
            return appointments.Where(a => a.PatientId == patientId).ToList();
            
        }

        
        public bool IsDoctorAvailable(int doctorId, DateTime appointmentDate)
        {
            Console.WriteLine(!appointments.Any(a => a.DoctorId == doctorId && a.AppointmentDate == appointmentDate));
            return !appointments.Any(a => a.DoctorId == doctorId && a.AppointmentDate == appointmentDate);
        }
    }
}
