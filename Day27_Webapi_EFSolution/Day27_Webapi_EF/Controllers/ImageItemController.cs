using Day27_Webapi_EF.DTO;
using Day27_Webapi_EF.Interface;
using Day27_Webapi_EF.Services;
using Microsoft.AspNetCore.Mvc;

namespace Day27_Webapi_EF.Controllers
{
    public class ImageItemController : ControllerBase
    {
       
        private readonly ImageItemService _imageItemService;

        public ImageItemController(ImageItemService imageItemService)
        {
            
            _imageItemService = imageItemService;
        }

        [HttpPost("AddImage")]
        public async Task<IActionResult> AddImage(ImageItemDTO imageItem)
        {
            try
            {
                var productId = await _imageItemService.AddImage(imageItem);
                return Ok(productId);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
