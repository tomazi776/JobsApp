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
        //private readonly ZavenDotNetInterviewContext _ctx;

        //public JobsRepository(ZavenDotNetInterviewContext ctx)
        //{
        //    _ctx = ctx;
        //}
        List<Job> Jobs = new List<Job>();
        public List<Job> GetAllJobs()
        {
            List<Job> sortedJobs = new List<Job>();
            using (ZavenContext context = new ZavenContext())
            {
                sortedJobs = context.Jobs.OrderBy(modificationDate => modificationDate.LastUpdatedAt).ToList();
                //jobs = sortedJobs.ToList();
            }
            return sortedJobs;
        }

        public int SaveJob(Job job)
        {
            int affectedRows = 0;
            using (ZavenContext context = new ZavenContext())
            {
                context.Jobs.Add(job);
                affectedRows = context.SaveChanges();
            }
            return affectedRows;
        }

        public void Update (Job item)
        {
            using (ZavenContext context = new ZavenContext())
            {
                var entity = context.Jobs.Find(item.Id);
                if (entity == null)
                {
                    return;
                }
                context.Entry(entity).CurrentValues.SetValues(item);
                context.SaveChanges();
            }
            
        }
    }
}
