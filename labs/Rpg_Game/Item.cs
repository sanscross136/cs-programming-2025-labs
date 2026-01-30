namespace Rpg_Game;

public enum StatusEffectType
{
     // Лечение каждый ход
    Regeneration, 
     // Урон каждый ход
    OnFire,       
    // Не может атаковать
    Stunned,       
    // Урон от яда каждый ход
    PoisonDot,    
     // Увеличенный урон на несколько ходов 
    DamageBoost   
}

public class StatusEffect
{
    public StatusEffectType Type { get; set; }
    public int Duration { get; set; }
    public int Power { get; set; }
    public string Name { get; }

    public StatusEffect(StatusEffectType type, int duration, int power)
    {
        Type = type;
        Duration = duration;
        Power = power;
        Name = type switch
        {
            StatusEffectType.Regeneration => "Регенерация",
            StatusEffectType.OnFire => "В огне",
            StatusEffectType.Stunned => "Оглушен",
            StatusEffectType.PoisonDot => "Отравлен",
            StatusEffectType.DamageBoost => "Боевой клич",
            _ => "Неизвестный эффект"
        };
    }
}
// Базовый класс для всех предметов
public abstract class Item
{
    public virtual int Level { get; set; } = 1;
}

public abstract class Potion : Item
{ 
    public abstract string Name { get; }
    public override int Level { get; set; }

    public Potion(int level)
    {
        if (level < 1 || level > 3)
        {
            throw new ArgumentOutOfRangeException(nameof(level), "Уровень зелья должен быть от 1 до 3.");
        }
        Level = level;
    }

    public abstract void UsePotion(Creature user);    
}

public class HealthPotion(int level) : Potion(level)
{
    private static readonly string[] Titles = { "", "Малое", "Среднее", "Большое" };
    public override string Name => $"{Titles[Level]} зелье здоровья";

    public override void UsePotion(Creature user)
    {
        uint healAmount = (uint)(5 * Level * Level);
        user.Heal(healAmount);
        
        Console.WriteLine($"Использовано {Name}. Восстановлено {healAmount} HP.");
    }
}

public class RegenerationPotion(int level) : Potion(level)
{
    private static readonly string[] Titles = { "", "Слабое", "Среднее", "Сильное" };
    public override string Name => $"{Titles[Level]} зелье регенерации";

    public override void UsePotion(Creature user)
    {
        var effect = new StatusEffect(StatusEffectType.Regeneration, 3 + Level, 10 + (Level * 5));
        user.AddStatusEffect(effect);
        Console.WriteLine($"Использовано {Name}. {user.Name} получит регенерацию на {effect.Duration} ходов!");
    }
}

public class DamageBoostPotion(int level) : Potion(level)
{
    private static readonly string[] Titles = { "", "Малый", "Средний", "Большой" };
    public override string Name => $"{Titles[Level]} зелье боевого клича";

    public override void UsePotion(Creature user)
    {
        var effect = new StatusEffect(StatusEffectType.DamageBoost, 2 + Level, 5 + (Level * 3));
        user.AddStatusEffect(effect);
        Console.WriteLine($"Использовано {Name}. {user.Name} получит бонус к урону на {effect.Duration} ходов!");
    }
}

public class Coins
{
    public int Amount { get; set; }

    public Coins(int InitialAmount = 0) => Amount = InitialAmount;

    public void Add(int count) => Amount += count;

    public bool TrySubtract(int count)
    {
        if (Amount < count) return false;

        Amount -= count;
        return true;
    }
}