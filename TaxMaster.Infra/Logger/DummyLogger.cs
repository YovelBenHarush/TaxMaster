using Microsoft.Extensions.Logging;

namespace TaxMaster.Infra
{
    public class DummyLogger : ILogger
    {
        public static ILogger CreateLogger()
        {
            return new DummyLogger();
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return false;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            return;
        }
    }
}
