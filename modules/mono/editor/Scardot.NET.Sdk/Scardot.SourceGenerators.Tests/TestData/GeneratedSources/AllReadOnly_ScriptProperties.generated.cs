using scardot;
using scardot.NativeInterop;

partial class AllReadOnly
{
#pragma warning disable CS0109 // Disable warning about redundant 'new' keyword
    /// <summary>
    /// Cached StringNames for the properties and fields contained in this class, for fast lookup.
    /// </summary>
    public new class PropertyName : global::scardot.scardotObject.PropertyName {
        /// <summary>
        /// Cached name for the 'ReadOnlyAutoProperty' property.
        /// </summary>
        public new static readonly global::scardot.StringName @ReadOnlyAutoProperty = "ReadOnlyAutoProperty";
        /// <summary>
        /// Cached name for the 'ReadOnlyProperty' property.
        /// </summary>
        public new static readonly global::scardot.StringName @ReadOnlyProperty = "ReadOnlyProperty";
        /// <summary>
        /// Cached name for the 'InitOnlyAutoProperty' property.
        /// </summary>
        public new static readonly global::scardot.StringName @InitOnlyAutoProperty = "InitOnlyAutoProperty";
        /// <summary>
        /// Cached name for the 'ReadOnlyField' field.
        /// </summary>
        public new static readonly global::scardot.StringName @ReadOnlyField = "ReadOnlyField";
    }
    /// <inheritdoc/>
    [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
    protected override bool GetscardotClassPropertyValue(in scardot_string_name name, out scardot_variant value)
    {
        if (name == PropertyName.@ReadOnlyAutoProperty) {
            value = global::scardot.NativeInterop.VariantUtils.CreateFrom<string>(this.@ReadOnlyAutoProperty);
            return true;
        }
        if (name == PropertyName.@ReadOnlyProperty) {
            value = global::scardot.NativeInterop.VariantUtils.CreateFrom<string>(this.@ReadOnlyProperty);
            return true;
        }
        if (name == PropertyName.@InitOnlyAutoProperty) {
            value = global::scardot.NativeInterop.VariantUtils.CreateFrom<string>(this.@InitOnlyAutoProperty);
            return true;
        }
        if (name == PropertyName.@ReadOnlyField) {
            value = global::scardot.NativeInterop.VariantUtils.CreateFrom<string>(this.@ReadOnlyField);
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
        properties.Add(new(type: (global::scardot.Variant.Type)4, name: PropertyName.@ReadOnlyField, hint: (global::scardot.PropertyHint)0, hintString: "", usage: (global::scardot.PropertyUsageFlags)4096, exported: false));
        properties.Add(new(type: (global::scardot.Variant.Type)4, name: PropertyName.@ReadOnlyAutoProperty, hint: (global::scardot.PropertyHint)0, hintString: "", usage: (global::scardot.PropertyUsageFlags)4096, exported: false));
        properties.Add(new(type: (global::scardot.Variant.Type)4, name: PropertyName.@ReadOnlyProperty, hint: (global::scardot.PropertyHint)0, hintString: "", usage: (global::scardot.PropertyUsageFlags)4096, exported: false));
        properties.Add(new(type: (global::scardot.Variant.Type)4, name: PropertyName.@InitOnlyAutoProperty, hint: (global::scardot.PropertyHint)0, hintString: "", usage: (global::scardot.PropertyUsageFlags)4096, exported: false));
        return properties;
    }
#pragma warning restore CS0109
}
