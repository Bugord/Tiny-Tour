using System;
using Level;
using Level.Data;
using UI;
using UnityEngine;

namespace States
{
    public class GameStateSystem : MonoBehaviour
    {
        [SerializeField]
        private NavigationSystem navigationSystem;

        [SerializeField]
        private LevelLibrary levelLibrary;

        private ILevelProvider levelProvider;
        
        public MainMenuState MainMenuState { get; private set; }
        public SelectLevelToPlayState SelectLevelToPlayState { get; private set; }
        public SelectLevelToEditState SelectLevelToEditState { get; private set; }

        private BaseGameState currentState;

        public void Init()
        {
            levelProvider = levelLibrary;

            MainMenuState = new MainMenuState(this, navigationSystem);
            SelectLevelToPlayState = new SelectLevelToPlayState(this, navigationSystem, levelProvider);
            SelectLevelToEditState = new SelectLevelToEditState(this, navigationSystem, levelProvider);
        }

        public void ChangeState(BaseGameState gameState)
        {
            currentState?.OnExit();
            currentState = gameState;
            currentState?.OnEnter();
        }
    }
}