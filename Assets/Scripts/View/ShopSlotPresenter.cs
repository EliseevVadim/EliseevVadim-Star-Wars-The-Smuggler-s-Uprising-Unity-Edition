using Game.Entities;
using Game.Entities.Items.Cards;
using Game.Management;
using UnityEngine;
using UnityEngine.UI;

namespace Game.View
{
    class ShopSlotPresenter : MonoBehaviour
    {
        [SerializeField] private GameObject _successMessage;
        [SerializeField] private GameObject _errorMessage;
        [SerializeField] private Image _iconField;
        [SerializeField] private Text _nameField;
        [SerializeField] private Text _descriptionField;
        [SerializeField] private Text _buyText;
        [SerializeField] private Text _sellText;
        [SerializeField] private Text _successText;
        [SerializeField] private Text _errorText;
        [SerializeField] private Text _cardValueField;

        private Player _currentPlayer = (Player)DataHolder.Data;
        private ShopSlot _slot;

        public void Visualize(ShopSlot slot)
        {
            _slot = slot;
            _iconField.sprite = _slot.Stuff.Image;
            _nameField.text = _slot.Stuff.Name;
            _descriptionField.text = _slot.Stuff.Descriprion;
            _buyText.text = $"Купить ({_slot.Price} кредитов)";
            _sellText.text = $"Продать ({_slot.Stuff.SalePrice} кредитов)";
            if (_slot.Stuff is Card)
            {
                Card card = _slot.Stuff as Card;
                _cardValueField.text = card.ValueInLine;
            }
        }
        public void Buy()
        {
            if (_currentPlayer.Credits >= _slot.Price)
            {
                _currentPlayer.Inventory.AddItem(_slot.Stuff);
                _currentPlayer.Credits -= _slot.Price;
                _slot.Revenue += _slot.Price;
                PlayerInformationVisualisator.UpdateView();
                _successText.text = "Предмет успешно куплен.";
                _successMessage.SetActive(true);
            }
            else
            {
                _errorText.text = "Недостаточно кредитов!";
                _errorMessage.SetActive(true);
            }
        }
        public void Sell()
        {
            if (_currentPlayer.HasA(_slot.Stuff))
            {
                _currentPlayer.Inventory.RemoveItem(_slot.Stuff);
                _currentPlayer.Credits += _slot.Stuff.SalePrice;
                PlayerInformationVisualisator.UpdateView();
                _successText.text = $"Предмет успешно продан. Получено {_slot.Stuff.SalePrice} кредитов.";
                _successMessage.SetActive(true);
            }
            else
            {
                _errorText.text = "Данный предмет у Вас отсутствует.";
                _errorMessage.SetActive(true);
            }
        }
    }
}
