using Day12_ClinicAssignment.Models;
using Microsoft.AspNetCore.Mvc;

namespace Day12_ClinicAssignment.Controllers
{
    public class ClinicController : Controller
    {
        public IActionResult Index()
        {
            List<Doctor> doctors = new List<Doctor>() {
               new Doctor(1,"Thomas","Gynocologist","Male","/Images/d1.jpeg",8129874737),
               new Doctor(2,"Mahesh","Othopedic","Male","/Images/d2.jpeg",8942346734),
               new Doctor(3,"Asha","Pediatritian","Female","/Images/d3.jpeg",9387466322),
               new Doctor(4,"Kavya","Surgeon","Female","/Images/d4.jpeg",7323797239),
			   new Doctor(5,"Pramila","Neurologist","Female","/Images/d5.jpeg",6274927927),
			   new Doctor(6,"Hema","Surgeon","Female","/Images/d6.jpeg",8227428433)

			};

            return View(doctors);
        }
    }
}
