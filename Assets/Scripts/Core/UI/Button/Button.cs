using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Core.UI
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
        
        [SerializeField]
        private Image image;

        [SerializeField]
        private UnityEvent unityClickedLeft;
        
        private ButtonState state;

        public ButtonSprites ButtonSprites {
            get => buttonSprites;
            set {
                buttonSprites = value;
                UpdateSprites();
            }
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
            unityClickedLeft?.Invoke();
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
                    image.sprite = buttonSprites.releasedSprite ?? image.sprite;
                    break;
                case ButtonState.Pressed:
                    image.sprite = buttonSprites.pressedSprite ?? image.sprite;
                    break;
                case ButtonState.Disabled:
                    image.sprite = buttonSprites.disabledSprite ?? image.sprite;
                    break;
            }
        }
    }
}