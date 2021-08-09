using Game.Entities;
using Game.Management;
using Game.Services;
using UnityEngine;
using UnityEngine.UI;

namespace Game.View
{
    class ShopVisualizator : MonoBehaviour
    {
        [SerializeField] private ShopSlotPresenter _shopSlotPresenterTemplate;
        [SerializeField] private Transform _parent;
        [SerializeField] private Text _title;

        private Player _currentPlayer = (Player)DataHolder.Data;
        private Shop _shop => _currentPlayer.Location.Shop;

        public void OnEnable()
        {
            Render(_shop);
        }

        public void Render(Shop shop)
        {
            try
            {
                foreach (Transform cell in _parent)
                {
                    Destroy(cell.gameObject);
                }
                _title.text = shop.Name;
                shop.Slots.ForEach(slot =>
                {
                    var slotView = Instantiate(_shopSlotPresenterTemplate, _parent);
                    slotView.Visualize(slot);
                });
            }
            catch
            {

            }
        }
    }
}
