using dwtDotNet.Data;
using dwtDotNet.Helper;
using dwtDotNet.Models;
using dwtDotNet.Share.Entites;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace dwtDotNet.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IFileStorage _fileStorage;

        public ImagesController(DataContext context, IFileStorage fileStorage)
        {
            _context = context;
            _fileStorage = fileStorage;
        }
        [HttpPost]
        public async Task<IActionResult> UploadImage([FromBody] ImageviewModel model)
        {
            if (!string.IsNullOrEmpty(model.ImageId))
            {
                var photoUser = Convert.FromBase64String(model.ImageId);
                model.ImageId = await _fileStorage.SaveFileAsync(photoUser, ".jpg", "images");
            }
            // Save image metadata to database
            var image = new image
            {
                Id = model.Id,
                Title = model.Title,
                ImageId = model.ImageId
            };

            _context.Images.Add(image);
            await _context.SaveChangesAsync();

            return Ok(new { success = true });
        }
    }
}
