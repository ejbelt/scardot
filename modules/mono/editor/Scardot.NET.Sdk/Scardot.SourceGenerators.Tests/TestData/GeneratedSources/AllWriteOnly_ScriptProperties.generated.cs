using scardot;
using scardot.NativeInterop;

partial class AllWriteOnly
{
#pragma warning disable CS0109 // Disable warning about redundant 'new' keyword
    /// <summary>
    /// Cached StringNames for the properties and fields contained in this class, for fast lookup.
    /// </summary>
    public new class PropertyName : global::scardot.scardotObject.PropertyName {
        /// <summary>
        /// Cached name for the 'WriteOnlyProperty' property.
        /// </summary>
        public new static readonly global::scardot.StringName @WriteOnlyProperty = "WriteOnlyProperty";
        /// <summary>
        /// Cached name for the '_writeOnlyBackingField' field.
        /// </summary>
        public new static readonly global::scardot.StringName @_writeOnlyBackingField = "_writeOnlyBackingField";
    }
    /// <inheritdoc/>
    [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
    protected override bool SetscardotClassPropertyValue(in scardot_string_name name, in scardot_variant value)
    {
        if (name == PropertyName.@WriteOnlyProperty) {
            this.@WriteOnlyProperty = global::scardot.NativeInterop.VariantUtils.ConvertTo<bool>(value);
            return true;
        }
        if (name == PropertyName.@_writeOnlyBackingField) {
            this.@_writeOnlyBackingField = global::scardot.NativeInterop.VariantUtils.ConvertTo<bool>(value);
            return true;
        }
        return base.SetscardotClassPropertyValue(name, value);
    }
    /// <inheritdoc/>
    [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
    protected override bool GetscardotClassPropertyValue(in scardot_string_name name, out scardot_variant value)
    {
        if (name == PropertyName.@_writeOnlyBackingField) {
            value = global::scardot.NativeInterop.VariantUtils.CreateFrom<bool>(this.@_writeOnlyBackingField);
            return true;
        }
        return base.GetscardotClassPropertyValue(name, out value);
    }
    /// <summary>
    /// Get the property information for all the properties declared in this class.
    /// This method is used by scardot to register the available properties in the editor.
    /// Do not call this method.
    /// </summary>
    [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
    internal new static global::System.Collections.Generic.List<global::scardot.Bridge.PropertyInfo> GetscardotPropertyList()
    {
        var properties = new global::System.Collections.Generic.List<global::scardot.Bridge.PropertyInfo>();
        properties.Add(new(type: (global::scardot.Variant.Type)1, name: PropertyName.@_writeOnlyBackingField, hint: (global::scardot.PropertyHint)0, hintString: "", usage: (global::scardot.PropertyUsageFlags)4096, exported: false));
        properties.Add(new(type: (global::scardot.Variant.Type)1, name: PropertyName.@WriteOnlyProperty, hint: (global::scardot.PropertyHint)0, hintString: "", usage: (global::scardot.PropertyUsageFlags)4096, exported: false));
        return properties;
    }
#pragma warning restore CS0109
}
