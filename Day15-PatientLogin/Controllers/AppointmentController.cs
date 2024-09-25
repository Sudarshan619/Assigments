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
            ViewBag.Doctors = new SelectList(_appointmentService.GetAllDoctors(), "DoctorId", "Name");
            Appointment appointment = new Appointment();
            return View(appointment);
        }

      
        [HttpPost]

        public IActionResult BookAppointment(Appointment appointment)
        {
            Console.WriteLine(appointment.AppointmentId);
            Console.WriteLine(appointment.DoctorId);
            Console.WriteLine(appointment.PatientId);
            if (ModelState.IsValid)
            {
                bool isBooked = _appointmentService.BookAppointment(appointment.DoctorId, appointment.PatientId, appointment.AppointmentDate);
                if (isBooked)
                {
                    return RedirectToAction("Index"); 
                }
                ModelState.AddModelError("", "Doctor is not available at the selected time.");
            }

          
            ViewBag.Doctors = new SelectList(_appointmentService.GetAllDoctors(), "DoctorId", "Name", appointment.DoctorId);
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

            if (patient == null)
            {
                return NotFound();
            }

            var appointments = _appointmentService.GetAppointmentsForPatient(patient.Id);
            Console.WriteLine(patient.Id);
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
