using AutoMapper;
using Day28_EventBooking.DTO;
using Day28_EventBooking.Interface;
using Day28_EventBooking.Model;

namespace Day28_EventBooking.Services
{
    public class EventBookingService
    {
        IRepository<int, EventBooking> _EventBookingRepository;
        IMapper _mapper;
        public EventBookingService(IRepository<int, EventBooking> repository, IMapper mapper)
        {
            _EventBookingRepository = repository;
            _mapper = mapper;

        }
        public async Task<bool> AddProduct(EventBookingDTO EventBooking)
        {

            EventBooking newProduct = _mapper.Map<EventBooking>(EventBooking);
            var addedProduct = await _EventBookingRepository.Add(newProduct);
            return addedProduct != null;
        }

        public async Task<IEnumerable<EventBooking>> GetAllEventBooking()
        {
            try
            {
                var product = await _EventBookingRepository.GetAll();
                if (product != null)
                {
                    return product;
                }
                else
                {
                    throw new NotFoundException("EventBooking not found");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Not found");

            }

        }
    }
}
