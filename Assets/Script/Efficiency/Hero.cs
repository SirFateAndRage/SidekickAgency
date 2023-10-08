using Efficiency;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Heroes
{
    public class Hero : MonoBehaviour
    {
        private int _id = -1;
        [SerializeField] private HeroEfficiencyConfiguration _configuration;
        [SerializeField] private HeroDataConfiguration _heroDataConfig;
        [SerializeField] private CanvasGroup _canvasGroup;
        private List<IEffiency> _heroEffeciency;
        private bool _isWorking;

        public List<IEffiency> HeroEffeciency { get => _heroEffeciency;}
        public int Id { get => _id; set => _id = value; }
        public HeroDataConfiguration HeroDataConfig { get => _heroDataConfig;}
        public bool IsWorking { get => _isWorking;}

        private void Awake()
        {
            _heroEffeciency = _configuration.GetEfficiencyList();
        }

        public void Workinghero(bool isWorking)
        {
            _isWorking = isWorking;

            if (isWorking)
            {
                _canvasGroup.alpha = 0.5f;
                return;
            }

            _canvasGroup.alpha = 1f;
        }

    }
}
