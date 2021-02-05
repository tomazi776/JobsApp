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

        public override void Log(Guid processedJobId, Exception ex, string msg)
        {
            var log = new Log()
            {
                Id = Guid.NewGuid(),
                JobId = processedJobId,
                CreatedAt = DateTime.Now,
                Description = BuildLogs(ex, msg)
            };
            // use separate context
            using (ZavenContext context = new ZavenContext())
            {
                context.Logs.Add(log);
                context.SaveChanges();
            }
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
