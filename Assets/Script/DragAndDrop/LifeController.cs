using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
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
            _count = _lifes.Count;
        }
        public void RemoveLife()
        {
            if (_count < 0)
                return;

            _lifes[_count - 1].GetComponentInParent<Animator>().SetTrigger("LifeOff");
            //_lifes[_count -1].enabled = false;
            _count--;

            if (_count == 0)
                GameCondition();
           // Invoke("DisableObject", .5f);

        }

        public void GameCondition()
        {
            if (_count <= 0)
            {
                _badEndGame.SetActive(true);
                return;
            }

            _winGame.SetActive(true);
        }

        public void DisableObject()
        {
            _lifes[_count - 1].enabled = false;
            if (_count == 0)
                GameCondition();
        }
    }
}
