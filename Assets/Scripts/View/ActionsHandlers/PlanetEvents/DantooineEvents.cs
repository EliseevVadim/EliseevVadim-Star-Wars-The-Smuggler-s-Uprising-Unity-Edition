using Game.Entities;
using Game.Entities.Items;
using Game.Enums;
using Game.Exceptions;
using Game.Management;
using Game.Management.Repositories;
using Game.Services;
using UnityEngine;
using UnityEngine.UI;

namespace Game.View.ActionsHandlers.PlanetEvents
{
    public class DantooineEvents : MonoBehaviour
    {
        [SerializeField] private GameObject _successLootMessage;
        [SerializeField] private Text _extractionNameField;
        [SerializeField] private Image _extractionIcon;
        [SerializeField] private GameObject _failMessage;
        [SerializeField] private Text _failText;
        [SerializeField] private GameObject _lootErrorMessage;
        [SerializeField] private GameObject _requestDefenceMessage;
        [SerializeField] private GameObject _successDefenceMessage;
        [SerializeField] private GameObject _errorDefenceMessage;
        [SerializeField] private Text _errorText;

        private Planet _currentPlanet;
        private Player _currentPlayer;

        private const int RequiredWisdom = 15000;

        private void Start()
        {
            _currentPlanet = PlanetsRepository.Planets[1];
            _currentPlayer = (Player)DataHolder.Data;
        }
        public void GoToRuins()
        {
            _currentPlayer.LocationId = _currentPlanet.Locations[1].Id;
            _currentPlanet.Locations[0].View.SetActive(false);
            _currentPlanet.Locations[1].View.SetActive(true);
            PlayerInformationVisualisator.UpdateView();
        }
        public void GoToTheTemple()
        {
            _currentPlayer.LocationId = _currentPlanet.Locations[2].Id;
            _currentPlanet.Locations[0].View.SetActive(false);
            _currentPlanet.Locations[2].View.SetActive(true);
            PlayerInformationVisualisator.UpdateView();
        }
        public void ReturnFromTheTemple()
        {
            _currentPlayer.LocationId = _currentPlanet.Locations[0].Id;
            _currentPlanet.Locations[2].View.SetActive(false);
            _currentPlanet.Locations[0].View.SetActive(true);
            PlayerInformationVisualisator.UpdateView();
        }
        public void ReturnToEnclaveEntrance()
        {
            _currentPlayer.LocationId = _currentPlanet.Locations[0].Id;
            _currentPlanet.Locations[1].View.SetActive(false);
            _currentPlanet.Locations[0].View.SetActive(true);
            PlayerInformationVisualisator.UpdateView();
        }
        public void Loot()
        {
            if (_currentPlayer.HasA(ItemsRepository.JediItems[1]))
            {
                LootPlace lootPlace = new LootPlace(LootPlaceType.JediRuins);
                try
                {
                    Item extraction = lootPlace.Loot();
                    _currentPlayer.Inventory.AddItem(extraction);
                    _extractionIcon.sprite = extraction.Image;
                    _extractionNameField.text = extraction.Name;
                    _successLootMessage.SetActive(true);
                }
                catch (FailedLootException ex)
                {
                    _currentPlayer.Inventory.RemoveItem(ItemsRepository.JediItems[1]);
                    _failText.text = ex.Message;
                    _failMessage.SetActive(true);
                }
            }
            else
            {
                _lootErrorMessage.SetActive(true);
            }
        }
        public void AcceptProtection()
        {
            _requestDefenceMessage.SetActive(false);
            if (_currentPlayer.StoryFinished)
            {
                _errorText.text = "????? ???? ??? ????????.";
                _errorDefenceMessage.SetActive(true);
            }
            else if (_currentPlayer.WisdomPoints < RequiredWisdom)
            {
                _errorText.text = "???????????? ????? ????????.";
                _errorDefenceMessage.SetActive(true);
            }
            else
            {
                _currentPlayer.StoryFinished = true;
                PlayerInformationVisualisator.UpdateView();
                _successDefenceMessage.SetActive(true);
            }
        }
    }
}