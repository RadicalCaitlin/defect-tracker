namespace DefectTracker.Core
{
    public class Activities
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int AreaId { get; set; }

        public int? ParentAreaId { get; set; }

        public int ProjectUserId { get; set; }

        public int ProjectId { get; set; }
    }
}
