﻿using DefectTracker.Contracts.Repositories;
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
        public async Task<IActionResult> Index(int? id = null)
        {
            if (id == null)
                return RedirectToAction("Index", "Home");

            var project = await _projectRepository.GetProjectByIdAsync(id.GetValueOrDefault());
            var defects = await _defectRepository.GetDefectsByProjectIdAsync(id.GetValueOrDefault());
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
                Project = projectForChart,
                Defects = defects.Select(x => new DefectsForChart
                {
                    Activity = x.Activity,
                    CreatedByUserId = x.CreatedByUserId,
                    DateCreated = x.DateCreatedOffset.ToString("MM/dd/yyyy"),
                    DefectQualifierTypeId = x.DefectQualifierTypeId,
                    DefectReportedByTypeId = x.DefectReportedByTypeId,
                    DefectTypeId = x.DefectTypeId,
                    Id = x.Id,
                    Impact = x.Impact,
                    Origin = x.Origin,
                    OriginDate = x.OriginDateCreatedOffset.ToString("MM/dd/yyyy"),
                    ProjectId = x.ProjectId,
                    Trigger = x.Trigger
                }),
                DefectTypes = await _defectRepository.GetDefectTypesAsync()
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
   