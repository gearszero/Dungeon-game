namespace CV_1
{
    public class Monster : Character
    {
        public int monsterID;
        public int rarity;
        public int experinceGain;
        public bool alive = true;
        public int godCount = 0;
        
        
        public Monster(string nameOfCharacter, int health, int maxDamage, int maxDef, int maxHealth, int monsterID, int rarity, int experinceGain) : base(nameOfCharacter, health, maxDamage, maxDef, maxHealth)
        {
            this.monsterID = monsterID;
            this.rarity = rarity;
            this.experinceGain = experinceGain;
        }

        public void respawnDead()
        {
            if (!alive)
            {
                health = maxHealth;
            }

            if (!alive && monsterID == 100)
                godCount++;

            if (!alive && monsterID == 666)
                godCount = 0;
            alive = true;
        }
    }
}