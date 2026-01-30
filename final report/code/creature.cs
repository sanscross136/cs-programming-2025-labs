public abstract class Creature
{
    public string Name { get; init; }
    public int MaxHealth { get; set; }
    public int CurrentHealth { get; set; }
    public int Damage { get; set; }
    public int Defense { get; set; }
    public int Agility { get; set; }
    public int Level { get; set; }
    public virtual void TakeDamage(int damage){
        int actualDamage = Math.Max(0, damage - Defense / 2);
        CurrentHealth = Math.Max(0, CurrentHealth - actualDamage);
    }
    public virtual void Heal(uint amount){
        if (!IsAlive) return;
        CurrentHealth = Math.Min(MaxHealth, CurrentHealth + (int)amount);
    }
    public void AddStatusEffect(StatusEffect effect){
        if (HasStatusEffect(effect.Type))
            return;
        _statusEffects.Add(effect);
    }
}
