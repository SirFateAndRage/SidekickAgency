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
        public MenaceType MenaceType => _menaceType;
        public float EfficiencyModificator => _efficiencyModificator;
        public bool IsKnowed => _isKnowed;
    }
}
