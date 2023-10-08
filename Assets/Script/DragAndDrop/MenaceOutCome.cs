using UnityEngine;

namespace DragAndDrop
{
    public class MenaceOutCome : MonoBehaviour
    {
        [SerializeField] HeroOnDutyController _heroOnDutyController;
        [SerializeField] LifeController _lifeController;


        public void MenaceDefeted(int idMenace)
        {
            _heroOnDutyController.TaskCompleted(idMenace);

            //usar el id para hacer aparecer los string en el canal de texto

        }

        public void MenaceLost(int idMenace)
        {
            _heroOnDutyController.TaskCompleted(idMenace);
            _lifeController.RemoveLife();

            //usar el id para hacer aparecer los string en el canal de texto

        }
    }
}
