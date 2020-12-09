using DefectTracker.Core;
using System;
using System.ComponentModel.DataAnnotations;

namespace DefectTracker.Web.Models.ProjectViewModels
{
    public class CreateProjectUserRequest
    {
        [Required]
        public int ProjectId { get; set; }

        [Required]
        [Display(Name = "User Name")]
        public string Name { get; set; }

        public ProjectUsers CreateProjectUser()
        {
            var user = new ProjectUsers
            {
                Name = Name,
                ProjectId = ProjectId,
                DateCreatedOffset = DateTime.UtcNow
            };

            return user;
        }
    }
}
