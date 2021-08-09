using Game.Entities;
using Game.Management;
using UnityEngine;

namespace Game.View
{
    class InventoryVisualizator : MonoBehaviour
    {
        [SerializeField] private InventoryCellPresenter _inventoryCellTemplate;
        [SerializeField] private Transform _container;
        private Player _currentPlayer = (Player)DataHolder.Data;
        private Inventory _inventory => _currentPlayer.Inventory;

        public void OnEnable()
        {
            Render(_inventory);
        }
        public void Render(Inventory inventory)
        {
            foreach (Transform cell in _container)
            {
                Destroy(cell.gameObject);
            }
            inventory.Cells.ForEach(cell =>
            {
                var cellView = Instantiate(_inventoryCellTemplate, _container);
                cellView.Visualize(cell);
            });
        }
    }
}
