using System;
using System.Collections.Generic;
using System.Linq;
using Level;
using Level.Data;
using TMPro;
using UnityEngine;

public class LevelManagerUI : MonoBehaviour
{
    public event Action SavePressed;
    public event Action<string> SaveAsPressed;

    public event Action<int> LoadWorkshopPressed;
    public event Action<int> LoadInGamePressed;
    
    [SerializeField]
    private TMP_Dropdown levelsDropdown;

    [SerializeField]
    private TMP_InputField levelNameInput;
    
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

    public void OnLoadWorkshopPressed()
    {
        LoadWorkshopPressed?.Invoke(levelsDropdown.value);
    }
    
    public void OnLoadInGamePressed()
    {
        LoadInGamePressed?.Invoke(levelsDropdown.value);
    }
}
