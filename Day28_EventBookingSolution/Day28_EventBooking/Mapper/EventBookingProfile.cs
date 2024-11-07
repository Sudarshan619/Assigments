using AutoMapper;
using Day28_EventBooking.DTO;
using Day28_EventBooking.Model;

namespace Day28_EventBooking.Mapper
{
    public class EventBookingProfile:Profile
    {
        public EventBookingProfile() {
            CreateMap<EventBooking, EventBookingDTO>();
            CreateMap<EventBookingDTO, EventBooking>();
        }
    }
}
