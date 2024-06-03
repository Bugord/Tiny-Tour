using System.Collections.Generic;
using Tiles.Options;
using UnityEngine;

namespace Tiles
{
    public interface ITileEditor
    {
        public void OnTileDown(Vector3Int pos);
        public void OnTileUp();
        public void OnTileMove(Vector3Int pos);
        public void OnTileEraseDown(Vector3Int pos);
        public void OnTileEraseMove(Vector3Int pos);
        public List<BaseEditorOption> GetOptions();
        public void SetOption(BaseEditorOption option);
    }
}