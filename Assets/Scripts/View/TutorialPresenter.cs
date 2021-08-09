using Game.Entities;
using Game.Management;
using UnityEngine;
using UnityEngine.UI;

namespace Game.View
{
    class TutorialPresenter : MonoBehaviour
    {
        [SerializeField] private GameObject _tutorial;
        [SerializeField] private Toggle _hideForever;
        [SerializeField] private Text _infoField;

        private Player _currentPlayer = (Player)DataHolder.Data;


        public void Hide()
        {
            if (_hideForever.isOn)
            {
                _currentPlayer.NeedToShowTutorial = false;
            }
            _tutorial.SetActive(false);
        }
        public void ExpandRecord(Button sender)
        {
            _infoField.text = TutorialDataHandler.TutorialChapters[int.Parse(sender.name)];
        }
    }
}
