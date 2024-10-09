using System;
using System.Runtime.InteropServices;
using scardot.NativeInterop;

namespace scardot
{
    public class SignalAwaiter : IAwaiter<Variant[]>, IAwaitable<Variant[]>
    {
        private bool _completed;
        private Variant[] _result;
        private Action _continuation;

        public SignalAwaiter(scardotObject source, StringName signal, scardotObject target)
        {
            var awaiterGcHandle = CustomGCHandle.AllocStrong(this);
            using scardot_string_name signalSrc = NativeFuncs.scardotsharp_string_name_new_copy(
                (scardot_string_name)(signal?.NativeValue ?? default));
            NativeFuncs.scardotsharp_internal_signal_awaiter_connect(scardotObject.GetPtr(source), in signalSrc,
                scardotObject.GetPtr(target), GCHandle.ToIntPtr(awaiterGcHandle));
        }

        public bool IsCompleted => _completed;

        public void OnCompleted(Action continuation)
        {
            _continuation = continuation;
        }

        public Variant[] GetResult() => _result;

        public IAwaiter<Variant[]> GetAwaiter() => this;

        [UnmanagedCallersOnly]
        internal static unsafe void SignalCallback(IntPtr awaiterGCHandlePtr, scardot_variant** args, int argCount,
            scardot_bool* outAwaiterIsNull)
        {
            try
            {
                var awaiter = (SignalAwaiter)GCHandle.FromIntPtr(awaiterGCHandlePtr).Target;

                if (awaiter == null)
                {
                    *outAwaiterIsNull = scardot_bool.True;
                    return;
                }

                *outAwaiterIsNull = scardot_bool.False;

                awaiter._completed = true;

                Variant[] signalArgs = new Variant[argCount];

                for (int i = 0; i < argCount; i++)
                    signalArgs[i] = Variant.CreateCopyingBorrowed(*args[i]);

                awaiter._result = signalArgs;

                awaiter._continuation?.Invoke();
            }
            catch (Exception e)
            {
                ExceptionUtils.LogException(e);
                *outAwaiterIsNull = scardot_bool.False;
            }
        }
    }
}
