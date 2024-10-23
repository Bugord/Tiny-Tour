using System;

namespace Game.Gameplay.Playing
{
    public interface IPlayRunningService
    {
        event Action LevelFailed;
        event Action LevelPassed;
        void Play();
        void ResetPlay();
        void CancelPlay();
        bool IsPlaying { get; }
    }
}