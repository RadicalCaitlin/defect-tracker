using DefectTracker.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DefectTracker.Contracts.Requests
{
    public class CreateDefectRequest
    {

        [Required]
        [Display(Name = "Defect Type")]
        public int DefectTypeId { get; set; }

        [Required]
        [Display(Name = "Defect Qualifier")]
        public int DefectQualifierId { get; set; }

        [Required]
        public DateTimeOffset OriginDateCreatedOffset { get; set; }
        

        //[Required]
        //public string CreatedByUserId { get; set; }

        [Required]
        public int ProjectId { get; set; }

        [Required]
        public int BugId { get; set; }

        [Required]
        public int DefectModelTypeId { get; set; }

        public IEnumerable<DefectQualifierTypes> DefectQualifiers { get; set; }

        public IEnumerable<DefectTypes> DefectTypes { get; set; }

        public ProjectBugs Bug { get; set; }

        public Defects CreateDefect()
        {
            var defect = new Defects
            {
                DefectTypeId = DefectTypeId,
                DefectQualifierTypeId = DefectQualifierId,
                DateCreatedOffset = DateTime.UtcNow,
                //CreatedByUserId = CreatedByUserId,
                ProjectId = ProjectId,
                BugId = BugId,
                DefectModelTypeId = DefectModelTypeId,
                OriginDateCreatedOffset = OriginDateCreatedOffset
            };

            return defect;
        }

    }
}
