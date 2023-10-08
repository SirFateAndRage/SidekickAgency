using UnityEngine;
using UnityEngine.UI;

namespace Menace
{
    [System.Serializable]
    public struct MenaceStructure
    {
        [Header("Use a different ID for each menace")]
        [SerializeField] private int _id;
        [SerializeField] private MenaceType _menaceType;
        [SerializeField] private float _startingTime;
        [SerializeField] private float _menaceMultiplicator;
        [SerializeField] private float _menaceGameTime;
        [SerializeField] private Sprite _menaceImage;

        public MenaceType MenaceType { get => _menaceType;}
        public float StartingTime { get => _startingTime;}
        public float MenaceMultiplicator { get => _menaceMultiplicator;}
        public float MenaceGameTime { get => _menaceGameTime;}
        public int Id { get => _id;}
        public Sprite MenaceSprite { get => _menaceImage;}
    }

}


