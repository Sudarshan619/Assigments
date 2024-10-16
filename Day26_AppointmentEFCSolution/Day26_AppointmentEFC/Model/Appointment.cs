using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day26_AppointmentEFC.Model
{
    public class Appointment
    {
       public int AppointmentId{ get; set; }

        [ForeignKey("PatientId")]
        public int PatientId { get; set; }

        public Patient Patient { get; set; }
        public DateTime DateTime { get; set; }

        [ForeignKey("DoctorId")]
        public int DoctorId { get; set; }

        public Doctor Doctor { get; set; }


    }
}
