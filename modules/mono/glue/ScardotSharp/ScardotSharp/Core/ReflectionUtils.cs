using System;
using System.Linq;

#nullable enable

namespace scardot;

internal class ReflectionUtils
{
    public static Type? FindTypeInLoadedAssemblies(string assemblyName, string typeFullName)
    {
        return AppDomain.CurrentDomain.GetAssemblies()
            .FirstOrDefault(a => a.GetName().Name == assemblyName)?
            .GetType(typeFullName);
    }
}
