using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml.Schema;
using System.Xml.Xsl;

namespace AdventureBoiSE
{
    public class GameManager    //ShowSceneDescription(Scene scene), GetRandomMonster(), DisplayScene(), GetPlayerSelection(), HandleSelection()
    {
        public List<Player> previousRuns = new List<Player>();
        public List<Scene> scenes = new List<Scene>();
        //public bool FightContinues = false;
        public static Scene currentScene;
        //mainMenu()
        public void ShowScene(Scene scene)      //me näytetään tämä scene
        {
            Console.WriteLine(scene.SceneDescription);
        }

        public void DisplayPlayerData(int health, int exp)
        {
            Console.SetCursorPosition(40,20);
            Console.Write($"\tExperience: {exp}");
            Console.Write("\tHealth: ");
            for (int i = 0; i < health; i++)
            {
                Console.Write($"♥");
            }
            Console.SetCursorPosition(0,0);
        }

        public Player GeneratePlayer()
        {
            Console.Clear();
            Player player = new Player();
            Console.WriteLine("Name your adventurer:");
            player.PlayerName = Console.ReadLine();
            while (string.IsNullOrEmpty(player.PlayerName))
            {
                Console.WriteLine("Invalid name given, try again:");
                player.PlayerName = Console.ReadLine();
            }
            player.PlayerAlive = true;
            player.PlayerHealth = 5;
            player.PlayerExperience = 0;
            player.PlayerDamage = 1;
            player.PlayerChanceHit = 3;
            return player;
        }

        public void InitializeGame()
        {
            #region Scenes
            Scene storyScene = new Scene();
            storyScene.SceneID = 0;
            storyScene.SceneDescription = "You are traveling in the woods, but then you trip on a branch.\nYou bumble your way into a camp of orcs and goblins.\nYou must fight!";

            Scene encounter1 = new Scene();
            encounter1.SceneID = 1;
            encounter1.SceneDescription = "Nasty little goblin comes at you with a knife!";

            Scene encounter2 = new Scene();
            encounter2.SceneID = 2;
            encounter2.SceneDescription = "Mighty orc charges at you!";

            Scene youDied = new Scene();
            youDied.SceneID = 3;
            youDied.SceneDescription = "You have died!";

            scenes.Add(storyScene);
            scenes.Add(encounter1);
            scenes.Add(encounter2);
            scenes.Add(youDied);

            currentScene = scenes[0];
            #endregion

            #region Monsters
            if ((encounter1.monsters.Count + encounter2.monsters.Count) > 0)
            {
                Console.WriteLine("Tule tänne!");
                currentScene.monsters.Clear();
            }
            Monster goblin = new Monster("Goblin", 1, 1, 4, 1, true);
            Monster orc = new Monster("Orc",2, 2, 3, 2, true);
            encounter1.monsters.Add(goblin);
            encounter2.monsters.Add(orc);
            #endregion
        }

        public Scene GetRandomScene()
        {
            int x = Dice.RandomInt(1,3);
            return scenes[x];
        }

        public int Heal(int health)
        {
            if (health < 5)
            {
                Console.WriteLine("Heal +1");
                return (health + 1);
            }
            else
            {
                Console.WriteLine("I'm too full");
                return (health + 0);
            }
        }

        public bool CheckIfHit(int yourHitChance)
        {
            int hit = Dice.RandomDice();
            if (hit >= yourHitChance)
            {
                Console.WriteLine("Hit!");
                return true;
            }
            else
            {
                Console.WriteLine("Miss!");
                return false;
            }
        }

        //public void TakeDamage(int health, int damage)
        //{
        //    health -= damage;
        //}

        public bool MainMenu()
        {
            Console.Clear();
            Console.WriteLine("1) Start game");
            Console.WriteLine("2) Score scene");
            Console.WriteLine("3) Quit game");
            switch (Console.ReadLine())
            {
                case "1":
                    BeginGame();
                    return true;
                case "2":
                    ShowScore();
                    return true;
                case "3":
                    return false;
                default:
                    return true;
            }
        }

        public void ShowScore()
        {
            Console.Clear();
            if (previousRuns.Count == 0)
            {
                Console.WriteLine("You have no recorded runs.");
            }
            else
            {
                foreach (Player player in previousRuns)
                {
                    Console.WriteLine($"Name: {player.PlayerName} - Experience: {player.PlayerExperience}");
                }
            }
            Console.ReadLine();
        }

        public bool CheckIfAlive(int health)
        {
            if (health <= 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void BeginGame()
        {
            Player player = GeneratePlayer();
            Console.Clear();
            InitializeGame();
            ShowScene(currentScene);
            Console.WriteLine("Press any key to continue: ");
            Console.ReadKey();
            GameplayLoop(player);
        }

        public void GameplayLoop(Player player)
        {
            bool FightContinues = false;
            Monster monster = new Monster();
            
            do
            {
                Console.Clear();
                DisplayPlayerData(player.PlayerHealth, player.PlayerExperience);
                #region Generate new Encounter
                if (FightContinues == false)
                {
                    currentScene = GetRandomScene();
                    monster = currentScene.monsters[0];
                    #region HYI!
                    if (monster.Name == "Goblin")
                    {
                        monster.Health = 1;
                        monster.MonsterAlive = true;
                    }
                    if (monster.Name == "Orc")
                    {
                        monster.Health = 2;
                        monster.MonsterAlive = true;
                    }
                    #endregion
                }
                #endregion
                ShowScene(currentScene);

                Console.WriteLine("1) Hit enemy");
                Console.WriteLine("2) Eat some meat");
                Console.WriteLine("3) Flee to Main Menu");
                switch (Console.ReadLine())
                {
                    case "1":
                        Console.Write("Player: ");
                        bool isHit = CheckIfHit(player.PlayerChanceHit);
                        if (isHit)
                        {
                            monster.TakeDamageM(player.PlayerDamage);
                        }
                        break;
                    case "2":
                        player.PlayerHealth = Heal(player.PlayerHealth);
                        break;
                    case "3":
                        return;
                    default:
                        break;
                }

                monster.MonsterAlive = CheckIfAlive(monster.Health);

                if (monster.MonsterAlive == false)
                {
                    player.PlayerExperience += monster.EXP;
                    FightContinues = false;
                    Console.WriteLine($"{monster.Name} dies");
                }
                else
                {
                    FightContinues = true;
                    Console.Write("Enemy: ");
                    bool isHit = CheckIfHit(monster.HitCha);
                    if (isHit)
                    {
                        player.TakeDamageP(monster.Damage);
                    }
                }

                if (player.PlayerHealth <= 0)
                {
                    player.PlayerAlive = false;
                }
                Console.ReadKey();
            } while (player.PlayerAlive);
            previousRuns.Add(player);
            currentScene = scenes[3];
            ShowScene(currentScene);
            Console.ReadKey();
        }
    }
}
