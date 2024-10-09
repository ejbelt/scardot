using System;

#nullable enable

namespace scardot
{
    /// <summary>
    /// Exposes the target class as a global script class to scardot Engine.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class GlobalClassAttribute : Attribute { }
}
