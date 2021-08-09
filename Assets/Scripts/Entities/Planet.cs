using Game.Management;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Entities
{
    class Planet
    {
        private int _id;
        private string _name;
        private string _descriprion;
        private Sprite _smallIcon;
        private int _travellCost;
        private long _treasury;
        private List<Location> _locations;
        private GameObject _view;

        public Planet(int id, string name, string description, Sprite smallIcon, int travellCost, long treasury)
        {
            _id = id;
            _name = name;
            _descriprion = description;
            _smallIcon = smallIcon;
            _travellCost = travellCost;
            _treasury = treasury;
            _locations = new List<Location>();
        }

        public void UpdateTreasuryInDatabase()
        {
            try
            {
                foreach (Location location in _locations)
                {
                    foreach (ShopSlot slot in location.Shop.Slots)
                    {
                        _treasury += slot.Revenue;
                    }
                }
            }
            catch
            {

            }
            using (MySqlConnection connection = new MySqlConnection(DatabaseInformation.ConnectionString))
            {
                connection.Open();
                string query = string.Format("UPDATE Planet SET Treasury = @treasury WHERE id = @id");
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@treasury", _treasury);
                command.Parameters.AddWithValue("@id", _id);
                command.ExecuteNonQuery();
            }
        }

        public string Name { get => _name; set => _name = value; }
        public Sprite SmallIcon { get => _smallIcon; set => _smallIcon = value; }
        public int TravellCost { get => _travellCost; set => _travellCost = value; }
        public long Treasury { get => _treasury; set => _treasury = value; }
        public int Id { get => _id; set => _id = value; }
        public List<Location> Locations { get => _locations; set => _locations = value; }
        public GameObject View { get => _view; set => _view = value; }
        public string Descriprion { get => _descriprion; set => _descriprion = value; }
    }
}