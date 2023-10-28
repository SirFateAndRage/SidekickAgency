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

        private HeroController _heroController;
        private Vector3 _effectTransform;
        private Transform _becierTransform;

        public void Initialize(HeroController heroController, Vector3 effectTransform, Transform becierTransform)
        {
            _heroController = heroController;
            _effectTransform = effectTransform;
            _becierTransform = becierTransform;

        }
        public void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag != null)
            {

                Hero hero = eventData.pointerDrag.GetComponent<Hero>();

                _initializeMenace.InitHeroJob(hero);
                _effectiveEmotes.DiscoverEfficiency(hero.HeroEffeciency);
                hero.ReturnToPosition();

                _becierTransform.position = _effectTransform;
                _heroController.SendHero();

                return;

            }

        }
    }
}
