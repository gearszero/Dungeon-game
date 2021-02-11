using System;
using System.Text;

namespace CV_1
{
    public class GameScreen
    {
        private int rows;
        private int cols;

        public char[,] gameArr;

        private const char background = ' ';
        public int topLimit = 5;
        private int downLimit = 12;

        public GameScreen(int rows, int cols)
        {
            this.rows = rows;
            this.cols = cols;
            
            gameArr = new char[rows,cols];
        }

        public void deleteGameScreen()
        {
            for (int y = 1; y < rows - 1; y++)
            {
                for (int x = 1; x < cols - 1; x++)
                {
                    gameArr[y, x] = background;
                }
            }
        }

        public void fillScreen()
        {
            const char wall = '█';
            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < cols; x++)
                {
                    if (y == 0 || y == (rows - 1))
                    {
                        gameArr[y, x] = wall;
                    }
                    else if (x == 0 || x == (cols - 1))
                    {
                        gameArr[y, x] = wall;
                    }
                    else if (y == 1 && x == 3 || y == 1 && x == (cols - 4))
                    {
                        gameArr[y, x] = wall;
                    }
                    else if ((y == 2 && (x == 2 || x == 1 || x == 3)) || (y == 2 && (x == (cols - 2) || x == (cols - 3) || x == (cols - 4))))
                    {
                        gameArr[y, x] = '▀';
                    }
                    else if (y == 35)
                    {
                        gameArr[y, x] = '▀';
                    }
                    else
                    {
                        gameArr[y, x] = background;
                    }
                }
            }
        }

        public void printScreen()
        {
            Console.SetCursorPosition(0, 0);
            for (int y = 0; y < rows; y++)
            {
                StringBuilder sb = new StringBuilder();
                for (int x = 0; x < cols; x++)
                {
                    sb.Append(gameArr[y, x]);
                }
                Console.WriteLine(sb);
            }
        }

        public void putString(String sentence)
        {
            int i = 0;
            char[] tmp = sentence.ToCharArray();
            int putInMiddle = (cols - tmp.Length) / 2;
            
            for (int x = putInMiddle; x < cols-putInMiddle; x++)
            {

                gameArr[8, 8] = 'x';
                if (i == tmp.Length-1)
                    break;
                i++;
            }

            if (topLimit > downLimit)
            {
                topLimit++;
            }
            else
            {
                topLimit = 5;
            }
        }
    }
}