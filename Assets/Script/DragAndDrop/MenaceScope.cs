using UnityEngine;
using UnityEngine.UI;

namespace DragAndDrop
{
    public class MenaceScope : MonoBehaviour,IScope
    {
        [SerializeField]
        private Image _image;
        public void SetActiveScope(bool active)
        {
            _image.enabled = active;
        }
    }
}
