namespace CV_1
{
    public class Stats
    {
        public Character hero;
        public GameScreen gameScreen;
        public StringSprite[] stringSpritesArr = new StringSprite[7];

        public Stats(Character hero, GameScreen gameScreen)
        {
            this.hero = hero;
            this.gameScreen = gameScreen;
        }

        public void makeStats()
        {
            int counter = 0;
            stringSpritesArr[counter] = new StringSprite("HERO STAT:",gameScreen,counter);
            counter++;
            stringSpritesArr[counter] = new StringSprite($"MAX ATTACK = {hero.maxDamage} ATK | MIN ATTACK = {hero.minDamage}",gameScreen,counter);
            counter++;
            stringSpritesArr[counter] = new StringSprite($"MAX ARMOR  = {hero.maxDef} ARM | MIN ARMOR = {hero.minDef}",gameScreen,counter);
            counter++;
            stringSpritesArr[counter] = new StringSprite($"MAX HEALTH = {hero.maxHealth} HP",gameScreen,counter);
            counter++;
            stringSpritesArr[counter] = new StringSprite($"CURRENT HP = {hero.health} HP",gameScreen,counter);
            counter++;
            stringSpritesArr[counter] = new StringSprite($"LEVEL = {hero.level}",gameScreen,counter);
            counter++;
            stringSpritesArr[counter] = new StringSprite($"{hero.experience} / {hero.nextLevel} XP",gameScreen,counter);
        }
    }
}