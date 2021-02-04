using DataLib;
using DataLib.Services;
using DataLib.Utilities;
using MVCApp.Helpers;
using MVCApp.Models;
using MVCApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace MVCApp.Controllers
{
    public class JobsController : Controller
    {
        private readonly IJobProcessorService jobProcessorService;
        private readonly ILogger logger;
        public JobsController(IJobProcessorService jobProcessorService, ILogger logger)
        {
            this.jobProcessorService = jobProcessorService;
            this.logger = logger;
        }

        // GET: Tasks
        public ActionResult Index()
        {
            List<Job> viewJobs = new List<Job>();
            using (ZavenContext ctx = new ZavenContext())
            {
                IJobsRepository jobsRepository = new JobsRepository(ctx);
                var modelJobs = jobsRepository.GetAllJobs();
                viewJobs = ModelMapper.MapAll(modelJobs);
            }
            return View(viewJobs);
        }

        // POST: Tasks/Process
        [HttpGet]
        [NoAsyncTimeout]
        public ActionResult Process()
        {
            jobProcessorService.ProcessJobs();
            return RedirectToAction("Index");
        }

        // GET: Tasks/Create
        public ActionResult Create(Job job)
        {
            return View(job);
        }

        // POST: Tasks/Create
        [HttpPost]
        public ActionResult Create(string name, DateTime? doAfter)
        {
            if (ModelState.IsValid)
            {
                using (ZavenContext ctx = new ZavenContext())
                {
                    IJobsRepository jobsRepository = new JobsRepository(ctx);
                    var job = CreateMappedJob(name, doAfter);
                    try
                    {
                        jobsRepository.SaveJob(job);
                        logger.Log(LogTarget.Database, ctx, null, job.Id, $"Job '{job.Name}' created successfully");
                        return RedirectToAction("Index");
                    }
                    catch (Exception ex)
                    {
                        var existingJobId = ctx.Jobs.Where(j => j.Name == job.Name).First().Id;
                        logger.Log(LogTarget.Database, ctx, ex, existingJobId, $"Job '{job.Name}' not created due to exception.");
                        return View();
                    }

                    //if (affectedRows > 0)
                    //{
                    //    logger.Log(LogTarget.Database, ctx, null, job.Id, "Job created successfully");
                    //    return RedirectToAction("Index");
                    //}
                    //else
                    //{
                    //    logger.Log(LogTarget.Database, ctx, null, job.Id, "Job not created");
                    //    return View();
                    //}
                }
            }
            return View();
        }

        private static DataLib.Models.Job CreateMappedJob(string name, DateTime? doAfter)
        {
            var viewModelJob = new Job()
            {
                JobId = Guid.NewGuid(),
                DoAfter = doAfter,
                Name = name,
                Status = JobStatus.New
            };
            var modelJob = ModelMapper.Map(viewModelJob);
            return modelJob;
        }

        public ActionResult Details(Guid jobId)
        {
            return View();
        }
    }
}