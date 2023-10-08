using Efficiency;
using Heroes;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DragAndDrop
{

    public interface IEffectiveEmotes
    {
        public void SetEmote(List<IEffiency> effiencyList);

        public void Reset();
    }
    public class DragAndDropp : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler,IDragHandler
    {
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private Canvas _canvas;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Camera _camera;
        [SerializeField] private Hero _hero;

        private HashSet<IScope> _scopeList = new HashSet<IScope>();
        private IScope _currentScope;
        private IEffectiveEmotes _effectiveEmotes;
        public void OnBeginDrag(PointerEventData eventData)
        {
            Debug.Log("OnBeginDrag");
            _canvasGroup.alpha = .6f;
            _canvasGroup.blocksRaycasts = false;
            FindObjectOfType<HeroController>().SpawnHero();
        }

        public void OnDrag(PointerEventData eventData)
        {
            _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;

            Ray ray = Camera.main.ScreenPointToRay(eventData.position);
            RaycastHit[] hits = Physics.RaycastAll(ray);

            HashSet<IScope> currentContacts = new HashSet<IScope>();
            float closestDistance = float.MaxValue;
            IScope closestScope = null;
            IEffectiveEmotes closestEmote = null;

            foreach (var hit in hits)
            {
               
                IScope scope = hit.collider.GetComponent<IScope>();
                IEffectiveEmotes effectiveEmoter = hit.collider.GetComponent<IEffectiveEmotes>();
                if (scope != null)
                {
                    currentContacts.Add(scope);

                    Vector2 screenPosition = _camera.WorldToScreenPoint(hit.point);

                    Vector2 finalPos = screenPosition - eventData.position;
                    

                    if (finalPos.magnitude < closestDistance)
                    {
                        if (_currentScope != null && _currentScope != scope)
                        {
                            _effectiveEmotes.Reset();
                            _currentScope.SetActiveScope(false);

                        }

                        closestScope = scope;
                        closestEmote = effectiveEmoter;
                        closestDistance = hit.distance;
                    }
                }
            }

            if (closestScope != null)
            {
                closestScope.SetActiveScope(true);
                closestEmote.SetEmote(_hero.HeroEffeciency);
            }

            if (_currentScope != null && !currentContacts.Contains(_currentScope))
            {
                // Si el currentScope no está en los contactos actuales, apagarlo
                _currentScope.SetActiveScope(false);
                _effectiveEmotes.Reset();
            }

            _effectiveEmotes = closestEmote;
            _currentScope = closestScope;
            _scopeList = currentContacts;
        }

        private void ResetScope()
        {
            if (_scopeList.Count <= 0)
                return;
            foreach (var item in _scopeList)
            {
                item.SetActiveScope(false);
            }

            _scopeList.Clear();

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
                else
                {
                    dropHandler.OnDrop(null);
                }
            }


            Hero hero = eventData.pointerDrag.GetComponent<Hero>();
            hero.ReturnToPosition();
            ResetScope();

        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Debug.Log("onPointerDown");
        }
    }
}
