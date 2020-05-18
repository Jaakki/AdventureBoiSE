using System;
using System.Collections.Generic;
using System.Text;

namespace AdventureBoiSE
{
    public class Player
    {
        public string PlayerName { get; set; }
        public int PlayerExperience { get; set; }
        public int PlayerDamage { get; set; }
        public int PlayerHealth { get; set; }
        public int PlayerChanceHit { get; set; }
        public bool PlayerAlive { get; set; }

        public void TakeDamageP(int damage)
        {
            this.PlayerHealth -= damage;
            if (this.PlayerHealth <= 0)
            {
                PlayerAlive = false;
            }
        }

        //public Player(string name, int EXP, int DMG)
        //{
        //    this.PlayerName = name;
        //    this.PlayerExperience = EXP;
        //    this.PlayerDamage = DMG;
        //}

        #region Generate Player Method
        //public Player GeneratePlayer()
        //{
        //    Console.Clear();
        //    Player player = new Player();
        //    Console.WriteLine("Name your adventurer:");
        //    player.PlayerName = Console.ReadLine();
        //    while (string.IsNullOrEmpty(player.PlayerName))
        //    {
        //        Console.WriteLine("Invalid name given, try again:");
        //        player.PlayerName = Console.ReadLine();
        //    }
        //    player.PlayerAlive = true;
        //    player.PlayerHealth = 5;
        //    player.PlayerExperience = 0;
        //    player.PlayerDamage = 1;
        //    player.PlayerChanceHit = 3;
        //    return player;
        //}
        #endregion
    }
}
