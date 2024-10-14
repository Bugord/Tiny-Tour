using System;
using System.Collections.Generic;
using Level.Data;
using UnityEngine;

namespace UI
{
    public class LevelsGridView : MonoBehaviour
    {
        public event Action<int> LevelButtonPressed;

        [SerializeField]
        private LevelButtonView levelButtonViewPrefab;

        [SerializeField]
        private Transform levelButtonsRoot;

        private List<LevelButtonView> buttonViews;

        public void SetLevels(LevelData[] levelsData)
        {
            buttonViews = new List<LevelButtonView>();
            
            foreach (var levelData in levelsData) {
                var buttonView = Instantiate(levelButtonViewPrefab, levelButtonsRoot);
                buttonView.Init(levelData.levelName, OnButtonPressed);

                buttonViews.Add(buttonView);
            }
        }

        public void OnButtonPressed(LevelButtonView buttonView)
        {
            LevelButtonPressed?.Invoke(buttonViews.IndexOf(buttonView));
        }
    }
}