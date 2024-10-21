using Application.GameState.Systems;
using UnityEngine;
using Zenject;

namespace Game.Main.Installers.Project
{
    [CreateAssetMenu(fileName = "installer_game_state", menuName = "Installers/Project/Game State")]
    public class GameStateInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IGameStateFactory>().To<GameStateFactory>().AsTransient();
            Container.Bind<GameStateMachine>().AsSingle();
        }
    }
}