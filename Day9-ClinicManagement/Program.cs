using System.ComponentModel.Design;

namespace Day9_ClinicManagement
{
    internal class Program
    {
        IClinicManagement clinicManagement;
        Doctor Doctor ;
        Patient Patient ;
        Appointment PatientAppointment ;

        static List<Patient> PatientList = new List<Patient>();
        static List<Doctor> DoctorList = new List<Doctor>();
        static List<Appointment> AppointmentList = new List<Appointment>();
        static int AppointmentId = 500;

        static Doctor currDoctor;
        static Patient currPatient;
        Program() {
            clinicManagement  = new ClinicManagement();
            Doctor = new Doctor();
            Patient = new Patient();
            PatientAppointment = new Appointment();
        }
        void Menu()
        {
            Console.WriteLine("1.Doctor");
            Console.WriteLine("2.Patient");
            
        }
        void ShowDoctors()
        {
            foreach (Doctor doctor in DoctorList)
            {
                Console.WriteLine(doctor);
            }
        }

        void ShowPatients()
        {
            foreach (Patient patient in PatientList)
            {
                Console.WriteLine(patient);
            }
        }
        void RegisterAsDoctor()
        {
           
                DoctorList.Add(Doctor.Register(DoctorList));

            
        }

        void BookAppointment()
        {
            AppointmentList.Add(Patient.MakeAppointment(DoctorList, AppointmentId,currPatient));
        }
        void RegisterAsPatient()
        {
            PatientList.Add(Patient.Register(PatientList));
        }
        
        static void Main(string[] args)
        {
            Program program = new Program();
            int choice = 0;
            try
            {
                do
                {
                    program.Menu();

                    choice = Convert.ToInt32(Console.ReadLine());
                    switch (choice)
                    {
                        case 0:
                            Console.WriteLine("Exit");
                            break;
                        case 1:
                            Console.WriteLine("1.Register");
                            Console.WriteLine("2.Login");
                            Console.WriteLine("To Exit press any number except 1 and 2");

                            int val = Convert.ToInt32(Console.ReadLine());
                            if (val == 1)
                            {
                                program.RegisterAsDoctor();
                            }
                            else if (val == 2)
                            {
                                currDoctor = program.clinicManagement.LoginAsDoctor(DoctorList);

                                int selection = 0;
                                do
                                {
                                    Console.WriteLine("1.Show Patients");
                                    Console.WriteLine("2.Show Appointment");
                                    Console.WriteLine("3.Logout");

                                    selection = Convert.ToInt32(Console.ReadLine());
                                    switch (selection)
                                    {
                                        case 1:
                                            program.ShowPatients();
                                            break;
                                        case 2:
                                            program.Doctor.ShowAppointments(currDoctor, AppointmentList);
                                            break;
                                        case 3:
                                            currDoctor = null;
                                            break;
                                        default:
                                            break;
                                    }

                                } while (selection != 3);

                            }

                            break;
                        case 2:

                            Console.WriteLine("1.Register");
                            Console.WriteLine("2.Login");

                            int val1 = Convert.ToInt32(Console.ReadLine());
                            if (val1 == 1)
                            {
                                program.RegisterAsPatient();
                            }
                            else if (val1 == 2)
                            {

                                currPatient = program.clinicManagement.LoginAsPatient(PatientList);

                                int selection = 0;
                                do
                                {

                                    Console.WriteLine("1.Show doctors");
                                    Console.WriteLine("2.Book Appointment");
                                    Console.WriteLine("3.Show Appointment");
                                    Console.WriteLine("4.Logout");
                                    selection = Convert.ToInt32(Console.ReadLine());
                                    switch (selection)
                                    {
                                        case 1:
                                            program.ShowDoctors();
                                            break;
                                        case 2:
                                            program.BookAppointment();
                                            break;
                                        case 3:
                                            program.Patient.ShowAppointments(currPatient, AppointmentList);
                                            break;
                                        case 4:
                                            currPatient = null;
                                            break;
                                        default:
                                            break;
                                    }
                                } while (selection != 4);

                            }

                            break;
                    }

                } while (choice != 0);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
