using System.Collections.Generic;
using UnityEngine;

namespace DragAndDrop
{
    public class MenaceOutCome : MonoBehaviour
    {
        [SerializeField] HeroOnDutyController _heroOnDutyController;
        [SerializeField] LifeController _lifeController;

        [SerializeField] private Dictionary<int, IEffectExecution> _effectExetution = new Dictionary<int, IEffectExecution>();


        public void MenaceDefeted(int idMenace)
        {
            PlayEffect(idMenace);
            _heroOnDutyController.TaskCompleted(idMenace);

            //usar el id para hacer aparecer los string en el canal de texto

        }

        public void MenaceLost(int idMenace)
        {
            PlayEffect(idMenace);

            _heroOnDutyController.TaskCompleted(idMenace);
            _lifeController.RemoveLife();

            //usar el id para hacer aparecer los string en el canal de texto

        }

        public void SetEffect(int idMenace, IEffectExecution effect)
        {
            Debug.Log("Agrego la interfaz");
            if (_effectExetution.ContainsKey(idMenace))
            {
                return;
            }

            _effectExetution.Add(idMenace, effect);
        }

        private void PlayEffect(int id)
        {
            if (!_effectExetution.ContainsKey(id))
                return;
            Debug.Log("ESTO FUNCIONA?");
            _effectExetution[id].Execute();
        }
    }
}
