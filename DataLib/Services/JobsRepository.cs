using DataLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLib.Services
{
    public class JobsRepository : IJobsRepository
    {
        private readonly ZavenContext _ctx;
        public JobsRepository(ZavenContext ctx)
        {
            _ctx = ctx;
        }

        List<Job> Jobs = new List<Job>();
        public List<Job> GetAllJobs()
        {
            return new List<Job>(_ctx.Jobs.OrderBy(modificationDate => modificationDate.LastUpdatedAt).ToList());
        }

        public int SaveJob(Job job)
        {
            _ctx.Jobs.Add(job);
            return _ctx.SaveChanges();
        }

        public void Update(Job job)
        {
            var searchedJob = _ctx.Jobs.Find(job.Id);
            if (searchedJob == null)
            {
                return;
            }
            _ctx.Entry(searchedJob).CurrentValues.SetValues(job);
            _ctx.SaveChanges();
        }
    }
}
