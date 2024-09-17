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
            if (DoctorList.Count <= 0)
            {
                Console.WriteLine("No doctors found");
            }
            else
            {
                foreach (Doctor doctor in DoctorList)
                {
                    Console.WriteLine(doctor);
                }
            }
            
        }

        void ShowPatients()
        {
            if (PatientList.Count <= 0)
            {
                Console.WriteLine("No patients found");
            }

            else
            {
                foreach (Patient patient in PatientList)
                {
                    Console.WriteLine(patient);
                }
            }
            
        }
        void RegisterAsDoctor()
        {
            var doctor = Doctor.Register(DoctorList);
            if (doctor != null)
            {
                DoctorList.Add(doctor);
            }
              

        }

        void BookAppointment()

        {
            var result = Patient.MakeAppointment(DoctorList, AppointmentList, AppointmentId, currPatient);
            if (result != null)
            {
                AppointmentList.Add(result);
                AppointmentId = AppointmentId + 1;
            }
  
        }
        void RegisterAsPatient()
        {
            var patient = Patient.Register(PatientList);
            if (patient != null)
            {
                PatientList.Add(patient);
            }
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
                                var result = program.clinicManagement.LoginAsDoctor(DoctorList);
                                Console.WriteLine(result);
                                if (result != null)
                                {
                                    currDoctor = result;

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

                                var result = program.clinicManagement.LoginAsPatient(PatientList);

                                if (result != null)
                                {
                                    currPatient = result;



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
