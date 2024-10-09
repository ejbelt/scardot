using System;
using System.Runtime.InteropServices;
using scardot.NativeInterop;

namespace scardot.Bridge
{
    internal static class CSharpInstanceBridge
    {
        [UnmanagedCallersOnly]
        internal static unsafe scardot_bool Call(IntPtr scardotObjectGCHandle, scardot_string_name* method,
            scardot_variant** args, int argCount, scardot_variant_call_error* refCallError, scardot_variant* ret)
        {
            try
            {
                var scardotObject = (scardotObject)GCHandle.FromIntPtr(scardotObjectGCHandle).Target;

                if (scardotObject == null)
                {
                    *ret = default;
                    (*refCallError).Error = scardot_variant_call_error_error.SCARDOT_CALL_ERROR_CALL_ERROR_INSTANCE_IS_NULL;
                    return scardot_bool.False;
                }

                bool methodInvoked = scardotObject.InvokescardotClassMethod(CustomUnsafe.AsRef(method),
                    new NativeVariantPtrArgs(args, argCount), out scardot_variant retValue);

                if (!methodInvoked)
                {
                    *ret = default;
                    // This is important, as it tells Object::call that no method was called.
                    // Otherwise, it would prevent Object::call from calling native methods.
                    (*refCallError).Error = scardot_variant_call_error_error.SCARDOT_CALL_ERROR_CALL_ERROR_INVALID_METHOD;
                    return scardot_bool.False;
                }

                *ret = retValue;
                return scardot_bool.True;
            }
            catch (Exception e)
            {
                ExceptionUtils.LogException(e);
                *ret = default;
                return scardot_bool.False;
            }
        }

        [UnmanagedCallersOnly]
        internal static unsafe scardot_bool Set(IntPtr scardotObjectGCHandle, scardot_string_name* name, scardot_variant* value)
        {
            try
            {
                var scardotObject = (scardotObject)GCHandle.FromIntPtr(scardotObjectGCHandle).Target;

                if (scardotObject == null)
                    throw new InvalidOperationException();

                if (scardotObject.SetscardotClassPropertyValue(CustomUnsafe.AsRef(name), CustomUnsafe.AsRef(value)))
                {
                    return scardot_bool.True;
                }

                var nameManaged = StringName.CreateTakingOwnershipOfDisposableValue(
                    NativeFuncs.scardotsharp_string_name_new_copy(CustomUnsafe.AsRef(name)));

                Variant valueManaged = Variant.CreateCopyingBorrowed(*value);

                return scardotObject._Set(nameManaged, valueManaged).ToscardotBool();
            }
            catch (Exception e)
            {
                ExceptionUtils.LogException(e);
                return scardot_bool.False;
            }
        }

        [UnmanagedCallersOnly]
        internal static unsafe scardot_bool Get(IntPtr scardotObjectGCHandle, scardot_string_name* name,
            scardot_variant* outRet)
        {
            try
            {
                var scardotObject = (scardotObject)GCHandle.FromIntPtr(scardotObjectGCHandle).Target;

                if (scardotObject == null)
                    throw new InvalidOperationException();

                // Properties
                if (scardotObject.GetscardotClassPropertyValue(CustomUnsafe.AsRef(name), out scardot_variant outRetValue))
                {
                    *outRet = outRetValue;
                    return scardot_bool.True;
                }

                // Signals
                if (scardotObject.HasscardotClassSignal(CustomUnsafe.AsRef(name)))
                {
                    scardot_signal signal = new scardot_signal(NativeFuncs.scardotsharp_string_name_new_copy(*name), scardotObject.GetInstanceId());
                    *outRet = VariantUtils.CreateFromSignalTakingOwnershipOfDisposableValue(signal);
                    return scardot_bool.True;
                }

                // Methods
                if (scardotObject.HasscardotClassMethod(CustomUnsafe.AsRef(name)))
                {
                    scardot_callable method = new scardot_callable(NativeFuncs.scardotsharp_string_name_new_copy(*name), scardotObject.GetInstanceId());
                    *outRet = VariantUtils.CreateFromCallableTakingOwnershipOfDisposableValue(method);
                    return scardot_bool.True;
                }

                var nameManaged = StringName.CreateTakingOwnershipOfDisposableValue(
                    NativeFuncs.scardotsharp_string_name_new_copy(CustomUnsafe.AsRef(name)));

                Variant ret = scardotObject._Get(nameManaged);

                if (ret.VariantType == Variant.Type.Nil)
                {
                    *outRet = default;
                    return scardot_bool.False;
                }

                *outRet = ret.CopyNativeVariant();
                return scardot_bool.True;
            }
            catch (Exception e)
            {
                ExceptionUtils.LogException(e);
                *outRet = default;
                return scardot_bool.False;
            }
        }

        [UnmanagedCallersOnly]
        internal static void CallDispose(IntPtr scardotObjectGCHandle, scardot_bool okIfNull)
        {
            try
            {
                var scardotObject = (scardotObject)GCHandle.FromIntPtr(scardotObjectGCHandle).Target;

                if (okIfNull.ToBool())
                    scardotObject?.Dispose();
                else
                    scardotObject!.Dispose();
            }
            catch (Exception e)
            {
                ExceptionUtils.LogException(e);
            }
        }

        [UnmanagedCallersOnly]
        internal static unsafe void CallToString(IntPtr scardotObjectGCHandle, scardot_string* outRes, scardot_bool* outValid)
        {
            try
            {
                var self = (scardotObject)GCHandle.FromIntPtr(scardotObjectGCHandle).Target;

                if (self == null)
                {
                    *outRes = default;
                    *outValid = scardot_bool.False;
                    return;
                }

                var resultStr = self.ToString();

                if (resultStr == null)
                {
                    *outRes = default;
                    *outValid = scardot_bool.False;
                    return;
                }

                *outRes = Marshaling.ConvertStringToNative(resultStr);
                *outValid = scardot_bool.True;
            }
            catch (Exception e)
            {
                ExceptionUtils.LogException(e);
                *outRes = default;
                *outValid = scardot_bool.False;
            }
        }

        [UnmanagedCallersOnly]
        internal static unsafe scardot_bool HasMethodUnknownParams(IntPtr scardotObjectGCHandle, scardot_string_name* method)
        {
            try
            {
                var scardotObject = (scardotObject)GCHandle.FromIntPtr(scardotObjectGCHandle).Target;

                if (scardotObject == null)
                    return scardot_bool.False;

                return scardotObject.HasscardotClassMethod(CustomUnsafe.AsRef(method)).ToscardotBool();
            }
            catch (Exception e)
            {
                ExceptionUtils.LogException(e);
                return scardot_bool.False;
            }
        }

        [UnmanagedCallersOnly]
        internal static unsafe void SerializeState(
            IntPtr scardotObjectGCHandle,
            scardot_dictionary* propertiesState,
            scardot_dictionary* signalEventsState
        )
        {
            try
            {
                var scardotObject = (scardotObject)GCHandle.FromIntPtr(scardotObjectGCHandle).Target;

                if (scardotObject == null)
                    return;

                // Call OnBeforeSerialize

                // ReSharper disable once SuspiciousTypeConversion.Global
                if (scardotObject is ISerializationListener serializationListener)
                    serializationListener.OnBeforeSerialize();

                // Save instance state

                using var info = scardotSerializationInfo.CreateCopyingBorrowed(
                    *propertiesState, *signalEventsState);

                scardotObject.SavescardotObjectData(info);
            }
            catch (Exception e)
            {
                ExceptionUtils.LogException(e);
            }
        }

        [UnmanagedCallersOnly]
        internal static unsafe void DeserializeState(
            IntPtr scardotObjectGCHandle,
            scardot_dictionary* propertiesState,
            scardot_dictionary* signalEventsState
        )
        {
            try
            {
                var scardotObject = (scardotObject)GCHandle.FromIntPtr(scardotObjectGCHandle).Target;

                if (scardotObject == null)
                    return;

                // Restore instance state

                using var info = scardotSerializationInfo.CreateCopyingBorrowed(
                    *propertiesState, *signalEventsState);

                scardotObject.RestorescardotObjectData(info);

                // Call OnAfterDeserialize

                // ReSharper disable once SuspiciousTypeConversion.Global
                if (scardotObject is ISerializationListener serializationListener)
                    serializationListener.OnAfterDeserialize();
            }
            catch (Exception e)
            {
                ExceptionUtils.LogException(e);
            }
        }
    }
}
