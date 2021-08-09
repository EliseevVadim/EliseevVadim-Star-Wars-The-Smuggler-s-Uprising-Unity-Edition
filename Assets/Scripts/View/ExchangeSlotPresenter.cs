using Game.Entities;
using Game.Entities.Items;
using Game.Management;
using UnityEngine;
using UnityEngine.UI;

namespace Game.View
{
    class ExchangeSlotPresenter : MonoBehaviour
    {
        [SerializeField] private Text _nameField;
        [SerializeField] private Image _iconField;
        [SerializeField] private Text _descriptionField;
        [SerializeField] private Text _buttonText;
        [SerializeField] private GameObject _successMessage;
        [SerializeField] private GameObject _errorMessage;
        [SerializeField] private Text _successText;

        private LootItem _item;
        private Player _currentPlayer = (Player)DataHolder.Data;

        public void Visualize(Item item)
        {
            LootItem lootItem = item as LootItem;
            _item = lootItem;
            _nameField.text = lootItem.Name;
            _iconField.sprite = lootItem.Image;
            _descriptionField.text = lootItem.Descriprion;
            _buttonText.text =
                lootItem.PrestigeValue > 0 ? $"Сдать ({lootItem.PrestigeValue} очков престижа)"
                : $"Сдать ({lootItem.WisdomValue} очков мудрости)";
        }
        public void Deliver()
        {
            if (_currentPlayer.HasA(_item))
            {
                _currentPlayer.Inventory.RemoveItem(_item);
                if (_item.PrestigeValue > 0)
                {
                    _currentPlayer.Prestige += _item.PrestigeValue;
                    _successText.text = $"Получено {_item.PrestigeValue} очков престижа.";
                }
                else
                {
                    _currentPlayer.WisdomPoints += _item.WisdomValue;
                    _successText.text = $"Получено {_item.WisdomValue} очков мудрости.";
                }
                PlayerInformationVisualisator.UpdateView();
                _successMessage.SetActive(true);
            }
            else
            {
                _errorMessage.SetActive(true);
            }
        }

    }
}
