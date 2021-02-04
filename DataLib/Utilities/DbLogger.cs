using DataLib.Models;
using System;

namespace DataLib.Utilities
{
    public class DbLogger : LogBase
    {
        private readonly ZavenContext ctx;
        public DbLogger(ZavenContext ctx)
        {
            this.ctx = ctx;
        }

        public override void Log(Guid processedJobId, Exception ex)
        {
            var log = new Log()
            {
                Id = Guid.NewGuid(),
                JobId = processedJobId,
                CreatedAt = DateTime.Now,
                Description = BuildLogs(ex)
            };
            ctx.Logs.Add(log);
        }

        public override void Log(Guid processedJobId, string msg)
        {
            var info = new Log()
            {
                Id = Guid.NewGuid(),
                JobId = processedJobId,
                CreatedAt = DateTime.Now,
                Description = msg,
            };

            ctx.Logs.Add(info);
            ctx.SaveChanges();
        }
    }
}
