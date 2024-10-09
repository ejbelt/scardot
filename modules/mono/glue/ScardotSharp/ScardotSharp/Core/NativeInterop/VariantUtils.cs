#pragma warning disable CA1707 // Identifiers should not contain underscores
#pragma warning disable IDE1006 // Naming rule violation
// ReSharper disable InconsistentNaming

using System;
using System.Runtime.CompilerServices;
using scardot.Collections;


#nullable enable

namespace scardot.NativeInterop
{
    public static partial class VariantUtils
    {
        public static scardot_variant CreateFromRid(Rid from)
            => new() { Type = Variant.Type.Rid, Rid = from };

        public static scardot_variant CreateFromBool(bool from)
            => new() { Type = Variant.Type.Bool, Bool = from.ToscardotBool() };

        public static scardot_variant CreateFromInt(long from)
            => new() { Type = Variant.Type.Int, Int = from };

        public static scardot_variant CreateFromInt(ulong from)
            => new() { Type = Variant.Type.Int, Int = (long)from };

        public static scardot_variant CreateFromFloat(double from)
            => new() { Type = Variant.Type.Float, Float = from };

        public static scardot_variant CreateFromVector2(Vector2 from)
            => new() { Type = Variant.Type.Vector2, Vector2 = from };

        public static scardot_variant CreateFromVector2I(Vector2I from)
            => new() { Type = Variant.Type.Vector2I, Vector2I = from };

        public static scardot_variant CreateFromVector3(Vector3 from)
            => new() { Type = Variant.Type.Vector3, Vector3 = from };

        public static scardot_variant CreateFromVector3I(Vector3I from)
            => new() { Type = Variant.Type.Vector3I, Vector3I = from };

        public static scardot_variant CreateFromVector4(Vector4 from)
            => new() { Type = Variant.Type.Vector4, Vector4 = from };

        public static scardot_variant CreateFromVector4I(Vector4I from)
            => new() { Type = Variant.Type.Vector4I, Vector4I = from };

        public static scardot_variant CreateFromRect2(Rect2 from)
            => new() { Type = Variant.Type.Rect2, Rect2 = from };

        public static scardot_variant CreateFromRect2I(Rect2I from)
            => new() { Type = Variant.Type.Rect2I, Rect2I = from };

        public static scardot_variant CreateFromQuaternion(Quaternion from)
            => new() { Type = Variant.Type.Quaternion, Quaternion = from };

        public static scardot_variant CreateFromColor(Color from)
            => new() { Type = Variant.Type.Color, Color = from };

        public static scardot_variant CreateFromPlane(Plane from)
            => new() { Type = Variant.Type.Plane, Plane = from };

        public static scardot_variant CreateFromTransform2D(Transform2D from)
        {
            NativeFuncs.scardotsharp_variant_new_transform2d(out scardot_variant ret, from);
            return ret;
        }

        public static scardot_variant CreateFromBasis(Basis from)
        {
            NativeFuncs.scardotsharp_variant_new_basis(out scardot_variant ret, from);
            return ret;
        }

        public static scardot_variant CreateFromTransform3D(Transform3D from)
        {
            NativeFuncs.scardotsharp_variant_new_transform3d(out scardot_variant ret, from);
            return ret;
        }

        public static scardot_variant CreateFromProjection(Projection from)
        {
            NativeFuncs.scardotsharp_variant_new_projection(out scardot_variant ret, from);
            return ret;
        }

        public static scardot_variant CreateFromAabb(Aabb from)
        {
            NativeFuncs.scardotsharp_variant_new_aabb(out scardot_variant ret, from);
            return ret;
        }

        // Explicit name to make it very clear
        public static scardot_variant CreateFromCallableTakingOwnershipOfDisposableValue(scardot_callable from)
            => new() { Type = Variant.Type.Callable, Callable = from };

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static scardot_variant CreateFromCallable(Callable from)
            => CreateFromCallableTakingOwnershipOfDisposableValue(
                Marshaling.ConvertCallableToNative(from));

        // Explicit name to make it very clear
        public static scardot_variant CreateFromSignalTakingOwnershipOfDisposableValue(scardot_signal from)
            => new() { Type = Variant.Type.Signal, Signal = from };

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static scardot_variant CreateFromSignal(Signal from)
            => CreateFromSignalTakingOwnershipOfDisposableValue(
                Marshaling.ConvertSignalToNative(from));

        // Explicit name to make it very clear
        public static scardot_variant CreateFromStringTakingOwnershipOfDisposableValue(scardot_string from)
            => new() { Type = Variant.Type.String, String = from };

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static scardot_variant CreateFromString(string? from)
            => CreateFromStringTakingOwnershipOfDisposableValue(Marshaling.ConvertStringToNative(from));

        public static scardot_variant CreateFromPackedByteArray(in scardot_packed_byte_array from)
        {
            NativeFuncs.scardotsharp_variant_new_packed_byte_array(out scardot_variant ret, from);
            return ret;
        }

        public static scardot_variant CreateFromPackedInt32Array(in scardot_packed_int32_array from)
        {
            NativeFuncs.scardotsharp_variant_new_packed_int32_array(out scardot_variant ret, from);
            return ret;
        }

        public static scardot_variant CreateFromPackedInt64Array(in scardot_packed_int64_array from)
        {
            NativeFuncs.scardotsharp_variant_new_packed_int64_array(out scardot_variant ret, from);
            return ret;
        }

        public static scardot_variant CreateFromPackedFloat32Array(in scardot_packed_float32_array from)
        {
            NativeFuncs.scardotsharp_variant_new_packed_float32_array(out scardot_variant ret, from);
            return ret;
        }

        public static scardot_variant CreateFromPackedFloat64Array(in scardot_packed_float64_array from)
        {
            NativeFuncs.scardotsharp_variant_new_packed_float64_array(out scardot_variant ret, from);
            return ret;
        }

        public static scardot_variant CreateFromPackedStringArray(in scardot_packed_string_array from)
        {
            NativeFuncs.scardotsharp_variant_new_packed_string_array(out scardot_variant ret, from);
            return ret;
        }

        public static scardot_variant CreateFromPackedVector2Array(in scardot_packed_vector2_array from)
        {
            NativeFuncs.scardotsharp_variant_new_packed_vector2_array(out scardot_variant ret, from);
            return ret;
        }

        public static scardot_variant CreateFromPackedVector3Array(in scardot_packed_vector3_array from)
        {
            NativeFuncs.scardotsharp_variant_new_packed_vector3_array(out scardot_variant ret, from);
            return ret;
        }

        public static scardot_variant CreateFromPackedVector4Array(in scardot_packed_vector4_array from)
        {
            NativeFuncs.scardotsharp_variant_new_packed_vector4_array(out scardot_variant ret, from);
            return ret;
        }

        public static scardot_variant CreateFromPackedColorArray(in scardot_packed_color_array from)
        {
            NativeFuncs.scardotsharp_variant_new_packed_color_array(out scardot_variant ret, from);
            return ret;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static scardot_variant CreateFromPackedByteArray(Span<byte> from)
        {
            using var nativePackedArray = Marshaling.ConvertSystemArrayToNativePackedByteArray(from);
            return CreateFromPackedByteArray(nativePackedArray);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static scardot_variant CreateFromPackedInt32Array(Span<int> from)
        {
            using var nativePackedArray = Marshaling.ConvertSystemArrayToNativePackedInt32Array(from);
            return CreateFromPackedInt32Array(nativePackedArray);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static scardot_variant CreateFromPackedInt64Array(Span<long> from)
        {
            using var nativePackedArray = Marshaling.ConvertSystemArrayToNativePackedInt64Array(from);
            return CreateFromPackedInt64Array(nativePackedArray);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static scardot_variant CreateFromPackedFloat32Array(Span<float> from)
        {
            using var nativePackedArray = Marshaling.ConvertSystemArrayToNativePackedFloat32Array(from);
            return CreateFromPackedFloat32Array(nativePackedArray);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static scardot_variant CreateFromPackedFloat64Array(Span<double> from)
        {
            using var nativePackedArray = Marshaling.ConvertSystemArrayToNativePackedFloat64Array(from);
            return CreateFromPackedFloat64Array(nativePackedArray);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static scardot_variant CreateFromPackedStringArray(Span<string> from)
        {
            using var nativePackedArray = Marshaling.ConvertSystemArrayToNativePackedStringArray(from);
            return CreateFromPackedStringArray(nativePackedArray);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static scardot_variant CreateFromPackedVector2Array(Span<Vector2> from)
        {
            using var nativePackedArray = Marshaling.ConvertSystemArrayToNativePackedVector2Array(from);
            return CreateFromPackedVector2Array(nativePackedArray);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static scardot_variant CreateFromPackedVector3Array(Span<Vector3> from)
        {
            using var nativePackedArray = Marshaling.ConvertSystemArrayToNativePackedVector3Array(from);
            return CreateFromPackedVector3Array(nativePackedArray);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static scardot_variant CreateFromPackedVector4Array(Span<Vector4> from)
        {
            using var nativePackedArray = Marshaling.ConvertSystemArrayToNativePackedVector4Array(from);
            return CreateFromPackedVector4Array(nativePackedArray);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static scardot_variant CreateFromPackedColorArray(Span<Color> from)
        {
            using var nativePackedArray = Marshaling.ConvertSystemArrayToNativePackedColorArray(from);
            return CreateFromPackedColorArray(nativePackedArray);
        }

        public static scardot_variant CreateFromSystemArrayOfStringName(Span<StringName> from)
        {
            if (from == null)
                return default;
            using var fromscardot = new Collections.Array(from);
            return CreateFromArray((scardot_array)fromscardot.NativeValue);
        }

        public static scardot_variant CreateFromSystemArrayOfNodePath(Span<NodePath> from)
        {
            if (from == null)
                return default;
            using var fromscardot = new Collections.Array(from);
            return CreateFromArray((scardot_array)fromscardot.NativeValue);
        }

        public static scardot_variant CreateFromSystemArrayOfRid(Span<Rid> from)
        {
            if (from == null)
                return default;
            using var fromscardot = new Collections.Array(from);
            return CreateFromArray((scardot_array)fromscardot.NativeValue);
        }

        public static scardot_variant CreateFromSystemArrayOfscardotObject(scardotObject[]? from)
        {
            if (from == null)
                return default; // Nil
            using var fromscardot = new Collections.Array(from);
            return CreateFromArray((scardot_array)fromscardot.NativeValue);
        }

        public static scardot_variant CreateFromArray(scardot_array from)
        {
            NativeFuncs.scardotsharp_variant_new_array(out scardot_variant ret, from);
            return ret;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static scardot_variant CreateFromArray(Collections.Array? from)
            => from != null ? CreateFromArray((scardot_array)from.NativeValue) : default;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static scardot_variant CreateFromArray<[MustBeVariant] T>(Array<T>? from)
            => from != null ? CreateFromArray((scardot_array)((Collections.Array)from).NativeValue) : default;

        public static scardot_variant CreateFromDictionary(scardot_dictionary from)
        {
            NativeFuncs.scardotsharp_variant_new_dictionary(out scardot_variant ret, from);
            return ret;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static scardot_variant CreateFromDictionary(Dictionary? from)
            => from != null ? CreateFromDictionary((scardot_dictionary)from.NativeValue) : default;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static scardot_variant CreateFromDictionary<[MustBeVariant] TKey, [MustBeVariant] TValue>(Dictionary<TKey, TValue>? from)
            => from != null ? CreateFromDictionary((scardot_dictionary)((Dictionary)from).NativeValue) : default;

        public static scardot_variant CreateFromStringName(scardot_string_name from)
        {
            NativeFuncs.scardotsharp_variant_new_string_name(out scardot_variant ret, from);
            return ret;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static scardot_variant CreateFromStringName(StringName? from)
            => from != null ? CreateFromStringName((scardot_string_name)from.NativeValue) : default;

        public static scardot_variant CreateFromNodePath(scardot_node_path from)
        {
            NativeFuncs.scardotsharp_variant_new_node_path(out scardot_variant ret, from);
            return ret;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static scardot_variant CreateFromNodePath(NodePath? from)
            => from != null ? CreateFromNodePath((scardot_node_path)from.NativeValue) : default;

        public static scardot_variant CreateFromscardotObjectPtr(IntPtr from)
        {
            if (from == IntPtr.Zero)
                return new scardot_variant();
            NativeFuncs.scardotsharp_variant_new_object(out scardot_variant ret, from);
            return ret;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static scardot_variant CreateFromscardotObject(scardotObject? from)
            => from != null ? CreateFromscardotObjectPtr(scardotObject.GetPtr(from)) : default;

        // We avoid the internal call if the stored type is the same we want.

        public static bool ConvertToBool(in scardot_variant p_var)
            => p_var.Type == Variant.Type.Bool ?
                p_var.Bool.ToBool() :
                NativeFuncs.scardotsharp_variant_as_bool(p_var).ToBool();

        public static char ConvertToChar(in scardot_variant p_var)
            => (char)(p_var.Type == Variant.Type.Int ?
                p_var.Int :
                NativeFuncs.scardotsharp_variant_as_int(p_var));

        public static sbyte ConvertToInt8(in scardot_variant p_var)
            => (sbyte)(p_var.Type == Variant.Type.Int ?
                p_var.Int :
                NativeFuncs.scardotsharp_variant_as_int(p_var));

        public static short ConvertToInt16(in scardot_variant p_var)
            => (short)(p_var.Type == Variant.Type.Int ?
                p_var.Int :
                NativeFuncs.scardotsharp_variant_as_int(p_var));

        public static int ConvertToInt32(in scardot_variant p_var)
            => (int)(p_var.Type == Variant.Type.Int ?
                p_var.Int :
                NativeFuncs.scardotsharp_variant_as_int(p_var));

        public static long ConvertToInt64(in scardot_variant p_var)
            => p_var.Type == Variant.Type.Int ? p_var.Int : NativeFuncs.scardotsharp_variant_as_int(p_var);

        public static byte ConvertToUInt8(in scardot_variant p_var)
            => (byte)(p_var.Type == Variant.Type.Int ?
                p_var.Int :
                NativeFuncs.scardotsharp_variant_as_int(p_var));

        public static ushort ConvertToUInt16(in scardot_variant p_var)
            => (ushort)(p_var.Type == Variant.Type.Int ?
                p_var.Int :
                NativeFuncs.scardotsharp_variant_as_int(p_var));

        public static uint ConvertToUInt32(in scardot_variant p_var)
            => (uint)(p_var.Type == Variant.Type.Int ?
                p_var.Int :
                NativeFuncs.scardotsharp_variant_as_int(p_var));

        public static ulong ConvertToUInt64(in scardot_variant p_var)
            => (ulong)(p_var.Type == Variant.Type.Int ?
                p_var.Int :
                NativeFuncs.scardotsharp_variant_as_int(p_var));

        public static float ConvertToFloat32(in scardot_variant p_var)
            => (float)(p_var.Type == Variant.Type.Float ?
                p_var.Float :
                NativeFuncs.scardotsharp_variant_as_float(p_var));

        public static double ConvertToFloat64(in scardot_variant p_var)
            => p_var.Type == Variant.Type.Float ?
                p_var.Float :
                NativeFuncs.scardotsharp_variant_as_float(p_var);

        public static Vector2 ConvertToVector2(in scardot_variant p_var)
            => p_var.Type == Variant.Type.Vector2 ?
                p_var.Vector2 :
                NativeFuncs.scardotsharp_variant_as_vector2(p_var);

        public static Vector2I ConvertToVector2I(in scardot_variant p_var)
            => p_var.Type == Variant.Type.Vector2I ?
                p_var.Vector2I :
                NativeFuncs.scardotsharp_variant_as_vector2i(p_var);

        public static Rect2 ConvertToRect2(in scardot_variant p_var)
            => p_var.Type == Variant.Type.Rect2 ?
                p_var.Rect2 :
                NativeFuncs.scardotsharp_variant_as_rect2(p_var);

        public static Rect2I ConvertToRect2I(in scardot_variant p_var)
            => p_var.Type == Variant.Type.Rect2I ?
                p_var.Rect2I :
                NativeFuncs.scardotsharp_variant_as_rect2i(p_var);

        public static unsafe Transform2D ConvertToTransform2D(in scardot_variant p_var)
            => p_var.Type == Variant.Type.Transform2D ?
                *p_var.Transform2D :
                NativeFuncs.scardotsharp_variant_as_transform2d(p_var);

        public static Vector3 ConvertToVector3(in scardot_variant p_var)
            => p_var.Type == Variant.Type.Vector3 ?
                p_var.Vector3 :
                NativeFuncs.scardotsharp_variant_as_vector3(p_var);

        public static Vector3I ConvertToVector3I(in scardot_variant p_var)
            => p_var.Type == Variant.Type.Vector3I ?
                p_var.Vector3I :
                NativeFuncs.scardotsharp_variant_as_vector3i(p_var);

        public static unsafe Vector4 ConvertToVector4(in scardot_variant p_var)
            => p_var.Type == Variant.Type.Vector4 ?
                p_var.Vector4 :
                NativeFuncs.scardotsharp_variant_as_vector4(p_var);

        public static unsafe Vector4I ConvertToVector4I(in scardot_variant p_var)
            => p_var.Type == Variant.Type.Vector4I ?
                p_var.Vector4I :
                NativeFuncs.scardotsharp_variant_as_vector4i(p_var);

        public static unsafe Basis ConvertToBasis(in scardot_variant p_var)
            => p_var.Type == Variant.Type.Basis ?
                *p_var.Basis :
                NativeFuncs.scardotsharp_variant_as_basis(p_var);

        public static Quaternion ConvertToQuaternion(in scardot_variant p_var)
            => p_var.Type == Variant.Type.Quaternion ?
                p_var.Quaternion :
                NativeFuncs.scardotsharp_variant_as_quaternion(p_var);

        public static unsafe Transform3D ConvertToTransform3D(in scardot_variant p_var)
            => p_var.Type == Variant.Type.Transform3D ?
                *p_var.Transform3D :
                NativeFuncs.scardotsharp_variant_as_transform3d(p_var);

        public static unsafe Projection ConvertToProjection(in scardot_variant p_var)
            => p_var.Type == Variant.Type.Projection ?
                *p_var.Projection :
                NativeFuncs.scardotsharp_variant_as_projection(p_var);

        public static unsafe Aabb ConvertToAabb(in scardot_variant p_var)
            => p_var.Type == Variant.Type.Aabb ?
                *p_var.Aabb :
                NativeFuncs.scardotsharp_variant_as_aabb(p_var);

        public static Color ConvertToColor(in scardot_variant p_var)
            => p_var.Type == Variant.Type.Color ?
                p_var.Color :
                NativeFuncs.scardotsharp_variant_as_color(p_var);

        public static Plane ConvertToPlane(in scardot_variant p_var)
            => p_var.Type == Variant.Type.Plane ?
                p_var.Plane :
                NativeFuncs.scardotsharp_variant_as_plane(p_var);

        public static Rid ConvertToRid(in scardot_variant p_var)
            => p_var.Type == Variant.Type.Rid ?
                p_var.Rid :
                NativeFuncs.scardotsharp_variant_as_rid(p_var);

        public static IntPtr ConvertToscardotObjectPtr(in scardot_variant p_var)
            => p_var.Type == Variant.Type.Object ? p_var.Object : IntPtr.Zero;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static scardotObject ConvertToscardotObject(in scardot_variant p_var)
            => InteropUtils.UnmanagedGetManaged(ConvertToscardotObjectPtr(p_var));

        public static string ConvertToString(in scardot_variant p_var)
        {
            switch (p_var.Type)
            {
                case Variant.Type.Nil:
                    return ""; // Otherwise, Variant -> String would return the string "Null"
                case Variant.Type.String:
                {
                    // We avoid the internal call if the stored type is the same we want.
                    return Marshaling.ConvertStringToManaged(p_var.String);
                }
                default:
                {
                    using scardot_string scardotString = NativeFuncs.scardotsharp_variant_as_string(p_var);
                    return Marshaling.ConvertStringToManaged(scardotString);
                }
            }
        }

        public static scardot_string_name ConvertToNativeStringName(in scardot_variant p_var)
            => p_var.Type == Variant.Type.StringName ?
                NativeFuncs.scardotsharp_string_name_new_copy(p_var.StringName) :
                NativeFuncs.scardotsharp_variant_as_string_name(p_var);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static StringName ConvertToStringName(in scardot_variant p_var)
            => StringName.CreateTakingOwnershipOfDisposableValue(ConvertToNativeStringName(p_var));

        public static scardot_node_path ConvertToNativeNodePath(in scardot_variant p_var)
            => p_var.Type == Variant.Type.NodePath ?
                NativeFuncs.scardotsharp_node_path_new_copy(p_var.NodePath) :
                NativeFuncs.scardotsharp_variant_as_node_path(p_var);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static NodePath ConvertToNodePath(in scardot_variant p_var)
            => NodePath.CreateTakingOwnershipOfDisposableValue(ConvertToNativeNodePath(p_var));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static scardot_callable ConvertToNativeCallable(in scardot_variant p_var)
            => NativeFuncs.scardotsharp_variant_as_callable(p_var);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Callable ConvertToCallable(in scardot_variant p_var)
            => Marshaling.ConvertCallableToManaged(ConvertToNativeCallable(p_var));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static scardot_signal ConvertToNativeSignal(in scardot_variant p_var)
            => NativeFuncs.scardotsharp_variant_as_signal(p_var);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Signal ConvertToSignal(in scardot_variant p_var)
            => Marshaling.ConvertSignalToManaged(ConvertToNativeSignal(p_var));

        public static scardot_array ConvertToNativeArray(in scardot_variant p_var)
            => p_var.Type == Variant.Type.Array ?
                NativeFuncs.scardotsharp_array_new_copy(p_var.Array) :
                NativeFuncs.scardotsharp_variant_as_array(p_var);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Collections.Array ConvertToArray(in scardot_variant p_var)
            => Collections.Array.CreateTakingOwnershipOfDisposableValue(ConvertToNativeArray(p_var));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Array<T> ConvertToArray<[MustBeVariant] T>(in scardot_variant p_var)
            => Array<T>.CreateTakingOwnershipOfDisposableValue(ConvertToNativeArray(p_var));

        public static scardot_dictionary ConvertToNativeDictionary(in scardot_variant p_var)
            => p_var.Type == Variant.Type.Dictionary ?
                NativeFuncs.scardotsharp_dictionary_new_copy(p_var.Dictionary) :
                NativeFuncs.scardotsharp_variant_as_dictionary(p_var);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Dictionary ConvertToDictionary(in scardot_variant p_var)
            => Dictionary.CreateTakingOwnershipOfDisposableValue(ConvertToNativeDictionary(p_var));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Dictionary<TKey, TValue> ConvertToDictionary<[MustBeVariant] TKey, [MustBeVariant] TValue>(in scardot_variant p_var)
            => Dictionary<TKey, TValue>.CreateTakingOwnershipOfDisposableValue(ConvertToNativeDictionary(p_var));

        public static byte[] ConvertAsPackedByteArrayToSystemArray(in scardot_variant p_var)
        {
            using var packedArray = NativeFuncs.scardotsharp_variant_as_packed_byte_array(p_var);
            return Marshaling.ConvertNativePackedByteArrayToSystemArray(packedArray);
        }

        public static int[] ConvertAsPackedInt32ArrayToSystemArray(in scardot_variant p_var)
        {
            using var packedArray = NativeFuncs.scardotsharp_variant_as_packed_int32_array(p_var);
            return Marshaling.ConvertNativePackedInt32ArrayToSystemArray(packedArray);
        }

        public static long[] ConvertAsPackedInt64ArrayToSystemArray(in scardot_variant p_var)
        {
            using var packedArray = NativeFuncs.scardotsharp_variant_as_packed_int64_array(p_var);
            return Marshaling.ConvertNativePackedInt64ArrayToSystemArray(packedArray);
        }

        public static float[] ConvertAsPackedFloat32ArrayToSystemArray(in scardot_variant p_var)
        {
            using var packedArray = NativeFuncs.scardotsharp_variant_as_packed_float32_array(p_var);
            return Marshaling.ConvertNativePackedFloat32ArrayToSystemArray(packedArray);
        }

        public static double[] ConvertAsPackedFloat64ArrayToSystemArray(in scardot_variant p_var)
        {
            using var packedArray = NativeFuncs.scardotsharp_variant_as_packed_float64_array(p_var);
            return Marshaling.ConvertNativePackedFloat64ArrayToSystemArray(packedArray);
        }

        public static string[] ConvertAsPackedStringArrayToSystemArray(in scardot_variant p_var)
        {
            using var packedArray = NativeFuncs.scardotsharp_variant_as_packed_string_array(p_var);
            return Marshaling.ConvertNativePackedStringArrayToSystemArray(packedArray);
        }

        public static Vector2[] ConvertAsPackedVector2ArrayToSystemArray(in scardot_variant p_var)
        {
            using var packedArray = NativeFuncs.scardotsharp_variant_as_packed_vector2_array(p_var);
            return Marshaling.ConvertNativePackedVector2ArrayToSystemArray(packedArray);
        }

        public static Vector3[] ConvertAsPackedVector3ArrayToSystemArray(in scardot_variant p_var)
        {
            using var packedArray = NativeFuncs.scardotsharp_variant_as_packed_vector3_array(p_var);
            return Marshaling.ConvertNativePackedVector3ArrayToSystemArray(packedArray);
        }

        public static Vector4[] ConvertAsPackedVector4ArrayToSystemArray(in scardot_variant p_var)
        {
            using var packedArray = NativeFuncs.scardotsharp_variant_as_packed_vector4_array(p_var);
            return Marshaling.ConvertNativePackedVector4ArrayToSystemArray(packedArray);
        }

        public static Color[] ConvertAsPackedColorArrayToSystemArray(in scardot_variant p_var)
        {
            using var packedArray = NativeFuncs.scardotsharp_variant_as_packed_color_array(p_var);
            return Marshaling.ConvertNativePackedColorArrayToSystemArray(packedArray);
        }

        public static StringName[] ConvertToSystemArrayOfStringName(in scardot_variant p_var)
        {
            using var scardotArray = NativeFuncs.scardotsharp_variant_as_array(p_var);
            return Marshaling.ConvertNativescardotArrayToSystemArrayOfStringName(scardotArray);
        }

        public static NodePath[] ConvertToSystemArrayOfNodePath(in scardot_variant p_var)
        {
            using var scardotArray = NativeFuncs.scardotsharp_variant_as_array(p_var);
            return Marshaling.ConvertNativescardotArrayToSystemArrayOfNodePath(scardotArray);
        }

        public static Rid[] ConvertToSystemArrayOfRid(in scardot_variant p_var)
        {
            using var scardotArray = NativeFuncs.scardotsharp_variant_as_array(p_var);
            return Marshaling.ConvertNativescardotArrayToSystemArrayOfRid(scardotArray);
        }

        public static T[] ConvertToSystemArrayOfscardotObject<T>(in scardot_variant p_var)
            where T : scardotObject
        {
            using var scardotArray = NativeFuncs.scardotsharp_variant_as_array(p_var);
            return Marshaling.ConvertNativescardotArrayToSystemArrayOfscardotObjectType<T>(scardotArray);
        }
    }
}
