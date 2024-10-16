using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day26_AppointmentEFC.Model
{
    public class Patient
    {

        public int PatientId {  get; set; }

        public string PatientName { get; set; } = string.Empty;

        public long PhoneNumber {  get; set; }

        public string Email { get; set; }

        public IEnumerable<Appointment> Appointments { get; set; }
    }
}
