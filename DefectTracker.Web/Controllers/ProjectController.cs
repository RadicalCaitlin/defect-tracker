using DefectTracker.Core;
using DefectTracker.Web.Models.AreaViewModels;
using DefectTracker.Web.Models.BugViewModels;
using DefectTracker.Web.Models.ProjectViewModels;
using DefectTracker.Web.ViewModels.Defect;
using DefectTracker.Web.ViewModels.Project;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace DefectTracker.Web.Controllers
{
    [Authorize]
    public class ProjectController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly DefectTrackerDbContext _dbContext;

        public ProjectController(
            UserManager<IdentityUser> userManager,
            DefectTrackerDbContext dbContext)
        {
            _userManager = userManager;
            _dbContext = dbContext;
        }

        [Route("Project/{id:int}")]
        public async Task<IActionResult> Index(int id, DateTime? startDate = null, DateTime? endDate = null, GroupChartBy groupBy = GroupChartBy.Week)
        {
            if (id == 0)
                return RedirectToAction("Index", "Home");

            var model = new IndexViewModel();

            if (startDate != null)
            {
                model.StartDate = startDate.GetValueOrDefault();
            }

            if (endDate != null)
            {
                model.EndDate = endDate.GetValueOrDefault();
            }

            var project = await _dbContext.Projects.SingleOrDefaultAsync(p => p.Id == id);
            var defects = await _dbContext.Defects.Where(d => d.ProjectId == id).ToListAsync();
            var projectForChart = new ProjectForChart
            {
                CreatedByUserId = project.CreatedByUserId,
                DateCreated = project.DateCreatedOffset.ToString("MM/dd/yyyy"),
                Id = project.Id,
                Name = project.Name,
                OriginDate = project.OriginDateOffset.ToString("MM/dd/yyyy")
            };

            var filteredDefects = defects
                .Where(d => d.OriginDateCreatedOffset.ToUniversalTime().Date >= model.StartDate
                && d.OriginDateCreatedOffset.ToUniversalTime().Date <= model.EndDate)
                .OrderBy(d => d.OriginDateCreatedOffset);

            model.Bugs = await _dbContext.ProjectBugs.Where(pb => pb.ProjectId == id).ToListAsync();
            model.Defects = filteredDefects.Select(x => new DefectsForChart
            {
                CreatedByUserId = x.CreatedByUserId,
                OriginDate = x.OriginDateCreatedOffset.ToString("MM/dd/yyyy"),
                DefectQualifierTypeId = x.DefectQualifierTypeId,
                DefectTypeId = x.DefectTypeId,
                Id = x.Id,
                ProjectId = x.ProjectId,
                BugId = x.BugId,
                DefectModelTypeId = x.DefectModelTypeId
            });
            model.DefectTypes = await _dbContext.DefectTypes.ToListAsync();
            model.Project = projectForChart;
            model.GroupBy = groupBy;

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

            var project = await _dbContext.Projects
                .Include(p => p.Activities)
                .Include(p => p.ProjectAreas)
                .SingleOrDefaultAsync(p => p.Id == id);


            var model = new ManageViewModel
            {
                Project = project,
                Activities = project.Activities,
                Areas = project.ProjectAreas,
                Tasks = project.Tasks,
                Users = project.ProjectUsers
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

            var project = request.CreateProject();

            _dbContext.Projects.Add(project);
            await _dbContext.SaveChangesAsync();

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

            _dbContext.ProjectAreas.Add(request.CreateProjectArea());
            await _dbContext.SaveChangesAsync();

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

            _dbContext.ProjectBugs.Add(request.CreateBug());
            await _dbContext.SaveChangesAsync();

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

            _dbContext.ProjectUsers.Add(request.CreateProjectUser());
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Manage", "Project", new { id = request.ProjectId });
        }

        #endregion
    }
}
