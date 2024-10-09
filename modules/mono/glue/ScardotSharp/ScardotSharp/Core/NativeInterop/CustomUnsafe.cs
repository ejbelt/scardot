using System.Runtime.CompilerServices;

namespace scardot.NativeInterop;

// Ref structs are not allowed as generic type parameters, so we can't use Unsafe.AsPointer<T>/AsRef<T>.
// As a workaround we create our own overloads for our structs with some tricks under the hood.

public static class CustomUnsafe
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe scardot_ref* AsPointer(ref scardot_ref value)
        => value.GetUnsafeAddress();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe scardot_ref* ReadOnlyRefAsPointer(in scardot_ref value)
        => value.GetUnsafeAddress();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe ref scardot_ref AsRef(scardot_ref* source)
        => ref *source;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe ref scardot_ref AsRef(in scardot_ref source)
        => ref *ReadOnlyRefAsPointer(in source);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe scardot_variant_call_error* AsPointer(ref scardot_variant_call_error value)
        => value.GetUnsafeAddress();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe scardot_variant_call_error* ReadOnlyRefAsPointer(in scardot_variant_call_error value)
        => value.GetUnsafeAddress();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe ref scardot_variant_call_error AsRef(scardot_variant_call_error* source)
        => ref *source;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe ref scardot_variant_call_error AsRef(in scardot_variant_call_error source)
        => ref *ReadOnlyRefAsPointer(in source);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe scardot_variant* AsPointer(ref scardot_variant value)
        => value.GetUnsafeAddress();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe scardot_variant* ReadOnlyRefAsPointer(in scardot_variant value)
        => value.GetUnsafeAddress();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe ref scardot_variant AsRef(scardot_variant* source)
        => ref *source;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe ref scardot_variant AsRef(in scardot_variant source)
        => ref *ReadOnlyRefAsPointer(in source);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe scardot_string* AsPointer(ref scardot_string value)
        => value.GetUnsafeAddress();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe scardot_string* ReadOnlyRefAsPointer(in scardot_string value)
        => value.GetUnsafeAddress();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe ref scardot_string AsRef(scardot_string* source)
        => ref *source;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe ref scardot_string AsRef(in scardot_string source)
        => ref *ReadOnlyRefAsPointer(in source);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe scardot_string_name* AsPointer(ref scardot_string_name value)
        => value.GetUnsafeAddress();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe scardot_string_name* ReadOnlyRefAsPointer(in scardot_string_name value)
        => value.GetUnsafeAddress();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe ref scardot_string_name AsRef(scardot_string_name* source)
        => ref *source;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe ref scardot_string_name AsRef(in scardot_string_name source)
        => ref *ReadOnlyRefAsPointer(in source);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe scardot_node_path* AsPointer(ref scardot_node_path value)
        => value.GetUnsafeAddress();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe scardot_node_path* ReadOnlyRefAsPointer(in scardot_node_path value)
        => value.GetUnsafeAddress();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe ref scardot_node_path AsRef(scardot_node_path* source)
        => ref *source;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe ref scardot_node_path AsRef(in scardot_node_path source)
        => ref *ReadOnlyRefAsPointer(in source);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe scardot_signal* AsPointer(ref scardot_signal value)
        => value.GetUnsafeAddress();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe scardot_signal* ReadOnlyRefAsPointer(in scardot_signal value)
        => value.GetUnsafeAddress();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe ref scardot_signal AsRef(scardot_signal* source)
        => ref *source;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe ref scardot_signal AsRef(in scardot_signal source)
        => ref *ReadOnlyRefAsPointer(in source);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe scardot_callable* AsPointer(ref scardot_callable value)
        => value.GetUnsafeAddress();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe scardot_callable* ReadOnlyRefAsPointer(in scardot_callable value)
        => value.GetUnsafeAddress();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe ref scardot_callable AsRef(scardot_callable* source)
        => ref *source;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe ref scardot_callable AsRef(in scardot_callable source)
        => ref *ReadOnlyRefAsPointer(in source);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe scardot_array* AsPointer(ref scardot_array value)
        => value.GetUnsafeAddress();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe scardot_array* ReadOnlyRefAsPointer(in scardot_array value)
        => value.GetUnsafeAddress();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe ref scardot_array AsRef(scardot_array* source)
        => ref *source;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe ref scardot_array AsRef(in scardot_array source)
        => ref *ReadOnlyRefAsPointer(in source);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe scardot_dictionary* AsPointer(ref scardot_dictionary value)
        => value.GetUnsafeAddress();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe scardot_dictionary* ReadOnlyRefAsPointer(in scardot_dictionary value)
        => value.GetUnsafeAddress();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe ref scardot_dictionary AsRef(scardot_dictionary* source)
        => ref *source;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe ref scardot_dictionary AsRef(in scardot_dictionary source)
        => ref *ReadOnlyRefAsPointer(in source);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe scardot_packed_byte_array* AsPointer(ref scardot_packed_byte_array value)
        => value.GetUnsafeAddress();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe scardot_packed_byte_array* ReadOnlyRefAsPointer(in scardot_packed_byte_array value)
        => value.GetUnsafeAddress();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe ref scardot_packed_byte_array AsRef(scardot_packed_byte_array* source)
        => ref *source;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe ref scardot_packed_byte_array AsRef(in scardot_packed_byte_array source)
        => ref *ReadOnlyRefAsPointer(in source);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe scardot_packed_int32_array* AsPointer(ref scardot_packed_int32_array value)
        => value.GetUnsafeAddress();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe scardot_packed_int32_array* ReadOnlyRefAsPointer(in scardot_packed_int32_array value)
        => value.GetUnsafeAddress();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe ref scardot_packed_int32_array AsRef(scardot_packed_int32_array* source)
        => ref *source;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe ref scardot_packed_int32_array AsRef(in scardot_packed_int32_array source)
        => ref *ReadOnlyRefAsPointer(in source);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe scardot_packed_int64_array* AsPointer(ref scardot_packed_int64_array value)
        => value.GetUnsafeAddress();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe scardot_packed_int64_array* ReadOnlyRefAsPointer(in scardot_packed_int64_array value)
        => value.GetUnsafeAddress();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe ref scardot_packed_int64_array AsRef(scardot_packed_int64_array* source)
        => ref *source;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe ref scardot_packed_int64_array AsRef(in scardot_packed_int64_array source)
        => ref *ReadOnlyRefAsPointer(in source);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe scardot_packed_float32_array* AsPointer(ref scardot_packed_float32_array value)
        => value.GetUnsafeAddress();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe scardot_packed_float32_array* ReadOnlyRefAsPointer(in scardot_packed_float32_array value)
        => value.GetUnsafeAddress();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe ref scardot_packed_float32_array AsRef(scardot_packed_float32_array* source)
        => ref *source;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe ref scardot_packed_float32_array AsRef(in scardot_packed_float32_array source)
        => ref *ReadOnlyRefAsPointer(in source);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe scardot_packed_float64_array* AsPointer(ref scardot_packed_float64_array value)
        => value.GetUnsafeAddress();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe scardot_packed_float64_array* ReadOnlyRefAsPointer(in scardot_packed_float64_array value)
        => value.GetUnsafeAddress();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe ref scardot_packed_float64_array AsRef(scardot_packed_float64_array* source)
        => ref *source;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe ref scardot_packed_float64_array AsRef(in scardot_packed_float64_array source)
        => ref *ReadOnlyRefAsPointer(in source);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe scardot_packed_string_array* AsPointer(ref scardot_packed_string_array value)
        => value.GetUnsafeAddress();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe scardot_packed_string_array* ReadOnlyRefAsPointer(in scardot_packed_string_array value)
        => value.GetUnsafeAddress();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe ref scardot_packed_string_array AsRef(scardot_packed_string_array* source)
        => ref *source;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe ref scardot_packed_string_array AsRef(in scardot_packed_string_array source)
        => ref *ReadOnlyRefAsPointer(in source);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe scardot_packed_vector2_array* AsPointer(ref scardot_packed_vector2_array value)
        => value.GetUnsafeAddress();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe scardot_packed_vector2_array* ReadOnlyRefAsPointer(in scardot_packed_vector2_array value)
        => value.GetUnsafeAddress();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe ref scardot_packed_vector2_array AsRef(scardot_packed_vector2_array* source)
        => ref *source;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe ref scardot_packed_vector2_array AsRef(in scardot_packed_vector2_array source)
        => ref *ReadOnlyRefAsPointer(in source);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe scardot_packed_vector3_array* AsPointer(ref scardot_packed_vector3_array value)
        => value.GetUnsafeAddress();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe scardot_packed_vector3_array* ReadOnlyRefAsPointer(in scardot_packed_vector3_array value)
        => value.GetUnsafeAddress();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe ref scardot_packed_vector3_array AsRef(scardot_packed_vector3_array* source)
        => ref *source;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe ref scardot_packed_vector3_array AsRef(in scardot_packed_vector3_array source)
        => ref *ReadOnlyRefAsPointer(in source);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe scardot_packed_vector4_array* AsPointer(ref scardot_packed_vector4_array value)
        => value.GetUnsafeAddress();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe scardot_packed_vector4_array* ReadOnlyRefAsPointer(in scardot_packed_vector4_array value)
        => value.GetUnsafeAddress();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe ref scardot_packed_vector4_array AsRef(scardot_packed_vector4_array* source)
        => ref *source;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe ref scardot_packed_vector4_array AsRef(in scardot_packed_vector4_array source)
        => ref *ReadOnlyRefAsPointer(in source);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe scardot_packed_color_array* AsPointer(ref scardot_packed_color_array value)
        => value.GetUnsafeAddress();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe scardot_packed_color_array* ReadOnlyRefAsPointer(in scardot_packed_color_array value)
        => value.GetUnsafeAddress();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe ref scardot_packed_color_array AsRef(scardot_packed_color_array* source)
        => ref *source;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe ref scardot_packed_color_array AsRef(in scardot_packed_color_array source)
        => ref *ReadOnlyRefAsPointer(in source);
}
