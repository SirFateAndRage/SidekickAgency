using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DragAndDrop
{
    public class LifeController : MonoBehaviour
    {
        [SerializeField] List<Image> _lifes;

        private int _count;

        private void Awake()
        {
            _count = _lifes.Count -1;
        }
        public void RemoveLife()
        {
            if (_count <= 0)
                return;

            _lifes[_count].enabled = false;
            _count--;

            if(_count<= 0)
            {
                //hacer la pantalla de derrota easy peasy
            }
        }
    }
}
