using System.Threading.Tasks;
using DefectTracker.Web.Models.DefectViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;

namespace DefectTracker.Web.Controllers
{
    [Authorize]
    public class DefectController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly DefectTrackerDbContext _dbContext;

        public DefectController(
            UserManager<IdentityUser> userManager,
            DefectTrackerDbContext dbContext
            )
        {
            _userManager = userManager;
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Create(int projectId, int bugId, int defectModelId)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var bug = await _dbContext.ProjectBugs.SingleOrDefaultAsync(pb => pb.Id == bugId);

            var model = new CreateDefectRequest
            {
                CreatedByUserId = user.Id,
                ProjectId = projectId,
                DefectQualifiers = await _dbContext.DefectQualifierTypes.ToListAsync(),
                DefectTypes = await _dbContext.DefectTypes.ToListAsync(),
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

            _dbContext.Defects.Add(request.CreateDefect());
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Index", "Project", new { id = request.ProjectId });
        }
    }
}