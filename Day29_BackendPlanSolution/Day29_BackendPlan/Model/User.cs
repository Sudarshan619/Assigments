using System.ComponentModel.DataAnnotations;

namespace Day29_BackendPlan.Model
{
    public enum Roles
    {
        Admin,
        Employee,
        PolicyHolder
    }
    public class User
    {
        [Key]
        public string Username { get; set; } = string.Empty;
        public byte[] Password { get; set; }
        public byte[] HashKey { get; set; }
        public Roles Role { get; set; }
        public PolicyHolder PolicyHolder { get; set; }

    }
}
