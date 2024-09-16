using Microsoft.Extensions.Logging;
using TaxMaster.Infra;

namespace TaxMaster.BL
{
    public class BaseWorker
    {
        public ILogger Logger => LoggerConfiguration.Logger;
    }
}

