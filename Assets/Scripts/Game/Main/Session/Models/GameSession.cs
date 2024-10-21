using Game.Common.Level.Data;

namespace Game.Main.Session.Models
{
    public class GameSession
    {
        public LevelData LevelData { get; private set; }

        public GameSession(LevelData levelData)
        {
            LevelData = levelData;
        }
    }
}