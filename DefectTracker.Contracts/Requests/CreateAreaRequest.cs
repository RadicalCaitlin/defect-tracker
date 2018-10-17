using DefectTracker.Core;
using System;
using System.ComponentModel.DataAnnotations;

namespace DefectTracker.Contracts.Requests
{
    public class CreateAreaRequest
    {
        [Required]
        public int ProjectId { get; set; }

        [Required]
        [Display(Name="Area Name")]
        public string Name { get; set; }

        public ProjectAreas CreateProjectArea()
        {
            var area = new ProjectAreas
            {
                Name = Name,
                ProjectId = ProjectId,
                DateCreatedOffset = DateTime.UtcNow
            };

            return area;
        }
    }
}
