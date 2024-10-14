using System;
using Core.UI;
using UnityEngine;

namespace Game.Common.UI.Editing.EditorOption.AlternativePicker
{
    [RequireComponent(typeof(Button))]
    public class EditorOptionAlternative : MonoBehaviour
    {
        public event Action<int> OptionSelected;

        [SerializeField]
        private Button button;
        
        private int id;

        public void SetData(int id, Sprite sprite)
        {
            this.id = id;
            button.ButtonSprites = new ButtonSprites {
                releasedSprite = sprite
            };
        }

        private void OnEnable()
        {
            button.ClickedLeft += OnButtonClickedLeft;
        }

        private void OnDisable()
        {
            button.ClickedLeft -= OnButtonClickedLeft;
        }

        private void OnButtonClickedLeft()
        {
            OptionSelected?.Invoke(id);
        }
    }
}