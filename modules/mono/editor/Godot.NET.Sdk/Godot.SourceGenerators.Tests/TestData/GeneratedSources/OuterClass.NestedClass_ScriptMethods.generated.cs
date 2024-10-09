using scardot;
using scardot.NativeInterop;

partial struct OuterClass
{
partial class NestedClass
{
#pragma warning disable CS0109 // Disable warning about redundant 'new' keyword
    /// <summary>
    /// Cached StringNames for the methods contained in this class, for fast lookup.
    /// </summary>
    public new class MethodName : global::scardot.RefCounted.MethodName {
        /// <summary>
        /// Cached name for the '_Get' method.
        /// </summary>
        public new static readonly global::scardot.StringName @_Get = "_Get";
    }
    /// <summary>
    /// Get the method information for all the methods declared in this class.
    /// This method is used by scardot to register the available methods in the editor.
    /// Do not call this method.
    /// </summary>
    [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
    internal new static global::System.Collections.Generic.List<global::scardot.Bridge.MethodInfo> GetscardotMethodList()
    {
        var methods = new global::System.Collections.Generic.List<global::scardot.Bridge.MethodInfo>(1);
        methods.Add(new(name: MethodName.@_Get, returnVal: new(type: (global::scardot.Variant.Type)0, name: "", hint: (global::scardot.PropertyHint)0, hintString: "", usage: (global::scardot.PropertyUsageFlags)131078, exported: false), flags: (global::scardot.MethodFlags)1, arguments: new() { new(type: (global::scardot.Variant.Type)21, name: "property", hint: (global::scardot.PropertyHint)0, hintString: "", usage: (global::scardot.PropertyUsageFlags)6, exported: false),  }, defaultArguments: null));
        return methods;
    }
#pragma warning restore CS0109
    /// <inheritdoc/>
    [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
    protected override bool InvokescardotClassMethod(in godot_string_name method, NativeVariantPtrArgs args, out godot_variant ret)
    {
        if (method == MethodName.@_Get && args.Count == 1) {
            var callRet = @_Get(global::scardot.NativeInterop.VariantUtils.ConvertTo<global::scardot.StringName>(args[0]));
            ret = global::scardot.NativeInterop.VariantUtils.CreateFrom<global::scardot.Variant>(callRet);
            return true;
        }
        return base.InvokescardotClassMethod(method, args, out ret);
    }
    /// <inheritdoc/>
    [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
    protected override bool HasscardotClassMethod(in godot_string_name method)
    {
        if (method == MethodName.@_Get) {
           return true;
        }
        return base.HasscardotClassMethod(method);
    }
}
}
