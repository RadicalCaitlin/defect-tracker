using System;

namespace DefectTracker.Core
{
    public class Client
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public DateTimeOffset DateCreatedUtc { get; set; }

        public DateTimeOffset DateLastUpdatedUtc { get; set; }
    }
}
