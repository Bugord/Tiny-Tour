using AYellowpaper.SerializedCollections;
using Tiles.Ground;
using UnityEngine;

namespace Game.Gameplay.Editing.Options.Data
{
    [CreateAssetMenu(fileName = "data_editor_terrain", menuName = "Data/Editor Option/Terrain Data")]
    public class TerrainEditorOptionData : EditorOptionData
    {
        [field: SerializeField]
        public SerializedDictionary<TerrainType, Sprite> AlternativeTerrains { get; private set; }
    }
}