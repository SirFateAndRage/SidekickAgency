using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Menace
{

    [CreateAssetMenu(fileName = "LevelMenaceConfiguration", menuName = "Configurations/LevelMenaceConfiguration")]
    public class LevelMenaceConfiguration : ScriptableObject
    {
        [SerializeField] private List<MenaceStructure> _menaceStructure;

        public List<MenaceStructure> MenacesStructure { get => _menaceStructure;}
    }

}


