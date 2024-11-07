using AutoMapper;
using Day28_EventBooking.DTO;
using Day28_EventBooking.Interface;
using Day28_EventBooking.Model;

namespace Day28_EventBooking.Services
{
    public class EventService
    {
        IRepository<int, Event> _eventRepository;
        IMapper _mapper;
        public EventService(IRepository<int, Event> repository, IMapper mapper)
        {
            _eventRepository = repository;
            _mapper = mapper;

        }
        public async Task<bool> AddProduct(EventDTO Event)
        {

            Event newProduct = _mapper.Map<Event>(Event);
            var addedProduct = await _eventRepository.Add(newProduct);
            return addedProduct != null;
        }

        public async Task<IEnumerable<Event>> GetAllEvent()
        {
            try
            {
                var product = await _eventRepository.GetAll();
                if (product != null)
                {
                    return product;
                }
                else
                {
                    throw new NotFoundException("Event not found");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Not found");

            }

        }
    }
}
