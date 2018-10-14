﻿using DefectTracker.Core;
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

        public DateTimeOffset DateCreatedOffset { get; set; }

        public Projects CreateProject()
        {
            var project = new Projects
            {
                UserId = UserId,
                Name = Name,
                DateCreatedOffset = DateTime.UtcNow
            };

            return project;
        }
    }
}