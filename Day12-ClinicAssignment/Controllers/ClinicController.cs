using Day12_ClinicAssignment.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace Day12_ClinicAssignment.Controllers
{
    public class ClinicController : Controller
    {
        static int Count = 6;
        static List<Doctor> doctors = new List<Doctor>() {
               new Doctor(1,"Thomas","Gynocologist","Male","Images/d1.jpeg",8129874737),
               new Doctor(2,"Mahesh","Othopedic","Male","Images/d2.jpeg",8942346734),
               new Doctor(3,"Asha","Pediatritian","Female","Images/d3.jpeg",9387466322),
               new Doctor(4,"Kavya","Surgeon","Female","Images/d4.jpeg",7323797239),
               new Doctor(5,"Pramila","Neurologist","Female","Images/d5.jpeg",6274927927),
               new Doctor(6,"Hema","Surgeon","Female","Images/d6.jpeg",8227428433)

            };
        private IWebHostEnvironment _environment;
        static string wwwPath;
        static string path;
        public ClinicController(IWebHostEnvironment Environment)
        {

            _environment = Environment;
             wwwPath = _environment.WebRootPath;
             path = Path.Combine(wwwPath, "Images");
        }

        

        
        public IActionResult Index()
        {
            return View(doctors);
        }

        [HttpGet]
        public IActionResult Create()
        {   
            return View();
        }

        [HttpPost]
        public IActionResult Create(Doctor doctor, IFormFile postedFile)
        {
          
            // Ensure the Images directory exists
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            if (postedFile != null && postedFile.Length > 0)
            {
                
                string fileName = Path.GetFileName(postedFile.FileName);
                string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(fileName);
                string fullPath = $"{path}/{uniqueFileName}";


                using (FileStream stream = new FileStream(fullPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }


               
                doctor.Image = Path.Combine("Images", uniqueFileName);
            }
            Count++;
            doctor.Id =Count ;
            doctors.Add(doctor); 
            return RedirectToAction("Index");
        }


        [HttpGet]

        public IActionResult Delete(int id) { 
          
            Doctor doctor = doctors.FirstOrDefault(e=>e.Id == id);
            return View(doctor);
        }

        [HttpPost]
		public IActionResult Delete(int id,Doctor doctor)
		{
			Doctor olddoctor = doctors.FirstOrDefault(e => e.Id == id);
            doctors.Remove(olddoctor);
            return RedirectToAction("Index");
		}

        [HttpGet]

        public IActionResult Edit(int id)
        {
            Doctor doctor = doctors.FirstOrDefault(e=>e.Id == id);
            return View(doctor);
        }

        [HttpPost]
		public IActionResult Edit(int id,Doctor doctor,IFormFile postedFile)
		{
			Doctor olddoctor = doctors.FirstOrDefault(e => e.Id == id);
            olddoctor.Name = doctor.Name;
            olddoctor.Contact = doctor.Contact;
            olddoctor.Specialization = doctor.Specialization;
            olddoctor.Gender = doctor.Gender;

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            if (postedFile != null && postedFile.Length > 0)
            {

                string fileName = Path.GetFileName(postedFile.FileName);
                string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(fileName);
                string fullPath = Path.Combine(path, uniqueFileName);


                using (FileStream stream = new FileStream(fullPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }



                olddoctor.Image = Path.Combine("Images", uniqueFileName);
            }


            return RedirectToAction("Index");
		}
	}
}
