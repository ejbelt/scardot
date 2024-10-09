using scardot;

// This works because it inherits from scardotObject and it doesn't have any generic type parameter.
[GlobalClass]
public partial class CustomGlobalClass : scardotObject
{

}

// This raises a GD0402 diagnostic error: global classes can't have any generic type parameter
[GlobalClass]
public partial class {|GD0402:CustomGlobalClass|}<T> : scardotObject
{

}
