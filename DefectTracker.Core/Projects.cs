using System;

namespace DefectTracker.Core
{
    public class Projects
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTimeOffset DateCreatedOffset { get; set; }

        public DateTimeOffset OriginDateOffset { get; set; }

        public string CreatedByUserId { get; set; }
    }
}
