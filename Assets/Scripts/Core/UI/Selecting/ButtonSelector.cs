using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Core.UI
{
    public class ButtonSelector : Button
    {
        public event Action<int> Selected; 
        
        [SerializeField]
        private SelectorView selectorView;
        
        protected override void OnRightMouseClicked()
        {
            if (selectorView.gameObject.activeSelf) {
                selectorView.gameObject.SetActive(false);
                selectorView.OptionSelected -= Selected;
                EventSystem.current.SetSelectedGameObject(null);
            }
            else {
                selectorView.gameObject.SetActive(true);
                selectorView.OptionSelected += Selected;
                EventSystem.current.SetSelectedGameObject(selectorView.gameObject);
            }
        }

        public void SetOptions(Dictionary<int, Sprite> options)
        {
            selectorView.SetOptions(options);
        }
    }
}