using scardot;
using scardot.NativeInterop;

partial struct OuterClass
{
partial class NestedClass
{
    /// <inheritdoc/>
    [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
    protected override void SavescardotObjectData(global::scardot.Bridge.scardotSerializationInfo info)
    {
        base.SavescardotObjectData(info);
    }
    /// <inheritdoc/>
    [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
    protected override void RestorescardotObjectData(global::scardot.Bridge.scardotSerializationInfo info)
    {
        base.RestorescardotObjectData(info);
    }
}
}
