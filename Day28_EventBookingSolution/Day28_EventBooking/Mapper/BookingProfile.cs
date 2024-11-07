using Day28_EventBooking.DTO;
using Day28_EventBooking.Model;
using AutoMapper;

namespace Day28_EventBooking.Mapper
{
    public class BookingProfile:Profile
    {
        public BookingProfile()
        {
            CreateMap<Booking, BookingDTO>();
            CreateMap<BookingDTO, Booking>();
        }
    }
}
