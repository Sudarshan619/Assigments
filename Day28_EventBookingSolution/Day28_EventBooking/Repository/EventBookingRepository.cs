using Day28_EventBooking.Interface;
using Day28_EventBooking.Model;
using Day28_EventBooking.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Day28_EventBooking.Repository
{
    public class EventBookingRepository : IRepository<int, EventBooking>
    {
        private readonly EventBookingContext _bookingContext;

        public EventBookingRepository(EventBookingContext bookingContext)
        {
            _bookingContext = bookingContext;
        }
        public async Task<EventBooking> Add(EventBooking Entity)
        {
            try
            {
               _bookingContext.EventBookings.Add(Entity);
                await _bookingContext.SaveChangesAsync();
                return Entity;
            }
            catch (Exception ex)
            {
                throw new NotImplementedException("Not available");
            }

        }

        public Task<EventBooking> Delete(int key)
        {
            throw new NotImplementedException();
        }

        public async Task<EventBooking> Get(int key)
        {
            try
            {
                var eventBooking = await _bookingContext.EventBookings.FirstOrDefaultAsync(e => e.BookId == key);
                if (eventBooking == null)
                {
                    throw new NotImplementedException();
                }
                return eventBooking;

            }
            catch (Exception ex)
            {
                throw new NotImplementedException();

            }

        }

        public async Task<IEnumerable<EventBooking>> GetAll()
        {
            try
            {
                var eventBookings = await _bookingContext.EventBookings.ToListAsync();
                if (eventBookings.Any())
                {
                    return eventBookings;
                }
                throw new NotImplementedException();

            }
            catch (Exception ex)
            {
                throw new NotImplementedException();
            }
        }

        public Task<EventBooking> Update(int id,EventBooking Entity)
        {
            throw new NotImplementedException();
        }
    }
}
