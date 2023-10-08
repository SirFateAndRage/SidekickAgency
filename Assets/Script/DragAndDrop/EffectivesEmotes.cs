using Efficiency;
using Menace;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DragAndDrop
{
    public class EffectivesEmotes : MonoBehaviour,IEffectiveEmotes
    {
         private MenaceStructure _menaceStructure;

        [SerializeField] private Image[] _images;


        public void InitEffectivesEmote(MenaceStructure menaceStructure)
        {
            _menaceStructure = menaceStructure;
            Reset();
        }

        public void DiscoverEfficiency(List<IEffiency> effiencyList)
        {
            Reset();

            foreach (var item in effiencyList)
            {
                Effiency efficiency = item.GetEffeciency();

                if (efficiency.MenaceType1 != _menaceStructure.MenaceType)
                    continue;

                if (!efficiency.IsKnowed)
                {
                    efficiency.IsKnowed = true;

                }

                if (efficiency.EfficiencyModificator > _menaceStructure.MenaceMultiplicator)
                {
                    _images[2].enabled = true;
                    return;
                }


                if (efficiency.EfficiencyModificator == 0)
                {
                    _images[1].enabled = true;
                    return;
                }

                if (efficiency.EfficiencyModificator < 0)
                {
                    _images[0].enabled = true;
                    return;
                }

            }

        }

        public void SetEmote(List<IEffiency> effiencyList)
        {
            foreach (var item in effiencyList)
            {
                Effiency efficiency = item.GetEffeciency();

                if (efficiency.MenaceType1 != _menaceStructure.MenaceType)
                    continue;

                if (!efficiency.IsKnowed)
                {
                    _images[3].enabled = true;
                    return;
                }

                if (efficiency.EfficiencyModificator > _menaceStructure.MenaceMultiplicator)
                {
                    _images[2].enabled = true;
                    return;
                }


                if (efficiency.EfficiencyModificator  == 0)
                {
                    _images[1].enabled = true;
                    return;
                }

                if (efficiency.EfficiencyModificator < 0)
                {
                    _images[0].enabled = true;
                    return;
                }

            }
        }

        public void Reset()
        {
            for (int i = 0; i < _images.Length; i++)
            {
                _images[i].enabled = false;
            }

        }
    }
}
