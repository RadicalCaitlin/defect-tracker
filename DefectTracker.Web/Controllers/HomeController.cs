using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DefectTracker.Web.Models;
using DefectTracker.Web.ViewModels.Home;
using Microsoft.AspNetCore.Identity;
using DefectTracker.Contracts.Repositories;
using System.Threading.Tasks;
using System.Collections.Generic;
using DefectTracker.Core;

namespace DefectTracker.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IProjectRepository _projectRepository;

        public HomeController(UserManager<IdentityUser> userManager, IProjectRepository projectRepository)
        {
            _userManager = userManager;
            _projectRepository = projectRepository;
        }

        public async Task<IActionResult> Index()
        {
            var model = new IndexViewModel
            {
                Projects = await _projectRepository.GetAllProjectsAsync()
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
