using Game.Entities;
using Game.Management;
using Game.Management.Repositories;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Game.View.Scenes
{
    public class MainScene : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _planetsViews;
        [SerializeField] private List<GameObject> _locationsViews;
        [SerializeField] private Text _infoLabel;
        [SerializeField] private GameObject _exitConfirmation;
        [SerializeField] private GameObject _playersInformation;
        [SerializeField] private Image _faceImage;
        [SerializeField] private Text _nameLabel;
        [SerializeField] private Text _creditsLabel;
        [SerializeField] private Text _planetLabel;
        [SerializeField] private Text _locationLabel;
        [SerializeField] private Text _prestigeLabel;
        [SerializeField] private Text _wisdomLabel;
        [SerializeField] private Image _medalImage;
        [SerializeField] private GameObject _settings;
        [SerializeField] private GameObject _spaceport;
        [SerializeField] private GameObject _rewardMessage;
        [SerializeField] private GameObject _inventoryView;
        [SerializeField] private GameObject _lackOfCardsMessage;
        [SerializeField] private GameObject _getAmountMessage;
        [SerializeField] private GameObject _tutorial;

        private Player _currentPlayer;
        private const int DailyReward = 5000;

        private void Awake()
        {
            using (MySqlConnection connection = new MySqlConnection(DatabaseInformation.ConnectionString))
            {
                connection.Open();
                string query = "SELECT id, Name, PlanetId FROM Location";
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
                int i = 0;
                while (reader.Read())
                {
                    LocationsRepository.Locations.Add(new Location((int)reader[0], reader[1].ToString(), _locationsViews[i], (int)reader[2]));
                    i++;
                }
                i = 0;
                foreach (Planet planet in PlanetsRepository.Planets)
                {
                    planet.Locations.AddRange(LocationsRepository.Locations.Where(l => l.PlanetId == planet.Id));
                    planet.View = _planetsViews[i];
                    i++;
                }
                _currentPlayer = (Player)DataHolder.Data;
                _currentPlayer.SetLocation();
                _currentPlayer.SetPlanet();
                _currentPlayer.LoadInventory();
                if (_currentPlayer.LogoutDateTime < DateTime.Today)
                {
                    _currentPlayer.Credits += DailyReward;
                    _rewardMessage.SetActive(true);
                }
                PlayerInformationVisualisator.CreditsLabel = _creditsLabel;
                PlayerInformationVisualisator.FaceImage = _faceImage;
                PlayerInformationVisualisator.LocationLabel = _locationLabel;
                PlayerInformationVisualisator.MedalImage = _medalImage;
                PlayerInformationVisualisator.NameLabel = _nameLabel;
                PlayerInformationVisualisator.PrestigeLabel = _prestigeLabel;
                PlayerInformationVisualisator.WisdomLabel = _wisdomLabel;
                PlayerInformationVisualisator.PlanetLabel = _planetLabel;
                PlayerInformationVisualisator.UpdateView();
                _currentPlayer.Planet.View.SetActive(true);
                _currentPlayer.Location.View.SetActive(true);
                if (_currentPlayer.NeedToShowTutorial)
                {
                    _tutorial.SetActive(true);
                }
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (_spaceport.activeSelf)
                {
                    _spaceport.SetActive(false);
                }
                else
                {
                    _exitConfirmation.SetActive(true);
                }
            }
            if (Input.GetKeyDown(KeyCode.C))
            {
                _playersInformation.SetActive(!_playersInformation.activeSelf);
            }
            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.P))
            {
                _settings.SetActive(true);
            }
            if (Input.GetKeyDown(KeyCode.I))
            {
                _inventoryView.SetActive(!_inventoryView.activeSelf);
            }
            if (Input.GetKey(KeyCode.T))
            {
                _tutorial.SetActive(true);
            }
        }
        public void OpenSpaceport()
        {
            _spaceport.SetActive(true);
        }
        public void StartPazaakGame()
        {
            if (_currentPlayer.CanPlayPazaak())
            {
                _getAmountMessage.SetActive(true);
            }
            else
            {
                _lackOfCardsMessage.SetActive(true);
            }
        }
        public void ExitGame()
        {
            _currentPlayer.SaveInDatabase();
            _currentPlayer.Planet.UpdateTreasuryInDatabase();
            Application.Quit();
        }
    }
}
