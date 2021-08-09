using Game.Services.PazaakTools;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game.View.ActionsHandlers
{
    class CardsActionsHandler : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Image _image;
        public void OnPointerClick(PointerEventData eventData)
        {
            switch (eventData.clickCount)
            {
                case 1:
                    _image.transform.parent.parent.gameObject.GetComponent<PazaakGame>().SelectCard(int.Parse(_image.tag));
                    break;
                case 2:
                    _image.transform.parent.parent.gameObject.GetComponent<PazaakGame>().AddPlayersCard(int.Parse(_image.tag));
                    break;
            }
        }
    }
}
