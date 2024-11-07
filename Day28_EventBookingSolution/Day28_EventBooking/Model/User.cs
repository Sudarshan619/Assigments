using System.ComponentModel.DataAnnotations;

namespace Day28_EventBooking.Model
{
    public enum Roles{
       Admin,
       Cutsomer,
       Manager
    }
    public class User
    {
        [Key]
        public string UserName { get; set; }
        public byte[] Password { get; set; }
        public byte[] HashKey { get; set; }
        public Roles Role { get; set; }
        public Employee Employee { get; set; }
    }
}
