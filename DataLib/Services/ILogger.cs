using DataLib.Utilities;
using System;

namespace DataLib.Services
{
    public interface ILogger
    {
        void Log(LogTarget target, ZavenContext context, Exception ex = null, Guid jobId = default(Guid), string msg = null);
    }
}
