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
        public Doctor() { }

        public int DoctorId { get; set; }

        public string DoctorName { get; set; }

        public string DoctorType { get; set; }

        public long Phone { get; set; }

        

        public Doctor Register(List<Doctor> doctors)
        {

            Doctor doctor = new Doctor();
            try
            {
                Console.WriteLine("Enter doctor id");
                doctor.DoctorId = Convert.ToInt32(Console.ReadLine());
                var filteredDoctor = doctors.Where(x => x.DoctorId == doctor.DoctorId).ToList();
                if (filteredDoctor.Count() > 0) {
                    throw new Exception("Doctor already exist");
                }
                else
                {
                    return doctor;
                }
                Console.WriteLine("Enter doctor name");
                doctor.DoctorName = Console.ReadLine();
                Console.WriteLine("Enter doctor type");
                doctor.DoctorType = Console.ReadLine();
                Console.WriteLine("Enter doctor phone");
                doctor.Phone = Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }

            return doctor;
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
            return $"Doctor Id: {DoctorId} \n Doctor Name:{DoctorName} \n Specialization:{DoctorType} \n Phone no:{Phone}";
        }
    }
}
