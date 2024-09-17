using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Day9_ClinicManagement
{
    public class Doctor
    {
        

        public int? DoctorId { get; set; } = null;

        public string DoctorName { get; set; } = string.Empty;

        public string DoctorType { get; set; } = string.Empty ;

        public long? Phone { get; set; } = null;

        

        public Doctor Register(List<Doctor> doctors)
        {
            try
            {
                Doctor doctor = new Doctor();
                Console.WriteLine("Enter doctor id");
                var id = Convert.ToInt32(Console.ReadLine());
                var filteredDoctor = doctors.Where(e => e.DoctorId == id).ToList();

                if (filteredDoctor.Count >0 ) {
                    throw new Exception("Doctor already exist");
                }
        
                doctor.DoctorId = id;
                Console.WriteLine("Enter doctor name");
                doctor.DoctorName = Console.ReadLine();
                Console.WriteLine("Enter doctor type");
                doctor.DoctorType = Console.ReadLine();
                Console.WriteLine("Enter doctor phone");
                doctor.Phone = Convert.ToInt64(Console.ReadLine());

                return doctor;
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }

            return null;
        }

        public void ShowAppointments(Doctor doctor, List<Appointment> appointment)
        {
            //Console.WriteLine("Enter doctor id");
            //var doctorId = Convert.ToInt32(Console.ReadLine());
            var FilteredAppointment = appointment.Where(e => e.AppointedDoctor.DoctorId == doctor.DoctorId);

            foreach (var app in FilteredAppointment)
            {
                Console.WriteLine($"AppointmentId :{app.AppointmentId}");
                Console.WriteLine($"Appointed Doctor : {app.AppointedDoctor}");
                Console.WriteLine($"Patient :{app.AppointedPatient}");
                Console.WriteLine($"Date-time: {app.AppointedTime}");
            }
        }

        public override string ToString()
        {
            return $"Doctor Id: {DoctorId} \n Doctor Name: {DoctorName} \n Specialization: {DoctorType} \n Phone no: {Phone}\n ------------------------------------";
        }
    }
}
