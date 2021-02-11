using System;

namespace CV_1
{
    public class BuffItem
    {
        public String buffItemName;
        public int buffItemID;

        public int regen;
        public Character hero;

        public BuffItem(string buffItemName, int buffItemId, int regen, Character hero)
        {
            this.buffItemName = buffItemName;
            this.buffItemID = buffItemId;
            this.regen = regen;

            this.hero = hero;
            addHP();
        }

        public void addHP()
        {
            Console.WriteLine($"You have found {buffItemName} +{regen}");
            if (hero.health + regen > hero.maxHealth)
                hero.health = hero.maxHealth;
            else
            {
                hero.health = hero.health + regen;   
            }
            hero.printStats();
        }

    }
}