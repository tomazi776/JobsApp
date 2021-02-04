using DataLib.Models;
using DataLib.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLib.Services
{
    public class Logger : ILogger
    {
        private LogBase logger;

        public void Log(LogTarget target, ZavenContext context, Exception ex = null, Guid jobId = default(Guid), string msg = null) 
        {
            switch (target)
            {
                case LogTarget.Database:
                    logger = new DbLogger(context);
                    //logger = new DbLogger();


                    if (jobId != null && ex is null)
                    {
                        logger.Log(jobId, msg);
                    }
                    else if (ex != null)
                    {
                        logger.Log(jobId, ex, msg);
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
