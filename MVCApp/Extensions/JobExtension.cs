using DataLib;
using DataLib.Services;

namespace MVCApp.Extensions
{
    internal static class JobExtension
    {
        public static void ChangeStatus(this DataLib.Models.Job job, JobStatus newStatus, ZavenContext ctx, ILogger logger)
        {
            if (newStatus == JobStatus.Failed)
            {
                job.FailedCounter++;
            }
            logger.Log(DataLib.Utilities.LogTarget.Database,ctx, null,job.Id,$"Changed '{job.Name}' status to - '{newStatus}'");
            job.Status = job.FailedCounter < 5 ? newStatus : JobStatus.Closed;
        }
    }
}