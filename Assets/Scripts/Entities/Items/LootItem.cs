using Game.Management.Repositories;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Entities.Items
{
    class LootItem : Item
    {
        private int _prestigeValue;
        private int _wisdomValue;

        public int PrestigeValue { get => _prestigeValue; set => _prestigeValue = value; }
        public int WisdomValue { get => _wisdomValue; set => _wisdomValue = value; }

        public LootItem(int id, string name, string description, int prestigeValue, int wisdomValue, int imageIndex)
        {
            _id = id;
            _name = name;
            _descriprion = description;
            _prestigeValue = prestigeValue;
            _wisdomValue = wisdomValue;
            _image = LootItemsImagesRepository.ItemsSprites[imageIndex];
        }

        public override bool Equals(object obj)
        {
            return obj is LootItem item &&
                   _id == item._id &&
                   _descriprion == item._descriprion &&
                   EqualityComparer<Sprite>.Default.Equals(_image, item._image) &&
                   _salePrice == item._salePrice &&
                   _name == item._name &&
                   _prestigeValue == item._prestigeValue &&
                   _wisdomValue == item._wisdomValue;
        }

        public override int GetHashCode()
        {
            int hashCode = -9107223;
            hashCode = hashCode * -1521134295 + _id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(_descriprion);
            hashCode = hashCode * -1521134295 + EqualityComparer<Sprite>.Default.GetHashCode(_image);
            hashCode = hashCode * -1521134295 + _salePrice.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(_name);
            hashCode = hashCode * -1521134295 + _prestigeValue.GetHashCode();
            hashCode = hashCode * -1521134295 + _wisdomValue.GetHashCode();
            return hashCode;
        }
    }
}
