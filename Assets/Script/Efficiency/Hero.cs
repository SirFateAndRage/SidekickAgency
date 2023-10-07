using Efficiency;
using System.Collections.Generic;
using UnityEngine;

namespace Heroes
{
    public class Hero : MonoBehaviour
    {
        [SerializeField] private HeroEfficiencyConfiguration _configuration;
        private List<IEffiency> _heroEffeciency;

        public List<IEffiency> HeroEffeciency { get => _heroEffeciency;}

        private void Awake()
        {
            _heroEffeciency = _configuration.GetEfficiencyList();
        }

    }
}
