using AutoMapper;
using Day27_Webapi_EF.DTO;
using Day27_Webapi_EF.Interface;
using Day27_Webapi_EF.Models;
using Day27_Webapi_EF.Repositories;

namespace Day27_Webapi_EF.Services
{
    public class ImageItemService
    {
        private readonly IRepository<int, ImageItem> _imageItemRepository;
        private readonly IMapper _mapper;
        public ImageItemService(IRepository<int, ImageItem> imageItemRepository, IMapper mapper) {
            _imageItemRepository = imageItemRepository;
            _mapper = mapper;
        }

        public async Task<int> AddImage(ImageItemDTO imageItem)
        {

            ImageItem newImageItem = _mapper.Map<ImageItem>(imageItem);
            var addedProduct = await _imageItemRepository.Add(newImageItem);
            return addedProduct.ImageId;
        }

    }
}
