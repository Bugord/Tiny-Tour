using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Core.UI.Button
{
    [AddComponentMenu("Custom UI/Button", 30)]
    [RequireComponent(typeof(Image))]
    public class Button : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
    {
        public event UnityAction ClickedLeft;
        public event UnityAction ClickedRight;
        public event UnityAction ClickedMiddle;

        [SerializeField]
        private ButtonSprites buttonSprites;
        private Image image;

        private ButtonState state;

        public ButtonSprites ButtonSprites {
            get => buttonSprites;
            set {
                buttonSprites = value;
                UpdateSprites();
            }
        }

        private void Awake()
        {
            image = GetComponent<Image>();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            switch (eventData.button) {
                case PointerEventData.InputButton.Left:
                    OnLeftMouseClicked();
                    break;
                case PointerEventData.InputButton.Right:
                    OnRightMouseClicked();
                    break;
                case PointerEventData.InputButton.Middle:
                    OnMiddleMouseClicked();
                    break;
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            ChangeState(ButtonState.Pressed);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            ChangeState(ButtonState.Released);
        }

        protected virtual void OnLeftMouseClicked()
        {
            ClickedLeft?.Invoke();
        }

        protected virtual void OnRightMouseClicked()
        {
            ClickedRight?.Invoke();
        }

        protected virtual void OnMiddleMouseClicked()
        {
            ClickedMiddle?.Invoke();
        }

        protected virtual void ChangeState(ButtonState state)
        {
            this.state = state;
            UpdateSprites();
        }

        private void UpdateSprites()
        {
            switch (state) {
                case ButtonState.Released:
                    image.sprite = buttonSprites.releasedSprite;
                    break;
                case ButtonState.Pressed:
                    image.sprite = buttonSprites.pressedSprite;
                    break;
                case ButtonState.Disabled:
                    image.sprite = buttonSprites.disabledSprite;
                    break;
            }
        }
    }
}