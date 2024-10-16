using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day26_AppointmentEFC.Model
{
    public class Doctor
    {
        public int DoctorId {  get; set; }

        public string DoctorName { get; set; }

        public string Specialization { get; set; }

        public long Phone {  get; set; }

        public IEnumerable<Appointment> Appointments { get; set; }
    }
}
