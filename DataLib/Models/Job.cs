using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLib.Models
{
    public class Job
    {
        public int Id { get; set; }
        public Guid JobId { get; set; }

        [StringLength(50)]
        [Index(IsUnique = true)]
        public string Name { get; set; }
        public JobStatus Status { get; set; }
        public DateTime? DoAfter { get; set; }
        public DateTime? LastUpdatedAt { get; set; }
        public int FailedCounter { get; set; }

        public virtual List<Log> Logs { get; set; }
    }
}
