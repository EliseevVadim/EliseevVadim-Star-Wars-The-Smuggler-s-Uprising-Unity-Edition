using Game.Entities.Items;
using Game.Entities.Items.Cards;
using Game.Management;
using Game.Management.Repositories;
using MySql.Data.MySqlClient;
using System;
using UnityEngine;

namespace Game.Entities
{
    class Player : IDatabaseAgent
    {
        private int _id;
        private string _nickname;
        private string _login;
        private string _password;
        private Sprite _avatar;
        private long _credits;
        private int _prestige;
        private int _wisdomPoints;
        private int _locationId;
        private bool _storyFinished;
        private bool _needToShowTutorial;
        private int _avatarIndex;
        private Location _location;
        private Planet _planet;
        private DateTime _logoutDateTime;
        private Inventory _inventory;

        public string Nickname { get => _nickname; set => _nickname = value; }
        public string Login { get => _login; set => _login = value; }
        public string Password { get => _password; set => _password = value; }
        public Sprite Avatar { get => _avatar; set => _avatar = value; }
        public long Credits { get => _credits; set => _credits = value; }
        public int Prestige { get => _prestige; set => _prestige = value; }
        public int WisdomPoints { get => _wisdomPoints; set => _wisdomPoints = value; }
        public int LocationId
        {
            get
            {
                return _locationId;
            }
            set
            {
                _locationId = value;
                SetLocation();
            }
        }
        public bool StoryFinished { get => _storyFinished; set => _storyFinished = value; }
        public int AvatarIndex { get => _avatarIndex; set => _avatarIndex = value; }
        internal Location Location { get => _location; set => _location = value; }
        internal Planet Planet { get => _planet; set => _planet = value; }
        public DateTime LogoutDateTime { get => _logoutDateTime; set => _logoutDateTime = value; }
        public Inventory Inventory { get => _inventory; set => _inventory = value; }
        public int Id { get => _id; set => _id = value; }
        public bool NeedToShowTutorial { get => _needToShowTutorial; set => _needToShowTutorial = value; }

        public Player(int id, string nickname, string login, string password, long credits, int prestige, int wisdomPoints, int locationId, int avatarIndex, bool storyFinished, bool showTutorial)
        {
            _id = id;
            _nickname = nickname;
            _login = login;
            _password = password;
            _avatarIndex = avatarIndex;
            _credits = credits;
            _prestige = prestige;
            _wisdomPoints = wisdomPoints;
            _locationId = locationId;
            _storyFinished = storyFinished;
            _avatar = AvatarsRepository.Avatars[_avatarIndex];
            _needToShowTutorial = showTutorial;
        }

        public int GetPlanetIndex()
        {
            return LocationsRepository.Locations[_locationId - 1].PlanetId - 1;
        }
        public void LoadInventory()
        {
            _inventory = new Inventory(_id, 30);
        }
        public void AddToDatabase()
        {
            using (MySqlConnection connection = new MySqlConnection(DatabaseInformation.ConnectionString))
            {
                connection.Open();
                string query = string.Format("INSERT INTO Player (Nickname, Login, Password, AvatarIndex, LocationId, Credits) " +
                    "VALUES (@name, @login, @password, @avatarIndex, 1, @credits)");
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@name", _nickname);
                command.Parameters.AddWithValue("@login", _login);
                command.Parameters.AddWithValue("@password", _password);
                command.Parameters.AddWithValue("@avatarIndex", _avatarIndex);
                command.Parameters.AddWithValue("@credits", _credits);
                command.ExecuteNonQuery();
                int id = (int)command.LastInsertedId;
                query = string.Format("INSERT INTO Inventory (PlayersId) VALUES (@id)");
                command.CommandText = query;
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();
            }
        }
        public void SetLocation()
        {
            _location = LocationsRepository.Locations[_locationId - 1];
        }
        public void SetPlanet()
        {
            _planet = PlanetsRepository.Planets[GetPlanetIndex()];
        }
        public bool HasA(Item item)
        {
            return _inventory.Contains(item);
        }
        public bool CanPlayPazaak()
        {
            int cardsCount = 0;
            foreach (InventoryCell cell in _inventory.Cells)
            {
                if (cell.Content is Card)
                {
                    cardsCount += cell.Count;
                }
            }
            return cardsCount >= 4;
        }
        public void SaveInDatabase()
        {
            _logoutDateTime = DateTime.Now;
            using (MySqlConnection connection = new MySqlConnection(DatabaseInformation.ConnectionString))
            {
                connection.Open();
                string query = string.Format("UPDATE Player SET " +
                    "Credits = @credits, " +
                    "Prestige = @prestige, " +
                    "WisdomPoints = @wisdom, " +
                    "LocationId = @locationId, " +
                    "LogoutDateTime = @logout, " +
                    "StoryFinished = @storyFinished, " +
                    "ShowTutorial = @needToShow " +
                    "WHERE Nickname = @name");
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@credits", _credits);
                command.Parameters.AddWithValue("@prestige", _prestige);
                command.Parameters.AddWithValue("@wisdom", _wisdomPoints);
                command.Parameters.AddWithValue("@locationId", _locationId);
                command.Parameters.AddWithValue("@storyFinished", _storyFinished ? 1 : 0);
                command.Parameters.AddWithValue("@name", _nickname);
                command.Parameters.AddWithValue("@logout", _logoutDateTime);
                command.Parameters.AddWithValue("@needToShow", _needToShowTutorial);
                command.ExecuteNonQuery();
            }
            _inventory.SaveInDatabase();
        }

        public void RemoveFromDatabase()
        {
            throw new NotImplementedException();
        }
    }
}
