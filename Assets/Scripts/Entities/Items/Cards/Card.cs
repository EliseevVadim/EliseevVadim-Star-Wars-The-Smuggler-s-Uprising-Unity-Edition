using Game.Management.Repositories;
using Game.Services.PazaakTools;
using UnityEngine;

namespace Game.Entities.Items.Cards
{
    class Card : Item
    {
        protected int _value;
        protected string _valueInLine;

        public int Value { get => _value; set => _value = value; }
        public string ValueInLine { get => _valueInLine; set => _valueInLine = value; }

        public Card(int id, string name, int value)
        {
            _id = id;
            _name = name;
            _value = value;
            _image = CardsImagesRepository.Cards[6];
            GenerateLineFromValue();
        }

        public virtual void AddToDeck(Deck deck)
        {
            try
            {
                int index = deck.CurrentIndex;
                deck.Cards[index] = this;
                deck.DeckView[index].sprite = _image;
                deck.DeckView[index].color = Color.white;
                deck.CardsValues[index].text = _valueInLine;
                deck.Sum += _value;
                deck.CurrentIndex++;
            }
            catch { }
        }
        public virtual void GenerateLineFromValue()
        {
            _valueInLine = _value.ToString();
        }
    }
}
