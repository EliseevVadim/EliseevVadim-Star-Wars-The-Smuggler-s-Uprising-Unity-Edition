using Game.Entities.Items;
using Game.Enums;
using Game.Management.Repositories;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Services
{
    class ExchangePoint : MonoBehaviour
    {
        [SerializeField] private ExchangePointType _type;
        private List<Item> _items;

        public List<Item> Items { get => _items; set => _items = value; }

        private void Awake()
        {
            _items = new List<Item>();
            switch (_type)
            {
                case ExchangePointType.JediEnclave:
                    _items = ItemsRepository.JediItems;
                    break;
                case ExchangePointType.SithAcademy:
                    _items = ItemsRepository.SithItems;
                    break;
            }
        }
    }
}
