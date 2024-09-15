using Microsoft.Extensions.Logging;

namespace TaxMaster.Infra
{
    public static class LoggerConfiguration
    {
        public static ILogger Logger = DummyLogger.CreateLogger();
    }
}
