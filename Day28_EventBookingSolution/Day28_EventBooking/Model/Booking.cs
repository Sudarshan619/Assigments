namespace Day28_EventBooking.Model
{
    public class Booking
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public DateTime DateOfBooking {  get; set; }
        public Employee Employee { get; set; }
        public Booking() { 
          Employee = new Employee();
        }

    }
}
