using System;
using Game.Common.Level.Data;

namespace Game.Workshop.Core
{
    public interface IWorkshopEditorService
    {
        public event Action<LevelData> TestLeveStarted;
        public event Action LevelEditingEnded;
        
        void EditLevel(LevelData levelData);
        void ResetLevel();
        void SaveLevel();
    }
}