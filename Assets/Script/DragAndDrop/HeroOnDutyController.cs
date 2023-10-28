using Heroes;
using Menace;
using System.Collections.Generic;
using UnityEngine;

namespace DragAndDrop
{
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

        public Hero GetheroFromJob(int id)
        {
            foreach (var item in _jobInProgress)
            {
                if (item.Id != id)
                    continue;

                return item.Hero;

            }

            return null;
        }
    }
}
