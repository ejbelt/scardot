using scardot;

namespace NamespaceA
{
    partial class SameName : scardotObject
    {
        private int _field;
    }
}

// SameName again but different namespace
namespace NamespaceB
{
    partial class {|GD0003:SameName|} : scardotObject
    {
        private int _field;
    }
}
