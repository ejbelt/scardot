using scardot;
using scardot.NativeInterop;

partial class ScriptBoilerplate
{
#pragma warning disable CS0109 // Disable warning about redundant 'new' keyword
    /// <summary>
    /// Cached StringNames for the methods contained in this class, for fast lookup.
    /// </summary>
    public new class MethodName : global::scardot.Node.MethodName {
        /// <summary>
        /// Cached name for the '_Process' method.
        /// </summary>
        public new static readonly global::scardot.StringName @_Process = "_Process";
        /// <summary>
        /// Cached name for the 'Bazz' method.
        /// </summary>
        public new static readonly global::scardot.StringName @Bazz = "Bazz";
    }
    /// <summary>
    /// Get the method information for all the methods declared in this class.
    /// This method is used by scardot to register the available methods in the editor.
    /// Do not call this method.
    /// </summary>
    [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
    internal new static global::System.Collections.Generic.List<global::scardot.Bridge.MethodInfo> GetscardotMethodList()
    {
        var methods = new global::System.Collections.Generic.List<global::scardot.Bridge.MethodInfo>(2);
        methods.Add(new(name: MethodName.@_Process, returnVal: new(type: (global::scardot.Variant.Type)0, name: "", hint: (global::scardot.PropertyHint)0, hintString: "", usage: (global::scardot.PropertyUsageFlags)6, exported: false), flags: (global::scardot.MethodFlags)1, arguments: new() { new(type: (global::scardot.Variant.Type)3, name: "delta", hint: (global::scardot.PropertyHint)0, hintString: "", usage: (global::scardot.PropertyUsageFlags)6, exported: false),  }, defaultArguments: null));
        methods.Add(new(name: MethodName.@Bazz, returnVal: new(type: (global::scardot.Variant.Type)2, name: "", hint: (global::scardot.PropertyHint)0, hintString: "", usage: (global::scardot.PropertyUsageFlags)6, exported: false), flags: (global::scardot.MethodFlags)1, arguments: new() { new(type: (global::scardot.Variant.Type)21, name: "name", hint: (global::scardot.PropertyHint)0, hintString: "", usage: (global::scardot.PropertyUsageFlags)6, exported: false),  }, defaultArguments: null));
        return methods;
    }
#pragma warning restore CS0109
    /// <inheritdoc/>
    [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
    protected override bool InvokescardotClassMethod(in scardot_string_name method, NativeVariantPtrArgs args, out scardot_variant ret)
    {
        if (method == MethodName.@_Process && args.Count == 1) {
            @_Process(global::scardot.NativeInterop.VariantUtils.ConvertTo<double>(args[0]));
            ret = default;
            return true;
        }
        if (method == MethodName.@Bazz && args.Count == 1) {
            var callRet = @Bazz(global::scardot.NativeInterop.VariantUtils.ConvertTo<global::scardot.StringName>(args[0]));
            ret = global::scardot.NativeInterop.VariantUtils.CreateFrom<int>(callRet);
            return true;
        }
        return base.InvokescardotClassMethod(method, args, out ret);
    }
    /// <inheritdoc/>
    [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
    protected override bool HasscardotClassMethod(in scardot_string_name method)
    {
        if (method == MethodName.@_Process) {
           return true;
        }
        if (method == MethodName.@Bazz) {
           return true;
        }
        return base.HasscardotClassMethod(method);
    }
}
