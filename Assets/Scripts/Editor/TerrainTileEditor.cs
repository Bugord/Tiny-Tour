using System;
using Tiles.Ground;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    /// <summary>
    /// The Editor for a TerrainTile.
    /// </summary>
    [CustomEditor(typeof(TerrainTile), true)]
    [CanEditMultipleObjects]
    public class TerrainTileEditor : RuleTileEditor
    {
        private const string WaterIconBase64 =
            "iVBORw0KGgoAAAANSUhEUgAAAA8AAAAPCAYAAAA71pVKAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAAAsSURBVDhPY6AEMIKI0sf//4N5JIBuWUZGJiibLDCqmUQwqplEQJFmCgADAwBOCAQazbgI9AAAAABJRU5ErkJggg==";

        private const string GroundIconBase64 =
            "iVBORw0KGgoAAAANSUhEUgAAAA8AAAAPCAYAAAA71pVKAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAAAsSURBVDhPY6AEMIIIl6MZ/8E8EsAe6xmMTFA2WWBUM4lgVDOJgCLNFAAGBgAfUAQar5o52gAAAABJRU5ErkJggg==";

        private const string WaterOrBridgeBaseIconBase64 =
            "iVBORw0KGgoAAAANSUhEUgAAAA8AAAAPCAYAAAA71pVKAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAAA3SURBVDhPY6AEMIKI0sf//4N5aOD18nUMP+89hPJQwfIZRYxMUDZZYFQziWBUM4mAIs0UAAYGAHdlChrh2lTjAAAAAElFTkSuQmCC";

        private const string GroundOrBridgeBaseIconBase64 =
            "iVBORw0KGgoAAAANSUhEUgAAAA8AAAAPCAYAAAA71pVKAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAAA3SURBVDhPY6AEMIKI0sf//4N5aOD18nUMP+89hPJQwfIZRYxMUDZZYFQziWBUM4mAIs0UAAYGAHdlChrh2lTjAAAAAElFTkSuQmCC";

        private const string BridgeBaseIconBase64 =
            "iVBORw0KGgoAAAANSUhEUgAAAA8AAAAPCAYAAAA71pVKAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAAAsSURBVDhPY6AEMIKIyIy+/2AeCWD5jCJGJiibLDCqmUQwqplEQJFmCgADAwAYaAQawG1sEgAAAABJRU5ErkJggg==";

        /// <summary>
        /// Draws a Sprite field for the Rule
        /// </summary>
        /// <param name="rect">Rect to draw Sprite Inspector in</param>
        /// <param name="tilingRule">Rule to draw Sprite Inspector for</param>
        public override void SpriteOnGUI(Rect rect, RuleTile.TilingRuleOutput tilingRule)
        {
            var newTileRect = new Rect(rect.x, rect.y, 55, 55);
            tilingRule.m_Sprites[0] = EditorGUI.ObjectField(newTileRect, GUIContent.none, tilingRule.m_Sprites[0], typeof(Sprite), false) as Sprite;
        }

        /// <summary>
        /// Draws a neighbor matching rule
        /// </summary>
        /// <param name="rect">Rect to draw on</param>
        /// <param name="position">The relative position of the arrow from the center</param>
        /// <param name="neighbor">The index to the neighbor matching criteria</param>
        public override void RuleOnGUI(Rect rect, Vector3Int position, int neighbor)
        {
            switch (neighbor) {
                case RuleTile.TilingRuleOutput.Neighbor.This:
                    GUI.DrawTexture(rect, arrows[GetArrowIndex(position)]);
                    break;
                case RuleTile.TilingRuleOutput.Neighbor.NotThis:
                    GUI.DrawTexture(rect, arrows[9]);
                    break;
                case TerrainTile.Neighbor.Ground:
                    GUI.DrawTexture(rect, Base64ToTexture(GroundIconBase64));
                    break;
                case TerrainTile.Neighbor.WaterOrBridgeBase:
                    GUI.DrawTexture(rect, Base64ToTexture(WaterOrBridgeBaseIconBase64));
                    break;
                case TerrainTile.Neighbor.GroundOrBridgeBase:
                    GUI.DrawTexture(rect, Base64ToTexture(GroundOrBridgeBaseIconBase64));
                    break;
                case TerrainTile.Neighbor.Water:
                    GUI.DrawTexture(rect, Base64ToTexture(WaterIconBase64));
                    break;
                case TerrainTile.Neighbor.BridgeBase:
                    GUI.DrawTexture(rect, Base64ToTexture(BridgeBaseIconBase64));
                    break;
                default:
                    var style = new GUIStyle();
                    style.alignment = TextAnchor.MiddleCenter;
                    style.fontSize = 10;
                    GUI.Label(rect, neighbor.ToString(), style);
                    break;
            }
        }
    }
}