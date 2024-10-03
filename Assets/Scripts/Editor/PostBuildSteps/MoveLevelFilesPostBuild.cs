using System.IO;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace Editor.PostBuildSteps
{
    public class MoveFilesPostBuild : IPostprocessBuildWithReport
    {
        public int callbackOrder => 0;

        private const string SourcePath = "./Assets/Levels";
        private const string DestinationPath = "./Assets/Resources/Levels";

        public void OnPostprocessBuild(BuildReport report)
        {
            if (!Directory.Exists(DestinationPath)) {
                Directory.CreateDirectory(DestinationPath);
            }

            foreach (var file in Directory.GetFiles(SourcePath)) {
                var fileName = Path.GetFileName(file);
                var destinationPath = Path.Combine(DestinationPath, fileName);

                File.Copy(file, destinationPath, true);
                Debug.Log($"File {fileName} moved to {destinationPath}");
            }
        }
    }
}