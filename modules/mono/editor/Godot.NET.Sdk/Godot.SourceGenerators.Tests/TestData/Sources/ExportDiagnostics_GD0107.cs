using scardot;
using scardot.Collections;

public partial class ExportDiagnostics_GD0107_OK : Node
{
    [Export]
    public Node NodeField;

    [Export]
    public Node[] SystemArrayOfNodesField;

    [Export]
    public Array<Node> scardotArrayOfNodesField;

    [Export]
    public Dictionary<Node, string> scardotDictionaryWithNodeAsKeyField;

    [Export]
    public Dictionary<string, Node> scardotDictionaryWithNodeAsValueField;

    [Export]
    public Node NodeProperty { get; set; }

    [Export]
    public Node[] SystemArrayOfNodesProperty { get; set; }

    [Export]
    public Array<Node> scardotArrayOfNodesProperty { get; set; }

    [Export]
    public Dictionary<Node, string> scardotDictionaryWithNodeAsKeyProperty { get; set; }

    [Export]
    public Dictionary<string, Node> scardotDictionaryWithNodeAsValueProperty { get; set; }
}

public partial class ExportDiagnostics_GD0107_KO : Resource
{
    [Export]
    public Node {|GD0107:NodeField|};

    [Export]
    public Node[] {|GD0107:SystemArrayOfNodesField|};

    [Export]
    public Array<Node> {|GD0107:scardotArrayOfNodesField|};

    [Export]
    public Dictionary<Node, string> {|GD0107:scardotDictionaryWithNodeAsKeyField|};

    [Export]
    public Dictionary<string, Node> {|GD0107:scardotDictionaryWithNodeAsValueField|};

    [Export]
    public Node {|GD0107:NodeProperty|} { get; set; }

    [Export]
    public Node[] {|GD0107:SystemArrayOfNodesProperty|} { get; set; }

    [Export]
    public Array<Node> {|GD0107:scardotArrayOfNodesProperty|} { get; set; }

    [Export]
    public Dictionary<Node, string> {|GD0107:scardotDictionaryWithNodeAsKeyProperty|} { get; set; }

    [Export]
    public Dictionary<string, Node> {|GD0107:scardotDictionaryWithNodeAsValueProperty|} { get; set; }
}
