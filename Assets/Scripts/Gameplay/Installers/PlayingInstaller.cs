using Common;
using Common.Editors.Logistic;
using Common.Editors.Road;
using Common.Editors.Terrain;
using Common.Level.Core;
using Gameplay.Editing.Editors;
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
            Container.Bind<ILevelService>().To<LevelService>().AsSingle();

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