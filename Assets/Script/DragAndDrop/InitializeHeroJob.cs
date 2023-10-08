using Heroes;
using Menace;
using Efficiency;
using UnityEngine;

namespace DragAndDrop
{
    public class InitializeHeroJob : MonoBehaviour
    {
        [SerializeField] private MenaceIconFill _menaceIcon;

        private HeroOnDutyController _heroOnDutyController;

        private MenaceStructure _menaceStructre;

        public void Init(MenaceStructure menaceStructure,HeroOnDutyController heroOnDutyController)
        {
            _menaceStructre = menaceStructure;
            _heroOnDutyController = heroOnDutyController;
            
        }

        public void InitHeroJob(Hero hero)
        {
            _heroOnDutyController.SetHeroToWork(hero, _menaceStructre, _menaceIcon);

            foreach (Effiency item in hero.HeroEffeciency)
            {
                if (item.MenaceType1 != _menaceStructre.MenaceType)
                    continue;

                _menaceIcon.SetFillSpeed(item.EfficiencyModificator);
                
                //ponerIcono del player

                return;

            }
        }



    }
}
