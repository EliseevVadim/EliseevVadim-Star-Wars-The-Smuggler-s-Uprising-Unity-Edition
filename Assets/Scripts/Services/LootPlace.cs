using Game.Entities.Items;
using Game.Enums;
using Game.Exceptions;
using Game.Management.Repositories;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Services
{
    class LootPlace
    {
        private List<Item> _items;
        private LootPlaceType _type;
        private string _explanation;

        public LootPlace(LootPlaceType type)
        {
            _items = new List<Item>();
            _type = type;
            switch (_type)
            {
                case LootPlaceType.SithTomb:
                    _items = ItemsRepository.SithItems;
                    _explanation = "Дух темного Лорда напал на Вас, вы проиграли. Вы потеряли голокрон.";
                    break;
                case LootPlaceType.JediRuins:
                    _items = ItemsRepository.JediItems;
                    _explanation = "Древний дроид-страж напал на вас, вы проиграли. Вы потеряли медальон.";
                    break;
            }
        }
        public Item Loot()
        {
            int pos = Random.Range(0, _items.Count * 2 + 1);
            try
            {
                return _items[pos];
            }
            catch
            {
                throw new FailedLootException(_explanation);
            }
        }
    }
}
