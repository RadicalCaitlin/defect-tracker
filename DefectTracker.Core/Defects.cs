using System;

namespace DefectTracker.Core
{
    public class Defects
    {
        public int Id { get; set; }

        public string Activity { get; set; }

        public string Trigger { get; set; }

        public string Impact { get; set; }

        public int DefectTypeId { get; set; }

        public int DefectQualifierTypeId { get; set; }

        public string Origin { get; set; }

        public DateTimeOffset OriginDateCreatedOffset { get; set; }

        public int DefectReportedByTypeId { get; set; }

        public DateTimeOffset DateCreatedOffset { get; set; }

        public string CreatedByUserId { get; set; }

        public int ProjectId { get; set; }

    }
}
