using Day26_AppointmentEFC.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day26_AppointmentEFC
{
    internal interface IClinicService
    {
        public Doctor AddDoctor();
        public Patient AddPatient();
        public Appointment BookAppointment();

    }
}
