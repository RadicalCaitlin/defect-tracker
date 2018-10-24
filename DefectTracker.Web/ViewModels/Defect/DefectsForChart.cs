namespace DefectTracker.Web.ViewModels.Defect
{
    public class DefectsForChart
    {
        public int Id { get; set; }

        public int DefectTypeId { get; set; }

        public int DefectQualifierTypeId { get; set; }

        public string DateCreated { get; set; }

        public string CreatedByUserId { get; set; }

        public int ProjectId { get; set; }

        public int BugId { get; set; }

        public int DefectModelTypeId { get; set; }
    }
}
