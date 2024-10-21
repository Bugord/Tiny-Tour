using Game.Common.Level.Data;
using Game.Main.Session.Models;

namespace Game.Main.Session.Core
{
    public class SessionManger : ISessionManger
    {
        public GameSession GameSession { get; private set; }

        public void StartSession(LevelData levelData)
        {
            GameSession = new GameSession(levelData);
        }
    }
}