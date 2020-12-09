using System;
using System.Collections.Generic;

namespace DefectTracker.Core
{
    public class Projects
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTimeOffset DateCreatedOffset { get; set; }

        public DateTimeOffset OriginDateOffset { get; set; }

        public string CreatedByUserId { get; set; }

        public List<Activities> Activities { get; set; } = new List<Activities>();

        public List<ProjectAreas> ProjectAreas { get; set; } = new List<ProjectAreas>();

        public List<Tasks> Tasks { get; set; } = new List<Tasks>();

        public List<ProjectUsers> ProjectUsers { get; set; } = new List<ProjectUsers>();
    }
}
