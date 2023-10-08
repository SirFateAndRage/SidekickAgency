using Heroes;
using Menace;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DragAndDrop
{
    public class LifeController : MonoBehaviour
    {
        [SerializeField] List<Image> _lifes;

        public void RemoveLife()
        {
            if (_lifes.Count < 0)
                return;

            _lifes[0].enabled = false;
            _lifes.RemoveAt(0);

            if(_lifes.Count <= 0)
            {
                //hacer la pantalla de derrota easy peasy
            }
        }
    }

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
    public class HeroOnDutyController : MonoBehaviour
    {
        private List<JobInProgress> _jobInProgress = new List<JobInProgress>();

        public void SetHeroToWork(Hero hero, MenaceStructure menaceStructure,MenaceIconFill menaceIconFill,MenaceIcon menaceIcon)
        {
            RemoveHeroFromWork(hero);

            foreach (JobInProgress item in _jobInProgress)
            {
                if (item.Id != menaceStructure.Id)
                    continue;

                if (item.Hero == hero)
                    return;

                item.AddHero(hero);
                return;
            }

            JobInProgress jobInProgres = new JobInProgress();


            _jobInProgress.Add(jobInProgres);
            jobInProgres.Init(menaceStructure, hero,menaceIconFill,menaceIcon);
        }

        public void RemoveHeroFromWork(Hero hero)
        {
            foreach (JobInProgress item in _jobInProgress)
            {
                item.RemoveHero(hero);
            }
        }


        public void TaskCompleted(int id)
        {
            JobInProgress jobToremove = null;
            foreach (var item in _jobInProgress)
            {
                if (item.Id != id)
                    continue;
                jobToremove = item;
            }

            if (null == jobToremove)
                return;

            jobToremove.EndedTask();
            _jobInProgress.Remove(jobToremove);
        }
    }
}
