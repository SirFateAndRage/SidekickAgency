using UnityEngine;
using UnityEngine.UI;

namespace DragAndDrop
{
    public class MenaceIcon : MonoBehaviour
    {
        [SerializeField] Image _imageCurrentImage;

        private Sprite _menaceSprite;


        public void Init(Sprite menaceSprite)
        {
            _imageCurrentImage.sprite = menaceSprite;
            _menaceSprite = menaceSprite;
        }

        public void OnWorkingHeroe(Sprite heroSprite)
        {
            _imageCurrentImage.sprite = heroSprite;
        }

        public void OnNoHero()
        {
            _imageCurrentImage.sprite = _menaceSprite;
        }

    }
}
