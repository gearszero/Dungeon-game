using System;
using System.Collections.Specialized;

namespace CV_1
{
    public class Fight
    {
        public Monster monster;
        public Character hero;
        public GameScreen gameScreen;
        
        public StringSprite[] stringSpritesArr = new StringSprite[14];
        public Item[] item;
        public BuffItem[] buffItem;

        private Random rnd = new Random();

        public bool survive = true;
        public bool attack = false;
        public bool defend = false;
        public bool escape = false;

        public bool tripped = false;
        private int sentenceCountItem = 1;
        
        private int saveMaxDef;
        private int saveMinDef;
        public Item chosenItem;
        

        public Fight(Monster monster, Character hero, GameScreen gameScreen, Item[] item, BuffItem[] buffItem)
        {
            this.monster = monster;
            this.hero = hero;
            this.gameScreen = gameScreen;
            this.item = item;
            this.buffItem = buffItem;

            saveMaxDef = hero.maxDef;
            saveMinDef = hero.minDef;
            
            fillStringArr();
            stringSpritesArr[0] = new StringSprite($"You have encountered {monster.nameOfCharacter} HP = {monster.health}",gameScreen,0,true);
        }
        

        public void fightMonster()
        {
            fillStringArr();
            int sentenceCount = 1;
            int addTmpMaxDef = hero.maxDef / 6;
            int addTmpMinDef = hero.minDef / 6;
            bool deadMonster = false;
            
            

            if (attack)
            {
                int hDMG = hero.attack();
                int mDEF = monster.defend();
                int dmg = hDMG - mDEF;
                if (dmg < 0)
                {
                    dmg = 0;
                }

                monster.damageRecived(dmg);

                stringSpritesArr[sentenceCount] = new StringSprite($"{hero.nameOfCharacter} TURN:",gameScreen,sentenceCount,true);
                sentenceCount++;
                stringSpritesArr[sentenceCount] = new StringSprite($"{hero.nameOfCharacter} attacked for {hDMG} DMG",gameScreen,sentenceCount,true);
                sentenceCount++;
                stringSpritesArr[sentenceCount] = new StringSprite($"{monster.nameOfCharacter} defended for {mDEF} ARM",gameScreen,sentenceCount,true);
                sentenceCount++;
                stringSpritesArr[sentenceCount] = new StringSprite($"{hero.nameOfCharacter} have dealt {dmg} DMG",gameScreen,sentenceCount,true);
                sentenceCount++;
                if (monster.dead())
                {
                    stringSpritesArr[sentenceCount] = new StringSprite($"{monster.nameOfCharacter} has died", gameScreen, sentenceCount, true);
                    sentenceCount++;
                    hero.gainXP(monster.experinceGain);
                    stringSpritesArr[sentenceCount] = new StringSprite($"{hero.nameOfCharacter} gained {monster.experinceGain} XP", gameScreen, sentenceCount, true);
                    sentenceCount++;
                    hero.maxDef = saveMaxDef;
                    hero.minDef = saveMinDef;
                    monster.alive = false;
                    
                    deadMonster = true;
                }
                else
                {
                    stringSpritesArr[sentenceCount] = new StringSprite($"{monster.nameOfCharacter} HP = {monster.health}", gameScreen, sentenceCount, true);
                    sentenceCount++;
                }
            }
            else if (defend)
            {
                hero.maxDef += addTmpMaxDef;
                hero.minDef += addTmpMinDef;
            }
            else if (escape)
            {
                if (hero.escape())
                {
                    stringSpritesArr[sentenceCount] = new StringSprite($"You have outrun {monster.nameOfCharacter}. Fucking coward.",gameScreen,sentenceCount,true);
                    monster.alive = false;
                    deadMonster = true;
                }
                else
                {
                    stringSpritesArr[sentenceCount] = new StringSprite($"You tripped on your shoelace. What a fucking idiot.",gameScreen,sentenceCount,true);
                    sentenceCount++;
                    stringSpritesArr[sentenceCount] = new StringSprite($"You have failed to escape.",gameScreen,sentenceCount,true);
                    sentenceCount++;
                    tripped = true;
                    escape = false;    
                }
            }

            if (monster.alive || !deadMonster)
            {
                int mDMG = monster.attack();
                int hDEF = hero.defend();
                int dmg1 = mDMG - hDEF;
                if (dmg1 < 0)
                {
                    dmg1 = 0;
                }

                if (tripped)
                {
                    hero.damageRecived(dmg1 + (dmg1 / 3));
                    tripped = false;
                }
                else
                {
                    hero.damageRecived(dmg1);
                }

                stringSpritesArr[sentenceCount] = new StringSprite($"",gameScreen,sentenceCount,true);
                sentenceCount++;
                stringSpritesArr[sentenceCount] = new StringSprite($"{monster.nameOfCharacter} TURN:", gameScreen, sentenceCount, true);
                sentenceCount++;
                stringSpritesArr[sentenceCount] = new StringSprite($"{monster.nameOfCharacter} attacked for {mDMG} DMG", gameScreen, sentenceCount, true);
                sentenceCount++;
                stringSpritesArr[sentenceCount] = new StringSprite($"{hero.nameOfCharacter} defended for {hDEF} ARM", gameScreen, sentenceCount, true);
                sentenceCount++;
                stringSpritesArr[sentenceCount] = new StringSprite($"{monster.nameOfCharacter} have dealt {dmg1} DMG", gameScreen, sentenceCount, true);
                sentenceCount++;
                if (hero.dead())
                {
                    survive = false;
                    stringSpritesArr[sentenceCount] = new StringSprite($"{hero.nameOfCharacter} has died", gameScreen, sentenceCount, true);
                }
            }

            attack = false;
            defend = false;
            escape = false;
        }

        public void action()
        {
            fillStringArr();
            int randomNumber = rnd.Next(0, 100);
            
            //IMP
            if (monster.monsterID == 1)
            {
                if (randomNumber <= 60)
                {
                    stringSpritesArr[sentenceCountItem] = new StringSprite("You were infected by Pink Eye PP disease. You have opened your third eye... Kinda", gameScreen, sentenceCountItem, true);
                    ++sentenceCountItem;
                    stringSpritesArr[sentenceCountItem] = new StringSprite("You have lost -5 HP", gameScreen, sentenceCountItem, true);
                    ++sentenceCountItem;
                    hero.damageRecived(5);
                }
                else if (randomNumber <= 95)
                {
                    stringSpritesArr[sentenceCountItem] = new StringSprite("You get Impomania... idk what that is...", gameScreen, sentenceCountItem, true);
                    ++sentenceCountItem;
                    stringSpritesArr[sentenceCountItem] = new StringSprite("You have lost -6 HP", gameScreen, sentenceCountItem, true);
                    ++sentenceCountItem;
                    hero.damageRecived(6);
                }
                else
                {
                    buffItem[0].addHP(); //+20
                    stringSpritesArr[sentenceCountItem] = new StringSprite($"You have found {buffItem[0].buffItemName} +{buffItem[0].regen}", gameScreen, sentenceCountItem, true);
                    chooseItem();
                }
            }
            
            //LOW DEMON
            else if (monster.monsterID == 2)
            {
                if (randomNumber <= 30)
                {
                    stringSpritesArr[sentenceCountItem] = new StringSprite("Ou no. Your self-esteem went really low. You are feeling like shit", gameScreen, sentenceCountItem, true);
                    ++sentenceCountItem;
                    stringSpritesArr[sentenceCountItem] = new StringSprite("You have lost -1 armor", gameScreen, sentenceCountItem, true);
                    ++sentenceCountItem;
                    hero.charMaxDef(-1);
                }
                else if (randomNumber <= 80)
                {
                    stringSpritesArr[sentenceCountItem] = new StringSprite("You have obtain small health potion... like really small", gameScreen, sentenceCountItem, true);
                    ++sentenceCountItem;
                    buffItem[1].addHP(); //+5
                }
                else if (randomNumber <= 92)
                {
                    stringSpritesArr[sentenceCountItem] = new StringSprite("Nothing Here", gameScreen, sentenceCountItem, true);
                    ++sentenceCountItem;
                }
                else
                {
                    buffItem[2].addHP(); //+10
                    stringSpritesArr[sentenceCountItem] = new StringSprite($"You have found {buffItem[2].buffItemName} +{buffItem[2].regen}", gameScreen, sentenceCountItem, true);
                    chooseItem();
                }
            }
            
            //DEMONOID
            else if (monster.monsterID == 3)
            {
                if (randomNumber <= 30)
                {
                    stringSpritesArr[sentenceCountItem] = new StringSprite("Small demon jumped out from Demoniods body and scratch your left eyebrow... You look kinda badass", gameScreen, sentenceCountItem, true);
                    ++sentenceCountItem;
                    stringSpritesArr[sentenceCountItem] = new StringSprite("You have lost -5 HP", gameScreen, sentenceCountItem, true);
                    ++sentenceCountItem;
                    hero.damageRecived(5);
                }
                else if (randomNumber <= 85)
                {
                    stringSpritesArr[sentenceCountItem] = new StringSprite("Nothing Here", gameScreen, sentenceCountItem, true);
                    ++sentenceCountItem;
                }
                else
                {
                    buffItem[2].addHP(); //+10
                    stringSpritesArr[sentenceCountItem] = new StringSprite($"You have found {buffItem[2].buffItemName} +{buffItem[2].regen}", gameScreen, sentenceCountItem, true);
                    chooseItem();
                }
            }

            //DEMON
            else if (monster.monsterID == 4)
            {
                if (randomNumber <= 50)
                {
                    stringSpritesArr[sentenceCountItem] = new StringSprite("Nothing Here", gameScreen, sentenceCountItem, true);
                    ++sentenceCountItem;
                }
                else if (randomNumber <= 80)
                {
                    stringSpritesArr[sentenceCountItem] = new StringSprite("You gained demon power from drinking demons piss...ew", gameScreen, sentenceCountItem, true);
                    ++sentenceCountItem;
                    stringSpritesArr[sentenceCountItem] = new StringSprite("You have gained +1 ARK", gameScreen, sentenceCountItem, true);
                    ++sentenceCountItem;
                    stringSpritesArr[sentenceCountItem] = new StringSprite("You have gained +1 ARM", gameScreen, sentenceCountItem, true);
                    ++sentenceCountItem;
                    stringSpritesArr[sentenceCountItem] = new StringSprite("You have gained +5 MAX HP", gameScreen, sentenceCountItem, true);
                    ++sentenceCountItem;
                    stringSpritesArr[sentenceCountItem] = new StringSprite("You have lost -10HP", gameScreen, sentenceCountItem, true);
                    ++sentenceCountItem;
                    hero.charMaxDamage(1);
                    hero.charMaxDef(1);
                    hero.charMaxHealth(5);
                    hero.damageRecived(10);
                }
                else
                {
                    buffItem[2].addHP(); //+10
                    stringSpritesArr[sentenceCountItem] = new StringSprite($"You have found {buffItem[2].buffItemName} +{buffItem[2].regen}", gameScreen, sentenceCountItem, true);
                    chooseItem();
                }
            }
            
            //DEMON WOLF
            else if (monster.monsterID == 5) //DEMON WOLF
            {
                if (randomNumber <= 30)
                {
                    buffItem[2].addHP(); //+10
                    stringSpritesArr[sentenceCountItem] = new StringSprite($"You have found {buffItem[2].buffItemName} +{buffItem[2].regen}", gameScreen, sentenceCountItem, true);
                }
                else if (randomNumber <= 60)
                {
                    stringSpritesArr[sentenceCountItem] = new StringSprite("Parasite worm jumped out from wolf corpse and bite you", gameScreen, sentenceCountItem, true);
                    ++sentenceCountItem;
                    stringSpritesArr[sentenceCountItem] = new StringSprite("You have lost -5 HP", gameScreen, sentenceCountItem, true);
                    ++sentenceCountItem;
                    hero.damageRecived(5);
                }
                else if (randomNumber <= 80)
                {
                    stringSpritesArr[sentenceCountItem] = new StringSprite("Nothing Here", gameScreen, sentenceCountItem, true);
                    ++sentenceCountItem;
                }
                else
                {
                    buffItem[2].addHP(); //+10
                    stringSpritesArr[sentenceCountItem] = new StringSprite($"You have found {buffItem[2].buffItemName} +{buffItem[2].regen}", gameScreen, sentenceCountItem, true);
                    chooseItem();
                }
            }
            
            //DEMON GENERAL
            else if (monster.monsterID == 6)
            {
                if (randomNumber <= 80)
                {
                    buffItem[0].addHP(); //+20
                    stringSpritesArr[sentenceCountItem] = new StringSprite($"You have found {buffItem[0].buffItemName} +{buffItem[0].regen}", gameScreen, sentenceCountItem, true);
                    chooseItem();
                }
                else
                {
                    stringSpritesArr[sentenceCountItem] = new StringSprite("Nothing Here", gameScreen, sentenceCountItem, true);
                    ++sentenceCountItem;
                }
            }
            
            //DEMON MAH-LU
            else if (monster.monsterID == 7)
            {
                if (randomNumber <= 95)
                {
                    buffItem[3].addHP(); //+30
                    stringSpritesArr[sentenceCountItem] = new StringSprite($"You have found {buffItem[3].buffItemName} +{buffItem[3].regen}", gameScreen, sentenceCountItem, true);
                    chooseItem();
                }
                else
                {
                    stringSpritesArr[sentenceCountItem] = new StringSprite("Nothing Here", gameScreen, sentenceCountItem, true);
                    ++sentenceCountItem;
                }
            }
            
            //Small Pick
            else if (monster.monsterID == 8) //Small Pick
            {
                stringSpritesArr[sentenceCountItem] = new StringSprite("You..you really killed him... he was so cute... smart... loving... caring...you monster", gameScreen, sentenceCountItem, true);
                ++sentenceCountItem;
                buffItem[0].addHP(); //+20
                stringSpritesArr[sentenceCountItem] = new StringSprite($"You have found {buffItem[0].buffItemName} +{buffItem[0].regen}", gameScreen, sentenceCountItem, true);
                chooseItem();
            }
            //DEMON WARRIOR
            else if (monster.monsterID == 9)
            {
                if (randomNumber <= 70)
                {
                    buffItem[0].addHP(); //+20
                    stringSpritesArr[sentenceCountItem] = new StringSprite($"You have found {buffItem[0].buffItemName} +{buffItem[0].regen}", gameScreen, sentenceCountItem, true);
                    chooseItem();
                }
                else
                {
                    stringSpritesArr[sentenceCountItem] = new StringSprite("He was such a brave warrior, even when at the verge of dead he bitten your pinkie off.. what a pain", gameScreen, sentenceCountItem, true);
                    ++sentenceCountItem;
                    stringSpritesArr[sentenceCountItem] = new StringSprite("You have lost -10 HP and your pinkie", gameScreen, sentenceCountItem, true);
                    ++sentenceCountItem;
                    hero.damageRecived(10);
                }
            }
            else if (monster.monsterID == 100)//DEMON KING
            {
                buffItem[3].addHP(); //+30
                stringSpritesArr[sentenceCountItem] = new StringSprite($"You have found {buffItem[3].buffItemName} +{buffItem[3].regen}", gameScreen, sentenceCountItem, true);
                chooseItem();
                chooseItem();
                chooseItem();
            }

            if (hero.dead())
            {
                survive = false;
                stringSpritesArr[sentenceCountItem] = new StringSprite($"{hero.nameOfCharacter} has died", gameScreen, sentenceCountItem, true);
            }
        }

        public void noAction()
        {
            fillStringArr();
            stringSpritesArr[1] = new StringSprite("Okay boomer", gameScreen, 1, true);
        }


        private void chooseItem()
        {
            int randomNumber = rnd.Next(1, 1410);
            for (int i = 0; i < item.Length; i++)
            {
                if (item[i].itemRarity >= randomNumber)
                {
                    chosenItem = item[i];
                    applyItem();
                    break;
                }
            }
        }

        private void applyItem()
        {
            fillStringArr();
            sentenceCountItem = 3;
            stringSpritesArr[sentenceCountItem] = new StringSprite($"You have found {chosenItem.itemName}", gameScreen, sentenceCountItem, true);
            ++sentenceCountItem;
            stringSpritesArr[sentenceCountItem] = new StringSprite($"You gain {chosenItem.attack} ATK", gameScreen, sentenceCountItem, true);
            ++sentenceCountItem;
            chosenItem.addMaxDamage();
            stringSpritesArr[sentenceCountItem] = new StringSprite($"You gain {chosenItem.armor} DEF", gameScreen, sentenceCountItem, true);
            ++sentenceCountItem;
            chosenItem.addMaxDef();
            stringSpritesArr[sentenceCountItem] = new StringSprite($"You gain {chosenItem.health} HP", gameScreen, sentenceCountItem, true);
            ++sentenceCountItem;
            chosenItem.addMaxHealth();
        }

        public void fillStringArr()
        {
            for (int i = 1; i < stringSpritesArr.Length; i++)
            {
                stringSpritesArr[i] = new StringSprite("",gameScreen,0,false);
            }
        }
    }
}