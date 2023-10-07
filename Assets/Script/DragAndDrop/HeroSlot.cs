using UnityEngine;
using UnityEngine.EventSystems;

namespace DragAndDrop
{
    public class HeroSlot : MonoBehaviour, IDropHandler
    {
        [SerializeField] private RectTransform _rectTransform;
        public void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag != null)
            {
                eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = _rectTransform.anchoredPosition;
            }
                Debug.Log("ENTRO AQUI");
        }
    }
}
