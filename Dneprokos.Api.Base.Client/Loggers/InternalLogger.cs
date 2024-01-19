using Microsoft.Extensions.Logging;

namespace Dneprokos.Api.Base.Client.Loggers
{
    public static class InternalLogger
    {
        public static ILogger Logger { get; set; } = new LoggerFactory().CreateLogger("");
    }
}
