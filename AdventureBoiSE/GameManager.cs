using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml.Schema;
using System.Xml.Xsl;

namespace AdventureBoiSE
{
    public class GameManager    //ShowSceneDescription(Scene scene), GetRandomMonster(), DisplayScene(), GetPlayerSelection(), HandleSelection()
    {
        public List<Scene> scenes = new List<Scene>();
        //public bool FightContinues = false;
        public static Scene currentScene;
        //mainMenu()
        public void ShowScene(Scene scene)      //me näytetään tämä scene
        {
            Console.WriteLine(scene.SceneDescription);
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
                    return false;
                case "3":
                    return false;
                default:
                    return true;
            }
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
            do
            {
                Console.Clear();
                #region Generate new Encounter
                if (FightContinues == false)
                {
                    currentScene = GetRandomScene();
                }
                #endregion

                ShowScene(currentScene);

                Console.WriteLine("1) Hit enemy");
                switch (Console.ReadLine())
                {
                    case "1":
                        bool isHit = CheckIfHit(player.PlayerChanceHit);
                        if (isHit)
                        {
                            currentScene.monsters[0].TakeDamageM(player.PlayerDamage);
                        }
                        break;
                    default:
                        break;
                }

                currentScene.monsters[0].MonsterAlive = CheckIfAlive(currentScene.monsters[0].Health);
                Console.WriteLine($"monster Alive check:{currentScene.monsters[0].MonsterAlive}");

                if (currentScene.monsters[0].MonsterAlive == false)
                {
                    player.PlayerExperience += currentScene.monsters[0].EXP;
                    FightContinues = false;
                    Console.WriteLine("monsu kuoli");
                }
                else
                {
                    FightContinues = true;
                    bool isHit = CheckIfHit(currentScene.monsters[0].HitCha);
                    if (isHit)
                    {
                        player.TakeDamageP(currentScene.monsters[0].Damage);
                    }
                }

                if (player.PlayerHealth <= 0)
                {
                    player.PlayerAlive = false;
                }
                Console.ReadKey();
            } while (player.PlayerAlive);

            currentScene = scenes[3];
            Console.ReadKey();

        }
    }
}
