using System.Diagnostics.CodeAnalysis;
using System.IO;
using scardot;
using scardot.NativeInterop;
using scardotTools.Core;
using static scardotTools.Internals.Globals;

namespace scardotTools.Internals
{
    public static class scardotSharpDirs
    {
        public static string ResMetadataDir
        {
            get
            {
                Internal.scardot_icall_scardotSharpDirs_ResMetadataDir(out scardot_string dest);
                using (dest)
                    return Marshaling.ConvertStringToManaged(dest);
            }
        }

        public static string MonoUserDir
        {
            get
            {
                Internal.scardot_icall_scardotSharpDirs_MonoUserDir(out scardot_string dest);
                using (dest)
                    return Marshaling.ConvertStringToManaged(dest);
            }
        }

        public static string BuildLogsDirs
        {
            get
            {
                Internal.scardot_icall_scardotSharpDirs_BuildLogsDirs(out scardot_string dest);
                using (dest)
                    return Marshaling.ConvertStringToManaged(dest);
            }
        }

        public static string DataEditorToolsDir
        {
            get
            {
                Internal.scardot_icall_scardotSharpDirs_DataEditorToolsDir(out scardot_string dest);
                using (dest)
                    return Marshaling.ConvertStringToManaged(dest);
            }
        }


        public static string CSharpProjectName
        {
            get
            {
                Internal.scardot_icall_scardotSharpDirs_CSharpProjectName(out scardot_string dest);
                using (dest)
                    return Marshaling.ConvertStringToManaged(dest);
            }
        }

        [MemberNotNull("_projectAssemblyName", "_projectSlnPath", "_projectCsProjPath")]
        public static void DetermineProjectLocation()
        {
            _projectAssemblyName = (string?)ProjectSettings.GetSetting("dotnet/project/assembly_name");
            if (string.IsNullOrEmpty(_projectAssemblyName))
            {
                _projectAssemblyName = CSharpProjectName;
                ProjectSettings.SetSetting("dotnet/project/assembly_name", _projectAssemblyName);
            }

            string? slnParentDir = (string?)ProjectSettings.GetSetting("dotnet/project/solution_directory");
            if (string.IsNullOrEmpty(slnParentDir))
                slnParentDir = "res://";
            else if (!slnParentDir.StartsWith("res://", System.StringComparison.Ordinal))
                slnParentDir = "res://" + slnParentDir;

            // The csproj should be in the same folder as project.scardot.
            string csprojParentDir = "res://";

            _projectSlnPath = Path.Combine(ProjectSettings.GlobalizePath(slnParentDir),
                string.Concat(_projectAssemblyName, ".sln"));

            _projectCsProjPath = Path.Combine(ProjectSettings.GlobalizePath(csprojParentDir),
                string.Concat(_projectAssemblyName, ".csproj"));
        }

        private static string? _projectAssemblyName;
        private static string? _projectSlnPath;
        private static string? _projectCsProjPath;

        public static string ProjectAssemblyName
        {
            get
            {
                if (_projectAssemblyName == null)
                    DetermineProjectLocation();
                return _projectAssemblyName;
            }
        }

        public static string ProjectSlnPath
        {
            get
            {
                if (_projectSlnPath == null)
                    DetermineProjectLocation();
                return _projectSlnPath;
            }
        }

        public static string ProjectCsProjPath
        {
            get
            {
                if (_projectCsProjPath == null)
                    DetermineProjectLocation();
                return _projectCsProjPath;
            }
        }

        public static string ProjectBaseOutputPath
        {
            get
            {
                if (_projectCsProjPath == null)
                    DetermineProjectLocation();
                return Path.Combine(Path.GetDirectoryName(_projectCsProjPath)!, ".scardot", "mono", "temp", "bin");
            }
        }

        public static string LogsDirPathFor(string solution, string configuration)
            => Path.Combine(BuildLogsDirs, $"{solution.Md5Text()}_{configuration}");

        public static string LogsDirPathFor(string configuration)
            => LogsDirPathFor(ProjectSlnPath, configuration);
    }
}
