using System;
using System.Text;
using System.Threading;
using Microsoft.Extensions.Logging;

namespace Hamster.Web
{
    public class LogAdapter : ILogger
    {
        private Hamster.Plugin.ILogger logger;

        public LogAdapter(Hamster.Plugin.ILogger logger)
        {
            this.logger = logger;
        }

        private static Hamster.Plugin.LogLevel TranslateLevel(LogLevel level)
        {
            switch (level)
            {
                case LogLevel.Debug:
                    return Hamster.Plugin.LogLevel.Debug;

                case LogLevel.Critical:
                    return Hamster.Plugin.LogLevel.Fatal;

                case LogLevel.Error:
                    return Hamster.Plugin.LogLevel.Error;

                case LogLevel.Information:
                    return Hamster.Plugin.LogLevel.Info;

                case LogLevel.None:
                    return Hamster.Plugin.LogLevel.None;

                case LogLevel.Trace:
                    return Hamster.Plugin.LogLevel.Trace;

                case LogLevel.Warning:
                    return Hamster.Plugin.LogLevel.Warn;

                default:
                    return Hamster.Plugin.LogLevel.Debug;
            }
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            if (state == null)
            {
                throw new ArgumentNullException(nameof(state));
            }

            return Scope.Push(state);
        }

        public bool IsEnabled(LogLevel level)
        {
            return logger.IsLevelEnabled(TranslateLevel(level));
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            Hamster.Plugin.LogLevel level = TranslateLevel(logLevel);
            if (!logger.IsLevelEnabled(level))
            {
                return;
            }

            var builder = new StringBuilder();
            for (var scope = Scope.Current; scope != null; scope = scope.Parent)
            {
                builder.Insert(0, $"=> {scope} ");
            }
            builder.Append(formatter(state, exception));
            logger.WriteMessage(level, null, builder.ToString());
        }

        private class Scope
        {
            private object state;

            public Scope(object state)
            {
                this.state = state;
                Parent = null;
            }

            public Scope Parent { get; private set; }

            private static AsyncLocal<Scope> _value = new AsyncLocal<Scope>();
            public static Scope Current
            {
                set { _value.Value = value; }
                get { return _value.Value; }
            }

            public static IDisposable Push(object state)
            {
                var temp = Current;
                Current = new Scope(state);
                Current.Parent = temp;

                return new DisposableScope();
            }

            public override string ToString()
            {
                return state?.ToString();
            }

            private class DisposableScope : IDisposable
            {
                public void Dispose()
                {
                    Current = Current.Parent;
                }
            }
        }
    }
}
