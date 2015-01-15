using System.Reflection;
using metrics;
using metrics.Reporting;

namespace BetfairAPING
{
    public static class MethodTimeLogger
    {
        private static readonly Metrics _metrics = new Metrics();

        public static void Log(MethodBase methodBase, long milliseconds)
        {
            if (methodBase.ReflectedType == null) return;

            var fqn = string.Format("{0}.{1}", methodBase.ReflectedType.FullName, methodBase.Name);
            _metrics.ManualTimer(typeof(MethodTimeLogger), fqn, TimeUnit.Milliseconds, TimeUnit.Seconds)
                .RecordElapsedMillis(milliseconds);
        }

        public static string HumanReport()
        {
            var report = new HumanReadableReportFormatter(_metrics);
            return report.GetSample();
        }
    }
}