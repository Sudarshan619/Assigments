using Microsoft.AspNetCore.Mvc;
using Day15_PatientLogin.Models;
using Day15_PatientLogin.Service;
using Microsoft.AspNetCore.Mvc.Rendering;
using Day15_PatientLogin.Repositories;

namespace Day15_PatientLogin.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly AppointmentService _appointmentService;
        private readonly PatientRepository _records ;
        
        public AppointmentController(AppointmentService appointmentService,PatientRepository patientRepository)
        {
            _appointmentService = appointmentService;
            _records = patientRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var username = HttpContext.Session.GetString("username");

            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("Login", "Account");
            }

            Patient patient = GetPatientByUsername(username);

            if (patient == null)
            {
                return NotFound();
            }

           
            ViewBag.Username = username;
            return View(patient);
        }

        [HttpGet]
        public IActionResult BookAppointment()
        {
            
            Appointment appointment = new Appointment();
            var doctor = _appointmentService.GetAllDoctors().ToList();
            List<int> ids = new List<int>();
            foreach (var doc in doctor)
            {
                ids.Add(doc.DoctorId);
            }
            ViewBag.Doctors = new SelectList(ids,"DoctorId");
            return View(appointment);
            
        }

      
        [HttpPost]

        public IActionResult BookAppointment(Appointment appointment)
        {
            bool isBooked = _appointmentService.BookAppointment(appointment.DoctorId, appointment.PatientId, appointment.AppointmentDate);
                if (isBooked)
                {
                    Console.WriteLine("isBooked");
                    return RedirectToAction("MyAppointments", "Appointment"); 
                }

            ModelState.AddModelError("", "Doctor is not available at the selected time or doctor does not exist");
            return View(appointment);
        }

        [HttpGet]
        public IActionResult MyAppointments()
        {
            var username = HttpContext.Session.GetString("username");

            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("Login", "Account");
            }

            Patient patient = GetPatientByUsername(username);
            Console.WriteLine("Patient" + patient);

            if (patient == null)
            {
                return NotFound();
            }

            var appointments = _appointmentService.GetAppointmentsForPatient(patient.Id);
            Console.WriteLine("line 91"+patient.Id);
            ViewBag.Username = username;
            Console.WriteLine(appointments.Count);
            return View(appointments);
        }


        private Patient GetPatientByUsername(string username)
        {        
            var patient = _records.GetPatientByUsername(username);
            Console.WriteLine(patient.UserName);
            return patient;
        }
    }
}
