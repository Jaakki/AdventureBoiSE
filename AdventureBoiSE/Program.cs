using System;

namespace AdventureBoiSE
{
    class Program
    {
        static void Main(string[] args)     //MainMenu()
        {
            GameManager gm = new GameManager();
            bool showMenu = true;
            while (showMenu)
            {
                showMenu = gm.MainMenu();
            }
        }
    }
}
