using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DragAndDrop
{
    public class LifeController : MonoBehaviour
    {
        [SerializeField] List<Image> _lifes;
        [SerializeField] GameObject _badEndGame;
        [SerializeField] GameObject _winGame;

        private int _count;

        private void Awake()
        {
            _count = _lifes.Count -1;
        }
        public void RemoveLife()
        {
            if (_count < 0)
                return;

            _lifes[_count].enabled = false;

            if (_count <= 0)
            {
                _badEndGame.SetActive(true);
            }

            _count--;

        }

        public void Win()
        {
            if (_count <= 0)
                return;
            _winGame.SetActive(true);
        }
    }
}
