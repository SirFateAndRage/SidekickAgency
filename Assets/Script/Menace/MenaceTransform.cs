using UnityEngine;

namespace Menace
{
    [System.Serializable]
    public struct MenaceTransform
    {
        [SerializeField] private Transform _iconToPlaceMenace;
        [SerializeField] private Transform _placeToPutEffect;

        public Transform IconToPlaceMenace { get => _iconToPlaceMenace;}
        public Transform PlaceToPutEffect { get => _placeToPutEffect;}
    }

}


