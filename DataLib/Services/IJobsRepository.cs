using DataLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLib.Services
{
    public interface IJobsRepository
    {
        List<Job> GetAllJobs();
        int SaveJob(Job job);
        void Update(Job item);
    }
}
