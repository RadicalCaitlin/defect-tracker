using DefectTracker.Contracts.Repositories;
using DefectTracker.Contracts.Requests;
using DefectTracker.Web.ViewModels.Project;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Threading.Tasks;

namespace DefectTracker.Web.Controllers
{
    [Authorize]
    public class ProjectController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IProjectRepository _projectRepository;

        public ProjectController(UserManager<IdentityUser> userManager, IProjectRepository projectRepository)
        {
            _userManager = userManager;
            _projectRepository = projectRepository;
        }

        [Route("Project/{id:int}")]
        public async Task<IActionResult> Index(int? id = null)
        {
            if (id == null)
                return RedirectToAction("Index", "Home");

            var model = new IndexViewModel
            {
                Project = await _projectRepository.GetProjectByIdAsync(id.GetValueOrDefault())
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var model = new CreateProjectRequest
            {
                UserId = user.Id
            };

            if (TempData["CreateProjectRequest"] != null)
            {
                model = (CreateProjectRequest)TempData["CreateProjectRequest"];
                ViewData = (ViewDataDictionary)TempData["ViewData"];
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProjectRequest request)
        {
            if (!ModelState.IsValid)
            {
                TempData["CreateProjectRequest"] = request;
                TempData["ViewData"] = ViewData;
                return View();
            }

            var project = await _projectRepository.CreateProjectAsync(request.CreateProject());

            return RedirectToAction("Index", "Project", new { id = project.Id });
        }
    }
}
   