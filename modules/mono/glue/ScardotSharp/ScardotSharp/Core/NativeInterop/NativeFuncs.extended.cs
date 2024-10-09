#pragma warning disable CA1707 // Identifiers should not contain underscores
#pragma warning disable IDE1006 // Naming rule violation
// ReSharper disable InconsistentNaming

namespace scardot.NativeInterop
{
    public static partial class NativeFuncs
    {
        public static scardot_variant scardotsharp_variant_new_copy(in scardot_variant src)
        {
            switch (src.Type)
            {
                case Variant.Type.Nil:
                    return default;
                case Variant.Type.Bool:
                    return new scardot_variant() { Bool = src.Bool, Type = Variant.Type.Bool };
                case Variant.Type.Int:
                    return new scardot_variant() { Int = src.Int, Type = Variant.Type.Int };
                case Variant.Type.Float:
                    return new scardot_variant() { Float = src.Float, Type = Variant.Type.Float };
                case Variant.Type.Vector2:
                    return new scardot_variant() { Vector2 = src.Vector2, Type = Variant.Type.Vector2 };
                case Variant.Type.Vector2I:
                    return new scardot_variant() { Vector2I = src.Vector2I, Type = Variant.Type.Vector2I };
                case Variant.Type.Rect2:
                    return new scardot_variant() { Rect2 = src.Rect2, Type = Variant.Type.Rect2 };
                case Variant.Type.Rect2I:
                    return new scardot_variant() { Rect2I = src.Rect2I, Type = Variant.Type.Rect2I };
                case Variant.Type.Vector3:
                    return new scardot_variant() { Vector3 = src.Vector3, Type = Variant.Type.Vector3 };
                case Variant.Type.Vector3I:
                    return new scardot_variant() { Vector3I = src.Vector3I, Type = Variant.Type.Vector3I };
                case Variant.Type.Vector4:
                    return new scardot_variant() { Vector4 = src.Vector4, Type = Variant.Type.Vector4 };
                case Variant.Type.Vector4I:
                    return new scardot_variant() { Vector4I = src.Vector4I, Type = Variant.Type.Vector4I };
                case Variant.Type.Plane:
                    return new scardot_variant() { Plane = src.Plane, Type = Variant.Type.Plane };
                case Variant.Type.Quaternion:
                    return new scardot_variant() { Quaternion = src.Quaternion, Type = Variant.Type.Quaternion };
                case Variant.Type.Color:
                    return new scardot_variant() { Color = src.Color, Type = Variant.Type.Color };
                case Variant.Type.Rid:
                    return new scardot_variant() { Rid = src.Rid, Type = Variant.Type.Rid };
            }

            scardotsharp_variant_new_copy(out scardot_variant ret, src);
            return ret;
        }

        public static scardot_string_name scardotsharp_string_name_new_copy(in scardot_string_name src)
        {
            if (src.IsEmpty)
                return default;
            scardotsharp_string_name_new_copy(out scardot_string_name ret, src);
            return ret;
        }

        public static scardot_node_path scardotsharp_node_path_new_copy(in scardot_node_path src)
        {
            if (src.IsEmpty)
                return default;
            scardotsharp_node_path_new_copy(out scardot_node_path ret, src);
            return ret;
        }

        public static scardot_array scardotsharp_array_new()
        {
            scardotsharp_array_new(out scardot_array ret);
            return ret;
        }

        public static scardot_array scardotsharp_array_new_copy(in scardot_array src)
        {
            scardotsharp_array_new_copy(out scardot_array ret, src);
            return ret;
        }

        public static scardot_dictionary scardotsharp_dictionary_new()
        {
            scardotsharp_dictionary_new(out scardot_dictionary ret);
            return ret;
        }

        public static scardot_dictionary scardotsharp_dictionary_new_copy(in scardot_dictionary src)
        {
            scardotsharp_dictionary_new_copy(out scardot_dictionary ret, src);
            return ret;
        }

        public static scardot_string_name scardotsharp_string_name_new_from_string(string name)
        {
            using scardot_string src = Marshaling.ConvertStringToNative(name);
            scardotsharp_string_name_new_from_string(out scardot_string_name ret, src);
            return ret;
        }

        public static scardot_node_path scardotsharp_node_path_new_from_string(string name)
        {
            using scardot_string src = Marshaling.ConvertStringToNative(name);
            scardotsharp_node_path_new_from_string(out scardot_node_path ret, src);
            return ret;
        }
    }
}
