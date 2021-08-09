using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.View.ActionsHandlers
{
    class MessageDragActionsHandler : MonoBehaviour, IDragHandler
    {
        public void OnDrag(PointerEventData eventData)
        {
            transform.position = Input.mousePosition;
        }
    }
}
