using System;
using System.Collections.Generic;
using System.Text;

namespace AdventureBoiSE
{
    public class Monster
    {
        public string Name;
        public int Health;
        public int Damage;
        public int HitCha;
        public int EXP;
        public bool MonsterAlive;

        public Monster(string name, int health, int damage, int hitChance, int EXP, bool MonsterAlive)     //Goblin & Orc
        {
            this.Name = name;
            this.Health = health;
            this.Damage = damage;
            this.HitCha = hitChance;
            this.EXP = EXP;
            this.MonsterAlive = MonsterAlive;
        }

        public void TakeDamage(int damage)
        {
            this.Health -= damage;
            if (this.Health <= 0)
            {
                MonsterAlive = false;
            }
        }

        //public Monster(int health, string name, int damage, int hitChance, int EXP, bool MonsterAlive)     //Orc
        //{
        //    this.Name = name;
        //    this.Health = health;
        //    this.Damage = damage;
        //    this.HitCha = hitChance;
        //    this.EXP = EXP;
        //    this.MonsterAlive = MonsterAlive;
        //}


        //public Monster GenerateMonster()
        //{
        //    var x = RandomInt(1,3);
        //    if (x == 1)
        //    {
        //        Monster goblin = new Monster("Goblin", 1, 1, 4, 1);
        //        return goblin;
        //    }
        //    else
        //    {
        //        Monster orc = new Monster(2, "Orc", 2, 3, 2);
        //        return orc;
        //    }
        //}
    }
}
