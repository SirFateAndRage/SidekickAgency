using ObjectPool;
using UnityEngine;

namespace Menace
{
    public class MenaceRecyclableObject : RecyclableObject
    {
        [SerializeField] private FloatingImage _floatImage;
        public override void OnRecycle()
        {
            gameObject.SetActive(false);
            _floatImage.Reset();
        }

        public override void OnRevived()
        {
            gameObject.SetActive(true);
        }
    }

}


