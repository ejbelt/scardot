using System;
using System.Runtime.InteropServices;
using scardot.NativeInterop;

namespace scardot
{
    public static partial class GD
    {
        [UnmanagedCallersOnly]
        internal static void OnCoreApiAssemblyLoaded(scardot_bool isDebug)
        {
            try
            {
                Dispatcher.InitializeDefaultscardotTaskScheduler();

                if (isDebug.ToBool())
                {
                    DebuggingUtils.InstallTraceListener();

                    AppDomain.CurrentDomain.UnhandledException += (_, e) =>
                    {
                        // Exception.ToString() includes the inner exception
                        ExceptionUtils.LogUnhandledException((Exception)e.ExceptionObject);
                    };
                }
            }
            catch (Exception e)
            {
                ExceptionUtils.LogException(e);
            }
        }
    }
}
