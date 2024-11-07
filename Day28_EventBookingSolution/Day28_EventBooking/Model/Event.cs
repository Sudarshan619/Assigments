namespace Day28_EventBooking.Model
{
    public class Event
    {
        public int EventId { get; set; }

        public string EventName { get; set; } = string.Empty;

        public string EventDescription { get; set; }

        public int MaxBooking { get; set; } = 50;

        public IEnumerable<EventBooking> EventBookings { get; set;}

        public Event()
        {

        }

    }
}
