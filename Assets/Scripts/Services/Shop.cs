using Game.Entities;
using Game.Entities.Items;
using Game.Entities.Items.Cards;
using Game.Enums;
using Game.Management;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace Game.Services
{
    class Shop
    {
        private int _id;
        private string _name;
        private int _locationId;
        private int _revenue;
        private List<ShopSlot> _slots;

        public Shop(int id, string name, int locationId, int revenue)
        {
            _id = id;
            _name = name;
            _locationId = locationId;
            _revenue = revenue;
            _slots = new List<ShopSlot>();
            LoadLootItems();
            LoadQuestItems();
            LoadClassicalCards();
            LoadFlippableCards();
            LoadGoldCards();
        }

        public int Id { get => _id; set => _id = value; }
        public int LocationId { get => _locationId; set => _locationId = value; }
        public int Revenue { get => _revenue; set => _revenue = value; }
        public List<ShopSlot> Slots { get => _slots; set => _slots = value; }
        public string Name { get => _name; set => _name = value; }

        public void LoadLootItems()
        {
            using (MySqlConnection connection = new MySqlConnection(DatabaseInformation.ConnectionString))
            {
                connection.Open();
                string query = string.Format("SELECT item.id, item.Name, item.Description, lootitem.PrestigeValue, lootitem.WisdomValue, lootitem.ImageIndex, shopslot.Price, item.SalePrice " +
                    "FROM item INNER JOIN lootitem ON item.id = lootitem.ItemId " +
                    "INNER JOIN shopslot ON shopslot.ItemId = item.id " +
                    "WHERE shopslot.ShopId = @shopId");
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@shopId", _id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    LootItem lootItem = new LootItem((int)reader[0], (string)reader[1], (string)reader[2],
                        (int)reader[3], (int)reader[4], (int)reader[5]);
                    try
                    {
                        lootItem.SalePrice = (int)reader[7];
                    }
                    catch
                    {

                    }
                    _slots.Add(new ShopSlot(_id, (int)reader[6], lootItem));
                }
            }
        }
        public void LoadQuestItems()
        {
            using (MySqlConnection connection = new MySqlConnection(DatabaseInformation.ConnectionString))
            {
                connection.Open();
                string query = string.Format("SELECT item.id, item.Name, item.Description, questitem.ImageIndex, shopslot.Price, item.SalePrice " +
                    "FROM item INNER JOIN questitem ON item.id = questitem.ItemId " +
                    "INNER JOIN shopslot ON shopslot.ItemId = item.id " +
                    "WHERE shopslot.ShopId = @shopId");
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@shopId", _id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    QuestItem questItem = new QuestItem((int)reader[0], (string)reader[1], (string)reader[2], (int)reader[3]);
                    questItem.SalePrice = (int)reader[5];
                    _slots.Add(new ShopSlot(_id, (int)reader[4], questItem));
                }
            }
        }
        public void LoadClassicalCards()
        {
            using (MySqlConnection connection = new MySqlConnection(DatabaseInformation.ConnectionString))
            {
                connection.Open();
                string query = string.Format("SELECT item.id, item.Name, classicalcard.Value, item.Description, shopslot.Price, item.SalePrice " +
                    "FROM item INNER JOIN classicalcard ON item.id = classicalcard.ItemId " +
                    "INNER JOIN shopslot ON shopslot.ItemId = item.id " +
                    "WHERE shopslot.ShopId = @shopId");
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@shopId", _id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Card card = new ClassicalCard((int)reader[0], (string)reader[1], (int)reader[2]);
                    card.Descriprion = (string)reader[3];
                    card.SalePrice = (int)reader[5];
                    _slots.Add(new ShopSlot(_id, (int)reader[4], card));
                }
            }
        }
        public void LoadFlippableCards()
        {
            using (MySqlConnection connection = new MySqlConnection(DatabaseInformation.ConnectionString))
            {
                connection.Open();
                string query = string.Format("SELECT item.id, item.Name, flippablecard.Value, item.Description, shopslot.Price, item.SalePrice " +
                    "FROM item INNER JOIN flippablecard ON item.id = flippablecard.ItemId " +
                    "INNER JOIN shopslot ON shopslot.ItemId = item.id " +
                    "WHERE shopslot.ShopId = @shopId");
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@shopId", _id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Card card = new FlippableCard((int)reader[0], (string)reader[1], (int)reader[2]);
                    card.Descriprion = (string)reader[3];
                    card.SalePrice = (int)reader[5];
                    _slots.Add(new ShopSlot(_id, (int)reader[4], card));
                }
            }
        }
        public void LoadGoldCards()
        {
            using (MySqlConnection connection = new MySqlConnection(DatabaseInformation.ConnectionString))
            {
                connection.Open();
                string query = string.Format("SELECT item.id, item.Name, goldcard.Value, item.Description, shopslot.Price, item.SalePrice " +
                    "FROM item INNER JOIN goldcard ON item.id = goldcard.ItemId " +
                    "INNER JOIN shopslot ON shopslot.ItemId = item.id " +
                    "WHERE shopslot.ShopId = @shopId");
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@shopId", _id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Card card = new GoldCard((int)reader[0], (string)reader[1],
                        (GoldCardType)Enum.Parse(typeof(GoldCardType), (string)reader[2]), 0);
                    card.Descriprion = (string)reader[3];
                    card.SalePrice = (int)reader[5];
                    _slots.Add(new ShopSlot(_id, (int)reader[4], card));
                }
            }
        }
    }
}
