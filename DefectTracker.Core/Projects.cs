using System;
using System.Collections.Generic;

namespace DefectTracker.Core
{
    public class Projects
    {
        public int ClientId { get; set; }

        public string CreatedByUserId { get; set; }

        public DateTimeOffset DateCreatedUtc { get; set; }

        public DateTimeOffset DateLastUpdatedUtc { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }

        public DateTimeOffset OriginDateUtc { get; set; }

        public int ProjectStatusId { get; set; }

        public string VersionNumber { get; set; }
    }
}
