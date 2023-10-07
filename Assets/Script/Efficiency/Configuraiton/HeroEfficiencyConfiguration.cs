using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Efficiency
{
    [CreateAssetMenu(fileName = "HeroEfficiencyConfiguration", menuName = "Configurations/EfficiencyConfiguration")]
    public class HeroEfficiencyConfiguration : ScriptableObject
    {
        [SerializeField] private List<Effiency> _efficiencylist = new List<Effiency>();

        public List<IEffiency> GetEfficiencyList()
        {
            return _efficiencylist.Cast<IEffiency>().ToList();
        }
    }
}