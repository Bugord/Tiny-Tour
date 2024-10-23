using Common.Road;
using Game.Gameplay.Core;
using Game.Gameplay.Editing;
using Game.Gameplay.Level;
using Game.Gameplay.Logistic;
using Game.Gameplay.Pathfinding;
using Game.Gameplay.Playing;
using Game.Gameplay.PlayState.Core;
using Game.Gameplay.Utility;
using UnityEngine;
using Zenject;

namespace Game.Gameplay.Installers
{
    [CreateAssetMenu(fileName = "installer_playing", menuName = "Installers/Playing")]
    public class PlayingInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            // Container.Bind<InGameTilemapEditor>().AsSingle();
            Container.BindInterfacesTo<PlayEntryPoint>().AsSingle().NonLazy();
            
            Container.Bind<PlayStateMachine>().AsSingle();
            Container.BindInterfacesTo<PlayStateFactory>().AsTransient();
            Container.Bind<ILevelService>().To<PlayLevelService>().AsSingle();

            Container.BindInterfacesAndSelfTo<PlayEditorController>().AsSingle();
            
            Container.BindInterfacesTo<RoadTilemapGridConverter>().AsSingle();

            Container.BindInterfacesTo<PlayService>().AsSingle();
            
            Container.BindInterfacesTo<PathfindingService>().AsSingle();
            Container.BindInterfacesTo<LogisticService>().AsSingle();
            Container.BindInterfacesTo<RoadService>().AsSingle();
            Container.BindInterfacesTo<PlayRunningService>().AsSingle();
        }
    }
}