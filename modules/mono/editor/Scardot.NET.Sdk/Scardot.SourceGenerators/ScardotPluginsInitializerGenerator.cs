using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace scardot.SourceGenerators
{
    [Generator]
    public class scardotPluginsInitializerGenerator : ISourceGenerator
    {
        public void Initialize(GeneratorInitializationContext context)
        {
        }

        public void Execute(GeneratorExecutionContext context)
        {
            if (context.IsscardotToolsProject() || context.IsscardotSourceGeneratorDisabled("scardotPluginsInitializer"))
                return;

            string source =
                @"using System;
using System.Runtime.InteropServices;
using scardot.Bridge;
using scardot.NativeInterop;

namespace scardotPlugins.Game
{
    internal static partial class Main
    {
        [UnmanagedCallersOnly(EntryPoint = ""scardotsharp_game_main_init"")]
        private static scardot_bool InitializeFromGameProject(IntPtr scardotDllHandle, IntPtr outManagedCallbacks,
            IntPtr unmanagedCallbacks, int unmanagedCallbacksSize)
        {
            try
            {
                DllImportResolver dllImportResolver = new scardotDllImportResolver(scardotDllHandle).OnResolveDllImport;

                var coreApiAssembly = typeof(global::scardot.scardotObject).Assembly;

                NativeLibrary.SetDllImportResolver(coreApiAssembly, dllImportResolver);

                NativeFuncs.Initialize(unmanagedCallbacks, unmanagedCallbacksSize);

                ManagedCallbacks.Create(outManagedCallbacks);

                ScriptManagerBridge.LookupScriptsInAssembly(typeof(global::scardotPlugins.Game.Main).Assembly);

                return scardot_bool.True;
            }
            catch (Exception e)
            {
                global::System.Console.Error.WriteLine(e);
                return false.ToscardotBool();
            }
        }
    }
}
";

            context.AddSource("scardotPlugins.Game.generated",
                SourceText.From(source, Encoding.UTF8));
        }
    }
}
