using Day26_AppointmentEFC.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day26_AppointmentEFC.Context
{
    internal class ClinicContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=LAPTOP-JIQ20LLJ\\ROOT;Integrated Security=true;Initial Catalog=dbEFCode15Oct24;TrustServerCertificate=True");
        }

        public DbSet<Doctor> Doctors { get; set; }

        public DbSet<Patient> Patients { get; set; }

        public DbSet<Appointment> Appointments { get; set; }

    }
}
