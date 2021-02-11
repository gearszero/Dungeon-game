using System;
using System.Net;
using System.Numerics;

namespace CV_1
{
    public class Character
    {
        public String nameOfCharacter;
        public int health;
        // public int charaID;

        public int maxHealth;
        public int maxDamage;
        public int maxDef;

        public int minDamage;
        public int minDef;

        public double experience;
        public double nextLevel;
        public int level;
        
        private Random rnd;



        public Character(string nameOfCharacter, int health, int maxDamage, int maxDef, int maxHealth,int minDamage,int minDef)
        {
            this.nameOfCharacter = nameOfCharacter;
            this.health = health;
            this.maxDamage = maxDamage;
            this.maxDef = maxDef;
            this.maxHealth = maxHealth;
            this.minDamage = minDamage;
            this.minDef = minDef;
            rnd = new Random();
        }

        public Character(string nameOfCharacter, int health, int maxDamage, int maxDef, int maxHealth)
        {
            this.nameOfCharacter = nameOfCharacter;
            this.health = health;
            this.maxHealth = maxHealth;
            this.maxDamage = maxDamage;
            this.maxDef = maxDef;
            minDamage = 0;
            minDef = 0;
            experience = 0;
            nextLevel = 100;
            rnd = new Random();
        }

        public int attack()
        {
            int damage = rnd.Next(minDamage, maxDamage);
            Console.WriteLine($"{nameOfCharacter} attacked for {damage} DMG");
            return damage;
        }

        public int defend()
        {
            int armor = rnd.Next(minDef, maxDef);
            Console.WriteLine($"{nameOfCharacter} defended for {armor}");
            return armor;
        }

        public void damageRecived(int damage)
        {
            if (damage < 0)
            {
            }
            else
            {
                health = health - damage;
            }
        }

        public bool dead()
        {
            if (health <= 0)
            {
                Console.WriteLine($"{nameOfCharacter} = 0 HP");
                Console.WriteLine($"{nameOfCharacter} has died");
                return true;
            }
            return false;
        }

        public void printStats()
        {
            Console.WriteLine();
            Console.WriteLine("HERO STAT:");
            Console.WriteLine($"MAX ATTACK = {maxDamage} ATK | MIN ATTACK = {minDamage}");
            Console.WriteLine($"MAX ARMOR  = {maxDef} ARM | MIN ARMOR = {minDef}");
            Console.WriteLine($"MAX HEALTH = {maxHealth} HP");
            Console.WriteLine($"CURRENT HP = {health} HP");
            Console.WriteLine($"LEVEL = {level}");
            Console.WriteLine();
        }
        
        public void charMaxDamage(int attack)
        {
            maxDamage = maxDamage + attack;
            minDamage = minDamage + attack/2;
            if (minDamage < 0)
            {
                minDamage = 0;
            }
        }
        
        public void charMaxDef(int armor)
        {
            maxDef = maxDef + armor;
            minDef = minDef + armor/2;
            if (minDef < 0)
            {
                minDef = 0;
            }
        }

        public void charMaxHealth(int health)
        {
            maxHealth = maxHealth + health;
        }

        public void maxHealthMatch()
        {
            if (maxHealth < health)
            {
                health = maxHealth;
            }
        }

        public void gainXP(double amount)
        {
            experience += amount;
            if (experience >= nextLevel)
            {
                getLevel();
                countNextLevel();
            }
        }

        public void countNextLevel()
        {
            nextLevel += nextLevel * 2;
        }

        public void getLevel()
        {
            level += 1;
            Console.WriteLine("LEVEL UP");
            if (level % 2 == 0)
            {
                charMaxDamage(1);
            }
            else if (level % 3 == 0)
            {
                charMaxDef(1);
            }
            else
            {
                charMaxDamage(1);
                charMaxDef(1);
            }
            printStats();
        }
    }
}