using DefectTracker.Core;
using DefectTracker.Web.ViewModels.Defect;
using System;
using System.Collections.Generic;

namespace DefectTracker.Web.ViewModels.Project
{
    public class IndexViewModel
    {
        public IEnumerable<ProjectBugs> Bugs { get; set; }

        public IEnumerable<DefectsForChart> Defects { get; set; }

        public IEnumerable<DefectTypes> DefectTypes { get; set; }

        public ProjectForChart Project { get; set; }

        public DateTime StartDate { get; set; } = DateTime.UtcNow.AddDays(-30);

        public DateTime EndDate { get; set; } = DateTime.UtcNow;

        public GroupChartBy GroupBy { get; set; } = GroupChartBy.Week;
    }
}
