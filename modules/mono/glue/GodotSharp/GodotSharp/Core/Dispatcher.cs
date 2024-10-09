using System;
using System.Runtime.InteropServices;
using scardot.NativeInterop;

namespace scardot
{
    public static class Dispatcher
    {
        internal static scardotTaskScheduler DefaultscardotTaskScheduler;

        internal static void InitializeDefaultscardotTaskScheduler()
        {
            DefaultscardotTaskScheduler?.Dispose();
            DefaultscardotTaskScheduler = new scardotTaskScheduler();
        }

        public static scardotSynchronizationContext SynchronizationContext => DefaultscardotTaskScheduler.Context;
    }
}
