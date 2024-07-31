using dwtDotNet.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dwtDotNet.Controllers
{
    public class ImagesController : Controller
    {
        private readonly DataContext _context;

        public ImagesController(DataContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Images.ToListAsync());
        }
    }
}
