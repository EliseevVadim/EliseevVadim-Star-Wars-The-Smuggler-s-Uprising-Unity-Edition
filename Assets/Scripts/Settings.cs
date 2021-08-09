using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class Settings : MonoBehaviour
    {
        private bool _screenMaximized = true;
        private Resolution[] _resolutions;
        private List<string> _resolutionsNames;
        [SerializeField] private Dropdown _dropdown;
        [SerializeField] private GameObject _settings;

        public void Start()
        {
            _resolutionsNames = new List<string>();
            _resolutions = Screen.resolutions;
            foreach (var i in Screen.resolutions)
            {
                _resolutionsNames.Add($"{i.width}x{i.height}");
            }
            _dropdown.ClearOptions();
            _dropdown.AddOptions(_resolutionsNames);
            Resolution currentResolution = Screen.currentResolution;
            _dropdown.value = _resolutionsNames.IndexOf($"{currentResolution.width}x{currentResolution.height}");
        }

        public void ChangeScreen()
        {
            _screenMaximized = !_screenMaximized;
            Screen.fullScreen = _screenMaximized;
        }
        public void SetQuality(int index)
        {
            QualitySettings.SetQualityLevel(index);
        }
        public void SetResolution(int index)
        {
            Screen.SetResolution(_resolutions[index].width, _resolutions[index].height, _screenMaximized);
        }
        public void CloseSettings()
        {
            _settings.SetActive(false);
        }
    }
}
