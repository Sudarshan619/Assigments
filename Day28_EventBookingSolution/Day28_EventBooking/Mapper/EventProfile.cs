using AutoMapper;
using Day28_EventBooking.Model;
using Day28_EventBooking.DTO;

namespace Day28_EventBooking.Mapper
{
    public class EventProfile:Profile
    {
        public EventProfile() {
            CreateMap<Event,EventDTO>();
            CreateMap<EventDTO, Event>();
        }

    }
}
