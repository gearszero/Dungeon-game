using System;

namespace CV_1
{
    public class Item
    {
        public String itemName;
        public int attack;
        public int armor;
        public int health;
        
        public Character hero;
        public int itemID;
        public int itemRarity;

        public Item(string itemName, int attack, int armor, int health, int itemId, Character hero, int itemRarity)
        {
            this.itemName = itemName;
            this.attack = attack;
            this.armor = armor;
            this.health = health;
            this.itemID = itemId;
            this.hero = hero;
            this.itemRarity = itemRarity;
        }
        
        
        public void addMaxDamage()
        {
            hero.maxDamage += attack;
            hero.minDamage += attack / 2;
            if (hero.minDamage < 0)
                hero.minDamage = 0;
        }
        
        public void addMaxDef()
        {
            hero.maxDef += armor;
            hero.minDef += armor / 2;
            if (hero.minDef < 0)
                hero.minDef = 0;
            Console.WriteLine($"You gained  {armor} ARM");
        }

        public void addMaxHealth()
        {
            hero.maxHealth +=health;
            Console.WriteLine($"You gained  {health} HP");
            hero.maxHealthMatch();
        }
    }
}