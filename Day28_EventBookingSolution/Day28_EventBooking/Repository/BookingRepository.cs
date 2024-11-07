using Day28_EventBooking.Interface;
using Day28_EventBooking.Model;
using Day28_EventBooking.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Day28_EventBooking.Repository
{
    public class BookingRepository : IRepository<int, Booking>
    {
        private readonly EventBookingContext _bookingContext;

        public BookingRepository(EventBookingContext BookingContext)
        {
            _bookingContext = BookingContext;
        }
        public async Task<Booking> Add(Booking Entity)
        {
            try
            {
                _bookingContext.Bookings.Add(Entity);
                await _bookingContext.SaveChangesAsync();
                return Entity;
            }
            catch (Exception ex)
            {
                throw new NotImplementedException("Not available");
            }

        }

        public Task<Booking> Delete(int key)
        {
            throw new NotImplementedException();
        }

        public async Task<Booking> Get(int key)
        {
            try
            {
                var Booking = await _bookingContext.Bookings.FirstOrDefaultAsync(e => e.Id == key);
                if (Booking == null)
                {
                    throw new NotImplementedException();
                }
                return Booking;

            }
            catch (Exception ex)
            {
                throw new NotImplementedException();

            }

        }

        public async Task<IEnumerable<Booking>> GetAll()
        {
            try
            {
                var Bookings = await _bookingContext.Bookings.ToListAsync();
                if (Bookings.Any())
                {
                    return Bookings;
                }
                throw new NotImplementedException();

            }
            catch (Exception ex)
            {
                throw new NotImplementedException();
            }
        }

        public Task<Booking> Update(int id,Booking Entity)
        {
            throw new NotImplementedException();
        }
    }
}
