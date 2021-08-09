using Game.Entities;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace Game.Management.Repositories
{
    static class PlanetsRepository
    {
        private static List<Planet> _planets;

        public static List<Planet> Planets { get => _planets; set => _planets = value; }

        static PlanetsRepository()
        {
            _planets = new List<Planet>();
            using (MySqlConnection connection = new MySqlConnection(DatabaseInformation.ConnectionString))
            {
                connection.Open();
                string query = "SELECT id, Name, Description, TravellCost, Treasury, IconIndex FROM Planet";
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    _planets.Add(new Planet((int)reader[0], (string)reader[1], (string)reader[2], PlanetIconsRepository.Icons[(int)reader[5]], (int)reader[3], (long)reader[4]));
                }
            }
        }
    }
}
