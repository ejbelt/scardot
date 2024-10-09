using scardot;

public abstract partial class AbstractGenericNode<[MustBeVariant] T> : Node
{
    [Export] // This should be included, but without type hints.
    public scardot.Collections.Array<T> MyArray { get; set; } = new();
}
