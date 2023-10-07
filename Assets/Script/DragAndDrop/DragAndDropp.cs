using UnityEngine;
using UnityEngine.EventSystems;

namespace DragAndDrop
{
    public class DragAndDropp : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler,IDragHandler
    {
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private Canvas _canvas;
        [SerializeField] private CanvasGroup _canvasGroup;
        public void OnBeginDrag(PointerEventData eventData)
        {
            Debug.Log("OnBeginDrag");
            _canvasGroup.alpha = .6f;
            _canvasGroup.blocksRaycasts = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            Debug.Log("OnDrag");
            _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            Debug.Log("OndEndDrag");
            _canvasGroup.alpha = 1f;
            _canvasGroup.blocksRaycasts = true;

            Ray ray = Camera.main.ScreenPointToRay(eventData.position);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                IDropHandler dropHandler = hit.collider.GetComponent<IDropHandler>();
                if (dropHandler != null)
                {
                    // Llama a la función de drop
                    dropHandler.OnDrop(eventData);
                }
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Debug.Log("onPointerDown");
        }
    }
}
