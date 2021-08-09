using Game.Entities;
using Game.Management;
using Game.Management.Repositories;
using UnityEngine;
using UnityEngine.UI;

namespace Game.View.ActionsHandlers.PlanetEvents
{
    public class TatooineEvents : MonoBehaviour
    {
        [SerializeField] private GameObject _successHuntingMessage;
        [SerializeField] private GameObject _failHuntingMessage;
        [SerializeField] private GameObject _errorHuntingMessage;
        [SerializeField] private Image _perlIconField;

        private Planet _currentPlanet;
        private Player _currentPlayer;

        private const int Chance = 10;

        private void Start()
        {
            _currentPlanet = PlanetsRepository.Planets[3];
            _currentPlayer = (Player)DataHolder.Data;
        }
        public void GoToDesert()
        {
            _currentPlayer.LocationId = _currentPlanet.Locations[1].Id;
            _currentPlanet.Locations[0].View.SetActive(false);
            _currentPlanet.Locations[1].View.SetActive(true);
            PlayerInformationVisualisator.UpdateView();
        }
        public void ReturnToMosEisley()
        {
            _currentPlayer.LocationId = _currentPlanet.Locations[0].Id;
            _currentPlanet.Locations[1].View.SetActive(false);
            _currentPlanet.Locations[0].View.SetActive(true);
            PlayerInformationVisualisator.UpdateView();
        }
        public void HuntTheDragon()
        {
            if (_currentPlayer.HasA(ItemsRepository.QuestItems[0]))
            {
                _currentPlayer.Inventory.RemoveItem(ItemsRepository.QuestItems[0]);
                int coefficient = Random.Range(0, 100);
                if (coefficient <= Chance)
                {
                    _currentPlayer.Inventory.AddItem(ItemsRepository.QuestItems[1]);
                    _perlIconField.sprite = ItemsRepository.QuestItems[1].Image;
                    _successHuntingMessage.SetActive(true);
                }
                else
                {
                    _failHuntingMessage.SetActive(true);
                }
            }
            else
            {
                _errorHuntingMessage.SetActive(true);
            }
        }
    }
}
