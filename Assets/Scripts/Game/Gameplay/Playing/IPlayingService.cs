using System;

namespace Gameplay.Playing
{
    public interface IPlayingService
    {
        void Play();
        void ResetPlay();
        event Action PlayEnded;
        void CancelPlay();
        bool IsPlaying { get; }
    }
}