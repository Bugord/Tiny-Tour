using System;
using Game.Common.Level.Data;

namespace Game.Gameplay.Core
{
    public interface IPlayService
    {
        event Action PlayingExitCalled;
        event Action LevelPassed;
        void PlayLevel(LevelData levelData);
        void RestartLevel();
        void ClearLevel();
        void OnLevelPassed();
    }
}