using Application.GameState.Systems;
using Core.GameState;
using UnityEngine;
using Zenject;

namespace Core.Installers
{
    [CreateAssetMenu(fileName = "installer_game_state", menuName = "Installers/Game State")]
    public class GameStateInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IGameStateFactory>().To<GameStateFactory>().AsTransient();
            Container.Bind<GameStateMachine>().AsSingle();
        }
    }
}