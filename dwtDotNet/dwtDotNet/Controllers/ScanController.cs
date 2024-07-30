using dwtDotNet.Share.Entites;
using Microsoft.AspNetCore.Mvc;

namespace dwtDotNet.Controllers
{
    public class ScanController : Controller
    {
        private readonly string _imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Scan()
        {
            return View();
        }
    }
}
