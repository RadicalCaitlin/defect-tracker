using System;
using System.ComponentModel.DataAnnotations;

namespace DefectTracker.Contracts.Requests
{
    public class EditProjectRequest
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [Display(Name="Project Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name="Origin Date")]
        public DateTimeOffset OriginDate { get; set; }
    }
}
