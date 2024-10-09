partial class ExportedFields
{
#pragma warning disable CS0109 // Disable warning about redundant 'new' keyword
#if TOOLS
    /// <summary>
    /// Get the default values for all properties declared in this class.
    /// This method is used by scardot to determine the value that will be
    /// used by the inspector when resetting properties.
    /// Do not call this method.
    /// </summary>
    [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
    internal new static global::System.Collections.Generic.Dictionary<global::scardot.StringName, global::scardot.Variant> GetscardotPropertyDefaultValues()
    {
        var values = new global::System.Collections.Generic.Dictionary<global::scardot.StringName, global::scardot.Variant>(60);
        bool ___fieldBoolean_default_value = true;
        values.Add(PropertyName.@_fieldBoolean, global::scardot.Variant.From<bool>(___fieldBoolean_default_value));
        char ___fieldChar_default_value = 'f';
        values.Add(PropertyName.@_fieldChar, global::scardot.Variant.From<char>(___fieldChar_default_value));
        sbyte ___fieldSByte_default_value = 10;
        values.Add(PropertyName.@_fieldSByte, global::scardot.Variant.From<sbyte>(___fieldSByte_default_value));
        short ___fieldInt16_default_value = 10;
        values.Add(PropertyName.@_fieldInt16, global::scardot.Variant.From<short>(___fieldInt16_default_value));
        int ___fieldInt32_default_value = 10;
        values.Add(PropertyName.@_fieldInt32, global::scardot.Variant.From<int>(___fieldInt32_default_value));
        long ___fieldInt64_default_value = 10;
        values.Add(PropertyName.@_fieldInt64, global::scardot.Variant.From<long>(___fieldInt64_default_value));
        byte ___fieldByte_default_value = 10;
        values.Add(PropertyName.@_fieldByte, global::scardot.Variant.From<byte>(___fieldByte_default_value));
        ushort ___fieldUInt16_default_value = 10;
        values.Add(PropertyName.@_fieldUInt16, global::scardot.Variant.From<ushort>(___fieldUInt16_default_value));
        uint ___fieldUInt32_default_value = 10;
        values.Add(PropertyName.@_fieldUInt32, global::scardot.Variant.From<uint>(___fieldUInt32_default_value));
        ulong ___fieldUInt64_default_value = 10;
        values.Add(PropertyName.@_fieldUInt64, global::scardot.Variant.From<ulong>(___fieldUInt64_default_value));
        float ___fieldSingle_default_value = 10;
        values.Add(PropertyName.@_fieldSingle, global::scardot.Variant.From<float>(___fieldSingle_default_value));
        double ___fieldDouble_default_value = 10;
        values.Add(PropertyName.@_fieldDouble, global::scardot.Variant.From<double>(___fieldDouble_default_value));
        string ___fieldString_default_value = "foo";
        values.Add(PropertyName.@_fieldString, global::scardot.Variant.From<string>(___fieldString_default_value));
        global::scardot.Vector2 ___fieldVector2_default_value = new(10f, 10f);
        values.Add(PropertyName.@_fieldVector2, global::scardot.Variant.From<global::scardot.Vector2>(___fieldVector2_default_value));
        global::scardot.Vector2I ___fieldVector2I_default_value = global::scardot.Vector2I.Up;
        values.Add(PropertyName.@_fieldVector2I, global::scardot.Variant.From<global::scardot.Vector2I>(___fieldVector2I_default_value));
        global::scardot.Rect2 ___fieldRect2_default_value = new(new global::scardot.Vector2(10f, 10f), new global::scardot.Vector2(10f, 10f));
        values.Add(PropertyName.@_fieldRect2, global::scardot.Variant.From<global::scardot.Rect2>(___fieldRect2_default_value));
        global::scardot.Rect2I ___fieldRect2I_default_value = new(new global::scardot.Vector2I(10, 10), new global::scardot.Vector2I(10, 10));
        values.Add(PropertyName.@_fieldRect2I, global::scardot.Variant.From<global::scardot.Rect2I>(___fieldRect2I_default_value));
        global::scardot.Transform2D ___fieldTransform2D_default_value = global::scardot.Transform2D.Identity;
        values.Add(PropertyName.@_fieldTransform2D, global::scardot.Variant.From<global::scardot.Transform2D>(___fieldTransform2D_default_value));
        global::scardot.Vector3 ___fieldVector3_default_value = new(10f, 10f, 10f);
        values.Add(PropertyName.@_fieldVector3, global::scardot.Variant.From<global::scardot.Vector3>(___fieldVector3_default_value));
        global::scardot.Vector3I ___fieldVector3I_default_value = global::scardot.Vector3I.Back;
        values.Add(PropertyName.@_fieldVector3I, global::scardot.Variant.From<global::scardot.Vector3I>(___fieldVector3I_default_value));
        global::scardot.Basis ___fieldBasis_default_value = new global::scardot.Basis(global::scardot.Quaternion.Identity);
        values.Add(PropertyName.@_fieldBasis, global::scardot.Variant.From<global::scardot.Basis>(___fieldBasis_default_value));
        global::scardot.Quaternion ___fieldQuaternion_default_value = new global::scardot.Quaternion(global::scardot.Basis.Identity);
        values.Add(PropertyName.@_fieldQuaternion, global::scardot.Variant.From<global::scardot.Quaternion>(___fieldQuaternion_default_value));
        global::scardot.Transform3D ___fieldTransform3D_default_value = global::scardot.Transform3D.Identity;
        values.Add(PropertyName.@_fieldTransform3D, global::scardot.Variant.From<global::scardot.Transform3D>(___fieldTransform3D_default_value));
        global::scardot.Vector4 ___fieldVector4_default_value = new(10f, 10f, 10f, 10f);
        values.Add(PropertyName.@_fieldVector4, global::scardot.Variant.From<global::scardot.Vector4>(___fieldVector4_default_value));
        global::scardot.Vector4I ___fieldVector4I_default_value = global::scardot.Vector4I.One;
        values.Add(PropertyName.@_fieldVector4I, global::scardot.Variant.From<global::scardot.Vector4I>(___fieldVector4I_default_value));
        global::scardot.Projection ___fieldProjection_default_value = global::scardot.Projection.Identity;
        values.Add(PropertyName.@_fieldProjection, global::scardot.Variant.From<global::scardot.Projection>(___fieldProjection_default_value));
        global::scardot.Aabb ___fieldAabb_default_value = new global::scardot.Aabb(10f, 10f, 10f, new global::scardot.Vector3(1f, 1f, 1f));
        values.Add(PropertyName.@_fieldAabb, global::scardot.Variant.From<global::scardot.Aabb>(___fieldAabb_default_value));
        global::scardot.Color ___fieldColor_default_value = global::scardot.Colors.Aquamarine;
        values.Add(PropertyName.@_fieldColor, global::scardot.Variant.From<global::scardot.Color>(___fieldColor_default_value));
        global::scardot.Plane ___fieldPlane_default_value = global::scardot.Plane.PlaneXZ;
        values.Add(PropertyName.@_fieldPlane, global::scardot.Variant.From<global::scardot.Plane>(___fieldPlane_default_value));
        global::scardot.Callable ___fieldCallable_default_value = new global::scardot.Callable(global::scardot.Engine.GetMainLoop(), "_process");
        values.Add(PropertyName.@_fieldCallable, global::scardot.Variant.From<global::scardot.Callable>(___fieldCallable_default_value));
        global::scardot.Signal ___fieldSignal_default_value = new global::scardot.Signal(global::scardot.Engine.GetMainLoop(), "property_list_changed");
        values.Add(PropertyName.@_fieldSignal, global::scardot.Variant.From<global::scardot.Signal>(___fieldSignal_default_value));
        global::ExportedFields.MyEnum ___fieldEnum_default_value = global::ExportedFields.MyEnum.C;
        values.Add(PropertyName.@_fieldEnum, global::scardot.Variant.From<global::ExportedFields.MyEnum>(___fieldEnum_default_value));
        global::ExportedFields.MyFlagsEnum ___fieldFlagsEnum_default_value = global::ExportedFields.MyFlagsEnum.C;
        values.Add(PropertyName.@_fieldFlagsEnum, global::scardot.Variant.From<global::ExportedFields.MyFlagsEnum>(___fieldFlagsEnum_default_value));
        byte[] ___fieldByteArray_default_value = { 0, 1, 2, 3, 4, 5, 6  };
        values.Add(PropertyName.@_fieldByteArray, global::scardot.Variant.From<byte[]>(___fieldByteArray_default_value));
        int[] ___fieldInt32Array_default_value = { 0, 1, 2, 3, 4, 5, 6  };
        values.Add(PropertyName.@_fieldInt32Array, global::scardot.Variant.From<int[]>(___fieldInt32Array_default_value));
        long[] ___fieldInt64Array_default_value = { 0, 1, 2, 3, 4, 5, 6  };
        values.Add(PropertyName.@_fieldInt64Array, global::scardot.Variant.From<long[]>(___fieldInt64Array_default_value));
        float[] ___fieldSingleArray_default_value = { 0f, 1f, 2f, 3f, 4f, 5f, 6f  };
        values.Add(PropertyName.@_fieldSingleArray, global::scardot.Variant.From<float[]>(___fieldSingleArray_default_value));
        double[] ___fieldDoubleArray_default_value = { 0d, 1d, 2d, 3d, 4d, 5d, 6d  };
        values.Add(PropertyName.@_fieldDoubleArray, global::scardot.Variant.From<double[]>(___fieldDoubleArray_default_value));
        string[] ___fieldStringArray_default_value = { "foo", "bar"  };
        values.Add(PropertyName.@_fieldStringArray, global::scardot.Variant.From<string[]>(___fieldStringArray_default_value));
        string[] ___fieldStringArrayEnum_default_value = { "foo", "bar"  };
        values.Add(PropertyName.@_fieldStringArrayEnum, global::scardot.Variant.From<string[]>(___fieldStringArrayEnum_default_value));
        global::scardot.Vector2[] ___fieldVector2Array_default_value = { global::scardot.Vector2.Up, global::scardot.Vector2.Down, global::scardot.Vector2.Left, global::scardot.Vector2.Right   };
        values.Add(PropertyName.@_fieldVector2Array, global::scardot.Variant.From<global::scardot.Vector2[]>(___fieldVector2Array_default_value));
        global::scardot.Vector3[] ___fieldVector3Array_default_value = { global::scardot.Vector3.Up, global::scardot.Vector3.Down, global::scardot.Vector3.Left, global::scardot.Vector3.Right   };
        values.Add(PropertyName.@_fieldVector3Array, global::scardot.Variant.From<global::scardot.Vector3[]>(___fieldVector3Array_default_value));
        global::scardot.Color[] ___fieldColorArray_default_value = { global::scardot.Colors.Aqua, global::scardot.Colors.Aquamarine, global::scardot.Colors.Azure, global::scardot.Colors.Beige   };
        values.Add(PropertyName.@_fieldColorArray, global::scardot.Variant.From<global::scardot.Color[]>(___fieldColorArray_default_value));
        global::scardot.scardotObject[] ___fieldscardotObjectOrDerivedArray_default_value = { null  };
        values.Add(PropertyName.@_fieldscardotObjectOrDerivedArray, global::scardot.Variant.CreateFrom(___fieldscardotObjectOrDerivedArray_default_value));
        global::scardot.StringName[] ___fieldStringNameArray_default_value = { "foo", "bar"  };
        values.Add(PropertyName.@_fieldStringNameArray, global::scardot.Variant.From<global::scardot.StringName[]>(___fieldStringNameArray_default_value));
        global::scardot.NodePath[] ___fieldNodePathArray_default_value = { "foo", "bar"  };
        values.Add(PropertyName.@_fieldNodePathArray, global::scardot.Variant.From<global::scardot.NodePath[]>(___fieldNodePathArray_default_value));
        global::scardot.Rid[] ___fieldRidArray_default_value = { default, default, default  };
        values.Add(PropertyName.@_fieldRidArray, global::scardot.Variant.From<global::scardot.Rid[]>(___fieldRidArray_default_value));
        int[] ___fieldEmptyInt32Array_default_value = global::System.Array.Empty<int>();
        values.Add(PropertyName.@_fieldEmptyInt32Array, global::scardot.Variant.From<int[]>(___fieldEmptyInt32Array_default_value));
        int[] ___fieldArrayFromList_default_value = new global::System.Collections.Generic.List<int>(global::System.Array.Empty<int>()).ToArray();
        values.Add(PropertyName.@_fieldArrayFromList, global::scardot.Variant.From<int[]>(___fieldArrayFromList_default_value));
        global::scardot.Variant ___fieldVariant_default_value = "foo";
        values.Add(PropertyName.@_fieldVariant, global::scardot.Variant.From<global::scardot.Variant>(___fieldVariant_default_value));
        global::scardot.scardotObject ___fieldscardotObjectOrDerived_default_value = default;
        values.Add(PropertyName.@_fieldscardotObjectOrDerived, global::scardot.Variant.From<global::scardot.scardotObject>(___fieldscardotObjectOrDerived_default_value));
        global::scardot.Texture ___fieldscardotResourceTexture_default_value = default;
        values.Add(PropertyName.@_fieldscardotResourceTexture, global::scardot.Variant.From<global::scardot.Texture>(___fieldscardotResourceTexture_default_value));
        global::scardot.StringName ___fieldStringName_default_value = new global::scardot.StringName("foo");
        values.Add(PropertyName.@_fieldStringName, global::scardot.Variant.From<global::scardot.StringName>(___fieldStringName_default_value));
        global::scardot.NodePath ___fieldNodePath_default_value = new global::scardot.NodePath("foo");
        values.Add(PropertyName.@_fieldNodePath, global::scardot.Variant.From<global::scardot.NodePath>(___fieldNodePath_default_value));
        global::scardot.Rid ___fieldRid_default_value = default;
        values.Add(PropertyName.@_fieldRid, global::scardot.Variant.From<global::scardot.Rid>(___fieldRid_default_value));
        global::scardot.Collections.Dictionary ___fieldscardotDictionary_default_value = new()  { { "foo", 10  }, { global::scardot.Vector2.Up, global::scardot.Colors.Chocolate   }  };
        values.Add(PropertyName.@_fieldscardotDictionary, global::scardot.Variant.From<global::scardot.Collections.Dictionary>(___fieldscardotDictionary_default_value));
        global::scardot.Collections.Array ___fieldscardotArray_default_value = new()  { "foo", 10, global::scardot.Vector2.Up, global::scardot.Colors.Chocolate   };
        values.Add(PropertyName.@_fieldscardotArray, global::scardot.Variant.From<global::scardot.Collections.Array>(___fieldscardotArray_default_value));
        global::scardot.Collections.Dictionary<string, bool> ___fieldscardotGenericDictionary_default_value = new()  { { "foo", true  }, { "bar", false  }  };
        values.Add(PropertyName.@_fieldscardotGenericDictionary, global::scardot.Variant.CreateFrom(___fieldscardotGenericDictionary_default_value));
        global::scardot.Collections.Array<int> ___fieldscardotGenericArray_default_value = new()  { 0, 1, 2, 3, 4, 5, 6  };
        values.Add(PropertyName.@_fieldscardotGenericArray, global::scardot.Variant.CreateFrom(___fieldscardotGenericArray_default_value));
        long[] ___fieldEmptyInt64Array_default_value = global::System.Array.Empty<long>();
        values.Add(PropertyName.@_fieldEmptyInt64Array, global::scardot.Variant.From<long[]>(___fieldEmptyInt64Array_default_value));
        return values;
    }
#endif // TOOLS
#pragma warning restore CS0109
}
