namespace scardot.SourceGenerators.Sample
{
    public partial class AllReadOnly : scardotObject
    {
        public readonly string ReadonlyField = "foo";
        public string ReadonlyAutoProperty { get; } = "foo";
        public string ReadonlyProperty { get => "foo"; }
        public string InitonlyAutoProperty { get; init; }
    }
}
