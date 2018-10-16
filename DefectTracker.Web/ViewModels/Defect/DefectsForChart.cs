using System;

namespace DefectTracker.Web.ViewModels.Defect
{
    public class DefectsForChart
    {
        public int Id { get; set; }

        public string Activity { get; set; }

        public string Trigger { get; set; }

        public string Impact { get; set; }

        public int DefectTypeId { get; set; }

        public int DefectQualifierTypeId { get; set; }

        public string Origin { get; set; }

        public string OriginDate { get; set; }

        public int DefectReportedByTypeId { get; set; }

        public string DateCreated { get; set; }

        public string CreatedByUserId { get; set; }

        public int ProjectId { get; set; }
    }
}
