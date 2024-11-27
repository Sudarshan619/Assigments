using System.ComponentModel.DataAnnotations;

namespace QuizzApplicationBackend.Models
{
    public enum Roles
    {
        QuizzCreator,
        QuizzTaker
    }
    public class User
    {
        [Key]
        public int Id {  get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Image {  get; set; } = string.Empty;

        public byte[] Password { get; set; }

        public byte[] HashKey { get; set; }

        public Roles Role { get; set; }

        public IEnumerable<Quiz> Quiz { get; set; }

        public IEnumerable<Query> Queries { get; set; }

    }
}
