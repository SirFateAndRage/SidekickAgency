using Heroes;
using Menace;
using System.Collections.Generic;
using UnityEngine;

namespace DragAndDrop
{
    public class JobInProgress
    {
        private Hero _hero;
        private MenaceStructure _menaceStructure;
        private MenaceIconFill _menaceIconFill;
        private MenaceIcon _menaceIcon;
        public int Id => _menaceStructure.Id;

        public Hero Hero { get => _hero;}

        public void Init(MenaceStructure menaceStructre,Hero hero,MenaceIconFill menaceIconFill,MenaceIcon menaceIcon)
        {
            _menaceStructure = menaceStructre;
            _menaceIconFill = menaceIconFill;
            _menaceIcon = menaceIcon;
            AddHero(hero);
        }

        public void RemoveHero(Hero hero)
        {
            if (_hero != hero)
            {
                return;
            }
            _hero.Workinghero(false);
            _hero = null;
            _menaceIcon.OnNoHero();

            _menaceIconFill.SetFillSpeed(_menaceStructure.MenaceMultiplicator);
        }

        public void ChangeHero(Hero hero)
        {
            if (_hero == hero)
                return;

            if(_hero == null)
            {
                _hero = hero;
                _hero.Workinghero(true);
                return;
            }

            if(_hero != hero)
            {
                _hero.Workinghero(false);
                _hero = hero;
                _hero.Workinghero(true);
            }

            _menaceIcon.OnWorkingHeroe(_hero.HeroDataConfig.Image);
     
        }

        public void AddHero(Hero hero)
        {
            if(_hero == null)
                _hero = hero;

            _hero.Workinghero(true);
            _menaceIcon.OnWorkingHeroe(_hero.HeroDataConfig.Image);

        }

        public void EndedTask()
        {
            _hero.Workinghero(false);
            _hero = null;
        }
    }
}
