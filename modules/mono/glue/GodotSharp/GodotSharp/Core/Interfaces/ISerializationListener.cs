namespace scardot
{
    /// <summary>
    /// Allows a scardotObject to react to the serialization/deserialization
    /// that occurs when scardot reloads assemblies.
    /// </summary>
    public interface ISerializationListener
    {
        /// <summary>
        /// Executed before serializing this instance's state when reloading assemblies.
        /// Clear any data that should not be serialized.
        /// </summary>
        void OnBeforeSerialize();

        /// <summary>
        /// Executed after deserializing this instance's state after reloading assemblies.
        /// Restore any state that has been lost.
        /// </summary>
        void OnAfterDeserialize();
    }
}
