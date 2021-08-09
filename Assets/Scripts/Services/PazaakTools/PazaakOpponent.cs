using Game.Entities.Items.Cards;
using Game.Enums;
using Game.Management.Repositories;
using System.Collections.Generic;

namespace Game.Services.PazaakTools
{
    class PazaakOpponent
    {
        private PazaakOpponentsLevel _level;
        private List<Card> _allCards;

        public PazaakOpponent(PazaakOpponentsLevel level)
        {
            _level = level;
            _allCards = new List<Card>();
            switch (_level)
            {
                case PazaakOpponentsLevel.Easy:
                    _allCards.AddRange(CardsRepository.ClassicalCards);
                    break;
                case PazaakOpponentsLevel.Medium:
                    _allCards.AddRange(CardsRepository.ClassicalCards);
                    _allCards.AddRange(CardsRepository.FlippableCards);
                    break;
                case PazaakOpponentsLevel.Hard:
                    _allCards.AddRange(CardsRepository.ClassicalCards);
                    _allCards.AddRange(CardsRepository.FlippableCards);
                    _allCards.AddRange(CardsRepository.GoldCards);
                    break;
            }
        }

        public List<Card> AllCards { get => _allCards; set => _allCards = value; }
    }
}
