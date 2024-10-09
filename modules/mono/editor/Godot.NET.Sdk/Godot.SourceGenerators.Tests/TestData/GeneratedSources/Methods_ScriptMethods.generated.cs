using scardot;
using scardot.NativeInterop;

partial class Methods
{
#pragma warning disable CS0109 // Disable warning about redundant 'new' keyword
    /// <summary>
    /// Cached StringNames for the methods contained in this class, for fast lookup.
    /// </summary>
    public new class MethodName : global::scardot.scardotObject.MethodName {
        /// <summary>
        /// Cached name for the 'MethodWithOverload' method.
        /// </summary>
        public new static readonly global::scardot.StringName @MethodWithOverload = "MethodWithOverload";
    }
    /// <summary>
    /// Get the method information for all the methods declared in this class.
    /// This method is used by scardot to register the available methods in the editor.
    /// Do not call this method.
    /// </summary>
    [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
    internal new static global::System.Collections.Generic.List<global::scardot.Bridge.MethodInfo> GetscardotMethodList()
    {
        var methods = new global::System.Collections.Generic.List<global::scardot.Bridge.MethodInfo>(3);
        methods.Add(new(name: MethodName.@MethodWithOverload, returnVal: new(type: (global::scardot.Variant.Type)0, name: "", hint: (global::scardot.PropertyHint)0, hintString: "", usage: (global::scardot.PropertyUsageFlags)6, exported: false), flags: (global::scardot.MethodFlags)1, arguments: null, defaultArguments: null));
        methods.Add(new(name: MethodName.@MethodWithOverload, returnVal: new(type: (global::scardot.Variant.Type)0, name: "", hint: (global::scardot.PropertyHint)0, hintString: "", usage: (global::scardot.PropertyUsageFlags)6, exported: false), flags: (global::scardot.MethodFlags)1, arguments: new() { new(type: (global::scardot.Variant.Type)2, name: "a", hint: (global::scardot.PropertyHint)0, hintString: "", usage: (global::scardot.PropertyUsageFlags)6, exported: false),  }, defaultArguments: null));
        methods.Add(new(name: MethodName.@MethodWithOverload, returnVal: new(type: (global::scardot.Variant.Type)0, name: "", hint: (global::scardot.PropertyHint)0, hintString: "", usage: (global::scardot.PropertyUsageFlags)6, exported: false), flags: (global::scardot.MethodFlags)1, arguments: new() { new(type: (global::scardot.Variant.Type)2, name: "a", hint: (global::scardot.PropertyHint)0, hintString: "", usage: (global::scardot.PropertyUsageFlags)6, exported: false), new(type: (global::scardot.Variant.Type)2, name: "b", hint: (global::scardot.PropertyHint)0, hintString: "", usage: (global::scardot.PropertyUsageFlags)6, exported: false),  }, defaultArguments: null));
        return methods;
    }
#pragma warning restore CS0109
    /// <inheritdoc/>
    [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
    protected override bool InvokescardotClassMethod(in godot_string_name method, NativeVariantPtrArgs args, out godot_variant ret)
    {
        if (method == MethodName.@MethodWithOverload && args.Count == 0) {
            @MethodWithOverload();
            ret = default;
            return true;
        }
        if (method == MethodName.@MethodWithOverload && args.Count == 1) {
            @MethodWithOverload(global::scardot.NativeInterop.VariantUtils.ConvertTo<int>(args[0]));
            ret = default;
            return true;
        }
        if (method == MethodName.@MethodWithOverload && args.Count == 2) {
            @MethodWithOverload(global::scardot.NativeInterop.VariantUtils.ConvertTo<int>(args[0]), global::scardot.NativeInterop.VariantUtils.ConvertTo<int>(args[1]));
            ret = default;
            return true;
        }
        return base.InvokescardotClassMethod(method, args, out ret);
    }
    /// <inheritdoc/>
    [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
    protected override bool HasscardotClassMethod(in godot_string_name method)
    {
        if (method == MethodName.@MethodWithOverload) {
           return true;
        }
        return base.HasscardotClassMethod(method);
    }
}
