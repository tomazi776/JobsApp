using MVCApp.Models;
using System;
using System.Collections.Generic;

namespace MVCApp.Helpers
{
    public static class ModelMapper
    {
        public static List<Job> MapAll(List<DataLib.Models.Job> modelJobs)
        {
            List<Job> viewJobs = new List<Job>();
            foreach (var job in modelJobs)
            {
                var viewJob = new Job()
                {
                    JobId = job.JobId,
                    Name = job.Name,
                    Status = job.Status,
                    DoAfter = job.DoAfter
                };
                viewJobs.Add(viewJob);
            }
            return viewJobs;
        }

        public static MVCApp.Models.Job Map(DataLib.Models.Job modelJob)
        {
            return new MVCApp.Models.Job()
            {
                JobId = modelJob.JobId,
                Name = modelJob.Name,
                DoAfter = modelJob.DoAfter,
                Status = modelJob.Status
            };
        }

        public static DataLib.Models.Job Map(Job viewJob)
        {
            return new DataLib.Models.Job()
            {
                JobId = viewJob.JobId,
                Name = viewJob.Name,
                DoAfter = viewJob.DoAfter,
                LastUpdatedAt = DateTime.Now,
                Status = viewJob.Status
            };
        }
    }
}