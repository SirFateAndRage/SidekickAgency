using UnityEngine;

namespace Menace
{
    public class FloatingImage : MonoBehaviour
    {
        [SerializeField] private Transform _mainCamera;
        [SerializeField] private Transform _building;

        [SerializeField] private Vector3 _offSett;

        private void Start()
        {
            transform.position = _building.position + _offSett;

            transform.LookAt(_mainCamera);
        }
    }
}
