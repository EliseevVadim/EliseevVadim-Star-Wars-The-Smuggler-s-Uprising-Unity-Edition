using Game.Entities.Items.Cards;
using Game.Enums;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace Game.Management.Repositories
{
    static class CardsRepository
    {
        private static List<Card> _systemCards;
        private static List<ClassicalCard> _classicalCards;
        private static List<FlippableCard> _flippableCards;
        private static List<GoldCard> _goldCards;

        public static List<Card> SystemCards { get => _systemCards; set => _systemCards = value; }
        public static List<ClassicalCard> ClassicalCards { get => _classicalCards; set => _classicalCards = value; }
        public static List<FlippableCard> FlippableCards { get => _flippableCards; set => _flippableCards = value; }
        public static List<GoldCard> GoldCards { get => _goldCards; set => _goldCards = value; }

        static CardsRepository()
        {
            _systemCards = new List<Card>();
            _classicalCards = new List<ClassicalCard>();
            _flippableCards = new List<FlippableCard>();
            _goldCards = new List<GoldCard>();
            InitSystemCards();
            InitClassicalCards();
            InitFlippableCards();
            InitGoldCards();
        }

        private static void InitSystemCards()
        {
            using (MySqlConnection connection = new MySqlConnection(DatabaseInformation.ConnectionString))
            {
                connection.Open();
                string query = "SELECT item.id, item.Name, item.Description, systemcard.Value " +
                    "FROM item INNER JOIN systemcard ON item.id = systemcard.ItemId";
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Card addition = new Card((int)reader[0], (string)reader[1], (int)reader[3]);
                    addition.Descriprion = (string)reader[2];
                    _systemCards.Add(addition);
                }
            }
        }
        private static void InitClassicalCards()
        {
            using (MySqlConnection connection = new MySqlConnection(DatabaseInformation.ConnectionString))
            {
                connection.Open();
                string query = "SELECT item.id, item.Name, item.Description, classicalcard.Value, item.SalePrice " +
                    "FROM item INNER JOIN classicalcard ON item.id = classicalcard.ItemId";
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ClassicalCard addition = new ClassicalCard((int)reader[0], (string)reader[1], (int)reader[3]);
                    addition.Descriprion = (string)reader[2];
                    addition.SalePrice = (int)reader[4];
                    _classicalCards.Add(addition);
                }
            }
        }
        private static void InitFlippableCards()
        {
            using (MySqlConnection connection = new MySqlConnection(DatabaseInformation.ConnectionString))
            {
                connection.Open();
                string query = "SELECT item.id, item.Name, item.Description, flippablecard.Value, item.SalePrice " +
                    "FROM item INNER JOIN flippablecard ON item.id = flippablecard.ItemId";
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    FlippableCard addition = new FlippableCard((int)reader[0], (string)reader[1], (int)reader[3]);
                    addition.Descriprion = (string)reader[2];
                    addition.SalePrice = (int)reader[4];
                    _flippableCards.Add(addition);
                }
            }
        }
        private static void InitGoldCards()
        {
            using (MySqlConnection connection = new MySqlConnection(DatabaseInformation.ConnectionString))
            {
                connection.Open();
                string query = "SELECT item.id, item.Name, item.Description, goldcard.Value, item.SalePrice " +
                    "FROM item INNER JOIN goldcard ON item.id = goldcard.ItemId";
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    GoldCard addition = new GoldCard((int)reader[0], (string)reader[1],
                        (GoldCardType)Enum.Parse(typeof(GoldCardType), (string)reader[3]), 0);
                    addition.Descriprion = (string)reader[2];
                    addition.SalePrice = (int)reader[4];
                    _goldCards.Add(addition);
                }
            }
        }
    }
}