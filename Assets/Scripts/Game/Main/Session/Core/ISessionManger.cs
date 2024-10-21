using Game.Common.Level.Data;
using Game.Main.Session.Models;

namespace Game.Main.Session.Core
{
    public interface ISessionManger
    {
        void StartSession(LevelData levelData);
        GameSession CurrentSession { get; }
        void EndSession();
    }
}