using System;

namespace CV_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WindowHeight = 50;
            Console.WindowWidth = 128;
            Console.CursorVisible = false;
            
            Game game = new Game();

            while (game.survive)
            {
                game.setFight();
                while (game.chosenOpponent.alive && game.survive)
                {
                    game.renderFrame();
                    while (!game.moveMade)
                    {
                        game.move();
                        game.renderFrame();
                    }
                    game.printAction();
                    game.moveMade = false;
                    if (!game.chosenOpponent.alive)
                    {
                        game.switchMed = true;
                        game.renderFrame();
                        while (!game.moveMade)
                        {
                            game.move();
                            game.renderFrame();
                        }
                        game.printAction();
                    }
                }
                game.switchMed = false;
                game.moveMade = false;
                game.chosenOpponent.respawnDead();
            }
        }
    }
}