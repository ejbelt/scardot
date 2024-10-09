using System;

namespace scardot.SourceGenerators.Sample;

public partial class NestedClass : scardotObject
{
    public partial class NestedClass2 : scardotObject
    {
        public partial class NestedClass3 : scardotObject
        {
            [Signal]
            public delegate void MySignalEventHandler(string str, int num);

            [Export] private String _fieldString = "foo";
            [Export] private String PropertyString { get; set; } = "foo";

            private void Method()
            {
            }
        }
    }
}
