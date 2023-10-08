using Heroes;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DragAndDrop
{
    public class HeroSlot : MonoBehaviour, IDropHandler
    {
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private MenaceIconFill _menaceIcon;
        [SerializeField] private InitializeHeroJob _initializeMenace;
        [SerializeField] private EffectivesEmotes _effectiveEmotes;
        public void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag != null)
            {
                eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = _rectTransform.anchoredPosition;

                Hero hero = eventData.pointerDrag.GetComponent<Hero>();

                _initializeMenace.InitHeroJob(hero);
                _effectiveEmotes.DiscoverEfficiency(hero.HeroEffeciency);
                hero.ReturnToPosition();

            }

        }
    }
}
