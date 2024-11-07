using AutoMapper;
using Day28_EventBooking.Interface;
using Day28_EventBooking.Model;
using Day28_EventBooking.DTO;

namespace Day28_EventBooking.Services
{
    public class BookingService
    {
        IRepository<int, Booking> _BookingRepository;
        IMapper _mapper;
        public BookingService(IRepository<int, Booking> repository, IMapper mapper)
        {
            _BookingRepository = repository;
            _mapper = mapper;

        }
        public async Task<bool> AddProduct(BookingDTO Booking)
        {

            Booking newProduct = _mapper.Map<Booking>(Booking);
            var addedProduct = await _BookingRepository.Add(newProduct);
            return addedProduct != null;
        }

        public async Task<IEnumerable<Booking>> GetAllBooking()
        {
            try
            {
                var product = await _BookingRepository.GetAll();
                if (product != null)
                {
                    return product;
                }
                else
                {
                    throw new NotFoundException("Booking not found");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Not found");

            }

        }
    }
}
