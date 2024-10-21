#if UNITY_EDITOR
using Core;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace Editor
{
    public static class SceneLoader
    {
        private const string Path = "Assets/scenes/";
        private const string SceneSuffix = ".unity";

        // % = ctrl/cmd
        // # = shift
        // & = alt
        //Example:

        [MenuItem("Scenes/Load Main Scene #1")] // ctrl/cmd + 1
        public static void LoadMainScene() => OpenScene(SceneNames.MainSceneName);

        [MenuItem("Scenes/Load Play Scene #2")]
        public static void LoadPlayScene() => OpenScene(SceneNames.PlaySceneName);

        [MenuItem("Scenes/Load Workshop Scene #3")]
        public static void LoadWorkshopScene() => OpenScene(SceneNames.EditorSceneName);

        private static void OpenScene(string sceneName)
        {
            if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo()) {
                EditorSceneManager.OpenScene(GetScenePath(sceneName), OpenSceneMode.Single);
            }
        }

        private static string GetScenePath(string sceneName)
        {
            return Path + sceneName + SceneSuffix;
        }
    }
}
#endif