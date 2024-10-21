using Common.Level.Core;
using Common.Road;
using Game.Gameplay.Editing;
using Game.Gameplay.Level;
using Gameplay;
using Gameplay.Logistic;
using Gameplay.Pathfinding;
using Gameplay.Playing;
using Gameplay.PlayState;
using Gameplay.PlayState.Core;
using Gameplay.Utility;
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
            
            Container.BindInterfacesTo<PathfindingService>().AsSingle();
            Container.BindInterfacesTo<LogisticService>().AsSingle();
            Container.BindInterfacesTo<RoadService>().AsSingle();
            Container.BindInterfacesTo<PlayingService>().AsSingle();
        }
    }
}