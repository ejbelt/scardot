using scardot;
using scardot.NativeInterop;

partial class AbstractGenericNode<T>
{
#pragma warning disable CS0109 // Disable warning about redundant 'new' keyword
    /// <summary>
    /// Cached StringNames for the properties and fields contained in this class, for fast lookup.
    /// </summary>
    public new class PropertyName : global::scardot.Node.PropertyName {
        /// <summary>
        /// Cached name for the 'MyArray' property.
        /// </summary>
        public new static readonly global::scardot.StringName @MyArray = "MyArray";
    }
    /// <inheritdoc/>
    [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
    protected override bool SetscardotClassPropertyValue(in scardot_string_name name, in scardot_variant value)
    {
        if (name == PropertyName.@MyArray) {
            this.@MyArray = global::scardot.NativeInterop.VariantUtils.ConvertToArray<T>(value);
            return true;
        }
        return base.SetscardotClassPropertyValue(name, value);
    }
    /// <inheritdoc/>
    [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
    protected override bool GetscardotClassPropertyValue(in scardot_string_name name, out scardot_variant value)
    {
        if (name == PropertyName.@MyArray) {
            value = global::scardot.NativeInterop.VariantUtils.CreateFromArray(this.@MyArray);
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
        properties.Add(new(type: (global::scardot.Variant.Type)28, name: PropertyName.@MyArray, hint: (global::scardot.PropertyHint)0, hintString: "", usage: (global::scardot.PropertyUsageFlags)4102, exported: true));
        return properties;
    }
#pragma warning restore CS0109
}
