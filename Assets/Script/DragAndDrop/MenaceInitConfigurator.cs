using Menace;
using UnityEngine;

namespace DragAndDrop
{
    public class MenaceInitConfigurator : MonoBehaviour
    {
        [SerializeField] private FloatingImage _floatingImage;
        [SerializeField] private MenaceIcon _menaceIcon;
        [SerializeField] private MenaceRecyclableObject _menaceRecyclableObject;
        [SerializeField] private EffectivesEmotes _effectiveEmotes;

        private MenaceStructure _menaceStructure;


        public void InitIcon(MenaceStructure menaceStructure, Transform cameraTransform, Transform buildingTransform)
        {
            _menaceStructure = menaceStructure;

            _effectiveEmotes.InitEffectivesEmote(_menaceStructure);
            _floatingImage.ConfigureFloatingImage(cameraTransform, buildingTransform);
            _menaceIcon.SetFillSpeed(_menaceStructure.MenaceMultiplicator);
            _menaceRecyclableObject.OnRevived();
        }
    }
}
