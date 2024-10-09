using scardot;

public class {|GD0002:OuterOuterClassPartialModifierAnalyzer|}
{
    public class {|GD0002:OuterClassPartialModifierAnalyzer|}
    {
        // MyNode is contained in a non-partial type so the source generators
        // can't enhance this type to work with scardot.
        public partial class MyNode : Node { }
    }
}
