using System;

namespace DefectTracker.Core
{
    public class ProjectUsers
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int ProjectId { get; set; }

        public DateTimeOffset DateCreatedOffset { get; set; }
    }
}
