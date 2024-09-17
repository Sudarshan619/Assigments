using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day9_ClinicManagement
{
    public class Appointment
    {
        public int AppointmentId {  get; set; }

        public Doctor AppointedDoctor { get; set; } = new Doctor();

        public Patient AppointedPatient { get; set; } = new Patient();

        public DateTime AppointedTime { get;set; } = DateTime.Now;

    }
}
