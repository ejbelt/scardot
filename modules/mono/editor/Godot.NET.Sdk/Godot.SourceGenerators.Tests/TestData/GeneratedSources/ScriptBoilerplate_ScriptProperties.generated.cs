using scardot;
using scardot.NativeInterop;

partial class ScriptBoilerplate
{
#pragma warning disable CS0109 // Disable warning about redundant 'new' keyword
    /// <summary>
    /// Cached StringNames for the properties and fields contained in this class, for fast lookup.
    /// </summary>
    public new class PropertyName : global::scardot.Node.PropertyName {
        /// <summary>
        /// Cached name for the '_nodePath' field.
        /// </summary>
        public new static readonly global::scardot.StringName @_nodePath = "_nodePath";
        /// <summary>
        /// Cached name for the '_velocity' field.
        /// </summary>
        public new static readonly global::scardot.StringName @_velocity = "_velocity";
    }
    /// <inheritdoc/>
    [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
    protected override bool SetscardotClassPropertyValue(in godot_string_name name, in godot_variant value)
    {
        if (name == PropertyName.@_nodePath) {
            this.@_nodePath = global::scardot.NativeInterop.VariantUtils.ConvertTo<global::scardot.NodePath>(value);
            return true;
        }
        if (name == PropertyName.@_velocity) {
            this.@_velocity = global::scardot.NativeInterop.VariantUtils.ConvertTo<int>(value);
            return true;
        }
        return base.SetscardotClassPropertyValue(name, value);
    }
    /// <inheritdoc/>
    [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
    protected override bool GetscardotClassPropertyValue(in godot_string_name name, out godot_variant value)
    {
        if (name == PropertyName.@_nodePath) {
            value = global::scardot.NativeInterop.VariantUtils.CreateFrom<global::scardot.NodePath>(this.@_nodePath);
            return true;
        }
        if (name == PropertyName.@_velocity) {
            value = global::scardot.NativeInterop.VariantUtils.CreateFrom<int>(this.@_velocity);
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
        properties.Add(new(type: (global::scardot.Variant.Type)22, name: PropertyName.@_nodePath, hint: (global::scardot.PropertyHint)0, hintString: "", usage: (global::scardot.PropertyUsageFlags)4096, exported: false));
        properties.Add(new(type: (global::scardot.Variant.Type)2, name: PropertyName.@_velocity, hint: (global::scardot.PropertyHint)0, hintString: "", usage: (global::scardot.PropertyUsageFlags)4096, exported: false));
        return properties;
    }
#pragma warning restore CS0109
}
