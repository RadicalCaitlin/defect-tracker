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

        public DefectController(UserManager<IdentityUser> userManager, IDefectRepository defectRepository)
        {
            _userManager = userManager;
            _defectRepository = defectRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Create(int projectId)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var model = new CreateDefectRequest
            {
                CreatedByUserId = user.Id,
                ProjectId = projectId,
                DefectQualifiers = await _defectRepository.GetDefectQualifiersAsync(),
                DefectReportedByTypes = await _defectRepository.GetDefectReportedByTypesAsync(),
                DefectTypes = await _defectRepository.GetDefectTypesAsync()
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
                TempData["CreateDefectRequest"] = request;
                TempData["ViewData"] = ViewData;
                return View();
            }

            var defect = await _defectRepository.CreateDefectAsync(request.CreateDefect());

            return RedirectToAction("Index", "Project", new { id = request.ProjectId });
        }
    }
}