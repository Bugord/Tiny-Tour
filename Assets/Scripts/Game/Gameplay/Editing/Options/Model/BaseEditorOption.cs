using Gameplay.Editing.Options.Data;
using UnityEngine;

namespace Game.Gameplay.Editing.Options.Model
{
    public class BaseEditorOption
    {
        public EditorOptionData EditorOptionData;

        public virtual void OnTileDown(Vector2Int position)
        {
        }

        public virtual void OnTileDrag(Vector2Int position)
        {
        }

        public virtual void OnTileUp(Vector2Int position)
        {
        }
        
        public virtual void OnAltTileDown(Vector2Int position)
        {
        }

        public virtual void OnAltTileDrag(Vector2Int position)
        {
        }

        public virtual void OnAltTileUp(Vector2Int position)
        {
        }
    }
}