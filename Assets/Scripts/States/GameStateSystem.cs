using System;
using Level;
using Level.Data;
using Tiles.Editing.Workshop;
using UI;
using UnityEngine;

namespace States
{
    public class GameStateSystem : MonoBehaviour
    {
        [SerializeField]
        private NavigationSystem navigationSystem;

        [SerializeField]
        private LevelManager levelManager;
        
        public MainMenuState MainMenuState { get; private set; }
        public SelectLevelToPlayState SelectLevelToPlayState { get; private set; }
        public SelectLevelToEditState SelectLevelToEditState { get; private set; }
        public EditLevelState EditLevelState { get; private set; }
        public PlayLevelState PlayLevelState { get; private set; }
        public TestPlayLevelState TestPlayLevelState { get; private set; }

        private BaseGameState currentState;

        public void Init()
        {
            MainMenuState = new MainMenuState(this, navigationSystem);
            SelectLevelToPlayState = new SelectLevelToPlayState(this, navigationSystem, levelManager);
            SelectLevelToEditState = new SelectLevelToEditState(this, navigationSystem, levelManager);
            EditLevelState = new EditLevelState(this, navigationSystem, levelManager);
            PlayLevelState = new PlayLevelState(this, navigationSystem, levelManager);
            TestPlayLevelState = new TestPlayLevelState(this, navigationSystem, levelManager);
        }

        public void ChangeState(BaseGameState gameState)
        {
            currentState?.OnExit();
            currentState = gameState;
            currentState?.OnEnter();
        }
    }
}