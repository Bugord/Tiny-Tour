using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Core.UI
{
    public class SelectorView : MonoBehaviour, IDeselectHandler
    {
        public event Action<int> OptionSelected;

        private readonly List<SelectorViewOption> selectorViewOptions = new List<SelectorViewOption>();
        
        public void OnDeselect(BaseEventData eventData)
        {
            //gameObject.SetActive(false);
        }

        public void SetOptions(Dictionary<int, Sprite> options)
        {
            ClearOptions();
            
            foreach (var option in options) {
                AddOption(option.Key, option.Value);
            }
        }

        private void AddOption(int id, Sprite sprite)
        {
            var buttonOptionGO = new GameObject("Option");
            var image = buttonOptionGO.AddComponent<Image>();
            buttonOptionGO.AddComponent<Button>();
            var selectorViewOption = buttonOptionGO.AddComponent<SelectorViewOption>();
                
            buttonOptionGO.transform.SetParent(transform);

            image.sprite = sprite;
            selectorViewOption.Id = id;
            selectorViewOption.OptionPressed += OptionSelected;

            selectorViewOptions.Add(selectorViewOption);
        }

        private void ClearOptions()
        {
            foreach (var selectorViewOption in selectorViewOptions) {
                selectorViewOption.OptionPressed -= OnOptionSelected;
                Destroy(selectorViewOption.gameObject);
            }
            
            selectorViewOptions.Clear();
        }

        private void OnOptionSelected(int id)
        {
            //OnDeselected
            gameObject.SetActive(false);
            OptionSelected?.Invoke(id);
        }
    }
}