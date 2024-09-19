using System.ComponentModel.DataAnnotations;

namespace Day12_WebApplicationTask.Models
{
    public class Profile
    {
        public string Name  { get; set; }

        public string Description { get; set; }

        public string Skills { get; set; }

        public string Image { get; set; }

        public string Address { get; set; }

        public long Phone { get; set; }

        public string Email { get; set; }

        public string GithubUrl { get; set; }

        public string Linkedin { get; set; }

        List<string> Projects ;
        public Profile() {
            Linkedin = "https://www.linkedin.com/in/sudarshan-k-8465b3252/";
            Projects = new List<string>() { "Job manager", "Image Restoration", "Morse code based authentication" };
            GithubUrl = "https://github.com/Sudarshan619";
            Image = "/Images/pic2.jpg";
            Address = "Bengaluru , Karnataka";
            Phone = 912838836;
            Email = "pujarsudarshan@gmail.com";
            Name = "Sudarshan K";
            Description = "Full Stack Dot net Developer";
            Skills = "HTML,CSS,JS,Bootstrap,C#";
        }
    }
}
