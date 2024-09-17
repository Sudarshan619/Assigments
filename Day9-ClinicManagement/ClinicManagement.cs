using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Day9_ClinicManagement
{
    internal class ClinicManagement : IClinicManagement
    {

        
        public Doctor LoginAsDoctor(List<Doctor> doctor)
        {
            try
            {
                Console.WriteLine("Enter doctor id");
                var id = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter doctor name");
                var name = Console.ReadLine();
                var currDoctor = doctor.FirstOrDefault(e => e.DoctorName == name && e.DoctorId == id);

                if (currDoctor != null)
                {
                    Console.WriteLine("login successfull as doctor");
                    return currDoctor;
                }
                else
                {
                    throw new Exception("Doctor does not exist");
                }
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }

            return null;
        }

        public Patient LoginAsPatient(List<Patient> patient)
        {

            try
            {
                Console.WriteLine("Enter patient id");
                var id = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter patient name");
                var name = Console.ReadLine();
                var currpatient = patient.FirstOrDefault(e => e.Name == name && e.Id == id);

                if (currpatient != null)
                {
                    Console.WriteLine("login successfull as patient");
                    return currpatient;
                }
                else
                {
                    throw new Exception("Patient does not exist");
                }
            }
            catch (Exception ex){
               Console.WriteLine(ex.Message);
            }

            return null;
            
        }

        
    }
}
