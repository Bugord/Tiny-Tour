using Game.Common.Level.Data;

namespace Game.Main.Session.Core
{
    public interface ISessionManger
    {
        void StartSession(LevelData levelData);
    }
}