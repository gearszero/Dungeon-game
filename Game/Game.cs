using System;

namespace CV_1
{
    public class Game
    {
        private Character hero;
        private MovableMedium movableMedium;
        private Stats stats;
        public bool survive = true;
        public bool switchMed = false;
        public bool moveMade = false;

        private static int monsterCount = 11;
        
        //ARRAYS
        private Monster[] monsterArr = new Monster[monsterCount];
        private Item[] itemArr = new Item[12];
        private InteractiveMedium[] mediumArr = new InteractiveMedium[4];
        private BuffItem[] buffItem = new BuffItem[4];

        //ChosenStuff
        public Monster chosenOpponent;
        private Item chosenItem;
        
        //screen
        private GameScreen gameScreen;

        private Fight fight;
        
        private Random rnd;
        private int timeToWait = 1000; //1000-1s

        public Game()
        {
            hero = new Character("Nemu", 100, 12, 7, 100);
            gameScreen = new GameScreen(49,125);
            movableMedium = new MovableMedium(gameScreen);
            stats = new Stats(hero,gameScreen);

            rnd = new Random();
            
            stats.makeStats();
            createMonsters();
            itemList();
            buffItemList();
        }

        //SCREEN CONTROL
        public void renderFrame()
        {
            gameScreen.deleteGameScreen();
            gameScreen.fillScreen();
            printStats();
            if (switchMed)
            {
                switchMedium();
                fight.stringSpritesArr[0] = new StringSprite($"Do you want to search {chosenOpponent.nameOfCharacter} body?", gameScreen, 0, true);
                
            }
            printEncounter();
            movableMedium.printMedium();
            for (int i = 0; i < mediumArr.Length; i++)
            {
                mediumArr[i].printMedium();
                gameScreen.printScreen();
            }
            stats.makeStats();
            gameScreen.printScreen();
        }

        //PRINTING MSGS FROM FIGHT CLASS
        public void printAction()
        {
            for (int i = 1; i < fight.stringSpritesArr.Length; i++)
            {
                if (fight.stringSpritesArr[i].visible)
                {
                    fight.stringSpritesArr[i].putString();
                    gameScreen.printScreen();
                    System.Threading.Thread.Sleep(timeToWait);
                }
            }
        }

        //PRINTING NAME AND HP OF MONSTER YOU ENCOUNTERED
        public void printEncounter()
        {
            fight.stringSpritesArr[0].putString();
            gameScreen.printScreen();
        }

        //PRINTING STATS OF YOUR HERO
        public void printStats()
        {
            for (int i = 0; i < stats.stringSpritesArr.Length; i++)
            {
                stats.stringSpritesArr[i].putCorner();
                gameScreen.printScreen();
            }
        }


        //GAME CONTROL
        public void move()
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey();

                switch (keyInfo.Key)
                {
                    case ConsoleKey.Enter:
                        for (int i = 0; i < mediumArr.Length; i++)
                        {
                            if (mediumArr[i].coordinates.y == movableMedium.currentPosition.y &&
                                mediumArr[i].coordinates.x == (movableMedium.currentPosition.x + 2))
                            {
                                mediumArr[i].enter = true;
                                mediumArr[i].doAction();
                                if (!fight.survive)
                                {
                                    survive = false;
                                }
                                moveMade = true;
                            }
                        }
                        break;
                    
                    case ConsoleKey.UpArrow:
                        if (movableMedium.currentPosition.y == 39)
                        {
                        }
                        else
                        {
                            movableMedium.move(-5, 0);
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (movableMedium.currentPosition.y == 44)
                        {
                        }
                        else if (mediumArr[1].screenType == 0)
                        {
                            movableMedium.move(5, 0);
                        }
                        break;
                    case ConsoleKey.RightArrow: 
                        if (movableMedium.currentPosition.x == 33)
                        {
                        }
                        else
                        {
                            movableMedium.move(0, 25);
                        }
                        break;

                    case ConsoleKey.LeftArrow:
                        if (movableMedium.currentPosition.x == 8)
                        {
                        }
                        else
                        {
                            movableMedium.move(0, -25);
                        }
                        break;
            }
        }

        //CREATING MONSTERS
        private void createMonsters()
        {
            monsterArr[0] = new Monster("Imp", 20, 5,3, 20,1, 300, 10);
            monsterArr[1] = new Monster("Low Demon", 35, 7, 1, 35,2,500, 20);
            monsterArr[2] = new Monster("Demonoid", 55, 2, 6, 55,3,650, 15);
            monsterArr[3] = new Monster("Demon", 45, 5, 5, 45,4,750, 25);
            monsterArr[4] = new Monster("Demon wolf", 30, 7, 2,30, 5,850, 20);
            monsterArr[5] = new Monster("Demon general", 60, 10, 7,60, 6,875, 300);
            monsterArr[6] = new Monster("Demon Mah-Lu", 70, 16, 3,70, 7,900, 500);
            monsterArr[7] = new Monster("Small Pick", 100, 1, 10,100, 8,920, 80);
            monsterArr[8] = new Monster("Demon Warrior", 50, 8, 7,50, 9,990, 100);
            monsterArr[9] = new Monster("Demon King", 280, 20, 10, 280,100,1000, 1000);
            monsterArr[10] = new Monster("God",700,0,0,700,666,6666, 10000);
        }

        //CHOOSING NEXT OPONENT
        private Monster chooseMonster()
        {
            if (monsterArr[9].godCount == 3)
            {
                chosenOpponent = monsterArr[10];
                return chosenOpponent;
            }
            else
            {
                int randomNumber = rnd.Next(1, 1000);
                for (int i = 0; i < monsterArr.Length; i++)
                {
                    if (monsterArr[i].rarity >= randomNumber)
                    {
                        chosenOpponent = monsterArr[i];
                        return chosenOpponent;
                        //break;
                    }
                }
            }
            return monsterArr[0];
        }

        //SETUP FIGHT AND PRINTING MEDIUM AT LEFT CORNER
        public void setFight()
        {
            fight = new Fight(chooseMonster(),hero,gameScreen,itemArr,buffItem);
            
            mediumArr[0] = new InteractiveMedium("ATTACK",0, 1,gameScreen,fight);
            mediumArr[1] = new InteractiveMedium("TRY TO ESCAPE",0, 3,gameScreen,fight);
            mediumArr[2] = new InteractiveMedium("DEFEND",0, 2,gameScreen,fight);
            mediumArr[3] = new InteractiveMedium("INVENTORY",0, 4,gameScreen,fight);
        }

        public void switchMedium()
        {
            mediumArr[0] = new InteractiveMedium("YES", 1, 1 ,gameScreen,fight);
            mediumArr[1] = new InteractiveMedium("", 2, 2 ,gameScreen,fight);
            mediumArr[2] = new InteractiveMedium("NO", 1, 3 ,gameScreen,fight);
            mediumArr[3] = new InteractiveMedium("", 2, 4 ,gameScreen,fight);
        }


        //MAKING ITEM LIST
        private void itemList()
        {
            itemArr[0] = new Item("Rusty Sword", 2, 0, 0, 1, hero,300);
            itemArr[1] = new Item("Rusty Armor Piece", 0, 2, 0, 2, hero,600);
            itemArr[2] = new Item("Ruby Cristal", 0, 0, 10, 3, hero,900);
            itemArr[3] = new Item("Sun Cristal", 2, 2, 10, 4, hero,1000);
            itemArr[4] = new Item("Moon Cristal", 4, 4, -10, 5, hero,1100);
            itemArr[5] = new Item("Silver Sword", 6, 0, 0, 6, hero,1200);
            itemArr[6] = new Item("Sacrifice gauntlet", -2, 10, -20, 7, hero,1250);
            itemArr[7] = new Item("Black Diamond", 0, 2, 50, 8, hero,1300);
            itemArr[8] = new Item("Parasite Necklace", -5, -5, -10, 9, hero,1350);
            itemArr[9] = new Item("Dragon Hammer", 10, 0, 0, 10, hero,1375);
            itemArr[10] = new Item("Shock Armor", 0, 15, 20, 11, hero,1400);
            itemArr[11] = new Item("God Sword", 20, 5, 30, 12, hero,1410);
            hero.maxHealthMatch();
        }

        private void buffItemList()
        {
            buffItem[0] = new BuffItem("Health Potion",1,20,hero);
            buffItem[1] = new BuffItem("Really Small Health Potion",2,5,hero);
            buffItem[2] = new BuffItem("Small Health Potion",3,10,hero);
            buffItem[3] = new BuffItem("Big Health Potion",4,30,hero);
        }
        //CHOOSE ITEM 
        private void chooseItem()
        {
            int randomNumber = rnd.Next(1, 1410);
            for (int i = 0; i < itemArr.Length; i++)
            {
                if (itemArr[i].itemRarity >= randomNumber)
                {
                    chosenItem = itemArr[i];
                    applyItem();
                    break;
                }
            }
        }

        //USING ITEM ON HERO
        private void applyItem()
        {
            Console.WriteLine($"You have found an item called {chosenItem.itemName}");
            chosenItem.addMaxDamage();
            chosenItem.addMaxDef();
            chosenItem.addMaxHealth();
            hero.printStats();
        }
        
        //BUFFING BOSS
        public void buffGod(int monsterID)
        {
            if (monsterID == 2)
            {
                monsterArr[10].maxDamage += 1;
            }
            else if (monsterID == 3)
            {
                monsterArr[10].maxDef += 1;
            }
            else if (monsterID == 6)
            {
                monsterArr[10].maxDef += 1;
                monsterArr[10].maxDamage += 1;
            }
            else if (monsterID == 7)
            {
                monsterArr[10].maxDef += 1;
                monsterArr[10].maxDamage += 4;
            }
            else if (monsterID == 8)
            {
                monsterArr[10].maxDef += 3;
            }
            else if (monsterID == 9)
            {
                monsterArr[10].maxDamage += -1;
            }
            else if (monsterID == 100)
            {
                monsterArr[10].maxDamage += 5;
                monsterArr[10].maxDef += 5;
            }
        }
    }
}