using DataLib;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MVCApp.Models
{
    public class Job
    {
        public int Id { get; set; }
        public Guid JobId { get; set; }

        [Display(Name = "Job Name")]
        [RegularExpression(@".*\S+.*", ErrorMessage = "No white space allowed")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "You need to provide long enough Job Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "You need to provide unique Job Name")]
        public string Name { get; set; }
        public JobStatus Status { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DoAfter { get; set; }
    }
}