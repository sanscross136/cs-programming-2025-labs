public class Player : Creature
{
    public int Experience { get; set; }
    public Inventory Inventory { get; set; }
    public int ExperienceThreshold { get; set; }

    public Player(string name, RaceStats stats) 
        : base(name, stats.MaxHp, stats.MaxAtk, 
               stats.MaxDef, stats.MaxAgi)
    {
        Experience = 0;
        ExperienceThreshold = 100;
        Inventory = new Inventory();
    }
    public void GainExperience(int amount)
    {
        Experience += amount;
        if (Experience >= ExperienceThreshold)
        {
            LevelUp();
        }
    }
    public void LevelUp()
    {
        Level++;
        Experience = 0;
        ExperienceThreshold = (int)(ExperienceThreshold * 1.5);
        
        MaxHealth += Random.Shared.Next(15, 26);
        Damage += Random.Shared.Next(2, 5);
        Defense += Random.Shared.Next(1, 4);
        CurrentHealth = MaxHealth;
    }
}
