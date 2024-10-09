using scardot;
using scardot.NativeInterop;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace scardotTools.Internals
{
    public static class Globals
    {
        public static float EditorScale => Internal.scardot_icall_Globals_EditorScale();

        // ReSharper disable once UnusedMethodReturnValue.Global
        public static Variant GlobalDef(string setting, Variant defaultValue, bool restartIfChanged = false)
        {
            using scardot_string settingIn = Marshaling.ConvertStringToNative(setting);
            using scardot_variant defaultValueIn = defaultValue.CopyNativeVariant();
            Internal.scardot_icall_Globals_GlobalDef(settingIn, defaultValueIn, restartIfChanged,
                out scardot_variant result);
            return Variant.CreateTakingOwnershipOfDisposableValue(result);
        }

        // ReSharper disable once UnusedMethodReturnValue.Global
        public static Variant EditorDef(string setting, Variant defaultValue, bool restartIfChanged = false)
        {
            using scardot_string settingIn = Marshaling.ConvertStringToNative(setting);
            using scardot_variant defaultValueIn = defaultValue.CopyNativeVariant();
            Internal.scardot_icall_Globals_EditorDef(settingIn, defaultValueIn, restartIfChanged,
                out scardot_variant result);
            return Variant.CreateTakingOwnershipOfDisposableValue(result);
        }

        public static Shortcut EditorDefShortcut(string setting, string name, Key keycode = Key.None, bool physical = false)
        {
            using scardot_string settingIn = Marshaling.ConvertStringToNative(setting);
            using scardot_string nameIn = Marshaling.ConvertStringToNative(name);
            Internal.scardot_icall_Globals_EditorDefShortcut(settingIn, nameIn, keycode, physical.ToscardotBool(), out scardot_variant result);
            return (Shortcut)Variant.CreateTakingOwnershipOfDisposableValue(result);
        }

        public static Shortcut EditorGetShortcut(string setting)
        {
            using scardot_string settingIn = Marshaling.ConvertStringToNative(setting);
            Internal.scardot_icall_Globals_EditorGetShortcut(settingIn, out scardot_variant result);
            return (Shortcut)Variant.CreateTakingOwnershipOfDisposableValue(result);
        }

        public static void EditorShortcutOverride(string setting, string feature, Key keycode = Key.None, bool physical = false)
        {
            using scardot_string settingIn = Marshaling.ConvertStringToNative(setting);
            using scardot_string featureIn = Marshaling.ConvertStringToNative(feature);
            Internal.scardot_icall_Globals_EditorShortcutOverride(settingIn, featureIn, keycode, physical.ToscardotBool());
        }

        [SuppressMessage("ReSharper", "InconsistentNaming")]
        public static string TTR(this string text)
        {
            using scardot_string textIn = Marshaling.ConvertStringToNative(text);
            Internal.scardot_icall_Globals_TTR(textIn, out scardot_string dest);
            using (dest)
                return Marshaling.ConvertStringToManaged(dest);
        }
    }
}
