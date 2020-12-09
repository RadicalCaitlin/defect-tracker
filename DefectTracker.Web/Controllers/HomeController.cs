using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DefectTracker.Web.Models;
using DefectTracker.Web.ViewModels.Home;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace DefectTracker.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly DefectTrackerDbContext _dbContext;

        public HomeController(
            UserManager<IdentityUser> userManager, 
            DefectTrackerDbContext dbContext
            )
        {
            _userManager = userManager;
            _dbContext = dbContext;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var model = new IndexViewModel
            {
                Projects = await _dbContext.Projects.Where(p => p.CreatedByUserId == user.Id).ToListAsync()
            };

            return View(model);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
