namespace Game.Management
{
    static class DatabaseInformation
    {
        private static string _connectionString = "Server = localhost; Port = 3306; Database = GameData; Uid = root; pwd = root; charset = utf8; pooling = false;";
        public static string ConnectionString { get => _connectionString; }
    }
}
