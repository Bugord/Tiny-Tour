using System;
using Cysharp.Threading.Tasks;
using Game.Common.Level.Data;

namespace Game.Main.Workshop
{
    public interface IWorkshopService
    {
        event Action TestLevelStarted;
        event Action TestLevelEnded;
        LevelData EditedLevelData { get; }
        void SetLevelData(LevelData levelData);
        UniTask LoadWorkshop();
        void UnloadWorkshop();
        UniTask LoadLevelTest();
        void UnloadLevelTest();
        event Action LevelEditingEnded;
        void Clear();
    }
}