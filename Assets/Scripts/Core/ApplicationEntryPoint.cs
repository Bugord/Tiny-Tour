using States;
using UnityEngine;

namespace Core
{
    public class ApplicationEntryPoint : MonoBehaviour
    {
        [SerializeField]
        private GameStateSystem gameStateSystem;
        
        private void Awake()
        {
            gameStateSystem.Init();
            gameStateSystem.ChangeState(gameStateSystem.MainMenuState);
        }
    }
}