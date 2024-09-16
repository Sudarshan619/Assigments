using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day9_ClinicManagement
{
    public class Patient
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public Patient() { 
            
         
        }
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

                }
                catch (FormatException ex) { 
                   Console.WriteLine(ex.Message);
                }
                catch (Exception ex)
                {
                Console.WriteLine(ex.Message);
                }
            return patient;

        }

        public Appointment MakeAppointment(List<Doctor> doctor, int AppointmentId, Patient currPatient)
        {
            Appointment appointment = new Appointment();
            appointment.AppointmentId = AppointmentId + 1;
            Console.WriteLine("Enter doctor id for appointment");
            var currDoctor = doctor.FirstOrDefault(e => e.DoctorId == Convert.ToInt32(Console.ReadLine()));
            appointment.AppointedDoctor = currDoctor;
            //var currDoctor = doctor.FirstOrDefault(e => e.DoctorId == Convert.ToInt32(Console.ReadLine()));
            appointment.AppointedPatient = currPatient;
            Console.WriteLine("Enter time for appointment");
            appointment.AppointedTime = DateTime.Parse(Console.ReadLine());

            return appointment;
        }


        public void ShowAppointments(Patient patient,List<Appointment> appointment)
        {
            //Console.WriteLine("Enter patient id");
            //var patientId = Convert.ToInt32(Console.ReadLine());
            var FilteredAppointment = appointment.Where(e => e.AppointedPatient.Id == patient.Id);
            //Console.WriteLine(FilteredAppointment);

            foreach(var app in FilteredAppointment)
            {
                Console.WriteLine(app.AppointmentId);
                Console.WriteLine(app.AppointedDoctor);
                Console.WriteLine(app.AppointedPatient);
                Console.WriteLine(app.AppointedTime);
            }
        }

        public override string ToString()
        {
            return $"Id: {Id} \n Name:{Name} \n Age:{Age}";
        }
    }
}
