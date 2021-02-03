using DataLib;
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
        private readonly IJobProcessorService _jobProcessorService;
        public JobsController(IJobProcessorService jobProcessorService)
        {
            _jobProcessorService = jobProcessorService;
        }

        // GET: Tasks
        public ActionResult Index()
        {
            DataLib.Services.IJobsRepository jobsRepository = new DataLib.Services.JobsRepository();
            var modelJobs = jobsRepository.GetAllJobs();
            List<Job> viewJobs = ModelMapper.MapAll(modelJobs);
            return View(viewJobs);
        }

        // POST: Tasks/Process
        [HttpGet]
        [NoAsyncTimeout]
        public ActionResult Process()
        {
            _jobProcessorService.ProcessJobs();
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
                DataLib.Services.IJobsRepository jobsRepository = new DataLib.Services.JobsRepository();
                var modelJob = CreateMappedJob(name, doAfter);
                var affectedRows = jobsRepository.SaveJob(modelJob);
                if (affectedRows < 1)
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("Index");
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