using DataLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLib.Utilities
{
    public abstract class LogBase
    {
        public abstract void Log(Guid processedJobId, Exception ex);

        public abstract void Log(Guid processedJobId, string msg);


        public virtual string BuildLogs(Exception ex)
        {
            StringBuilder exBuilder = new StringBuilder();
            exBuilder.Append("Exception Type: ");
            exBuilder.Append(Environment.NewLine);
            exBuilder.Append(ex.GetType());

            exBuilder.Append(Environment.NewLine);
            exBuilder.Append(Environment.NewLine);

            exBuilder.Append("Message: ");
            exBuilder.Append(Environment.NewLine);
            exBuilder.Append(ex.Message);
            exBuilder.Append(Environment.NewLine);
            exBuilder.Append(Environment.NewLine);

            exBuilder.Append("Stack Trace: ");
            exBuilder.Append(ex.StackTrace);
            exBuilder.Append(Environment.NewLine);
            exBuilder.Append(Environment.NewLine);

            Exception inner = ex.InnerException;

            while (inner.InnerException != null)
            {
                exBuilder.Append("Inner Exception Type: ");
                exBuilder.Append(Environment.NewLine);
                exBuilder.Append(ex.GetType());

                exBuilder.Append(Environment.NewLine);
                exBuilder.Append(Environment.NewLine);

                exBuilder.Append("Message: ");
                exBuilder.Append(Environment.NewLine);
                exBuilder.Append(ex.Message);
                exBuilder.Append(Environment.NewLine);
                exBuilder.Append(Environment.NewLine);

                exBuilder.Append("Stack Trace: ");
                exBuilder.Append(ex.StackTrace);
                exBuilder.Append(Environment.NewLine);
                exBuilder.Append(Environment.NewLine);

                inner = ex.InnerException;
            }
            return exBuilder.ToString();
        }
    }
}
