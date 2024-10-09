namespace scardot.SourceGenerators.Sample;

public partial class EventSignals : scardotObject
{
    [Signal]
    public delegate void MySignalEventHandler(string str, int num);
}
