using DefectTracker.Core;
using System;
using System.ComponentModel.DataAnnotations;

namespace DefectTracker.Contracts.Requests
{
    public class CreateProjectRequest
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Origin Date")]
        public DateTimeOffset OriginDateOffset { get; set; } = DateTime.UtcNow;

        public DateTimeOffset DateCreatedOffset { get; set; }

        public Projects CreateProject()
        {
            var project = new Projects
            {
                CreatedByUserId = UserId,
                Name = Name,
                DateCreatedOffset = DateTime.UtcNow,
                OriginDateOffset = OriginDateOffset
            };

            return project;
        }
    }
}
