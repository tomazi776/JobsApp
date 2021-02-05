using System;
using System.Text;

namespace DataLib.Utilities
{
    public abstract class LogBase
    {
        public abstract void Log(Guid processedJobId, Exception ex, string msg);

        public abstract void Log(Guid processedJobId, string msg);


        public virtual string BuildLogs(Exception ex, string msg)
        {
            StringBuilder exBuilder = new StringBuilder();
            exBuilder.Append(msg);
            exBuilder.Append(Environment.NewLine);
            exBuilder.Append(Environment.NewLine);

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
            exBuilder.Append(Environment.NewLine);
            exBuilder.Append(Environment.NewLine);

            exBuilder.Append("INNER EXCEPTION DETAILS ---->>>>>");
            exBuilder.Append(Environment.NewLine);
            exBuilder.Append(Environment.NewLine);

            Exception inner = ex.InnerException;

            while (inner.InnerException != null)
            {
                exBuilder.Append("Inner Exception Type: ");
                exBuilder.Append(Environment.NewLine);
                exBuilder.Append(inner.GetType());

                exBuilder.Append(Environment.NewLine);
                exBuilder.Append(Environment.NewLine);

                exBuilder.Append("Message: ");
                exBuilder.Append(Environment.NewLine);
                exBuilder.Append(inner.Message);
                exBuilder.Append(Environment.NewLine);
                exBuilder.Append(Environment.NewLine);

                exBuilder.Append("Stack Trace: ");
                exBuilder.Append(inner.StackTrace);
                exBuilder.Append(Environment.NewLine);
                exBuilder.Append(Environment.NewLine);

                inner = inner.GetBaseException();
            }
            return exBuilder.ToString();
        }
    }
}
