using Menace;
using UnityEngine;

namespace Efficiency
{
    [System.Serializable]
    public struct Effiency : IEffiency
    {
        [SerializeField] private MenaceType _menaceType;
        [SerializeField] private float _efficiencyModificator;
        [SerializeField] private bool _isKnowed;

        public MenaceType MenaceType1 { get => _menaceType;}
        public float EfficiencyModificator { get => _efficiencyModificator;}
        public bool IsKnowed { get => _isKnowed; set => _isKnowed = value; }

        public Effiency GetEffeciency() => this;
    }
}
