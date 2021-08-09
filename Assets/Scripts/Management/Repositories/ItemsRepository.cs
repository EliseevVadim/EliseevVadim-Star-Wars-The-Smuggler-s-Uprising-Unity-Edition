using Game.Entities.Items;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;

namespace Game.Management.Repositories
{
    static class ItemsRepository
    {
        private static List<Item> _lootItems;
        private static List<Item> _questItems;
        private static List<Item> _sithItems;
        private static List<Item> _jediItems;

        public static List<Item> LootItems { get => _lootItems; set => _lootItems = value; }
        public static List<Item> QuestItems { get => _questItems; set => _questItems = value; }
        public static List<Item> SithItems { get => _sithItems; set => _sithItems = value; }
        public static List<Item> JediItems { get => _jediItems; set => _jediItems = value; }

        static ItemsRepository()
        {
            _lootItems = new List<Item>();
            _questItems = new List<Item>();
            LoadLootItems();
            LoadQuestItems();
        }

        private static void LoadLootItems()
        {
            using (MySqlConnection connection = new MySqlConnection(DatabaseInformation.ConnectionString))
            {
                connection.Open();
                string query = "SELECT item.id, item.Name, item.Description, item.SalePrice, lootitem.PrestigeValue, lootitem.WisdomValue, lootitem.ImageIndex " +
                    "FROM item INNER JOIN lootitem ON item.id = lootitem.ItemId";
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Item addition = new LootItem((int)reader[0], (string)reader[1], (string)reader[2], (int)reader[4], (int)reader[5], (int)reader[6]);
                    try
                    {
                        addition.SalePrice = (int)reader[3];
                    }
                    catch
                    {

                    }
                    _lootItems.Add(addition);
                }
                _jediItems = new List<Item>();
                _sithItems = new List<Item>();
                _sithItems = _lootItems.Take(4).ToList();
                _jediItems = _lootItems.Skip(4).ToList();
            }
        }
        private static void LoadQuestItems()
        {
            using (MySqlConnection connection = new MySqlConnection(DatabaseInformation.ConnectionString))
            {
                connection.Open();
                string query = "SELECT item.id, item.Name, item.Description, questitem.ImageIndex, item.SalePrice " +
                    "FROM item INNER JOIN questitem ON item.id = questitem.ItemId";
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Item addition = new QuestItem((int)reader[0], (string)reader[1], (string)reader[2], (int)reader[3]);
                    addition.SalePrice = (int)reader[4];
                    _questItems.Add(addition);
                }
            }
        }
    }
}
