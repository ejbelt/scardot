#pragma warning disable IDE1006 // Naming rule violation
// ReSharper disable InconsistentNaming

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using scardot;
using scardot.NativeInterop;
using scardot.SourceGenerators.Internal;
using scardotTools.IdeMessaging.Requests;

namespace scardotTools.Internals
{
    [GenerateUnmanagedCallbacks(typeof(InternalUnmanagedCallbacks))]
    internal static partial class Internal
    {
        public const string CSharpLanguageType = "CSharpScript";
        public const string CSharpLanguageExtension = ".cs";

        public static string FullExportTemplatesDir
        {
            get
            {
                scardot_icall_Internal_FullExportTemplatesDir(out scardot_string dest);
                using (dest)
                    return Marshaling.ConvertStringToManaged(dest);
            }
        }

        public static string SimplifyscardotPath(this string path) => scardot.StringExtensions.SimplifyPath(path);

        public static bool IsMacOSAppBundleInstalled(string bundleId)
        {
            using scardot_string bundleIdIn = Marshaling.ConvertStringToNative(bundleId);
            return scardot_icall_Internal_IsMacOSAppBundleInstalled(bundleIdIn);
        }

        public static bool LipOCreateFile(string outputPath, string[] files)
        {
            using scardot_string outputPathIn = Marshaling.ConvertStringToNative(outputPath);
            using scardot_packed_string_array filesIn = Marshaling.ConvertSystemArrayToNativePackedStringArray(files);
            return scardot_icall_Internal_LipOCreateFile(outputPathIn, filesIn);
        }

        public static bool scardotIs32Bits() => scardot_icall_Internal_scardotIs32Bits();

        public static bool scardotIsRealTDouble() => scardot_icall_Internal_scardotIsRealTDouble();

        public static void scardotMainIteration() => scardot_icall_Internal_scardotMainIteration();

        public static bool IsAssembliesReloadingNeeded() => scardot_icall_Internal_IsAssembliesReloadingNeeded();

        public static void ReloadAssemblies(bool softReload) => scardot_icall_Internal_ReloadAssemblies(softReload);

        public static void EditorDebuggerNodeReloadScripts() => scardot_icall_Internal_EditorDebuggerNodeReloadScripts();

        public static bool ScriptEditorEdit(Resource resource, int line, int col, bool grabFocus = true) =>
            scardot_icall_Internal_ScriptEditorEdit(resource.NativeInstance, line, col, grabFocus);

        public static void EditorNodeShowScriptScreen() => scardot_icall_Internal_EditorNodeShowScriptScreen();

        public static void EditorRunPlay() => scardot_icall_Internal_EditorRunPlay();

        public static void EditorRunStop() => scardot_icall_Internal_EditorRunStop();

        public static void EditorPlugin_AddControlToEditorRunBar(Control control) =>
            scardot_icall_Internal_EditorPlugin_AddControlToEditorRunBar(control.NativeInstance);

        public static void ScriptEditorDebugger_ReloadScripts() =>
            scardot_icall_Internal_ScriptEditorDebugger_ReloadScripts();

        public static string[] CodeCompletionRequest(CodeCompletionRequest.CompletionKind kind,
            string scriptFile)
        {
            using scardot_string scriptFileIn = Marshaling.ConvertStringToNative(scriptFile);
            scardot_icall_Internal_CodeCompletionRequest((int)kind, scriptFileIn, out scardot_packed_string_array res);
            using (res)
                return Marshaling.ConvertNativePackedStringArrayToSystemArray(res);
        }

        #region Internal

        private static bool initialized = false;

        // ReSharper disable once ParameterOnlyUsedForPreconditionCheck.Global
        internal static unsafe void Initialize(IntPtr unmanagedCallbacks, int unmanagedCallbacksSize)
        {
            if (initialized)
                throw new InvalidOperationException("Already initialized.");
            initialized = true;

            if (unmanagedCallbacksSize != sizeof(InternalUnmanagedCallbacks))
                throw new ArgumentException("Unmanaged callbacks size mismatch.", nameof(unmanagedCallbacksSize));

            _unmanagedCallbacks = Unsafe.AsRef<InternalUnmanagedCallbacks>((void*)unmanagedCallbacks);
        }

        private partial struct InternalUnmanagedCallbacks
        {
        }

        /*
         * IMPORTANT:
         * The order of the methods defined in NativeFuncs must match the order
         * in the array defined at the bottom of 'editor/editor_internal_calls.cpp'.
         */

        public static partial void scardot_icall_scardotSharpDirs_ResMetadataDir(out scardot_string r_dest);

        public static partial void scardot_icall_scardotSharpDirs_MonoUserDir(out scardot_string r_dest);

        public static partial void scardot_icall_scardotSharpDirs_BuildLogsDirs(out scardot_string r_dest);

        public static partial void scardot_icall_scardotSharpDirs_DataEditorToolsDir(out scardot_string r_dest);

        public static partial void scardot_icall_scardotSharpDirs_CSharpProjectName(out scardot_string r_dest);

        public static partial void scardot_icall_EditorProgress_Create(in scardot_string task, in scardot_string label,
            int amount, bool canCancel);

        public static partial void scardot_icall_EditorProgress_Dispose(in scardot_string task);

        public static partial bool scardot_icall_EditorProgress_Step(in scardot_string task, in scardot_string state,
            int step,
            bool forceRefresh);

        private static partial void scardot_icall_Internal_FullExportTemplatesDir(out scardot_string dest);

        private static partial bool scardot_icall_Internal_IsMacOSAppBundleInstalled(in scardot_string bundleId);

        private static partial bool scardot_icall_Internal_LipOCreateFile(in scardot_string outputPath, in scardot_packed_string_array files);

        private static partial bool scardot_icall_Internal_scardotIs32Bits();

        private static partial bool scardot_icall_Internal_scardotIsRealTDouble();

        private static partial void scardot_icall_Internal_scardotMainIteration();

        private static partial bool scardot_icall_Internal_IsAssembliesReloadingNeeded();

        private static partial void scardot_icall_Internal_ReloadAssemblies(bool softReload);

        private static partial void scardot_icall_Internal_EditorDebuggerNodeReloadScripts();

        private static partial bool scardot_icall_Internal_ScriptEditorEdit(IntPtr resource, int line, int col,
            bool grabFocus);

        private static partial void scardot_icall_Internal_EditorNodeShowScriptScreen();

        private static partial void scardot_icall_Internal_EditorRunPlay();

        private static partial void scardot_icall_Internal_EditorRunStop();

        private static partial void scardot_icall_Internal_EditorPlugin_AddControlToEditorRunBar(IntPtr p_control);

        private static partial void scardot_icall_Internal_ScriptEditorDebugger_ReloadScripts();

        private static partial void scardot_icall_Internal_CodeCompletionRequest(int kind, in scardot_string scriptFile,
            out scardot_packed_string_array res);

        public static partial float scardot_icall_Globals_EditorScale();

        public static partial void scardot_icall_Globals_GlobalDef(in scardot_string setting, in scardot_variant defaultValue,
            bool restartIfChanged, out scardot_variant result);

        public static partial void scardot_icall_Globals_EditorDef(in scardot_string setting, in scardot_variant defaultValue,
            bool restartIfChanged, out scardot_variant result);

        public static partial void
            scardot_icall_Globals_EditorDefShortcut(in scardot_string setting, in scardot_string name, Key keycode, scardot_bool physical, out scardot_variant result);

        public static partial void
            scardot_icall_Globals_EditorGetShortcut(in scardot_string setting, out scardot_variant result);

        public static partial void
            scardot_icall_Globals_EditorShortcutOverride(in scardot_string setting, in scardot_string feature, Key keycode, scardot_bool physical);

        public static partial void scardot_icall_Globals_TTR(in scardot_string text, out scardot_string dest);

        public static partial void scardot_icall_Utils_OS_GetPlatformName(out scardot_string dest);

        public static partial bool scardot_icall_Utils_OS_UnixFileHasExecutableAccess(in scardot_string filePath);

        #endregion
    }
}
