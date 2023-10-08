using Menace;
using UnityEngine;

namespace DragAndDrop
{
    public class MenaceInitConfigurator : MonoBehaviour
    {
        [SerializeField] private FloatingImage _floatingImage;
        [SerializeField] private MenaceIconFill _menaceIconFill;
        [SerializeField] private MenaceRecyclableObject _menaceRecyclableObject;
        [SerializeField] private EffectivesEmotes _effectiveEmotes;
        [SerializeField] private InitializeHeroJob _initHeroJob;
        [SerializeField] private MenaceIcon _menaceIcon;

        private HeroOnDutyController _heroOnDutyController;
        private MenaceStructure _menaceStructure;


        public void InitIcon(MenaceStructure menaceStructure, Transform cameraTransform, Transform buildingTransform,HeroOnDutyController heroOnDutyController,MenaceOutCome menaceOutCome)
        {
            _menaceStructure = menaceStructure;
            _heroOnDutyController = heroOnDutyController;
            _initHeroJob.Init(_menaceStructure,heroOnDutyController);
            _effectiveEmotes.InitEffectivesEmote(_menaceStructure);
            _floatingImage.ConfigureFloatingImage(cameraTransform, buildingTransform);

            _menaceIconFill.InitFillAmount(_menaceStructure.MenaceMultiplicator,_menaceStructure.Id,menaceOutCome);
            _menaceIconFill.SetFillSpeed(_menaceStructure.MenaceMultiplicator);

            _menaceIcon.Init(menaceStructure.MenaceSprite);
            _menaceRecyclableObject.OnRevived();
        }
    }
}
