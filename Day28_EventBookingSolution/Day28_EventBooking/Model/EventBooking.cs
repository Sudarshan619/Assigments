namespace Day28_EventBooking.Model
{
    public class EventBooking
    {
        public int BookId { get; set; }
        public DateTime BookingDate { get; set; }
        public int EmployeeId { get; set; }
     
        public int EventId { get; set; }
        public int NoOfTickets  { get; set; }

        public Employee Employee { get; set; }
        public Event Event { get; set; }

        public EventBooking()
        {
            Employee = new Employee();
            Event = new Event();
        }
    }
}
