using DataLib;
using MVCApp.Helpers;

namespace MVCApp.Extensions
{
    internal static class JobExtension
    {
        public static void ChangeStatus(this DataLib.Models.Job job, JobStatus newStatus)
        {
            if (newStatus == JobStatus.Failed)
            {
                job.FailedCounter++;
                //SingleJobsFailedCounter.Instance.FailedJobs.Add(job);
            }

            job.Status = job.FailedCounter < 5 ? newStatus : JobStatus.Closed;
        }
    }
}