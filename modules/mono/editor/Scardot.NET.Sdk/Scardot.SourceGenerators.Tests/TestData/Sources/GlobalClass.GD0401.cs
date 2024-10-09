using scardot;

// This works because it inherits from scardotObject.
[GlobalClass]
public partial class CustomGlobalClass1 : scardotObject
{

}

// This works because it inherits from an object that inherits from scardotObject
[GlobalClass]
public partial class CustomGlobalClass2 : Node
{

}

// This raises a GD0401 diagnostic error: global classes must inherit from scardotObject
[GlobalClass]
public partial class {|GD0401:CustomGlobalClass3|}
{

}
