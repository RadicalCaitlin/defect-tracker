using System.Threading.Tasks;
using DefectTracker.Contracts.Repositories;
using DefectTracker.Contracts.Requests;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace DefectTracker.Web.Controllers
{
    public class DefectController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IDefectRepository _defectRepository;
        private readonly IProjectRepository _projectRepository;

        public DefectController(
            UserManager<IdentityUser> userManager, 
            IDefectRepository defectRepository,
            IProjectRepository projectRepository)
        {
            _userManager = userManager;
            _defectRepository = defectRepository;
            _projectRepository = projectRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Create(int projectId, int bugId, int defectModelId)
        {
            //var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var bug = await _projectRepository.GetProjectBugByIdAsync(bugId);

            var model = new CreateDefectRequest
            {
                //CreatedByUserId = user.Id,
                ProjectId = projectId,
                DefectQualifiers = await _defectRepository.GetDefectQualifiersAsync(),
                DefectTypes = await _defectRepository.GetDefectTypesAsync(),
                Bug = bug,
                DefectModelTypeId = defectModelId,
                OriginDateCreatedOffset = bug.OriginDateOffset
            };

            if (TempData["CreateDefectRequest"] != null)
            {
                model = (CreateDefectRequest)TempData["CreateDefectRequest"];
                ViewData = (ViewDataDictionary)TempData["ViewData"];
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateDefectRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            var defect = await _defectRepository.CreateDefectAsync(request.CreateDefect());

            return RedirectToAction("Index", "Project", new { id = request.ProjectId });
        }
    }
}