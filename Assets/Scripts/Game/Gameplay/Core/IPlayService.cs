using System;
using Game.Common.Level.Data;
using Game.Main.Session.Models;

namespace Game.Gameplay.Core
{
    public interface IPlayService
    {
        void PlayLevel(LevelData levelData);
        void EndPlaying();
        event Action PlayingStarted;
        event Action PlayingEnded;
    }
}