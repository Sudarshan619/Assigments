using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day9_ClinicManagement
{
    public class Appointment
    {
        public Appointment() { }

        public int AppointmentId {  get; set; }

        public Doctor AppointedDoctor { get; set; }

        public Patient AppointedPatient { get; set; }

        public DateTime AppointedTime { get;set; }



    }
}
