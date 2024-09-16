using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day9_ClinicManagement
{
    public interface IClinicManagement
    {
        

        public Doctor LoginAsDoctor(List<Doctor> doctor);

        public Patient LoginAsPatient(List<Patient> patient);



    }
}
