using Game.Entities;
using Game.Management;
using Game.Management.Repositories;
using UnityEngine;
using UnityEngine.UI;

namespace Game.View.ActionsHandlers.PlanetEvents
{
    public class SpaceportEvents : MonoBehaviour
    {
        [SerializeField] private GameObject _planetInformation;
        [SerializeField] private GameObject _errorMessage;
        [SerializeField] private GameObject _spaceport;
        [SerializeField] private Text _errorText;
        [SerializeField] private Text _planetName;
        [SerializeField] private Text _planetDescription;
        [SerializeField] private Text _travellButtonText;

        private Player _player;
        private Planet _hutta;
        private Planet _dantooine;
        private Planet _korriban;
        private Planet _tatooine;
        private Planet _narShaddaa;
        private Planet _selectedPlanet;

        private void Start()
        {
            _player = (Player)DataHolder.Data;
            _hutta = PlanetsRepository.Planets[0];
            _dantooine = PlanetsRepository.Planets[1];
            _korriban = PlanetsRepository.Planets[2];
            _tatooine = PlanetsRepository.Planets[3];
            _narShaddaa = PlanetsRepository.Planets[4];
        }
        public void SelectHutta()
        {
            _selectedPlanet = _hutta;
            PreparePlanetInformationView();
        }
        public void SelectKorriban()
        {
            _selectedPlanet = _korriban;
            PreparePlanetInformationView();
        }
        public void SelectDantooine()
        {
            _selectedPlanet = _dantooine;
            PreparePlanetInformationView();
        }
        public void SelectTatooine()
        {
            _selectedPlanet = _tatooine;
            PreparePlanetInformationView();
        }
        public void SelectNarShaddaa()
        {
            _selectedPlanet = _narShaddaa;
            PreparePlanetInformationView();
        }
        public void Travell()
        {
            if (PlanetsRepository.Planets.IndexOf(_selectedPlanet) == _player.GetPlanetIndex())
            {
                _errorText.text = "Перелет невозможен. Вы уже на этой планете.";
                _errorMessage.SetActive(true);
            }
            else if (_player.Credits < _selectedPlanet.TravellCost)
            {
                _errorText.text = "Перелет невозможен. Недостаточно кредитов.";
                _errorMessage.SetActive(true);
            }
            else
            {
                _player.Credits -= _selectedPlanet.TravellCost;
                _selectedPlanet.Treasury += _selectedPlanet.TravellCost;
                _selectedPlanet.UpdateTreasuryInDatabase();
                _player.Planet.View.SetActive(false);
                _player.Location.View.SetActive(false);
                _player.LocationId = _selectedPlanet.Locations[0].Id;
                _player.SetPlanet();
                _spaceport.SetActive(false);
                _planetInformation.SetActive(false);
                _player.Planet.View.SetActive(true);
                _player.Location.View.SetActive(true);
                PlayerInformationVisualisator.UpdateView();
            }
        }
        public void CancelDeparture()
        {
            _selectedPlanet = null;
            _planetInformation.SetActive(false);
        }
        private void PreparePlanetInformationView()
        {
            _planetName.text = _selectedPlanet.Name;
            _planetDescription.text = _selectedPlanet.Descriprion;
            _planetInformation.SetActive(true);
            _travellButtonText.text = $"Отправиться ({_selectedPlanet.TravellCost} кредитов)";
        }
    }
}
