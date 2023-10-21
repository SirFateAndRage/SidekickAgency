using UnityEngine;

namespace Menace
{
    public class FloatingImage : MonoBehaviour
    {

        [SerializeField] private Vector3 _offSett;

        public void ConfigureFloatingImage(Transform cameraTransform, Transform buildingTransform)
        {
            transform.position = buildingTransform.position + _offSett;
            

            transform.LookAt(cameraTransform);
            transform.Rotate(Vector3.up, 180);
        }

        public void Reset()
        {
            transform.parent = null;
            
        }
    }
}
