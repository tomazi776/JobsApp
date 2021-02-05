using DataLib.Models;
using System.Collections.Generic;

namespace DataLib.Services
{
    public interface IJobsRepository
    {
        List<Job> GetAllJobs();
        int SaveJob(Job job);
        void Update(Job item);
    }
}
