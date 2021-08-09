using Game.Management;
using Game.Services;
using MySql.Data.MySqlClient;
using UnityEngine;

namespace Game.Entities
{
    class Location
    {
        private string _name;
        private GameObject _view;
        private int _planetId;
        private int _id;
        private Shop _shop;

        public string Name { get => _name; set => _name = value; }
        public GameObject View { get => _view; set => _view = value; }
        public int PlanetId { get => _planetId; set => _planetId = value; }
        public int Id { get => _id; set => _id = value; }
        public Shop Shop { get => _shop; set => _shop = value; }

        public Location(int id, string name, GameObject view, int planetId)
        {
            _id = id;
            _name = name;
            _view = view;
            _planetId = planetId;
            using (MySqlConnection connection = new MySqlConnection(DatabaseInformation.ConnectionString))
            {
                connection.Open();
                string query = string.Format("SELECT shop.id, shop.Name, shop.Revenue " +
                    "FROM shop INNER JOIN location ON shop.LocationId = location.id " +
                    "WHERE shop.LocationId = @id");
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", _id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    try
                    {
                        _shop = new Shop((int)reader[0], (string)reader[1], _id, (int)reader[2]);
                    }
                    catch
                    {

                    }
                }
            }
        }
    }
}
