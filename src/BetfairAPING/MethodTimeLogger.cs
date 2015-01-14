using System.Reflection;
using Anotar.LibLog;

namespace BetfairAPING
{
    public static class MethodTimeLogger
    {
        public static void Log(MethodBase methodBase, long milliseconds)
        {
            LogTo.Trace("{0}.{1}: {2}ms", methodBase.ReflectedType.FullName, methodBase.Name, milliseconds);
        }
    }
}