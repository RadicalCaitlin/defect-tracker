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
        [DataType(DataType.Date)]
        [Display(Name = "Defect Reported Date")]
        public DateTimeOffset OriginDateCreatedOffset { get; set; } = DateTime.UtcNow;

        [Required]
        [Display(Name = "Defect Reported By")]
        public int DefectReportedByTypeId { get; set; }

        [Required]
        public string CreatedByUserId { get; set; }

        public string Origin { get; set; }

        public string Activity { get; set; }

        public string Trigger { get; set; }

        public string Impact { get; set; }

        public int ProjectId { get; set; }

        public IEnumerable<DefectQualifierTypes> DefectQualifiers { get; set; }

        public IEnumerable<DefectReportedByTypes> DefectReportedByTypes { get; set; }

        public IEnumerable<DefectTypes> DefectTypes { get; set; }

        public Defects CreateDefect()
        {
            var defect = new Defects
            {
                Activity = Activity,
                Trigger = Trigger,
                Impact = Impact,
                DefectTypeId = DefectTypeId,
                DefectQualifierTypeId = DefectQualifierId,
                Origin = Origin,
                OriginDateCreatedOffset = OriginDateCreatedOffset,
                DefectReportedByTypeId = DefectReportedByTypeId,
                DateCreatedOffset = DateTime.UtcNow,
                CreatedByUserId = CreatedByUserId,
                ProjectId = ProjectId
            };

            return defect;
        }

    }
}
