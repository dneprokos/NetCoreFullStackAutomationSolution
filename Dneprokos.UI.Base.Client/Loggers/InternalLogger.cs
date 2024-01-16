using Microsoft.Extensions.Logging;

namespace Dneprokos.UI.Base.Client.Loggers
{
    public static class InternalLogger
    {
        public static ILogger Logger { get; set; } = new LoggerFactory().CreateLogger("");
    }
}
