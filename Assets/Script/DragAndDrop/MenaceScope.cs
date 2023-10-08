using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace DragAndDrop
{
    public class MenaceScope : MonoBehaviour,IScope
    {
        [SerializeField]
        private GameObject _gameObject;

        public void SetActiveScope(bool active)
        {
            _gameObject.SetActive(active);
        }
    }
}
