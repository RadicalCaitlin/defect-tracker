using System;

namespace DefectTracker.Core
{
    public class Defects
    {
        public int Id { get; set; }

        public int DefectTypeId { get; set; }

        public int DefectQualifierTypeId { get; set; }

        public DateTimeOffset OriginDateCreatedOffset { get; set; }

        public DateTimeOffset DateCreatedOffset { get; set; }

        public string CreatedByUserId { get; set; }

        public int ProjectId { get; set; }

        public int BugId { get; set; }

        public int DefectModelTypeId { get; set; }
    }
}
