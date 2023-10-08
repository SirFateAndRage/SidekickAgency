using DragAndDrop;
using ObjectPool;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Menace
{
    [System.Serializable]
    public struct HeroActivation
    {
        [SerializeField] GameObject _heroContainer;
        [SerializeField] private float TimeToShow;

        public GameObject HeroContainer { get => _heroContainer;}
        public float TimeToShow1 { get => TimeToShow;}
    }
    public class HeroActivator : MonoBehaviour
    {
        [SerializeField]
        private List<HeroActivation> _heroActivation;
    }
    public class MenaceConfigurationManager : MonoBehaviour
    {
        [SerializeField] private LevelMenaceConfiguration _levelMenaceConfiguration;
        [SerializeField] private float _levelTime;

        [SerializeField] private Transform _WorldSpaceCanvas;
        [SerializeField] private Transform _camera;

        [SerializeField] private MenaceRecyclableObject _menaceObject;
        [SerializeField] private List<MenaceTransform> _menaceTransforms = new List<MenaceTransform>();
        [SerializeField] private HeroOnDutyController _heroOnDutyController;
        [SerializeField] private MenaceOutCome _menaceOutCome;

        [SerializeField] private TMP_Text _timerText;

        [Header("heromodel")]
        [SerializeField] private HeroController _heroController;
        [SerializeField] private Transform _becierTransform;

        private float _timeMultiplier =1f;

        private IObjectPool _menacePool;
        private float _menaceTimeSpawner;
        private bool _menaceExist = true;
        private int _currentMenaceIndex = 0;
        private MenaceStructure _currentMenaceStructure;

        private int count;

        private void Awake()
        {
            SetCurrentMenace();
            _menacePool = new ObjectPool.ObjectPool(_menaceObject);
            _timerText.text = _levelTime.ToString();
        }

        private void Start()
        {
            //SpawnObject(_currentMenaceStructure);
        }

        private void SetCurrentMenace()
        {
            if (_currentMenaceIndex < _levelMenaceConfiguration.MenacesStructure.Count)
            {
                _currentMenaceStructure = _levelMenaceConfiguration.MenacesStructure[_currentMenaceIndex];
                _currentMenaceIndex++;
                return;

            }

            _menaceExist = !_menaceExist;
        }

        private void Update()
        {
            _levelTime -= Time.deltaTime * _timeMultiplier;

            TimeSpan time = TimeSpan.FromSeconds(_levelTime);

            // Formatear el tiempo a minutos y segundos
            _timerText.text = string.Format("{0:D2}:{1:D2}", time.Minutes, time.Seconds);
  

            if (!_menaceExist)
                return;
            if (_levelTime <= 0)
                return;



            if (_levelTime <= _currentMenaceStructure.MenaceGameTime)
            {
                MenaceStructure current = _currentMenaceStructure;
                SetCurrentMenace();
                SpawnObject(current);
            }
        }




        public void SpawnObject(MenaceStructure menaceStructure)
        {
            GameObject menaceObject = _menacePool.GetGameObject();

            menaceObject.transform.parent = _WorldSpaceCanvas;

            Transform currentBuilding = _menaceTransforms[count].IconToPlaceMenace;

            Transform effectTransfrom = _menaceTransforms[count].PlaceToPutEffect;

            GameObject effect = menaceStructure.CityEffect;

            GameObject currentEffect = Instantiate(effect, effectTransfrom.position, Quaternion.identity, effectTransfrom);
            currentEffect.SetActive(true);

            currentEffect.TryGetComponent<IEffectExecution>(out IEffectExecution effectExecutor);

            if(effectExecutor != null)
            {
                _menaceOutCome.SetEffect(menaceStructure.Id, effectExecutor);
            }




            menaceObject.TryGetComponent(out MenaceInitConfigurator configurator);

            configurator.InitIcon(menaceStructure, _camera, currentBuilding,_heroOnDutyController,_menaceOutCome,_heroController,currentEffect.transform.position,_becierTransform);

            count++;
        }

        public void SuperTime()
        {
            _timeMultiplier = 2f;
        }

        public void ResetSpeed()
        {
            _timeMultiplier = 1f;
        }
    }

}


