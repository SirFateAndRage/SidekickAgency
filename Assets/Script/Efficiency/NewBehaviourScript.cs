using Efficiency;
using System.Collections.Generic;
using UnityEngine;

namespace Heroes
{
    public class HeroConfiguration
    {
        [SerializeField] private string _name;
        [SerializeField] private string _description;
    }
    public class Hero : MonoBehaviour
    {
        [SerializeField] private HeroEfficiencyConfiguration _configuration;
        private List<IEffiency> _heroEffeciency;


        private void Awake()
        {
            _heroEffeciency = _configuration.GetEfficiencyList();
        }

    }
}

namespace Menace
{
}

namespace DragAndDrop
{
}
