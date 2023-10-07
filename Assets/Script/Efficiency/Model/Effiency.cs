using UnityEngine;

namespace Efficiency
{
    [System.Serializable]
    public struct Effiency : IEffiency
    {
        [SerializeField] private MenaceType _menaceType;
        [SerializeField] private float _efficiencyModificator;
        public MenaceType MenaceType => _menaceType;

        public float EfficiencyModificator => _efficiencyModificator;
    }
}
