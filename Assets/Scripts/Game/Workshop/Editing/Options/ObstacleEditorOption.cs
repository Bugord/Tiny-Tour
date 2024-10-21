using System.Linq;
using Common.Editors.Obstacles;
using Common.Editors.Terrain;
using Core;
using Game.Common.Cars.Core;
using Game.Common.Obstacles;
using Game.Common.UI.Editing.EditorOption;
using Game.Gameplay.Editing.Options.Data;
using Game.Gameplay.Editing.Options.Model;
using Tiles.Ground;
using UnityEngine;

namespace Game.Workshop.Editing.Options
{
    public class ObstacleEditorOption : BaseEditorOption
    {
        private readonly IObstaclesEditor obstaclesEditor;
        private readonly ITerrainEditor terrainEditor;
        private readonly ObstacleEditorOptionData obstacleEditorOptionData;

        public ObstacleEditorOption(EditorOptionUI editorOptionUI, EditorOptionDataLibrary editorOptionDataLibrary,
            IObstaclesEditor obstaclesEditor, ITerrainEditor terrainEditor) : base(editorOptionUI, editorOptionDataLibrary.ObstacleEditorOptionData)
        {
            this.obstaclesEditor = obstaclesEditor;
            this.terrainEditor = terrainEditor;
            obstacleEditorOptionData = editorOptionDataLibrary.ObstacleEditorOptionData;
            
            EditorOptionsConfiguration.EnableColorPicker();
            SetObstacleAlternatives();
        }
        
        protected override void OnColorSelected(TeamColor color)
        {
            SetObstacleAlternatives();
            UpdateSprite();
        }

        protected override void OnAlternativeSelected(int alternativeId)
        {
            UpdateSprite();
        }
        
        private void SetObstacleAlternatives()
        {
            var selectedColor = EditorOptionsConfiguration.SelectedColor;
            
            var mappedAlternatives = obstacleEditorOptionData.ObstacleSprites
                .ToDictionary(data => data.Key,
                    data => data.Value.GetColoredObstacleVariant(selectedColor));

            EditorOptionsConfiguration.SetAlternatives(mappedAlternatives);
        }
        
        private void UpdateSprite()
        {
            var color = EditorOptionsConfiguration.SelectedColor;
            var obstacleType = (ObstacleType)EditorOptionsConfiguration.SelectedAlternativeIndex;

            var icon = obstacleEditorOptionData.ObstacleSprites[obstacleType].GetColoredObstacleVariant(color);
            EditorOptionUI.SetIcon(icon);
        }
        
        public override void OnTileDown(Vector2Int position)
        {
            SetObstacle(position);
        }

        public override void OnTileDrag(Vector2Int position)
        {
            SetObstacle(position);
        }

        public override void OnAltTileDown(Vector2Int position)
        {
            obstaclesEditor.EraseTile(position);
        }

        public override void OnAltTileDrag(Vector2Int position)
        {
            obstaclesEditor.EraseTile(position);
        }

        private void SetObstacle(Vector2Int position)
        {
            if (!terrainEditor.HasSolidTile(position)) {
                return;
            }
            
            obstaclesEditor.SetObstacleTile(position, EditorOptionsConfiguration.SelectedColor, (ObstacleType)EditorOptionsConfiguration.SelectedAlternativeIndex);
        }
    }
}