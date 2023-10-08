using Menace;
using UnityEngine;

namespace DragAndDrop
{
    public class MenaceInitConfigurator : MonoBehaviour
    {
        [SerializeField] private FloatingImage _floatingImage;
        [SerializeField] private MenaceIcon _menaceIcon;
        [SerializeField] private MenaceRecyclableObject _menaceRecyclableObject;
        [SerializeField] private EffectivesEmotes _effectiveEmotes;
        [SerializeField] private InitializeHeroJob _initHeroJob;

        private HeroOnDutyController _heroOnDutyController;
        private MenaceStructure _menaceStructure;


        public void InitIcon(MenaceStructure menaceStructure, Transform cameraTransform, Transform buildingTransform,HeroOnDutyController heroOnDutyController)
        {
            _menaceStructure = menaceStructure;
            _heroOnDutyController = heroOnDutyController;
            _initHeroJob.Init(_menaceStructure,heroOnDutyController);
            _effectiveEmotes.InitEffectivesEmote(_menaceStructure);
            _floatingImage.ConfigureFloatingImage(cameraTransform, buildingTransform);
            _menaceIcon.InitFillAmount(_menaceStructure.MenaceMultiplicator);
            _menaceIcon.SetFillSpeed(_menaceStructure.MenaceMultiplicator);
            _menaceRecyclableObject.OnRevived();
        }
    }
}
