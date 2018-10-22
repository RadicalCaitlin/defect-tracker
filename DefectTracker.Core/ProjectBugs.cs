using System;

namespace DefectTracker.Core
{
    public class ProjectBugs
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int WorkItemId { get; set; }

        public int ProjectId { get; set; }

        public DateTimeOffset OriginDateOffset { get; set; }

        public DateTimeOffset DateCreatedOffset { get; set; }
    }
}
