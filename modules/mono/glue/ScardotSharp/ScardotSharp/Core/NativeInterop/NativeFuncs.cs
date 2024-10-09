#pragma warning disable CA1707 // Identifiers should not contain underscores
#pragma warning disable IDE1006 // Naming rule violation
// ReSharper disable InconsistentNaming

using System;
using System.Runtime.CompilerServices;
using scardot.SourceGenerators.Internal;


namespace scardot.NativeInterop
{
    /*
     * IMPORTANT:
     * The order of the methods defined in NativeFuncs must match the order
     * in the array defined at the bottom of 'glue/runtime_interop.cpp'.
     */

    [GenerateUnmanagedCallbacks(typeof(UnmanagedCallbacks))]
    public static unsafe partial class NativeFuncs
    {
        private static bool initialized;

        // ReSharper disable once ParameterOnlyUsedForPreconditionCheck.Global
        public static void Initialize(IntPtr unmanagedCallbacks, int unmanagedCallbacksSize)
        {
            if (initialized)
                throw new InvalidOperationException("Already initialized.");
            initialized = true;

            if (unmanagedCallbacksSize != sizeof(UnmanagedCallbacks))
                throw new ArgumentException("Unmanaged callbacks size mismatch.", nameof(unmanagedCallbacksSize));

            _unmanagedCallbacks = Unsafe.AsRef<UnmanagedCallbacks>((void*)unmanagedCallbacks);
        }

        private partial struct UnmanagedCallbacks
        {
        }

        // Custom functions

        internal static partial scardot_bool scardotsharp_dotnet_module_is_initialized();

        public static partial IntPtr scardotsharp_method_bind_get_method(in scardot_string_name p_classname,
            in scardot_string_name p_methodname);

        public static partial IntPtr scardotsharp_method_bind_get_method_with_compatibility(
            in scardot_string_name p_classname, in scardot_string_name p_methodname, ulong p_hash);

        public static partial delegate* unmanaged<scardot_bool, IntPtr> scardotsharp_get_class_constructor(
            in scardot_string_name p_classname);

        public static partial IntPtr scardotsharp_engine_get_singleton(in scardot_string p_name);


        internal static partial Error scardotsharp_stack_info_vector_resize(
            ref DebuggingUtils.scardot_stack_info_vector p_stack_info_vector, int p_size);

        internal static partial void scardotsharp_stack_info_vector_destroy(
            ref DebuggingUtils.scardot_stack_info_vector p_stack_info_vector);

        internal static partial void scardotsharp_internal_editor_file_system_update_files(in scardot_packed_string_array p_script_paths);

        internal static partial void scardotsharp_internal_script_debugger_send_error(in scardot_string p_func,
            in scardot_string p_file, int p_line, in scardot_string p_err, in scardot_string p_descr,
            scardot_error_handler_type p_type, in DebuggingUtils.scardot_stack_info_vector p_stack_info_vector);

        internal static partial scardot_bool scardotsharp_internal_script_debugger_is_active();

        internal static partial IntPtr scardotsharp_internal_object_get_associated_gchandle(IntPtr ptr);

        internal static partial void scardotsharp_internal_object_disposed(IntPtr ptr, IntPtr gcHandleToFree);

        internal static partial void scardotsharp_internal_refcounted_disposed(IntPtr ptr, IntPtr gcHandleToFree,
            scardot_bool isFinalizer);

        internal static partial Error scardotsharp_internal_signal_awaiter_connect(IntPtr source,
            in scardot_string_name signal,
            IntPtr target, IntPtr awaiterHandlePtr);

        internal static partial void scardotsharp_internal_tie_native_managed_to_unmanaged(IntPtr gcHandleIntPtr,
            IntPtr unmanaged, in scardot_string_name nativeName, scardot_bool refCounted);

        internal static partial void scardotsharp_internal_tie_user_managed_to_unmanaged(IntPtr gcHandleIntPtr,
            IntPtr unmanaged, scardot_ref* scriptPtr, scardot_bool refCounted);

        internal static partial void scardotsharp_internal_tie_managed_to_unmanaged_with_pre_setup(
            IntPtr gcHandleIntPtr, IntPtr unmanaged);

        internal static partial IntPtr scardotsharp_internal_unmanaged_get_script_instance_managed(IntPtr p_unmanaged,
            out scardot_bool r_has_cs_script_instance);

        internal static partial IntPtr scardotsharp_internal_unmanaged_get_instance_binding_managed(IntPtr p_unmanaged);

        internal static partial IntPtr scardotsharp_internal_unmanaged_instance_binding_create_managed(IntPtr p_unmanaged,
            IntPtr oldGCHandlePtr);

        internal static partial void scardotsharp_internal_new_csharp_script(scardot_ref* r_dest);

        internal static partial scardot_bool scardotsharp_internal_script_load(in scardot_string p_path, scardot_ref* r_dest);

        internal static partial void scardotsharp_internal_reload_registered_script(IntPtr scriptPtr);

        internal static partial void scardotsharp_array_filter_scardot_objects_by_native(in scardot_string_name p_native_name,
            in scardot_array p_input, out scardot_array r_output);

        internal static partial void scardotsharp_array_filter_scardot_objects_by_non_native(in scardot_array p_input,
            out scardot_array r_output);

        public static partial void scardotsharp_ref_new_from_ref_counted_ptr(out scardot_ref r_dest,
            IntPtr p_ref_counted_ptr);

        public static partial void scardotsharp_ref_destroy(ref scardot_ref p_instance);

        public static partial void scardotsharp_string_name_new_from_string(out scardot_string_name r_dest,
            in scardot_string p_name);

        public static partial void scardotsharp_node_path_new_from_string(out scardot_node_path r_dest,
            in scardot_string p_name);

        public static partial void
            scardotsharp_string_name_as_string(out scardot_string r_dest, in scardot_string_name p_name);

        public static partial void scardotsharp_node_path_as_string(out scardot_string r_dest, in scardot_node_path p_np);

        public static partial scardot_packed_byte_array scardotsharp_packed_byte_array_new_mem_copy(byte* p_src,
            int p_length);

        public static partial scardot_packed_int32_array scardotsharp_packed_int32_array_new_mem_copy(int* p_src,
            int p_length);

        public static partial scardot_packed_int64_array scardotsharp_packed_int64_array_new_mem_copy(long* p_src,
            int p_length);

        public static partial scardot_packed_float32_array scardotsharp_packed_float32_array_new_mem_copy(float* p_src,
            int p_length);

        public static partial scardot_packed_float64_array scardotsharp_packed_float64_array_new_mem_copy(double* p_src,
            int p_length);

        public static partial scardot_packed_vector2_array scardotsharp_packed_vector2_array_new_mem_copy(Vector2* p_src,
            int p_length);

        public static partial scardot_packed_vector3_array scardotsharp_packed_vector3_array_new_mem_copy(Vector3* p_src,
            int p_length);

        public static partial scardot_packed_vector4_array scardotsharp_packed_vector4_array_new_mem_copy(Vector4* p_src,
            int p_length);

        public static partial scardot_packed_color_array scardotsharp_packed_color_array_new_mem_copy(Color* p_src,
            int p_length);

        public static partial void scardotsharp_packed_string_array_add(ref scardot_packed_string_array r_dest,
            in scardot_string p_element);

        public static partial void scardotsharp_callable_new_with_delegate(IntPtr p_delegate_handle, IntPtr p_trampoline,
            IntPtr p_object, out scardot_callable r_callable);

        internal static partial scardot_bool scardotsharp_callable_get_data_for_marshalling(in scardot_callable p_callable,
            out IntPtr r_delegate_handle, out IntPtr r_trampoline, out IntPtr r_object, out scardot_string_name r_name);

        internal static partial scardot_variant scardotsharp_callable_call(in scardot_callable p_callable,
            scardot_variant** p_args, int p_arg_count, out scardot_variant_call_error p_call_error);

        internal static partial void scardotsharp_callable_call_deferred(in scardot_callable p_callable,
            scardot_variant** p_args, int p_arg_count);

        internal static partial Color scardotsharp_color_from_ok_hsl(float p_h, float p_s, float p_l, float p_alpha);

        // GDNative functions

        // gdnative.h

        public static partial void scardotsharp_method_bind_ptrcall(IntPtr p_method_bind, IntPtr p_instance, void** p_args,
            void* p_ret);

        public static partial scardot_variant scardotsharp_method_bind_call(IntPtr p_method_bind, IntPtr p_instance,
            scardot_variant** p_args, int p_arg_count, out scardot_variant_call_error p_call_error);

        // variant.h

        public static partial void
            scardotsharp_variant_new_string_name(out scardot_variant r_dest, in scardot_string_name p_s);

        public static partial void scardotsharp_variant_new_copy(out scardot_variant r_dest, in scardot_variant p_src);

        public static partial void scardotsharp_variant_new_node_path(out scardot_variant r_dest, in scardot_node_path p_np);

        public static partial void scardotsharp_variant_new_object(out scardot_variant r_dest, IntPtr p_obj);

        public static partial void scardotsharp_variant_new_transform2d(out scardot_variant r_dest, in Transform2D p_t2d);

        public static partial void scardotsharp_variant_new_basis(out scardot_variant r_dest, in Basis p_basis);

        public static partial void scardotsharp_variant_new_transform3d(out scardot_variant r_dest, in Transform3D p_trans);

        public static partial void scardotsharp_variant_new_projection(out scardot_variant r_dest, in Projection p_proj);

        public static partial void scardotsharp_variant_new_aabb(out scardot_variant r_dest, in Aabb p_aabb);

        public static partial void scardotsharp_variant_new_dictionary(out scardot_variant r_dest,
            in scardot_dictionary p_dict);

        public static partial void scardotsharp_variant_new_array(out scardot_variant r_dest, in scardot_array p_arr);

        public static partial void scardotsharp_variant_new_packed_byte_array(out scardot_variant r_dest,
            in scardot_packed_byte_array p_pba);

        public static partial void scardotsharp_variant_new_packed_int32_array(out scardot_variant r_dest,
            in scardot_packed_int32_array p_pia);

        public static partial void scardotsharp_variant_new_packed_int64_array(out scardot_variant r_dest,
            in scardot_packed_int64_array p_pia);

        public static partial void scardotsharp_variant_new_packed_float32_array(out scardot_variant r_dest,
            in scardot_packed_float32_array p_pra);

        public static partial void scardotsharp_variant_new_packed_float64_array(out scardot_variant r_dest,
            in scardot_packed_float64_array p_pra);

        public static partial void scardotsharp_variant_new_packed_string_array(out scardot_variant r_dest,
            in scardot_packed_string_array p_psa);

        public static partial void scardotsharp_variant_new_packed_vector2_array(out scardot_variant r_dest,
            in scardot_packed_vector2_array p_pv2a);

        public static partial void scardotsharp_variant_new_packed_vector3_array(out scardot_variant r_dest,
            in scardot_packed_vector3_array p_pv3a);

        public static partial void scardotsharp_variant_new_packed_vector4_array(out scardot_variant r_dest,
            in scardot_packed_vector4_array p_pv4a);

        public static partial void scardotsharp_variant_new_packed_color_array(out scardot_variant r_dest,
            in scardot_packed_color_array p_pca);

        public static partial scardot_bool scardotsharp_variant_as_bool(in scardot_variant p_self);

        public static partial Int64 scardotsharp_variant_as_int(in scardot_variant p_self);

        public static partial double scardotsharp_variant_as_float(in scardot_variant p_self);

        public static partial scardot_string scardotsharp_variant_as_string(in scardot_variant p_self);

        public static partial Vector2 scardotsharp_variant_as_vector2(in scardot_variant p_self);

        public static partial Vector2I scardotsharp_variant_as_vector2i(in scardot_variant p_self);

        public static partial Rect2 scardotsharp_variant_as_rect2(in scardot_variant p_self);

        public static partial Rect2I scardotsharp_variant_as_rect2i(in scardot_variant p_self);

        public static partial Vector3 scardotsharp_variant_as_vector3(in scardot_variant p_self);

        public static partial Vector3I scardotsharp_variant_as_vector3i(in scardot_variant p_self);

        public static partial Transform2D scardotsharp_variant_as_transform2d(in scardot_variant p_self);

        public static partial Vector4 scardotsharp_variant_as_vector4(in scardot_variant p_self);

        public static partial Vector4I scardotsharp_variant_as_vector4i(in scardot_variant p_self);

        public static partial Plane scardotsharp_variant_as_plane(in scardot_variant p_self);

        public static partial Quaternion scardotsharp_variant_as_quaternion(in scardot_variant p_self);

        public static partial Aabb scardotsharp_variant_as_aabb(in scardot_variant p_self);

        public static partial Basis scardotsharp_variant_as_basis(in scardot_variant p_self);

        public static partial Transform3D scardotsharp_variant_as_transform3d(in scardot_variant p_self);

        public static partial Projection scardotsharp_variant_as_projection(in scardot_variant p_self);

        public static partial Color scardotsharp_variant_as_color(in scardot_variant p_self);

        public static partial scardot_string_name scardotsharp_variant_as_string_name(in scardot_variant p_self);

        public static partial scardot_node_path scardotsharp_variant_as_node_path(in scardot_variant p_self);

        public static partial Rid scardotsharp_variant_as_rid(in scardot_variant p_self);

        public static partial scardot_callable scardotsharp_variant_as_callable(in scardot_variant p_self);

        public static partial scardot_signal scardotsharp_variant_as_signal(in scardot_variant p_self);

        public static partial scardot_dictionary scardotsharp_variant_as_dictionary(in scardot_variant p_self);

        public static partial scardot_array scardotsharp_variant_as_array(in scardot_variant p_self);

        public static partial scardot_packed_byte_array scardotsharp_variant_as_packed_byte_array(in scardot_variant p_self);

        public static partial scardot_packed_int32_array scardotsharp_variant_as_packed_int32_array(in scardot_variant p_self);

        public static partial scardot_packed_int64_array scardotsharp_variant_as_packed_int64_array(in scardot_variant p_self);

        public static partial scardot_packed_float32_array scardotsharp_variant_as_packed_float32_array(
            in scardot_variant p_self);

        public static partial scardot_packed_float64_array scardotsharp_variant_as_packed_float64_array(
            in scardot_variant p_self);

        public static partial scardot_packed_string_array scardotsharp_variant_as_packed_string_array(
            in scardot_variant p_self);

        public static partial scardot_packed_vector2_array scardotsharp_variant_as_packed_vector2_array(
            in scardot_variant p_self);

        public static partial scardot_packed_vector3_array scardotsharp_variant_as_packed_vector3_array(
            in scardot_variant p_self);

        public static partial scardot_packed_vector4_array scardotsharp_variant_as_packed_vector4_array(
            in scardot_variant p_self);

        public static partial scardot_packed_color_array scardotsharp_variant_as_packed_color_array(in scardot_variant p_self);

        public static partial scardot_bool scardotsharp_variant_equals(in scardot_variant p_a, in scardot_variant p_b);

        // string.h

        public static partial void scardotsharp_string_new_with_utf16_chars(out scardot_string r_dest, char* p_contents);

        // string_name.h

        public static partial void scardotsharp_string_name_new_copy(out scardot_string_name r_dest,
            in scardot_string_name p_src);

        // node_path.h

        public static partial void scardotsharp_node_path_new_copy(out scardot_node_path r_dest, in scardot_node_path p_src);

        // array.h

        public static partial void scardotsharp_array_new(out scardot_array r_dest);

        public static partial void scardotsharp_array_new_copy(out scardot_array r_dest, in scardot_array p_src);

        public static partial scardot_variant* scardotsharp_array_ptrw(ref scardot_array p_self);

        // dictionary.h

        public static partial void scardotsharp_dictionary_new(out scardot_dictionary r_dest);

        public static partial void scardotsharp_dictionary_new_copy(out scardot_dictionary r_dest,
            in scardot_dictionary p_src);

        // destroy functions

        public static partial void scardotsharp_packed_byte_array_destroy(ref scardot_packed_byte_array p_self);

        public static partial void scardotsharp_packed_int32_array_destroy(ref scardot_packed_int32_array p_self);

        public static partial void scardotsharp_packed_int64_array_destroy(ref scardot_packed_int64_array p_self);

        public static partial void scardotsharp_packed_float32_array_destroy(ref scardot_packed_float32_array p_self);

        public static partial void scardotsharp_packed_float64_array_destroy(ref scardot_packed_float64_array p_self);

        public static partial void scardotsharp_packed_string_array_destroy(ref scardot_packed_string_array p_self);

        public static partial void scardotsharp_packed_vector2_array_destroy(ref scardot_packed_vector2_array p_self);

        public static partial void scardotsharp_packed_vector3_array_destroy(ref scardot_packed_vector3_array p_self);

        public static partial void scardotsharp_packed_vector4_array_destroy(ref scardot_packed_vector4_array p_self);

        public static partial void scardotsharp_packed_color_array_destroy(ref scardot_packed_color_array p_self);

        public static partial void scardotsharp_variant_destroy(ref scardot_variant p_self);

        public static partial void scardotsharp_string_destroy(ref scardot_string p_self);

        public static partial void scardotsharp_string_name_destroy(ref scardot_string_name p_self);

        public static partial void scardotsharp_node_path_destroy(ref scardot_node_path p_self);

        public static partial void scardotsharp_signal_destroy(ref scardot_signal p_self);

        public static partial void scardotsharp_callable_destroy(ref scardot_callable p_self);

        public static partial void scardotsharp_array_destroy(ref scardot_array p_self);

        public static partial void scardotsharp_dictionary_destroy(ref scardot_dictionary p_self);

        // Array

        public static partial int scardotsharp_array_add(ref scardot_array p_self, in scardot_variant p_item);

        public static partial int scardotsharp_array_add_range(ref scardot_array p_self, in scardot_array p_collection);

        public static partial int scardotsharp_array_binary_search(ref scardot_array p_self, int p_index, int p_count, in scardot_variant p_value);

        public static partial void
            scardotsharp_array_duplicate(ref scardot_array p_self, scardot_bool p_deep, out scardot_array r_dest);

        public static partial void scardotsharp_array_fill(ref scardot_array p_self, in scardot_variant p_value);

        public static partial int scardotsharp_array_index_of(ref scardot_array p_self, in scardot_variant p_item, int p_index = 0);

        public static partial void scardotsharp_array_insert(ref scardot_array p_self, int p_index, in scardot_variant p_item);

        public static partial int scardotsharp_array_last_index_of(ref scardot_array p_self, in scardot_variant p_item, int p_index);

        public static partial void scardotsharp_array_make_read_only(ref scardot_array p_self);

        public static partial void scardotsharp_array_max(ref scardot_array p_self, out scardot_variant r_value);

        public static partial void scardotsharp_array_min(ref scardot_array p_self, out scardot_variant r_value);

        public static partial void scardotsharp_array_pick_random(ref scardot_array p_self, out scardot_variant r_value);

        public static partial scardot_bool scardotsharp_array_recursive_equal(ref scardot_array p_self, in scardot_array p_other);

        public static partial void scardotsharp_array_remove_at(ref scardot_array p_self, int p_index);

        public static partial Error scardotsharp_array_resize(ref scardot_array p_self, int p_new_size);

        public static partial void scardotsharp_array_reverse(ref scardot_array p_self);

        public static partial void scardotsharp_array_shuffle(ref scardot_array p_self);

        public static partial void scardotsharp_array_slice(ref scardot_array p_self, int p_start, int p_end,
            int p_step, scardot_bool p_deep, out scardot_array r_dest);

        public static partial void scardotsharp_array_sort(ref scardot_array p_self);

        public static partial void scardotsharp_array_to_string(ref scardot_array p_self, out scardot_string r_str);

        // Dictionary

        public static partial scardot_bool scardotsharp_dictionary_try_get_value(ref scardot_dictionary p_self,
            in scardot_variant p_key,
            out scardot_variant r_value);

        public static partial void scardotsharp_dictionary_set_value(ref scardot_dictionary p_self, in scardot_variant p_key,
            in scardot_variant p_value);

        public static partial void scardotsharp_dictionary_keys(ref scardot_dictionary p_self, out scardot_array r_dest);

        public static partial void scardotsharp_dictionary_values(ref scardot_dictionary p_self, out scardot_array r_dest);

        public static partial int scardotsharp_dictionary_count(ref scardot_dictionary p_self);

        public static partial void scardotsharp_dictionary_key_value_pair_at(ref scardot_dictionary p_self, int p_index,
            out scardot_variant r_key, out scardot_variant r_value);

        public static partial void scardotsharp_dictionary_add(ref scardot_dictionary p_self, in scardot_variant p_key,
            in scardot_variant p_value);

        public static partial void scardotsharp_dictionary_clear(ref scardot_dictionary p_self);

        public static partial scardot_bool scardotsharp_dictionary_contains_key(ref scardot_dictionary p_self,
            in scardot_variant p_key);

        public static partial void scardotsharp_dictionary_duplicate(ref scardot_dictionary p_self, scardot_bool p_deep,
            out scardot_dictionary r_dest);

        public static partial void scardotsharp_dictionary_merge(ref scardot_dictionary p_self, in scardot_dictionary p_dictionary, scardot_bool p_overwrite);

        public static partial scardot_bool scardotsharp_dictionary_recursive_equal(ref scardot_dictionary p_self, in scardot_dictionary p_other);

        public static partial scardot_bool scardotsharp_dictionary_remove_key(ref scardot_dictionary p_self,
            in scardot_variant p_key);

        public static partial void scardotsharp_dictionary_make_read_only(ref scardot_dictionary p_self);

        public static partial void scardotsharp_dictionary_to_string(ref scardot_dictionary p_self, out scardot_string r_str);

        // StringExtensions

        public static partial void scardotsharp_string_simplify_path(in scardot_string p_self,
            out scardot_string r_simplified_path);

        public static partial void scardotsharp_string_to_camel_case(in scardot_string p_self,
            out scardot_string r_camel_case);

        public static partial void scardotsharp_string_to_pascal_case(in scardot_string p_self,
            out scardot_string r_pascal_case);

        public static partial void scardotsharp_string_to_snake_case(in scardot_string p_self,
            out scardot_string r_snake_case);

        // NodePath

        public static partial void scardotsharp_node_path_get_as_property_path(in scardot_node_path p_self,
            ref scardot_node_path r_dest);

        public static partial void scardotsharp_node_path_get_concatenated_names(in scardot_node_path p_self,
            out scardot_string r_names);

        public static partial void scardotsharp_node_path_get_concatenated_subnames(in scardot_node_path p_self,
            out scardot_string r_subnames);

        public static partial void scardotsharp_node_path_get_name(in scardot_node_path p_self, int p_idx,
            out scardot_string r_name);

        public static partial int scardotsharp_node_path_get_name_count(in scardot_node_path p_self);

        public static partial void scardotsharp_node_path_get_subname(in scardot_node_path p_self, int p_idx,
            out scardot_string r_subname);

        public static partial int scardotsharp_node_path_get_subname_count(in scardot_node_path p_self);

        public static partial scardot_bool scardotsharp_node_path_is_absolute(in scardot_node_path p_self);

        public static partial scardot_bool scardotsharp_node_path_equals(in scardot_node_path p_self, in scardot_node_path p_other);

        public static partial int scardotsharp_node_path_hash(in scardot_node_path p_self);

        // GD, etc

        internal static partial void scardotsharp_bytes_to_var(in scardot_packed_byte_array p_bytes,
            scardot_bool p_allow_objects,
            out scardot_variant r_ret);

        internal static partial void scardotsharp_convert(in scardot_variant p_what, int p_type,
            out scardot_variant r_ret);

        internal static partial int scardotsharp_hash(in scardot_variant p_var);

        internal static partial IntPtr scardotsharp_instance_from_id(ulong p_instance_id);

        internal static partial void scardotsharp_print(in scardot_string p_what);

        public static partial void scardotsharp_print_rich(in scardot_string p_what);

        internal static partial void scardotsharp_printerr(in scardot_string p_what);

        internal static partial void scardotsharp_printraw(in scardot_string p_what);

        internal static partial void scardotsharp_prints(in scardot_string p_what);

        internal static partial void scardotsharp_printt(in scardot_string p_what);

        internal static partial float scardotsharp_randf();

        internal static partial uint scardotsharp_randi();

        internal static partial void scardotsharp_randomize();

        internal static partial double scardotsharp_randf_range(double from, double to);

        internal static partial double scardotsharp_randfn(double mean, double deviation);

        internal static partial int scardotsharp_randi_range(int from, int to);

        internal static partial uint scardotsharp_rand_from_seed(ulong seed, out ulong newSeed);

        internal static partial void scardotsharp_seed(ulong seed);

        internal static partial void scardotsharp_weakref(IntPtr p_obj, out scardot_ref r_weak_ref);

        internal static partial void scardotsharp_str_to_var(in scardot_string p_str, out scardot_variant r_ret);

        internal static partial void scardotsharp_var_to_bytes(in scardot_variant p_what, scardot_bool p_full_objects,
            out scardot_packed_byte_array r_bytes);

        internal static partial void scardotsharp_var_to_str(in scardot_variant p_var, out scardot_string r_ret);

        internal static partial void scardotsharp_err_print_error(in scardot_string p_function, in scardot_string p_file, int p_line, in scardot_string p_error, in scardot_string p_message = default, scardot_bool p_editor_notify = scardot_bool.False, scardot_error_handler_type p_type = scardot_error_handler_type.ERR_HANDLER_ERROR);

        // Object

        public static partial void scardotsharp_object_to_string(IntPtr ptr, out scardot_string r_str);
    }
}
