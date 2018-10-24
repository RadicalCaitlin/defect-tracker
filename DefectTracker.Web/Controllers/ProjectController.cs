using DefectTracker.Contracts.Repositories;
using DefectTracker.Contracts.Requests;
using DefectTracker.Web.ViewModels.Defect;
using DefectTracker.Web.ViewModels.Project;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Linq;
using System.Threading.Tasks;

namespace DefectTracker.Web.Controllers
{
    [Authorize]
    public class ProjectController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IDefectRepository _defectRepository;
        private readonly IProjectRepository _projectRepository;

        public ProjectController(
            UserManager<IdentityUser> userManager,
            IDefectRepository defectRepository,
            IProjectRepository projectRepository)
        {
            _userManager = userManager;
            _defectRepository = defectRepository;
            _projectRepository = projectRepository;
        }

        [Route("Project/{id:int}")]
        public async Task<IActionResult> Index(int id)
        {
            if (id == 0)
                return RedirectToAction("Index", "Home");

            var project = await _projectRepository.GetProjectByIdAsync(id);
            var defects = await _defectRepository.GetDefectsByProjectIdAsync(id);
            var projectForChart = new ProjectForChart
            {
                CreatedByUserId = project.CreatedByUserId,
                DateCreated = project.DateCreatedOffset.ToString("MM/dd/yyyy"),
                Id = project.Id,
                Name = project.Name,
                OriginDate = project.OriginDateOffset.ToString("MM/dd/yyyy")
            };

            var model = new IndexViewModel
            {
                Bugs = await _projectRepository.GetBugsByProjectIdAsync(id),
                Defects = defects.Select(x => new DefectsForChart
                {
                    CreatedByUserId = x.CreatedByUserId,
                    DateCreated = x.DateCreatedOffset.ToString("MM/dd/yyyy"),
                    DefectQualifierTypeId = x.DefectQualifierTypeId,
                    DefectTypeId = x.DefectTypeId,
                    Id = x.Id,
                    ProjectId = x.ProjectId,
                    BugId = x.BugId,
                    DefectModelTypeId = x.DefectModelTypeId
                }),
                DefectTypes = await _defectRepository.GetDefectTypesAsync(),
                Project = projectForChart
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

        [HttpGet]
        public IActionResult CreateBug(int projectId)
        {
            var model = new CreateBugRequest
            {
                ProjectId = projectId
            };

            if (TempData["CreateBugRequest"] != null)
            {
                var data = TempData.Get<CreateBugRequest>("CreateBugRequest");
                model.OriginDateOffset = data.OriginDateOffset;
                model.ProjectId = data.ProjectId;
            }

            return View(model);
        }

        [Route("Project/{id:int}/Manage")]
        public async Task<IActionResult> Manage(int id)
        {
            if (id == 0)
                return RedirectToAction("Index", "Home");

            var model = new ManageViewModel
            {
                Project = await _projectRepository.GetProjectByIdAsync(id),
                Activities = await _projectRepository.GetActivitiesByProjectIdAsync(id),
                Areas = await _projectRepository.GetAreasByProjectIdAsync(id),
                Tasks = await _projectRepository.GetTasksByProjectIdAsync(id),
                Users = await _projectRepository.GetProjectUsersByProjectIdAsync(id)
            };

            if (TempData["CreateAreaRequest"] != null)
            {
                model.CreateAreaRequest = (CreateAreaRequest)TempData["CreateAreaRequest"];
                ViewData = (ViewDataDictionary)TempData["ViewData"];
            }

            if (TempData["CreateProjectUserRequest"] != null)
            {
                model.CreateProjectUserRequest = (CreateProjectUserRequest)TempData["CreateProjectUserRequest"];
                ViewData = (ViewDataDictionary)TempData["ViewData"];
            }

            return View(model);
        }

        #region FormPosts

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

        [HttpPost]
        public async Task<IActionResult> CreateArea(CreateAreaRequest request)
        {
            if (!ModelState.IsValid)
            {
                TempData["CreateAreaRequest"] = request;
                TempData["ViewData"] = ViewData;
                return RedirectToAction("Manage", "Project", new { id = request.ProjectId });
            }

            var area = await _projectRepository.CreateProjectAreasAsync(request.CreateProjectArea());

            return RedirectToAction("Manage", "Project", new { id = request.ProjectId });
        }

        [HttpPost]
        public async Task<IActionResult> CreateBug(CreateBugRequest request)
        {
            TempData.Put("CreateBugRequest", request);

            if (!ModelState.IsValid)
            {
                TempData["ViewData"] = ViewData;
                return RedirectToAction("CreateBug", "Project", new { projectId = request.ProjectId });
            }

            var bug = await _projectRepository.CreateProjectBugAsync(request.CreateBug());

            return RedirectToAction("CreateBug", "Project", new { projectId = request.ProjectId });
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateProjectUserRequest request)
        {
            if (!ModelState.IsValid)
            {
                TempData["CreateProjectUserRequest"] = request;
                TempData["ViewData"] = ViewData;
                return RedirectToAction("Manage", "Project", new { id = request.ProjectId });
            }

            var area = await _projectRepository.CreateProjectUserAsync(request.CreateProjectUser());

            return RedirectToAction("Manage", "Project", new { id = request.ProjectId });
        }

        #endregion
    }
}
