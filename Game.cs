using System;

namespace CV_1
{
    public class Game
    {
        private Character hero;
        public Monster monster;
        public Item item;
        public BuffItem buffItem;
        public bool survive = true;

        private static int monsterCount = 11;
        public Monster[] monsterArr = new Monster[monsterCount];
        public Item[] itemArr = new Item[12];
        public Monster chosenOpponent;
        public Item chosenItem;
        
        //screen
        public GameScreen gameScreen;
        
        private Random rnd;
        private int timeToWait = 100; //1000-1s

        public Game()
        {
            hero = new Character("Nemu", 100, 12, 7, 100);
            gameScreen = new GameScreen(49,125);
            rnd = new Random();
            
            createMonsters();
            itemList();
        }

        //SCREEN CONTROL
        public void renderFrame()
        {
            gameScreen.deleteGameScreen();
                gameScreen.fillScreen();
                gameScreen.printScreen();
        }

        //GAME CONTROL
        private void createMonsters()
        {
            monsterArr[0] = new Monster("Imp", 20, 5,3, 20,1, 300, 10);
            monsterArr[1] = new Monster("Low Demon", 35, 7, 1, 35,2,500, 20);
            monsterArr[2] = new Monster("Demonoid", 55, 2, 6, 55,3,650, 15);
            monsterArr[3] = new Monster("Demon", 45, 5, 5, 45,4,750, 25);
            monsterArr[4] = new Monster("Demon wolf", 30, 7, 2,30, 5,850, 20); //TODO Odmeny
            monsterArr[5] = new Monster("Demon general", 60, 10, 7,60, 6,875, 300);//TODO Odmeny
            monsterArr[6] = new Monster("Demon Mah-Lu", 70, 16, 3,70, 7,900, 500);//TODO Odmeny
            monsterArr[7] = new Monster("Small Pick", 100, 1, 10,100, 8,930, 80);//TODO Odmeny
            monsterArr[8] = new Monster("Demon Warrior", 50, 8, 7,50, 9,990, 100);//TODO Odmeny
            monsterArr[9] = new Monster("Demon King", 280, 20, 10, 280,100,1000, 1000);//TODO Odmeny
            monsterArr[10] = new Monster("God",700,0,0,700,666,6666, 10000);
        }

        private void chooseMonster()
        {
            if (monsterArr[9].godCount == 3)
                chosenOpponent = monsterArr[10];
            else
            {
                int randomNumber = rnd.Next(1, 1000);
                for (int i = 0; i < monsterArr.Length; i++)
                {
                    if (monsterArr[i].rarity >= randomNumber)
                    {
                        chosenOpponent = monsterArr[i];
                        break;
                    }
                }
            }

        }

        public void fightMonster()
        {
            chooseMonster();
            gameScreen.putString($"You have encountered monster called {chosenOpponent.nameOfCharacter}");
            //Console.WriteLine($"You have encountered monster called {chosenOpponent.nameOfCharacter}");
            System.Threading.Thread.Sleep(2000);
            while (chosenOpponent.health != 0 || hero.health != 0)
            {
                //MONSTER TURN
                Console.WriteLine($"{chosenOpponent.nameOfCharacter} TURN:");
                int mDMG = chosenOpponent.attack();
                System.Threading.Thread.Sleep(timeToWait);
                int hDEF = hero.defend();
                System.Threading.Thread.Sleep(timeToWait);

                int dmgToHero = mDMG - hDEF;
                hero.damageRecived(dmgToHero);
                if (dmgToHero < 0)
                    dmgToHero = 0;
                Console.WriteLine($"{chosenOpponent.nameOfCharacter} dealt {dmgToHero} DMG to {hero.nameOfCharacter}");
                System.Threading.Thread.Sleep(timeToWait);
                if (hero.dead())
                {
                    survive = false;
                    break;
                }
                Console.WriteLine($"{hero.nameOfCharacter} HP = {hero.health}");
                Console.WriteLine();

                //HERO TURN
                Console.WriteLine($"{hero.nameOfCharacter} TURN:");
                System.Threading.Thread.Sleep(timeToWait);
                int hDMG = hero.attack();
                System.Threading.Thread.Sleep(timeToWait);
                int mDEF = chosenOpponent.defend();
                System.Threading.Thread.Sleep(timeToWait);

                int dmgToMonster = hDMG - mDEF;
                chosenOpponent.damageRecived(dmgToMonster);
                if (dmgToMonster < 0)
                    dmgToMonster = 0;
                Console.WriteLine($"{hero.nameOfCharacter} dealt {dmgToMonster} DMG to {chosenOpponent.nameOfCharacter}");
                System.Threading.Thread.Sleep(timeToWait);
                if (chosenOpponent.health <= 0)
                {
                    chosenOpponent.alive = false;
                    survive = true;
                    Console.WriteLine($"{chosenOpponent.nameOfCharacter} has died");
                    hero.gainXP(chosenOpponent.experinceGain);
                    System.Threading.Thread.Sleep(timeToWait);
                    Console.WriteLine($"You have gained {chosenOpponent.experinceGain}XP");
                    Console.WriteLine($"{hero.experience} XP/{hero.nextLevel} XP");
                    //Respawn Monster
                    chosenOpponent.respawnDead();
                    Actions(chosenOpponent.monsterID, 1);
                    buffGod(chosenOpponent.monsterID);
                    break;
                }
                else
                {
                    Console.WriteLine($"{chosenOpponent.nameOfCharacter} HP = {chosenOpponent.health}");
                    Console.WriteLine();   
                }
            }
        }

        //AKCE A ODMENY
        public void Actions(int monsterID, int killedMonster) //killedMonster = 1, other actions = 0
        {
            if (killedMonster == 1)
            {
                Console.WriteLine();
                Console.WriteLine($"Do you want to go thru {chosenOpponent.nameOfCharacter} corpse? [Y/N]");
                String answer = Console.ReadLine();
                answer = answer.ToUpper();
                if (answer == "Y")
                {
                    int randomNumber = rnd.Next(0, 100);
                    if (monsterID == 1) //IMP
                    {
                        if (randomNumber <= 60)
                        {
                            Console.WriteLine($"You were infected by Pink Eye PP disease. You have opened your third eye... Kinda");
                            System.Threading.Thread.Sleep(timeToWait);
                            Console.WriteLine($"You have lost -5 HP");
                            System.Threading.Thread.Sleep(timeToWait);
                            Console.WriteLine();
                            hero.damageRecived(5);
                            hero.printStats();
                        }
                        else if (randomNumber <= 95)
                        {
                            Console.WriteLine($"You get Impomania... idk what that is...");
                            System.Threading.Thread.Sleep(timeToWait);
                            Console.WriteLine($"You have lost -6 HP");
                            System.Threading.Thread.Sleep(timeToWait);
                            Console.WriteLine();
                            hero.damageRecived(6);
                            hero.printStats();
                            if (hero.dead())
                            {
                                survive = false;
                            }
                        }
                        else
                        {
                            buffItem = new BuffItem("Health Potion",1,20, hero);
                            chooseItem();
                            Console.WriteLine();
                        }
                    }

                    else if (monsterID == 2)//LOW DEMON
                    {
                        if (randomNumber <= 30)
                        {
                            Console.WriteLine($"Ou no. Your self-esteem went really low. You are feeling like shit");
                            System.Threading.Thread.Sleep(timeToWait);
                            Console.WriteLine($"You have lost -1 armor");
                            System.Threading.Thread.Sleep(timeToWait);
                            Console.WriteLine();
                            hero.charMaxDef(-1);
                            hero.printStats();
                        }
                        else if (randomNumber <= 60)
                        {
                            Console.WriteLine($"You recived small healt potion... like really small");
                            System.Threading.Thread.Sleep(timeToWait);
                            Console.WriteLine($"You gained +5 hp");
                            System.Threading.Thread.Sleep(timeToWait);
                            Console.WriteLine();
                            buffItem = new BuffItem("Really Small Health Potion",0,5, hero);
                        }
                        else if (randomNumber <= 90)
                        {
                            Console.WriteLine($"Nothing Here");
                            hero.printStats();
                            Console.WriteLine();
                        }
                        else if (randomNumber <= 100)
                        {
                            buffItem = new BuffItem("Health Potion",1,20, hero);
                            chooseItem();
                            Console.WriteLine();
                        }
                    }
                    else if (monsterID == 3)//DEMONOID
                    {
                        if (randomNumber <= 30)
                        {
                            Console.WriteLine($"Small demon jumped out from Demoniods body and scratch your left eyebrow... You look kinda badass");
                            System.Threading.Thread.Sleep(timeToWait);
                            Console.WriteLine($"You have lost -5 HP");
                            System.Threading.Thread.Sleep(timeToWait);
                            Console.WriteLine();
                            hero.damageRecived(5);
                            hero.printStats();
                        }
                        else if(randomNumber <= 85)
                        {
                            Console.WriteLine($"Nothing Here");
                            hero.printStats();
                            Console.WriteLine();
                        }
                        else if (randomNumber <= 100)
                        {
                            buffItem = new BuffItem("Small Health Potion",1,20, hero);
                            chooseItem();
                            Console.WriteLine();
                        }
                    }
                    else if (monsterID == 4)//DEMON
                    {
                        if (randomNumber <= 50)
                        {
                            Console.WriteLine($"Nothing Here");
                            hero.printStats();
                            Console.WriteLine();
                        }
                        else if (randomNumber <= 80)
                        {
                            Console.WriteLine("You gained demon power from drinking demons piss...ew");
                            System.Threading.Thread.Sleep(timeToWait);
                            Console.WriteLine($"You have gained +1 ARK");
                            Console.WriteLine($"You have gained +1 ARM");
                            Console.WriteLine($"You have gained +5 HP");
                            hero.charMaxDamage(1);
                            hero.charMaxDef(1);
                            hero.charMaxHealth(5);
                            System.Threading.Thread.Sleep(timeToWait);
                            hero.printStats();
                        }
                        else if (randomNumber <= 100)
                        {
                            buffItem = new BuffItem("Small Health Potion",1,10, hero);
                            chooseItem();
                            Console.WriteLine();
                        }
                    }
                    else if (monsterID == 5) //DEMON WOLF
                    {
                        if (randomNumber <= 30)
                        {
                            buffItem = new BuffItem("Small Health Potion",1,10, hero);
                            Console.WriteLine();
                        }
                        else if (randomNumber <= 60)
                        {
                            Console.WriteLine($"Parasite worm jumped out from wolf corpse and bite you");
                            System.Threading.Thread.Sleep(timeToWait);
                            Console.WriteLine($"You have lost -5 HP");
                            System.Threading.Thread.Sleep(timeToWait);
                            hero.damageRecived(5);
                            hero.printStats();
                        }
                        else if (randomNumber <= 80)
                        {
                            Console.WriteLine($"Nothing Here");
                            hero.printStats();
                            Console.WriteLine();
                        }
                        else
                        {
                            chooseItem();
                            Console.WriteLine();
                        }
                    }
                    else if (monsterID == 6) //DEMON GENERAL
                    {
                        if (randomNumber <= 80)
                        {
                            buffItem = new BuffItem("Large Health Potion", 3, 30, hero);
                            chooseItem();
                            Console.WriteLine();
                        }
                        else
                        {
                            Console.WriteLine($"Nothing Here");
                            hero.printStats();
                            Console.WriteLine();
                        }
                    }
                    else if (monsterID == 7) //DEMON MAH-LU
                        if (randomNumber <= 95)
                        {
                            buffItem = new BuffItem("Extra Large Health Potion", 4, 40, hero);
                            chooseItem();
                            Console.WriteLine();
                        }
                        else
                        {
                            Console.WriteLine($"Nothing Here");
                            hero.printStats();
                            Console.WriteLine();
                        }
                    else if (monsterID == 8) //Small Pick
                    {
                        Console.WriteLine("You..you really killed him... he was so cute... smart... loving... caring...you monster");
                        buffItem = new BuffItem("Health Potion", 2, 20, hero);
                        chooseItem();
                        Console.WriteLine();
                    }
                    else if (monsterID == 9) //DEMON WARRIOR
                    {
                        if (randomNumber <= 70)
                        {
                            buffItem = new BuffItem("Health Potion", 2, 20, hero);
                            chooseItem();
                            Console.WriteLine();
                        }
                        else
                        {
                            Console.WriteLine("He was such a brave warrior, even when at the verge of dead he bitten your pinkie off.. what a pain");
                            System.Threading.Thread.Sleep(timeToWait);
                            Console.WriteLine($"You have lost -10 HP and your pinkie");
                            System.Threading.Thread.Sleep(timeToWait);
                            hero.damageRecived(10);
                            hero.printStats();
                        }

                    }
                    else if (monsterID == 100)//DEMON KING
                    {
                        buffItem = new BuffItem("Health Potion",3,80, hero);
                        chooseItem();
                        chooseItem();
                        chooseItem();
                        Console.WriteLine();
                    }
                }
                Console.WriteLine("To continue press enter");
                Console.ReadLine();
            }
        }

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

        private void applyItem()
        {
            Console.WriteLine($"You have found an item called {chosenItem.itemName}");
            chosenItem.addMaxDamage();
            chosenItem.addMaxDef();
            chosenItem.addMaxHealth();
            hero.printStats();
        }
        
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