using Day28_EventBooking.Interface;
using Day28_EventBooking.Model;
using Day28_EventBooking.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Day28_EventBooking.Repository
{
    public class EventRepository : IRepository<int, Event>
    {
        private readonly EventBookingContext _eventBookingContext;

        public EventRepository(EventBookingContext eventBookingContext)
        {
            _eventBookingContext = eventBookingContext;
        }
        public async Task<Event> Add(Event Entity)
        {
            try
            {
                _eventBookingContext.Events.Add(Entity);
                await _eventBookingContext.SaveChangesAsync();
                return Entity;
            }
            catch (Exception ex) {
                throw new NotImplementedException("Not available");
            }

        }

        public Task<Event> Delete(int key)
        {
            throw new NotImplementedException();
        }

        public async Task<Event> Get(int key)
        {
            try { 
                 var Event = await _eventBookingContext.Events.FirstOrDefaultAsync(e => e.EventId ==key);
                 if(Event == null ){
                     throw new NotImplementedException();
                 }
                 return Event;

            }
           catch(Exception ex) {
              throw new NotImplementedException();

             }
           
        }

        public async Task<IEnumerable<Event>> GetAll()
        {
            try
            {
                var Events = await _eventBookingContext.Events.ToListAsync();
                if (Events.Any()) {
                    return Events;
                }
                throw new NotImplementedException();
                
            }
            catch (Exception ex) { throw new NotImplementedException(); }
        }

        public Task<Event> Update(int id,Event Entity)
        {
            throw new NotImplementedException();
        }
    }
}
