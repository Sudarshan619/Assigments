using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day9_ClinicManagement
{
    public class Patient
    {

        public int? Id { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
        public int? Age { get; set; } = 0;
 
        public Patient Register(List<Patient> patients)
        {
               Patient patient = new Patient();
               try
               {
                Console.WriteLine("Enter patient id");
                patient.Id = Convert.ToInt32(Console.ReadLine());
                var filteredPatient = patients.Where(e => e.Id == patient.Id).ToList();
                if (filteredPatient.Count > 0) {
                    throw new Exception("Patient already exist");
                }
                Console.WriteLine("Enter patient name");
                patient.Name = Console.ReadLine();
                Console.WriteLine("Enter patient age");
                patient.Age = Convert.ToInt32(Console.ReadLine());

                return patient;

                }
                catch (FormatException ex) { 
                   Console.WriteLine(ex.Message);
                }
                catch (Exception ex)
                {
                Console.WriteLine(ex.Message);
                }
            return null;

        }

        public Appointment MakeAppointment(List<Doctor> doctors, List<Appointment> appointments, int AppointmentId, Patient currPatient)
        {
            Appointment appointment = new Appointment();
            try
            {  
                
                if (doctors.Count <=0)
                {
                    throw new Exception("No doctors found");
                }
                Console.WriteLine("Enter doctor id for appointment");
                var currDoctor = doctors.FirstOrDefault(e => e.DoctorId == Convert.ToInt32(Console.ReadLine()));

                if (currDoctor == null) {
                    throw new Exception("No doctors found");
                }
                appointment.AppointedDoctor = currDoctor;
                Console.WriteLine("Enter time for appointment");
                var date_time = DateTime.Parse(Console.ReadLine());
                var appointmentTime = appointments.FirstOrDefault(e => e.AppointedTime.Date == date_time.Date  && e.AppointedTime.ToLocalTime() == date_time.ToLocalTime() || Math.Abs((e.AppointedTime.ToLocalTime() - date_time.ToLocalTime()).TotalMinutes) <= 30);
                
                
                if (appointmentTime != null)
                {
                    throw new Exception("Appointment is already scheduled");
                }
                
                    appointment.AppointedPatient = currPatient;
                    appointment.AppointedTime = date_time;
                    appointment.AppointmentId = AppointmentId;

                    Console.WriteLine("Appointment succesfull");

                return appointment;
                
                
            }
            catch (Exception ex) { 
              Console.WriteLine(ex.Message);
            }

            return null;
        }


        public void ShowAppointments(Patient patient,List<Appointment> appointment)
        {
            try
            {
                
                var FilteredAppointment = appointment.Where(e => e.AppointedPatient.Id == patient.Id);
                foreach (var app in FilteredAppointment)
                {
                    Console.WriteLine(app.AppointmentId);
                    Console.WriteLine(app.AppointedDoctor);
                    Console.WriteLine(app.AppointedPatient);
                    Console.WriteLine(app.AppointedTime);
                    Console.WriteLine("---------------------------");
                }
            }
            catch (Exception ex) { 
               Console.WriteLine (ex.Message);   
            }

        }

        public override string ToString()
        {
            return $"Id: {Id} \n Name: {Name} \n Age: {Age} \n ------------------------------------";
        }
    }
}
