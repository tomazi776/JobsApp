using DataLib;
using DataLib.Services;
using MVCApp.Extensions;
using MVCApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;


namespace ZavenDotNetInterview.App.Services
{
    public class JobProcessorService : IJobProcessorService
    {
        private readonly ZavenContext ctx;
        private readonly ILogger logger;

        public JobProcessorService(ZavenContext ctx, ILogger logger)
        {
            this.ctx = ctx;
            this.logger = logger;
        }

        public void ProcessJobs()
        {
            IJobsRepository jobsRepository = new JobsRepository(ctx);
            var allJobs = jobsRepository.GetAllJobs();
            var jobsToProcess = allJobs.Where(x => x.Status == JobStatus.New || x.Status == JobStatus.Failed).ToList();

            // check performance
            jobsToProcess.ForEach(job => job.ChangeStatus(JobStatus.InProgress, ctx, logger));
            List<DataLib.Models.Job> processedJobs = new List<DataLib.Models.Job>();
            //Parallel.ForEach(jobsToProcess, (currentjob) =>
            //{
            //    //var mappedJob = ModelMapper.Map(currentjob);
            //    Console.WriteLine($"Processing {currentjob.Name} on thread {Thread.CurrentThread.ManagedThreadId}");
            //    bool succeeded = ProcessJob(currentjob);
            //    if (succeeded)
            //    {
            //        currentjob.ChangeStatus(JobStatus.Done);
            //        processedJobs.Add(currentjob);
            //    }
            //    else
            //    {
            //        currentjob.ChangeStatus(JobStatus.Failed);
            //        processedJobs.Add(currentjob);
            //    }
            //});

            foreach (var currentjob in jobsToProcess)
            {
                Console.WriteLine($"Processing {currentjob.Name} on thread {Thread.CurrentThread.ManagedThreadId}");
                bool succeeded = ProcessJob(currentjob);
                if (succeeded)
                {
                    UpdateJobStatus(jobsRepository, JobStatus.Done, currentjob);
                }
                else
                {
                    UpdateJobStatus(jobsRepository, JobStatus.Failed, currentjob);
                }
            }
        }

        private bool ProcessJob(DataLib.Models.Job job)
        {
            Random rand = new Random();
            if (rand.Next(10) < 5)
            {
                Thread.Sleep(2000);
                return false;
            }
            else
            {
                Thread.Sleep(1000);
                return true;
            }
        }

        private void UpdateJobStatus(IJobsRepository jobsRepository, JobStatus status, DataLib.Models.Job job)
        {
            job.ChangeStatus(JobStatus.Done, ctx, logger);
            jobsRepository.Update(job);
        }
    }
}