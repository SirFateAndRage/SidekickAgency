using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Menace
{


    public interface IMenace
    {
        MenaceStructure GetMenaceStructure();
    }
    [System.Serializable]
    public struct MenaceStructure
    {
        [SerializeField] private MenaceType _menaceType;
        [SerializeField] private float _startingTime;
        [SerializeField] private float _menaceMultiplicator;
        [SerializeField] private float _menaceGameTime;
        [SerializeField] private Image _menaceImage;

        public MenaceType MenaceType { get => _menaceType;}
        public float StartingTime { get => _startingTime;}
        public float MenaceMultiplicator { get => _menaceMultiplicator;}
        public float MenaceGameTime { get => _menaceGameTime;}
    }

    [CreateAssetMenu(fileName = "LevelMenaceConfiguration", menuName = "Configurations/LevelMenaceConfiguration")]
    public class LevelMenaceConfiguration : ScriptableObject
    {
        [SerializeField] private List<MenaceStructure> _menaceStructure;

        public List<MenaceStructure> MenacesStructure { get => _menaceStructure;}
    }
    public class NewBehaviourScript : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }

}


