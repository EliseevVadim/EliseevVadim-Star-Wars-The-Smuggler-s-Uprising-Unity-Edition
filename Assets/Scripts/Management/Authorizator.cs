using Game.Entities;
using MySql.Data.MySqlClient;
using System;

namespace Game.Management
{
    class Authorizator
    {
        private string _nickname;
        private string _login;
        private string _password;
        private Player _player;

        public Authorizator(string nickname, string login, string password)
        {
            _nickname = nickname;
            _login = login;
            TextEncryptor encryptor = new TextEncryptor(password);
            _password = encryptor.GetSha1EncryptedLine();
        }

        public bool UserExists()
        {
            using (MySqlConnection connection = new MySqlConnection(DatabaseInformation.ConnectionString))
            {
                connection.Open();
                string query = string.Format("SELECT id, Nickname, Login, Password, Credits, Prestige, WisdomPoints, LocationId, AvatarIndex, StoryFinished, LogoutDateTime, ShowTutorial FROM Player " +
                    "WHERE Nickname = @name " +
                    "AND Login = @login " +
                    "AND Password = @password");
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@name", _nickname);
                command.Parameters.AddWithValue("@login", _login);
                command.Parameters.AddWithValue("@password", _password);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Player player = new Player((int)reader[0], (string)reader[1], (string)reader[2], (string)reader[3], (long)reader[4],
                        (int)reader[5], (int)reader[6], (int)reader[7], (int)reader[8],
                        reader.GetBoolean("StoryFinished"), reader.GetBoolean("ShowTutorial"));
                    player.LogoutDateTime = (DateTime)reader[10];
                    DataHolder.Data = player;
                    return true;
                }
            }
            return false;
        }
    }
}
