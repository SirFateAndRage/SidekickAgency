using DragAndDrop;
using ObjectPool;
using UnityEngine;

namespace Menace
{
    public class MenaceRecyclableObject : RecyclableObject
    {
        [SerializeField] private FloatingImage _floatImage;
        [SerializeField] private MenaceIcon _menaceIcon;
        [SerializeField] private EffectivesEmotes _effectiveEmotes;
        public override void OnRecycle()
        {
            gameObject.SetActive(false);
            _floatImage.Reset();
            _menaceIcon.Reset();
            _effectiveEmotes.Reset();
            Recycle();
        }

        public override void OnRevived()
        {
            gameObject.SetActive(true);
        }
    }

}


