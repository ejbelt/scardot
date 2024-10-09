using System;
using System.Runtime.InteropServices;
using scardot.NativeInterop;

namespace scardot.Bridge
{
    internal static class GCHandleBridge
    {
        [UnmanagedCallersOnly]
        internal static void FreeGCHandle(IntPtr gcHandlePtr)
        {
            try
            {
                CustomGCHandle.Free(GCHandle.FromIntPtr(gcHandlePtr));
            }
            catch (Exception e)
            {
                ExceptionUtils.LogException(e);
            }
        }

        // Returns true, if releasing the provided handle is necessary for assembly unloading to succeed.
        // This check is not perfect and only intended to prevent things in scardotTools from being reloaded.
        [UnmanagedCallersOnly]
        internal static scardot_bool GCHandleIsTargetCollectible(IntPtr gcHandlePtr)
        {
            try
            {
                var target = GCHandle.FromIntPtr(gcHandlePtr).Target;

                if (target is Delegate @delegate)
                    return DelegateUtils.IsDelegateCollectible(@delegate).ToscardotBool();

                return target.GetType().IsCollectible.ToscardotBool();
            }
            catch (Exception e)
            {
                ExceptionUtils.LogException(e);
                return scardot_bool.True;
            }
        }
    }
}
