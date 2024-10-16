using Day26_AppointmentEFC.Context;
using Day26_AppointmentEFC.Model;

namespace Day26_AppointmentEFC
{
    internal class Program
    {
        ClinicContext context = new ClinicContext();
        IClinicService service;

        public Program() { 
          service = new ClinicService();
        
        }
        void Menu()
        {
            Console.WriteLine("1.Add Patient");
            Console.WriteLine("2.Add Doctor");
            Console.WriteLine("3.List Doctor");
            Console.WriteLine("4.Create Appointment");
            Console.WriteLine("5.Exit");

        }      

        void AddDoctor()
        {
            Doctor doctor = service.AddDoctor();
            context.Doctors.Add(doctor);
            context.SaveChanges();
        }
        void AddPatient()
        {
            Patient patient = service.AddPatient();
            context.Patients.Add(patient);
            context.SaveChanges();
        }

        void BookAppointment()
        {
            Appointment appointment = service.BookAppointment();
            var appointments = context.Appointments.ToList();
            var result = appointments.FirstOrDefault(a => a.DateTime.Month == appointment.DateTime.Month && a.DateTime.Date == appointment.DateTime.Date && a.DateTime.Hour == appointment.DateTime.Hour);
            if (result!=null)
            {
                Console.WriteLine("Appointment already exist at this time");
            }
            else
            {
                context.Appointments.Add(appointment);
                context.SaveChanges();
                Console.WriteLine($"Appointment succesfull for {appointment.AppointmentId}");
            }
            
        }

        void ListDoctors()
        {
            var doctors = context.Doctors.ToList();
            foreach(var doctor in doctors)
            {
                Console.WriteLine($"Doctor Id:{doctor.DoctorId}");
                Console.WriteLine($"Doctor Name:{doctor.DoctorName}");
                Console.WriteLine($"Doctor Specialization:{doctor.Specialization}");
                Console.WriteLine($"Doctor Phone number:{doctor.Phone}");
                Console.WriteLine($"---------------------");
            }
        }
        static void Main(string[] args)
        {
            Program program = new Program();
            int count = 0;
            do
            {
                program.Menu();
                Console.WriteLine("Enter the choice");
                count = Convert.ToInt32(Console.ReadLine());
                switch (count) { 
                   case 1:
                        program.AddPatient();
                        break;
                   case 2:
                        program.AddDoctor();
                        break;
                    case 3:
                        program.ListDoctors();
                        break;
                   case 4:
                        program.BookAppointment();
                        break;
                    default: Console.WriteLine("Select appropriate option");
                        break;
                
                }

            }while (count!=5);

        }
    }
}
