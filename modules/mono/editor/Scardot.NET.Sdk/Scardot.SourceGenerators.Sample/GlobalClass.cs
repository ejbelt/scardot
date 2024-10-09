namespace scardot.SourceGenerators.Sample;

[GlobalClass]
public partial class CustomGlobalClass : scardotObject
{
}

// This doesn't works because global classes can't have any generic type parameter.
/*
[GlobalClass]
public partial class CustomGlobalClass<T> : Node
{
}
*/
