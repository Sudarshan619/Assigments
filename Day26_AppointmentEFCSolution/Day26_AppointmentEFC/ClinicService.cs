﻿using Day26_AppointmentEFC.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Day26_AppointmentEFC
{
    public class ClinicService : IClinicService
    {
        public Doctor AddDoctor()
        {
            try { 
            Doctor doctor = new Doctor();
            Console.WriteLine("Enter doctor name");
            doctor.DoctorName = Console.ReadLine() ?? "";
            Console.WriteLine("Enter specialization");
            doctor.Specialization = Console.ReadLine() ?? "";
            Console.WriteLine("Enter phone");
            doctor.Phone = Convert.ToInt64(Console.ReadLine());

            return doctor;
             }
            catch(Exception ex)
            {
                throw new Exception("Error occured");
            }
        }

        public Patient AddPatient()
        {
            try
            {


                Patient patient = new Patient();
                Console.WriteLine("Enter patient name");
                patient.PatientName = Console.ReadLine() ?? "";
                Console.WriteLine("Enter patient email");
                patient.Email = Console.ReadLine() ?? "";
                Console.WriteLine("Enter patient phonenumber");
                patient.PhoneNumber = Convert.ToInt64(Console.ReadLine() ?? "");

                return patient;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occured");
            }
        }

        public Appointment BookAppointment()
        {
            try
            {
                Appointment appointment = new Appointment();
                Console.WriteLine("Enter patient id");
                appointment.PatientId = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter doctor id");
                appointment.DoctorId = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter appointment time");
                appointment.DateTime = Convert.ToDateTime(Console.ReadLine());

                return appointment;
            }

            catch(Exception ex)
            {
                throw new Exception("Error occured"); 
            }
            
        }

        
    }
}
