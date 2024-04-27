using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace IotRemoteLab.Blazor.Tools
{
    public static class ConsoleDebug
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static string FormDebugString(object line, string memberName, string sourceFilePath, int sourceLineNumber)
        {
            string prefix = $"[{Path.GetFileName(sourceFilePath).Replace("D:\\Programming\\Urfu\\IotRemoteLab\\IotRemoteLab\\IotRemoteLab.Blazor\\", "...")}:{memberName}:{sourceLineNumber}]";
            var nowTime = DateTimeOffset.Now;
            string time = $"[{nowTime.Hour}:{nowTime.Minute}:{nowTime.Second}.{nowTime.Millisecond}]";

            return $"{time} {prefix} {line}";
        }

        [Conditional("DEBUG")]
        public static void DebugConsoleWrite<T>(T line, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
#if DEBUG
            string str = FormDebugString(line, memberName, sourceFilePath, sourceLineNumber);
            Console.WriteLine(str);
#endif
        }

        private static object _debugWriteLocker = new object();

        public static void WriteLine<T>(T line, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
#if DEBUG
            string str = FormDebugString(line, memberName, sourceFilePath, sourceLineNumber);
            Console.WriteLine(str);
#endif
        }

        [Conditional("DEBUG")]
        public static void DebugWrite()
        {
#if DEBUG
            Console.WriteLine();
#endif
        }
    }
}
