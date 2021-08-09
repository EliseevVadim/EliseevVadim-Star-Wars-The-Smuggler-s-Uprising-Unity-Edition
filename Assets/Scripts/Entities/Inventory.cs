using Game.Entities.Items;
using Game.Entities.Items.Cards;
using Game.Enums;
using Game.Management;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.Entities
{
    class Inventory : IDatabaseAgent
    {
        private List<InventoryCell> _cells;
        private int _capacity;
        private int _id;

        public List<InventoryCell> Cells { get => _cells; set => _cells = value; }
        public int Capacity { get => _capacity; set => _capacity = value; }
        public int Id { get => _id; set => _id = value; }

        public Inventory(int id, int capacity)
        {
            _cells = new List<InventoryCell>();
            _id = id;
            _capacity = capacity;
            LoadLootItems();
            LoadClassicalCards();
            LoadFlippableCards();
            LoadGoldCards();
            LoadQuestItems();
        }

        private void LoadLootItems()
        {
            using (MySqlConnection connection = new MySqlConnection(DatabaseInformation.ConnectionString))
            {
                connection.Open();
                string query = string.Format(@"SELECT item.id, item.Name, item.Description, lootitem.PrestigeValue, lootitem.WisdomValue, lootitem.ImageIndex, inventoryCell.Count, item.SalePrice
                FROM item INNER JOIN lootitem ON item.id = lootitem.ItemId
                INNER JOIN inventorycell on item.id = inventorycell.ItemId
                WHERE inventorycell.InventoryId = @invId");
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@invId", _id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    LootItem lootItem = new LootItem((int)reader[0], (string)reader[1], (string)reader[2], (int)reader[3],
                        (int)reader[4], (int)reader[5]);
                    try
                    {
                        lootItem.SalePrice = (int)reader[7];
                    }
                    catch
                    {

                    }
                    _cells.Add(new InventoryCell((int)reader[6], lootItem, _id));
                }
            }
        }
        private void LoadQuestItems()
        {
            using (MySqlConnection connection = new MySqlConnection(DatabaseInformation.ConnectionString))
            {
                connection.Open();
                string query = string.Format(@"SELECT item.id, item.Name, item.Description, questitem.ImageIndex, item.SalePrice, inventoryCell.count
                FROM item INNER JOIN questitem ON item.id = questitem.ItemId
                INNER JOIN inventorycell ON inventorycell.ItemId = item.id
                WHERE inventorycell.InventoryId = @invId");
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@invId", _id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    QuestItem questItem = new QuestItem((int)reader[0], (string)reader[1], (string)reader[2], (int)reader[3]);
                    questItem.SalePrice = (int)reader[4];
                    _cells.Add(new InventoryCell((int)reader[5], questItem, _id));
                }
            }
        }
        private void LoadClassicalCards()
        {
            using (MySqlConnection connection = new MySqlConnection(DatabaseInformation.ConnectionString))
            {
                connection.Open();
                string query = string.Format(@"SELECT item.id, item.Name, classicalcard.Value, inventorycell.Count, item.Description, item.SalePrice
                FROM item INNER JOIN classicalcard ON classicalcard.ItemId = item.id
                INNER JOIN inventorycell ON inventorycell.ItemId = item.id
                WHERE inventorycell.InventoryId = @invId");
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@invId", _id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ClassicalCard card = new ClassicalCard((int)reader[0], (string)reader[1], (int)reader[2]);
                    card.Descriprion = (string)reader[4];
                    card.SalePrice = (int)reader[5];
                    _cells.Add(new InventoryCell((int)reader[3], card, _id));
                }
            }
        }
        private void LoadFlippableCards()
        {
            using (MySqlConnection connection = new MySqlConnection(DatabaseInformation.ConnectionString))
            {
                connection.Open();
                string query = string.Format(@"SELECT item.id, item.Name, flippablecard.Value, inventorycell.Count, item.Description, item.SalePrice
                FROM item INNER JOIN flippablecard ON flippablecard.ItemId = item.id
                INNER JOIN inventorycell ON inventorycell.ItemId = item.id
                WHERE inventorycell.InventoryId = @invId");
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@invId", _id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    FlippableCard card = new FlippableCard((int)reader[0], (string)reader[1], (int)reader[2]);
                    card.Descriprion = (string)reader[4];
                    card.SalePrice = (int)reader[5];
                    _cells.Add(new InventoryCell((int)reader[3], card, _id));
                }
            }
        }
        private void LoadGoldCards()
        {
            using (MySqlConnection connection = new MySqlConnection(DatabaseInformation.ConnectionString))
            {
                connection.Open();
                string query = string.Format(@"SELECT item.id, item.Name, goldcard.Value, inventorycell.Count, item.Description, item.SalePrice
                FROM item INNER JOIN goldcard ON goldcard.ItemId = item.id
                INNER JOIN inventorycell ON inventorycell.ItemId = item.id
                WHERE inventorycell.InventoryId = @invId");
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@invId", _id);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    GoldCard card = new GoldCard((int)reader[0], (string)reader[1],
                        (GoldCardType)Enum.Parse(typeof(GoldCardType), (string)reader[2]), 0);
                    card.Descriprion = (string)reader[4];
                    card.SalePrice = (int)reader[5];
                    _cells.Add(new InventoryCell((int)reader[3], card, _id));
                }
            }
        }
        public bool Contains(Item item)
        {
            foreach (InventoryCell cell in _cells)
            {
                if (cell.Content.Equals(item))
                {
                    return true;
                }
            }
            return false;
        }
        public void AddItem(Item item)
        {
            if (Contains(item))
            {
                var cell = _cells.Where(cell => cell.Content.Equals(item)).First();
                cell.Count++;
            }
            else
            {
                InventoryCell cell = new InventoryCell(1, item, _id);
                cell.AddToDatabase();
                _cells.Add(cell);
            }
        }
        public void RemoveItem(Item item)
        {
            var cell = _cells.Where(cell => cell.Content.Equals(item)).First();
            if (cell.Count > 1)
            {
                cell.Count--;
            }
            else
            {
                cell.RemoveFromDatabase();
                _cells.Remove(cell);
            }
        }
        public void AddToDatabase()
        {
            throw new NotImplementedException();
        }

        public void SaveInDatabase()
        {
            _cells.ForEach(cell => cell.SaveInDatabase());
        }

        public void RemoveFromDatabase()
        {
            throw new NotImplementedException();
        }
    }
}
