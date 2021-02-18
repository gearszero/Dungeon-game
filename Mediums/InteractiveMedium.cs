using System;

namespace CV_1
{
    public class InteractiveMedium
    {
        public String text;
        public int screenType; //0-FIGHT 1-YES/NO
        public GameScreen gameScreen;
        public Coordinates coordinates;
        public Fight fight;
        
        public bool enter = false;

        public int row; // 1,2,3,4

        private int ySpace = 4; //+1 mezi 47-12=35 od35-do47
        private int xSpace = 123;

        private int top = 35;
        private int bottom = 47;
        



        public InteractiveMedium(String text, int screenType, int row, GameScreen gameScreen,Fight fight)
        {
            this.text = text;
            this.screenType = screenType;
            this.row = row;
            this.gameScreen = gameScreen;
            this.fight = fight;

        }

        public void printMedium()
        {
            char[] tmp = text.ToCharArray();
            if (row == 1)
            {
                coordinates.y = top + ySpace;
                coordinates.x = 10;
                fillArr(coordinates.y,coordinates.x,tmp);
            }
            else if (row == 2 && screenType == 0)
            {
                coordinates.y = top + (ySpace*2)+1;
                coordinates.x = 10;
                fillArr(coordinates.y,coordinates.x,tmp);
            }
            else if (row == 3)
            {
                coordinates.y = top + ySpace;
                coordinates.x = 35;
                fillArr(coordinates.y ,coordinates.x,tmp);
            }
            else if (row == 4 && screenType == 0)
            {
                coordinates.y = top + (ySpace*2)+1;
                coordinates.x = 35;
                fillArr(coordinates.y ,coordinates.x,tmp);
            }
        }

        public void fillArr(int y, int x, char[] tmp)
        {
            for (int i = 0; i < tmp.Length; i++)
            {
                gameScreen.gameArr[y, x+i] = tmp[i];
            }
        }

        public void doAction()
        {
            if (enter)
            {
                if (row == 1 && screenType == 0)
                {
                    fight.attack = true;
                    fight.fightMonster();
                }
                else if (row == 2 && screenType == 0)
                {
                    fight.defend = true;
                    fight.fightMonster();
                }
                else if (row == 3 && screenType == 0)
                {
                    fight.escape = true;
                    fight.fightMonster();
                }
                else if (row == 1 && screenType == 1)
                {
                    fight.action();
                }
                else if (row == 3 && screenType == 1)
                {
                    fight.noAction();
                }
            }
        }
    }
}