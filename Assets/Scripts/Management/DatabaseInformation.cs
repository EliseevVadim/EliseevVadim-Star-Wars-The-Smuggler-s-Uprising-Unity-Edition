namespace Game.Management
{
    static class DatabaseInformation
    {
        // THIS FUCKING SHIT SHOULD BE FIXED!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        //private static string _connectionString = @"Data Source=DESKTOP-NDF1AKD\SQLEXPRESS;Initial Catalog=GameData;User ID = admin; Password = 1731619";
        private static string _connectionString = "Server = localhost; Port = 3306; Database = GameData; Uid = root; pwd = root; charset = utf8; pooling = false;";
        //private static string _connectionString = ConfigurationManager.ConnectionStrings["MainConnectionString"].ConnectionString;
        public static string ConnectionString { get => _connectionString; }
    }
}
