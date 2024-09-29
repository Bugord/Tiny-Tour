using UnityEngine;

namespace Gameplay.Editing.Editors
{
    public class BaseEditorOption
    {
        public string ID;

        public virtual void OnTileDown(Vector3Int position)
        {
        }

        public virtual void OnTileDrag(Vector3Int position)
        {
        }

        public virtual void OnTileUp(Vector3Int position)
        {
        }
        
        public virtual void OnAltTileDown(Vector3Int position)
        {
        }

        public virtual void OnAltTileDrag(Vector3Int position)
        {
        }

        public virtual void OnAltTileUp(Vector3Int position)
        {
        }
    }
}