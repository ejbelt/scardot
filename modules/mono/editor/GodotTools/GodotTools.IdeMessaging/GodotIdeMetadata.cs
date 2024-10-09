using System.Diagnostics.CodeAnalysis;

namespace scardotTools.IdeMessaging
{
    public readonly struct scardotIdeMetadata
    {
        public int Port { get; }
        public string EditorExecutablePath { get; }

        public const string DefaultFileName = "ide_messaging_meta.txt";

        public scardotIdeMetadata(int port, string editorExecutablePath)
        {
            Port = port;
            EditorExecutablePath = editorExecutablePath;
        }

        public static bool operator ==(scardotIdeMetadata a, scardotIdeMetadata b)
        {
            return a.Port == b.Port && a.EditorExecutablePath == b.EditorExecutablePath;
        }

        public static bool operator !=(scardotIdeMetadata a, scardotIdeMetadata b)
        {
            return !(a == b);
        }

        public override bool Equals([NotNullWhen(true)] object? obj)
        {
            return obj is scardotIdeMetadata metadata && metadata == this;
        }

        public bool Equals(scardotIdeMetadata other)
        {
            return Port == other.Port && EditorExecutablePath == other.EditorExecutablePath;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Port * 397) ^ (EditorExecutablePath != null ? EditorExecutablePath.GetHashCode() : 0);
            }
        }
    }
}
