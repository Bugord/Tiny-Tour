using System;
using Core;
using Game.Common.UI.Editing.EditorOption;
using Game.Gameplay.Editing.Options.Data;
using UnityEngine;

namespace Game.Gameplay.Editing.Options.Model
{
    public abstract class BaseEditorOption
    {
        public event Action<BaseEditorOption> Selected;
        
        protected readonly EditorOptionsConfiguration EditorOptionsConfiguration;
        protected readonly EditorOptionUI EditorOptionUI;

        private bool isSelected;
        
        protected BaseEditorOption(EditorOptionUI editorOptionUI, EditorOptionData editorOptionData)
        {
            EditorOptionUI = editorOptionUI;
            EditorOptionUI.SetIcon(editorOptionData.DefaultIcon);
            
            EditorOptionsConfiguration = new EditorOptionsConfiguration(editorOptionUI.EditorOptionsConfigurationUI);

            editorOptionUI.LeftClicked += OnLeftClicked;
            editorOptionUI.RightClicked += OnRightClicked;
            
            EditorOptionsConfiguration.ColorSelected += OnColorSelected;
            EditorOptionsConfiguration.AlternativeSelected += OnAlternativeSelected;
        }

        public void OnSelected()
        {
            isSelected = true;
            
            EditorOptionUI.SetToggled(true);
        }

        public void OnDeselected()
        {
            isSelected = false;
            
            EditorOptionUI.SetToggled(false);
            EditorOptionsConfiguration.Close();
        }

        private void OnRightClicked()
        {
            if (!isSelected) {
                Selected?.Invoke(this);
            }
            
            EditorOptionsConfiguration.Toggle();
        }

        private void OnLeftClicked()
        {
            if (!isSelected) {
                Selected?.Invoke(this);
            }
            else {
                EditorOptionsConfiguration.Toggle();
            }
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