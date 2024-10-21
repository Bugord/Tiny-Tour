using Game.Common.Level.Data;
using Game.Main.Session.Models;

namespace Game.Main.Session.Core
{
    public class SessionManger : ISessionManger
    {
        public GameSession CurrentSession { get; private set; }

        public void StartSession(LevelData levelData)
        {
            CurrentSession = new GameSession(levelData);
        }
    }
}