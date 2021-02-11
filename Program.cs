using System;

namespace CV_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WindowHeight = 50;
            Console.WindowWidth = 128;
            
            Game game = new Game();

            while (game.survive == true)
            {
                game.renderFrame();
                game.fightMonster();
            }
        }
    }
}