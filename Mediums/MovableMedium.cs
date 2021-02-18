using System;

namespace CV_1
{
    public class MovableMedium
    {
        private char inMedium = '■';
        public Coordinates currentPosition = new Coordinates(39, 8);

        private GameScreen gameScreen;

        public MovableMedium(GameScreen gameScreen)
        {
            this.gameScreen = gameScreen;
        }
        
        public void move(int plusY, int plusX)
        {
            currentPosition.y += plusY;
            currentPosition.x += plusX;
            printMedium();
        }

        public void printMedium()
        {
            gameScreen.gameArr[currentPosition.y, currentPosition.x] = inMedium;
        }
    }
}