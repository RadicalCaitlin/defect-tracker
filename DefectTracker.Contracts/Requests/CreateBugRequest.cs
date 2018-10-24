using DefectTracker.Core;
using System;
using System.ComponentModel.DataAnnotations;

namespace DefectTracker.Contracts.Requests
{
    public class CreateBugRequest
    {
        [Required]
        public string Title { get; set; }

        [Required]
        [Display(Name="Work Item Id")]
        public int WorkItemId { get; set; }

        [Required]
        public int ProjectId { get; set; }

        [Required]
        [Display(Name="Origin Date")]
        [DataType(DataType.Date)]
        public DateTime OriginDateOffset { get; set; } = DateTime.UtcNow;

        public ProjectBugs CreateBug()
        {
            var bug = new ProjectBugs
            {
                DateCreatedOffset = DateTime.UtcNow,
                OriginDateOffset = OriginDateOffset,
                ProjectId = ProjectId,
                Title = Title,
                WorkItemId = WorkItemId
            };

            return bug;
        }
    }
}
