using scardot;

public partial class Generic<T> : scardotObject
{
    private int _field;
}

// Generic again but different generic parameters
public partial class {|GD0003:Generic|}<T, R> : scardotObject
{
    private int _field;
}

// Generic again but without generic parameters
public partial class {|GD0003:Generic|} : scardotObject
{
    private int _field;
}
