using DataLib.Models;
using DataLib.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLib.Services
{
    public interface ILogger
    {
        void Log(LogTarget target, ZavenContext context, Exception ex = null, Guid jobId = default(Guid), string msg = null);
    }
}
