namespace Day28_EventBooking.Model
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int Age { get; set; }

        public string UserName { get; set; } = string.Empty;

        public User User { get; set; }

        public IEnumerable<EventBooking> EventBooking { get; set; }

        public Booking Booking { get; set; }


    }
}
