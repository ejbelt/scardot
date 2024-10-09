partial class ExportedProperties
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
        var values = new global::System.Collections.Generic.Dictionary<global::scardot.StringName, global::scardot.Variant>(64);
        string __NotGenerateComplexLamdaProperty_default_value = default;
        values.Add(PropertyName.@NotGenerateComplexLamdaProperty, global::scardot.Variant.From<string>(__NotGenerateComplexLamdaProperty_default_value));
        string __NotGenerateLamdaNoFieldProperty_default_value = default;
        values.Add(PropertyName.@NotGenerateLamdaNoFieldProperty, global::scardot.Variant.From<string>(__NotGenerateLamdaNoFieldProperty_default_value));
        string __NotGenerateComplexReturnProperty_default_value = default;
        values.Add(PropertyName.@NotGenerateComplexReturnProperty, global::scardot.Variant.From<string>(__NotGenerateComplexReturnProperty_default_value));
        string __NotGenerateReturnsProperty_default_value = default;
        values.Add(PropertyName.@NotGenerateReturnsProperty, global::scardot.Variant.From<string>(__NotGenerateReturnsProperty_default_value));
        string __FullPropertyString_default_value = "FullPropertyString";
        values.Add(PropertyName.@FullPropertyString, global::scardot.Variant.From<string>(__FullPropertyString_default_value));
        string __FullPropertyString_Complex_default_value = new string("FullPropertyString_Complex")   + global::System.Convert.ToInt32("1");
        values.Add(PropertyName.@FullPropertyString_Complex, global::scardot.Variant.From<string>(__FullPropertyString_Complex_default_value));
        string __LamdaPropertyString_default_value = "LamdaPropertyString";
        values.Add(PropertyName.@LamdaPropertyString, global::scardot.Variant.From<string>(__LamdaPropertyString_default_value));
        bool __PropertyBoolean_default_value = true;
        values.Add(PropertyName.@PropertyBoolean, global::scardot.Variant.From<bool>(__PropertyBoolean_default_value));
        char __PropertyChar_default_value = 'f';
        values.Add(PropertyName.@PropertyChar, global::scardot.Variant.From<char>(__PropertyChar_default_value));
        sbyte __PropertySByte_default_value = 10;
        values.Add(PropertyName.@PropertySByte, global::scardot.Variant.From<sbyte>(__PropertySByte_default_value));
        short __PropertyInt16_default_value = 10;
        values.Add(PropertyName.@PropertyInt16, global::scardot.Variant.From<short>(__PropertyInt16_default_value));
        int __PropertyInt32_default_value = 10;
        values.Add(PropertyName.@PropertyInt32, global::scardot.Variant.From<int>(__PropertyInt32_default_value));
        long __PropertyInt64_default_value = 10;
        values.Add(PropertyName.@PropertyInt64, global::scardot.Variant.From<long>(__PropertyInt64_default_value));
        byte __PropertyByte_default_value = 10;
        values.Add(PropertyName.@PropertyByte, global::scardot.Variant.From<byte>(__PropertyByte_default_value));
        ushort __PropertyUInt16_default_value = 10;
        values.Add(PropertyName.@PropertyUInt16, global::scardot.Variant.From<ushort>(__PropertyUInt16_default_value));
        uint __PropertyUInt32_default_value = 10;
        values.Add(PropertyName.@PropertyUInt32, global::scardot.Variant.From<uint>(__PropertyUInt32_default_value));
        ulong __PropertyUInt64_default_value = 10;
        values.Add(PropertyName.@PropertyUInt64, global::scardot.Variant.From<ulong>(__PropertyUInt64_default_value));
        float __PropertySingle_default_value = 10;
        values.Add(PropertyName.@PropertySingle, global::scardot.Variant.From<float>(__PropertySingle_default_value));
        double __PropertyDouble_default_value = 10;
        values.Add(PropertyName.@PropertyDouble, global::scardot.Variant.From<double>(__PropertyDouble_default_value));
        string __PropertyString_default_value = "foo";
        values.Add(PropertyName.@PropertyString, global::scardot.Variant.From<string>(__PropertyString_default_value));
        global::scardot.Vector2 __PropertyVector2_default_value = new(10f, 10f);
        values.Add(PropertyName.@PropertyVector2, global::scardot.Variant.From<global::scardot.Vector2>(__PropertyVector2_default_value));
        global::scardot.Vector2I __PropertyVector2I_default_value = global::scardot.Vector2I.Up;
        values.Add(PropertyName.@PropertyVector2I, global::scardot.Variant.From<global::scardot.Vector2I>(__PropertyVector2I_default_value));
        global::scardot.Rect2 __PropertyRect2_default_value = new(new global::scardot.Vector2(10f, 10f), new global::scardot.Vector2(10f, 10f));
        values.Add(PropertyName.@PropertyRect2, global::scardot.Variant.From<global::scardot.Rect2>(__PropertyRect2_default_value));
        global::scardot.Rect2I __PropertyRect2I_default_value = new(new global::scardot.Vector2I(10, 10), new global::scardot.Vector2I(10, 10));
        values.Add(PropertyName.@PropertyRect2I, global::scardot.Variant.From<global::scardot.Rect2I>(__PropertyRect2I_default_value));
        global::scardot.Transform2D __PropertyTransform2D_default_value = global::scardot.Transform2D.Identity;
        values.Add(PropertyName.@PropertyTransform2D, global::scardot.Variant.From<global::scardot.Transform2D>(__PropertyTransform2D_default_value));
        global::scardot.Vector3 __PropertyVector3_default_value = new(10f, 10f, 10f);
        values.Add(PropertyName.@PropertyVector3, global::scardot.Variant.From<global::scardot.Vector3>(__PropertyVector3_default_value));
        global::scardot.Vector3I __PropertyVector3I_default_value = global::scardot.Vector3I.Back;
        values.Add(PropertyName.@PropertyVector3I, global::scardot.Variant.From<global::scardot.Vector3I>(__PropertyVector3I_default_value));
        global::scardot.Basis __PropertyBasis_default_value = new global::scardot.Basis(global::scardot.Quaternion.Identity);
        values.Add(PropertyName.@PropertyBasis, global::scardot.Variant.From<global::scardot.Basis>(__PropertyBasis_default_value));
        global::scardot.Quaternion __PropertyQuaternion_default_value = new global::scardot.Quaternion(global::scardot.Basis.Identity);
        values.Add(PropertyName.@PropertyQuaternion, global::scardot.Variant.From<global::scardot.Quaternion>(__PropertyQuaternion_default_value));
        global::scardot.Transform3D __PropertyTransform3D_default_value = global::scardot.Transform3D.Identity;
        values.Add(PropertyName.@PropertyTransform3D, global::scardot.Variant.From<global::scardot.Transform3D>(__PropertyTransform3D_default_value));
        global::scardot.Vector4 __PropertyVector4_default_value = new(10f, 10f, 10f, 10f);
        values.Add(PropertyName.@PropertyVector4, global::scardot.Variant.From<global::scardot.Vector4>(__PropertyVector4_default_value));
        global::scardot.Vector4I __PropertyVector4I_default_value = global::scardot.Vector4I.One;
        values.Add(PropertyName.@PropertyVector4I, global::scardot.Variant.From<global::scardot.Vector4I>(__PropertyVector4I_default_value));
        global::scardot.Projection __PropertyProjection_default_value = global::scardot.Projection.Identity;
        values.Add(PropertyName.@PropertyProjection, global::scardot.Variant.From<global::scardot.Projection>(__PropertyProjection_default_value));
        global::scardot.Aabb __PropertyAabb_default_value = new global::scardot.Aabb(10f, 10f, 10f, new global::scardot.Vector3(1f, 1f, 1f));
        values.Add(PropertyName.@PropertyAabb, global::scardot.Variant.From<global::scardot.Aabb>(__PropertyAabb_default_value));
        global::scardot.Color __PropertyColor_default_value = global::scardot.Colors.Aquamarine;
        values.Add(PropertyName.@PropertyColor, global::scardot.Variant.From<global::scardot.Color>(__PropertyColor_default_value));
        global::scardot.Plane __PropertyPlane_default_value = global::scardot.Plane.PlaneXZ;
        values.Add(PropertyName.@PropertyPlane, global::scardot.Variant.From<global::scardot.Plane>(__PropertyPlane_default_value));
        global::scardot.Callable __PropertyCallable_default_value = new global::scardot.Callable(global::scardot.Engine.GetMainLoop(), "_process");
        values.Add(PropertyName.@PropertyCallable, global::scardot.Variant.From<global::scardot.Callable>(__PropertyCallable_default_value));
        global::scardot.Signal __PropertySignal_default_value = new global::scardot.Signal(global::scardot.Engine.GetMainLoop(), "Propertylist_changed");
        values.Add(PropertyName.@PropertySignal, global::scardot.Variant.From<global::scardot.Signal>(__PropertySignal_default_value));
        global::ExportedProperties.MyEnum __PropertyEnum_default_value = global::ExportedProperties.MyEnum.C;
        values.Add(PropertyName.@PropertyEnum, global::scardot.Variant.From<global::ExportedProperties.MyEnum>(__PropertyEnum_default_value));
        global::ExportedProperties.MyFlagsEnum __PropertyFlagsEnum_default_value = global::ExportedProperties.MyFlagsEnum.C;
        values.Add(PropertyName.@PropertyFlagsEnum, global::scardot.Variant.From<global::ExportedProperties.MyFlagsEnum>(__PropertyFlagsEnum_default_value));
        byte[] __PropertyByteArray_default_value = { 0, 1, 2, 3, 4, 5, 6  };
        values.Add(PropertyName.@PropertyByteArray, global::scardot.Variant.From<byte[]>(__PropertyByteArray_default_value));
        int[] __PropertyInt32Array_default_value = { 0, 1, 2, 3, 4, 5, 6  };
        values.Add(PropertyName.@PropertyInt32Array, global::scardot.Variant.From<int[]>(__PropertyInt32Array_default_value));
        long[] __PropertyInt64Array_default_value = { 0, 1, 2, 3, 4, 5, 6  };
        values.Add(PropertyName.@PropertyInt64Array, global::scardot.Variant.From<long[]>(__PropertyInt64Array_default_value));
        float[] __PropertySingleArray_default_value = { 0f, 1f, 2f, 3f, 4f, 5f, 6f  };
        values.Add(PropertyName.@PropertySingleArray, global::scardot.Variant.From<float[]>(__PropertySingleArray_default_value));
        double[] __PropertyDoubleArray_default_value = { 0d, 1d, 2d, 3d, 4d, 5d, 6d  };
        values.Add(PropertyName.@PropertyDoubleArray, global::scardot.Variant.From<double[]>(__PropertyDoubleArray_default_value));
        string[] __PropertyStringArray_default_value = { "foo", "bar"  };
        values.Add(PropertyName.@PropertyStringArray, global::scardot.Variant.From<string[]>(__PropertyStringArray_default_value));
        string[] __PropertyStringArrayEnum_default_value = { "foo", "bar"  };
        values.Add(PropertyName.@PropertyStringArrayEnum, global::scardot.Variant.From<string[]>(__PropertyStringArrayEnum_default_value));
        global::scardot.Vector2[] __PropertyVector2Array_default_value = { global::scardot.Vector2.Up, global::scardot.Vector2.Down, global::scardot.Vector2.Left, global::scardot.Vector2.Right   };
        values.Add(PropertyName.@PropertyVector2Array, global::scardot.Variant.From<global::scardot.Vector2[]>(__PropertyVector2Array_default_value));
        global::scardot.Vector3[] __PropertyVector3Array_default_value = { global::scardot.Vector3.Up, global::scardot.Vector3.Down, global::scardot.Vector3.Left, global::scardot.Vector3.Right   };
        values.Add(PropertyName.@PropertyVector3Array, global::scardot.Variant.From<global::scardot.Vector3[]>(__PropertyVector3Array_default_value));
        global::scardot.Color[] __PropertyColorArray_default_value = { global::scardot.Colors.Aqua, global::scardot.Colors.Aquamarine, global::scardot.Colors.Azure, global::scardot.Colors.Beige   };
        values.Add(PropertyName.@PropertyColorArray, global::scardot.Variant.From<global::scardot.Color[]>(__PropertyColorArray_default_value));
        global::scardot.scardotObject[] __PropertyscardotObjectOrDerivedArray_default_value = { null  };
        values.Add(PropertyName.@PropertyscardotObjectOrDerivedArray, global::scardot.Variant.CreateFrom(__PropertyscardotObjectOrDerivedArray_default_value));
        global::scardot.StringName[] __field_StringNameArray_default_value = { "foo", "bar"  };
        values.Add(PropertyName.@field_StringNameArray, global::scardot.Variant.From<global::scardot.StringName[]>(__field_StringNameArray_default_value));
        global::scardot.NodePath[] __field_NodePathArray_default_value = { "foo", "bar"  };
        values.Add(PropertyName.@field_NodePathArray, global::scardot.Variant.From<global::scardot.NodePath[]>(__field_NodePathArray_default_value));
        global::scardot.Rid[] __field_RidArray_default_value = { default, default, default  };
        values.Add(PropertyName.@field_RidArray, global::scardot.Variant.From<global::scardot.Rid[]>(__field_RidArray_default_value));
        global::scardot.Variant __PropertyVariant_default_value = "foo";
        values.Add(PropertyName.@PropertyVariant, global::scardot.Variant.From<global::scardot.Variant>(__PropertyVariant_default_value));
        global::scardot.scardotObject __PropertyscardotObjectOrDerived_default_value = default;
        values.Add(PropertyName.@PropertyscardotObjectOrDerived, global::scardot.Variant.From<global::scardot.scardotObject>(__PropertyscardotObjectOrDerived_default_value));
        global::scardot.Texture __PropertyscardotResourceTexture_default_value = default;
        values.Add(PropertyName.@PropertyscardotResourceTexture, global::scardot.Variant.From<global::scardot.Texture>(__PropertyscardotResourceTexture_default_value));
        global::scardot.StringName __PropertyStringName_default_value = new global::scardot.StringName("foo");
        values.Add(PropertyName.@PropertyStringName, global::scardot.Variant.From<global::scardot.StringName>(__PropertyStringName_default_value));
        global::scardot.NodePath __PropertyNodePath_default_value = new global::scardot.NodePath("foo");
        values.Add(PropertyName.@PropertyNodePath, global::scardot.Variant.From<global::scardot.NodePath>(__PropertyNodePath_default_value));
        global::scardot.Rid __PropertyRid_default_value = default;
        values.Add(PropertyName.@PropertyRid, global::scardot.Variant.From<global::scardot.Rid>(__PropertyRid_default_value));
        global::scardot.Collections.Dictionary __PropertyscardotDictionary_default_value = new()  { { "foo", 10  }, { global::scardot.Vector2.Up, global::scardot.Colors.Chocolate   }  };
        values.Add(PropertyName.@PropertyscardotDictionary, global::scardot.Variant.From<global::scardot.Collections.Dictionary>(__PropertyscardotDictionary_default_value));
        global::scardot.Collections.Array __PropertyscardotArray_default_value = new()  { "foo", 10, global::scardot.Vector2.Up, global::scardot.Colors.Chocolate   };
        values.Add(PropertyName.@PropertyscardotArray, global::scardot.Variant.From<global::scardot.Collections.Array>(__PropertyscardotArray_default_value));
        global::scardot.Collections.Dictionary<string, bool> __PropertyscardotGenericDictionary_default_value = new()  { { "foo", true  }, { "bar", false  }  };
        values.Add(PropertyName.@PropertyscardotGenericDictionary, global::scardot.Variant.CreateFrom(__PropertyscardotGenericDictionary_default_value));
        global::scardot.Collections.Array<int> __PropertyscardotGenericArray_default_value = new()  { 0, 1, 2, 3, 4, 5, 6  };
        values.Add(PropertyName.@PropertyscardotGenericArray, global::scardot.Variant.CreateFrom(__PropertyscardotGenericArray_default_value));
        return values;
    }
#endif // TOOLS
#pragma warning restore CS0109
}
