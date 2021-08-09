using Game.Entities;
using Game.Management;
using Game.Management.Repositories;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.View
{
    static class PlayerInformationVisualisator
    {
        private static Image _faceImage;
        private static Text _nameLabel;
        private static Text _creditsLabel;
        private static Text _planetLabel;
        private static Text _locationLabel;
        private static Text _prestigeLabel;
        private static Text _wisdomLabel;
        private static Image _medalImage;
        private static Player _player;
        private static Sprite _medal;

        static PlayerInformationVisualisator()
        {
            _player = (Player)DataHolder.Data;
            string path = Environment.CurrentDirectory.Replace(@"\Builds", "") + @"\Assets\Images\Icons\glory-medal.png";
            _medal = SpritesLoader.LoadNewSprite(path);
        }

        public static Image FaceImage { get => _faceImage; set => _faceImage = value; }
        public static Text NameLabel { get => _nameLabel; set => _nameLabel = value; }
        public static Text CreditsLabel { get => _creditsLabel; set => _creditsLabel = value; }
        public static Text PlanetLabel { get => _planetLabel; set => _planetLabel = value; }
        public static Text LocationLabel { get => _locationLabel; set => _locationLabel = value; }
        public static Text PrestigeLabel { get => _prestigeLabel; set => _prestigeLabel = value; }
        public static Text WisdomLabel { get => _wisdomLabel; set => _wisdomLabel = value; }
        public static Image MedalImage { get => _medalImage; set => _medalImage = value; }

        public static void UpdateView()
        {
            _faceImage.sprite = _player.Avatar;
            _nameLabel.text = _player.Nickname;
            _creditsLabel.text = _player.Credits.ToString();
            _planetLabel.text = PlanetsRepository.Planets[_player.GetPlanetIndex()].Name;
            _locationLabel.text = LocationsRepository.Locations[_player.LocationId - 1].Name;
            _prestigeLabel.text = _player.Prestige.ToString();
            _wisdomLabel.text = _player.WisdomPoints.ToString();
            if (!_player.StoryFinished)
            {
                _medalImage.sprite = null;
                _medalImage.color = Color.clear;
            }
            else
            {
                _medalImage.sprite = _medal;
                _medalImage.color = Color.white;
            }
        }
    }
}
