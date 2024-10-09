using UnityEngine;

namespace Core.LevelEditing
{
    public interface ILevelEditor<T> where T : BaseTileData
    {
        void SetTile(T tileData);
        void EraseTile(Vector2Int position);
        bool HasTile(Vector2Int position);
        void Load(T[] tilesData);
        T[] GetTilesData();
        void Clear();
        void Reset();
    }
}