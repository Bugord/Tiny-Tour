using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using Game.Common.UI.Editing.EditorOption;
using Game.Gameplay.Editing.Options.Data;
using Game.Workshop.Editing.Core;
using UnityEngine;

namespace Game.Gameplay.Editing.Options.Model
{
    public class BaseEditorOption
    {
        public string Id => EditorOptionData.Id;
        
        protected EditorOptionData EditorOptionData;
        protected EditorOptionUI EditorOptionUI;

        protected void SetupUI(ILevelEditorController levelEditorController)
        {
            EditorOptionUI = levelEditorController.AddEditorOptionUI(EditorOptionData.Id);
            EditorOptionUI.SetVisuals(EditorOptionData.DefaultIcon);

            EditorOptionUI.AlternativeSelected += OnAlternativeSelected;
            EditorOptionUI.ColorSelected += OnColorSelected;
        }

        protected void SetCustomBorders(Sprite activeBorderSprite, Sprite inactiveBorderSprite)
        {
            EditorOptionUI.SetVisuals(EditorOptionData.DefaultIcon, activeBorderSprite, inactiveBorderSprite);
        }

        protected void SetAlternatives<T>(Dictionary<T, Sprite> alternatives) where T : Enum
        {
            EditorOptionUI.SetAlternatives(
                alternatives.ToDictionary(
                    alternative => (int)(object)alternative.Key,
                    alternative => alternative.Value));
        }

        protected virtual void OnAlternativeSelected(int alternativeId)
        {
        }
        
        protected virtual void OnColorSelected(TeamColor color)
        {
        }

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