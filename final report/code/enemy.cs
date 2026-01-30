public class Enemy : Creature
{
    public static Enemy GenerateRandom(int floor){
        string name = Names[Random.Shared.Next(Names.Length)];
        int level = Math.Max(1, floor / 2); 
        int health = 20 + (floor * 5);
        int damage = 5 + (floor * 2);
        int defense = 2 + (floor / 2);
        int agility = 3 + Random.Shared.Next(1, 3);
        return new Enemy(name, health, damage, defense, agility, level);
    }
    public static Enemy GenerateBoss(int floor){
        string name = "Dungeon Boss";
        int level = floor;
        int health = 50 + (floor * 10);
        int damage = 10 + (floor * 3);
        int defense = 5 + (floor * 2);
        int agility = 5 + floor;

        return new Enemy(name, health, damage, defense, agility, level);
    }
    public int GetRewardExperience(){
        return 50 + (Level * 15);
    }
    public int GetRewardGold(){
        return 25 + (Level * 10);
    }
}
