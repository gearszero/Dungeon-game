using System;
    

namespace CV_1
{
    public class StringSprite
    {
        public String sentence;
        public GameScreen gameScreen;
        public int sentencePosition;
        public bool visible = false;
        private int topLimit = 5;
        private int downLimit = 40;

        private int firstX = 62;
        private int firstY = 38;

        public StringSprite(String sentence, GameScreen gameScreen, int sentencePosition, bool visible)
        {
            this.sentence = sentence;
            this.gameScreen = gameScreen;
            this.sentencePosition = sentencePosition;
            this.visible = visible;
            topLimit = topLimit + sentencePosition;
            if (topLimit == downLimit)
            {
                topLimit = 5;
            }
        }

        public StringSprite(string sentence, GameScreen gameScreen,int sentencePosition)
        {
            this.sentence = sentence;
            this.gameScreen = gameScreen;
            this.sentencePosition = sentencePosition;
            firstY += sentencePosition;
        }

        public void putString()
        {
            if (visible)
            {
                char[] tmp = sentence.ToCharArray();
                int putInMiddle = ((gameScreen.cols - 1) - tmp.Length) / 2;
                int x = 0;

                for (int i = putInMiddle; i < (gameScreen.cols-1) - putInMiddle; i++)
                {
                    gameScreen.gameArr[topLimit, i] = tmp[x];
                    x++;
                    if (x == tmp.Length)
                    {
                        break;
                    }
                }
            }
        }

        public void putCorner()
        {
            char[] tmp = sentence.ToCharArray();
            for (int i = 0; i < tmp.Length; i++)
            {
                gameScreen.gameArr[firstY, firstX + i] = tmp[i];
            }
        }
    }
    
    
   // public void fillData
}