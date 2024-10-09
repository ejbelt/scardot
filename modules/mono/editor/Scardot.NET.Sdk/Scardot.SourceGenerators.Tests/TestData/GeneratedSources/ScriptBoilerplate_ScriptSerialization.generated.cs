using scardot;
using scardot.NativeInterop;

partial class ScriptBoilerplate
{
    /// <inheritdoc/>
    [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
    protected override void SavescardotObjectData(global::scardot.Bridge.scardotSerializationInfo info)
    {
        base.SavescardotObjectData(info);
        info.AddProperty(PropertyName.@_nodePath, global::scardot.Variant.From<global::scardot.NodePath>(this.@_nodePath));
        info.AddProperty(PropertyName.@_velocity, global::scardot.Variant.From<int>(this.@_velocity));
    }
    /// <inheritdoc/>
    [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
    protected override void RestorescardotObjectData(global::scardot.Bridge.scardotSerializationInfo info)
    {
        base.RestorescardotObjectData(info);
        if (info.TryGetProperty(PropertyName.@_nodePath, out var _value__nodePath))
            this.@_nodePath = _value__nodePath.As<global::scardot.NodePath>();
        if (info.TryGetProperty(PropertyName.@_velocity, out var _value__velocity))
            this.@_velocity = _value__velocity.As<int>();
    }
}
