using Common;
using Common.Editors.Logistic;
using Gameplay.Editing.Editors;
using Gameplay.Editing.Editors.Terrain;
using Gameplay.Editing.Options.Data;
using UnityEngine;
using Zenject;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "installer_playing", menuName = "Installers/Playing")]
    public class PlayingInstaller : ScriptableObjectInstaller
    {
        [SerializeField]
        private EditorOptionDataLibrary editorOptionDataLibrary;
        
        public override void InstallBindings()
        {
            // Container.Bind<InGameTilemapEditor>().AsSingle();
            Container.BindInterfacesTo<PlayLevelService>().AsSingle().NonLazy();
            Container.Bind<ILevelLoader>().To<LevelLoader>().AsSingle();

            Container.Bind<IRoadEditor>().To<RoadEditor>().AsSingle();
            Container.Bind<ITerrainEditor>().To<TerrainEditor>().AsSingle();
            Container.Bind<IGoalEditor>().To<GoalEditor>().AsSingle();
            
            Container.Bind<IEditorOptionFactory>().To<EditorOptionOptionFactory>().AsTransient();
            Container.BindInterfacesAndSelfTo<InGameEditor>().AsSingle();

            Container.Bind<EditorOptionDataLibrary>().FromInstance(editorOptionDataLibrary);
        }
    }
}