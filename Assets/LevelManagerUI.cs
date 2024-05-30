using System;
using System.Collections.Generic;
using System.Linq;
using Level;
using TMPro;
using UnityEngine;

public class LevelManagerUI : MonoBehaviour
{
    public event Action<int> LevelSelected; 
    public event Action SavePressed;
    public event Action<string> SaveAsPressed;
    
    [SerializeField]
    private TMP_Dropdown levelsDropdown;

    [SerializeField]
    private TMP_InputField levelNameInput;
    
    private void Awake()
    {
        levelsDropdown.onValueChanged.AddListener(OnLevelSelected);
    }

    private void OnDestroy()
    {
        levelsDropdown.onValueChanged.RemoveAllListeners();
    }

    public void SetData(List<LevelData> levelsData)
    {
        levelsDropdown.options = levelsData.Select(level => new TMP_Dropdown.OptionData(level.levelName)).ToList();
    }

    public void SetSelectedLevel(int levelId)
    {
        levelsDropdown.SetValueWithoutNotify(levelId);
    }
    
    public void OnSavePressed()
    {
        SavePressed?.Invoke();
    }

    public void OnSaveAsPressed()
    {
        if (string.IsNullOrWhiteSpace(levelNameInput.text)) {
            return;
        }
        
        SaveAsPressed?.Invoke(levelNameInput.text);
    }

    private void OnLevelSelected(int levelId)
    {
        LevelSelected?.Invoke(levelId);
    }
}
