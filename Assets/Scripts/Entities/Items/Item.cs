using UnityEngine;

namespace Game.Entities.Items
{
    abstract class Item
    {
        protected int _id;
        protected string _descriprion;
        protected Sprite _image;
        protected int _salePrice;
        protected string _name;

        public int Id { get => _id; set => _id = value; }
        public string Descriprion { get => _descriprion; set => _descriprion = value; }
        public Sprite Image { get => _image; set => _image = value; }
        public int SalePrice { get => _salePrice; set => _salePrice = value; }
        public string Name { get => _name; set => _name = value; }
    }
}
