using DragAndDrop;
using ObjectPool;
using System.Collections.Generic;
using UnityEngine;

namespace Menace
{
    public class MenaceConfigurationManager : MonoBehaviour
    {
        [SerializeField] private LevelMenaceConfiguration _levelMenaceConfiguration;
        [SerializeField] private float _levelTime;

        [SerializeField] private Transform _WorldSpaceCanvas;
        [SerializeField] private Transform _camera;

        [SerializeField] private MenaceRecyclableObject _menaceObject;
        [SerializeField] private List<Transform> _buildingTransform;

        private IObjectPool _menacePool;
        private float _menaceTimeSpawner;
        private bool _menaceExist = true;
        private int _currentMenaceIndex = 0;
        private MenaceStructure _currentMenaceStructure;

        private void Awake()
        {
            SetCurrentMenace();
            _menacePool = new ObjectPool.ObjectPool(_menaceObject);
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
            if (!_menaceExist)
                return;
            if (_levelTime <= 0)
                return;

            _levelTime -= Time.deltaTime;

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

            Transform currentBuilding = _buildingTransform[0];
            _buildingTransform.RemoveAt(0);

            menaceObject.TryGetComponent(out MenaceInitConfigurator configurator);

            configurator.InitIcon(menaceStructure, _camera, currentBuilding);

        }
    }

}


