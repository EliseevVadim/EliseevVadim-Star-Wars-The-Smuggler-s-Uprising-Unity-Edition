using Game.Entities.Items;
using Game.Management;
using MySql.Data.MySqlClient;

namespace Game.Entities
{
    class InventoryCell : IDatabaseAgent
    {
        private int _count;
        private Item _content;
        private int _inventoryId;

        public InventoryCell(int count, Item content, int inventoryId)
        {
            _count = count;
            _content = content;
            _inventoryId = inventoryId;
        }

        public int Count { get => _count; set => _count = value; }
        public int InventoryId { get => _inventoryId; set => _inventoryId = value; }
        internal Item Content { get => _content; set => _content = value; }

        public void AddToDatabase()
        {
            using (MySqlConnection connection = new MySqlConnection(DatabaseInformation.ConnectionString))
            {
                connection.Open();
                string query = string.Format("INSERT INTO InventoryCell (InventoryId, ItemId, Count) VALUES (@invId, @itemId, @count)");
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@invId", _inventoryId);
                command.Parameters.AddWithValue("@itemId", _content.Id);
                command.Parameters.AddWithValue("@count", _count);
                command.ExecuteNonQuery();
            }
        }

        public void RemoveFromDatabase()
        {
            using (MySqlConnection connection = new MySqlConnection(DatabaseInformation.ConnectionString))
            {
                connection.Open();
                string query = string.Format("DELETE FROM inventorycell " +
                    "WHERE InventoryId = @invId " +
                    "AND ItemId = @itemId");
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@invId", _inventoryId);
                command.Parameters.AddWithValue("@itemId", _content.Id);
                command.ExecuteNonQuery();
            }
        }

        public void SaveInDatabase()
        {
            using (MySqlConnection connection = new MySqlConnection(DatabaseInformation.ConnectionString))
            {
                connection.Open();
                string query = string.Format("UPDATE InventoryCell SET " +
                    "Count = @count " +
                    "WHERE InventoryId = @invId " +
                    "AND ItemId = @itemId");
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@invId", _inventoryId);
                command.Parameters.AddWithValue("@itemId", _content.Id);
                command.Parameters.AddWithValue("@count", _count);
                command.ExecuteNonQuery();
            }
        }
    }
}
