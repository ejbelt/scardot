using System;

namespace scardot.SourceGenerators.Sample
{
    public partial class AllWriteOnly : scardotObject
    {
        private bool _writeOnlyBackingField = false;
        public bool WriteOnlyProperty { set => _writeOnlyBackingField = value; }
    }
}
