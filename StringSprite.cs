using System;

namespace CV_1
{
    public class StringSprite
    {
        public char[,] data;
        public int positionY; //height
        public int positionX; //center
        public int count = 0;

        public StringSprite(int positionY, int positionX)
        {
            this.positionY = positionY;
            this.positionX = positionX;
            
            data = new char[8,40];
        }

        public void fillSprite(String sentence)
        {

        }
    }
    
    
   // public void fillData
}