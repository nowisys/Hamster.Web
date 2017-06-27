using System;
using Microsoft.Extensions.Logging;

namespace Hamster.Web
{
    public class LogProvider : ILoggerProvider
    {
        // Flag: Has Dispose already been called?
        bool disposed = false;
        private Hamster.Plugin.ILogger logger;

        public LogProvider(Hamster.Plugin.ILogger logger)
        {
            this.logger = logger;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new LogAdapter(this.logger.CreateChildLogger(categoryName));
        }

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing) {
                // Free any other managed objects here.
                //
            }

            // Free any unmanaged objects here.
            //
            disposed = true;
        }

        ~LogProvider()
        {
            Dispose(false);
        }
    }
}
