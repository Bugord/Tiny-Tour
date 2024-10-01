using Common;
using Common.Editors.Logistic;
using Gameplay.Editing.Editors;
using Gameplay.Editing.Editors.Terrain;
using Gameplay.Editing.Options.Data;
using Gameplay.Logistic;
using Gameplay.Pathfinding;
using Gameplay.Playing;
using Gameplay.PlayState;
using Gameplay.PlayState.Core;
using Gameplay.Utility;
using Pathfinding;
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
            Container.BindInterfacesTo<PlayEntryPoint>().AsSingle().NonLazy();
            
            Container.Bind<PlayStateMachine>().AsSingle();
            Container.BindInterfacesTo<PlayStateFactory>().AsTransient();
            Container.Bind<ILevelLoader>().To<LevelLoader>().AsSingle();

            Container.Bind<IRoadEditor>().To<RoadEditor>().AsSingle();
            Container.Bind<ITerrainEditor>().To<TerrainEditor>().AsSingle();
            Container.Bind<IGoalEditor>().To<GoalEditor>().AsSingle();
            
            Container.BindInterfacesTo<EditorOptionOptionFactory>().AsTransient();
            Container.BindInterfacesAndSelfTo<InGameEditor>().AsSingle();

            Container.Bind<EditorOptionDataLibrary>().FromInstance(editorOptionDataLibrary);

            Container.BindInterfacesTo<RoadTilemapGridConverter>().AsSingle();
            
            Container.BindInterfacesTo<PathfindingService>().AsSingle();
            Container.BindInterfacesTo<LogisticService>().AsSingle();
            Container.BindInterfacesTo<PlayingService>().AsSingle();
        }
    }
}