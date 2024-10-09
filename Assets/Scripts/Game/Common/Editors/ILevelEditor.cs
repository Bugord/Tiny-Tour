using System.Numerics;

namespace Core.LevelEditing
{
    public interface ILevelEditor
    {
        void SetTile(Vector2 position);
        void EraseTile(Vector2 position);
        void Clear();
    }
}