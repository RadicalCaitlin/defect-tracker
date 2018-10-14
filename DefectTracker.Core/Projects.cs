using System;

namespace DefectTracker.Core
{
    public class Projects
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTimeOffset DateCreatedOffset { get; set; }

        public string UserId { get; set; }
    }
}
