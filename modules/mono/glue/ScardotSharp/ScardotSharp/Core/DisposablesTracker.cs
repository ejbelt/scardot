using System;
using System.Collections.Concurrent;
using System.Runtime.InteropServices;
using scardot.NativeInterop;

#nullable enable

namespace scardot
{
    internal static class DisposablesTracker
    {
        [UnmanagedCallersOnly]
        internal static void OnscardotShuttingDown()
        {
            try
            {
                OnscardotShuttingDownImpl();
            }
            catch (Exception e)
            {
                ExceptionUtils.LogException(e);
            }
        }

        private static void OnscardotShuttingDownImpl()
        {
            bool isStdoutVerbose;

            try
            {
                isStdoutVerbose = OS.IsStdOutVerbose();
            }
            catch (ObjectDisposedException)
            {
                // OS singleton already disposed. Maybe OnUnloading was called twice.
                isStdoutVerbose = false;
            }

            if (isStdoutVerbose)
                GD.Print("Unloading: Disposing tracked instances...");

            // Dispose scardot Objects first, and only then dispose other disposables
            // like StringName, NodePath, scardot.Collections.Array/Dictionary, etc.
            // The scardot Object Dispose() method may need any of the later instances.

            foreach (WeakReference<scardotObject> item in scardotObjectInstances.Keys)
            {
                if (item.TryGetTarget(out scardotObject? self))
                    self.Dispose();
            }

            foreach (WeakReference<IDisposable> item in OtherInstances.Keys)
            {
                if (item.TryGetTarget(out IDisposable? self))
                    self.Dispose();
            }

            if (isStdoutVerbose)
                GD.Print("Unloading: Finished disposing tracked instances.");
        }

        private static ConcurrentDictionary<WeakReference<scardotObject>, byte> scardotObjectInstances { get; } =
            new();

        private static ConcurrentDictionary<WeakReference<IDisposable>, byte> OtherInstances { get; } =
            new();

        public static WeakReference<scardotObject> RegisterscardotObject(scardotObject scardotObject)
        {
            var weakReferenceToSelf = new WeakReference<scardotObject>(scardotObject);
            scardotObjectInstances.TryAdd(weakReferenceToSelf, 0);
            return weakReferenceToSelf;
        }

        public static WeakReference<IDisposable> RegisterDisposable(IDisposable disposable)
        {
            var weakReferenceToSelf = new WeakReference<IDisposable>(disposable);
            OtherInstances.TryAdd(weakReferenceToSelf, 0);
            return weakReferenceToSelf;
        }

        public static void UnregisterscardotObject(scardotObject scardotObject, WeakReference<scardotObject> weakReferenceToSelf)
        {
            if (!scardotObjectInstances.TryRemove(weakReferenceToSelf, out _))
                throw new ArgumentException("scardot Object not registered.", nameof(weakReferenceToSelf));
        }

        public static void UnregisterDisposable(WeakReference<IDisposable> weakReference)
        {
            if (!OtherInstances.TryRemove(weakReference, out _))
                throw new ArgumentException("Disposable not registered.", nameof(weakReference));
        }
    }
}
