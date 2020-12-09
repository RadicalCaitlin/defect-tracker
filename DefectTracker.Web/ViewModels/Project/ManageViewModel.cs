using DefectTracker.Core;
using DefectTracker.Web.Models.AreaViewModels;
using DefectTracker.Web.Models.ProjectViewModels;
using System.Collections.Generic;

namespace DefectTracker.Web.ViewModels.Project
{
    public class ManageViewModel
    {
        public Projects Project { get; set; }

        public IEnumerable<Activities> Activities { get; set; }

        public IEnumerable<ProjectAreas> Areas { get; set; }

        public IEnumerable<Tasks> Tasks { get; set; }

        public IEnumerable<ProjectUsers> Users { get; set; }

        public EditProjectRequest EditProjectModel { get; set; }

        public CreateAreaRequest CreateAreaRequest { get; set; }

        public CreateProjectUserRequest CreateProjectUserRequest { get; set; }
    }
}
