namespace Rpg_Game;

public enum BattleAction
{
    Attack,
    UsePotion,
    Dodge,
    Retreat
}

public class Battle
{
    public Player Player { get; private set; }
    public Enemy Enemy { get; private set; }
    public bool IsPlayerTurn { get; private set; }

    public Battle(Player player, Enemy enemy)
    {
        Player = player;
        Enemy = enemy;
        IsPlayerTurn = true;
    }

    public void DisplayBattleStatus()
    {
        Console.WriteLine($"\n=== БОЙ ===");
        Console.WriteLine($"{Player.Name} (HP: {Player.CurrentHealth}/{Player.MaxHealth}) vs {Enemy.Name} (HP: {Enemy.CurrentHealth}/{Enemy.MaxHealth})");
        
        if (Player.StatusEffects.Count > 0)
        {
            Console.Write($"{Player.Name} эффекты: ");
            Console.WriteLine(string.Join(", ", Player.StatusEffects.Select(e => $"{e.Name} ({e.Duration})")));
        }
        
        if (Enemy.StatusEffects.Count > 0)
        {
            Console.Write($"{Enemy.Name} эффекты: ");
            Console.WriteLine(string.Join(", ", Enemy.StatusEffects.Select(e => $"{e.Name} ({e.Duration})")));
        }
    }

    public bool ExecutePlayerAction(BattleAction action)
    {
        DisplayBattleStatus();

        switch (action)
        {
            case BattleAction.Attack:
                Player.Attack(Enemy);
                break;

            case BattleAction.UsePotion:
                bool potionUsed = TryUsePotion();
                if (!potionUsed)
                    return false;
                break;

            case BattleAction.Dodge:
                int dodgeChance = Player.Agility * 3;
                if (RandomProvider.Next(100) < dodgeChance)
                {
                    Console.WriteLine($"{Player.Name} успешно уклонился от атаки!");
                    return true;
                }
                else
                {
                    Console.WriteLine($"{Player.Name} не смог уклониться!");
                    return false;
                }

            case BattleAction.Retreat:
                Console.WriteLine($"{Player.Name} пытается убежать...");
                if (RandomProvider.Next(100) < 40)
                {
                    Console.WriteLine("Удалось убежать!");
                    return false; 
                    // бой заканчивается
                }
                else
                {
                    Console.WriteLine("Не удалось убежать!");
                }
                break;
        }

        return true;
    }

    private bool TryUsePotion()
    {
        var potions = Player.Inventory.Items.OfType<HealthPotion>().ToList();
        if (potions.Count == 0)
        {
            Console.WriteLine("Нет зелий в инвентаре!");
            return false;
        }

        var potion = potions[0];
        potion.UsePotion(Player);
        Player.Inventory.RemoveItem(potion);
        return true;
    }

    public void ExecuteEnemyAction()
    {
        if (!Enemy.IsAlive) return;

        // Враг решает случайное действие
        int action = RandomProvider.Next(100);

        if (action < 80)
        {
            // 80% - атака
            Enemy.Attack(Player);
        }
        else
        {
            // 20% - попытка уклониться
            int dodgeChance = Enemy.Agility * 2;
            if (RandomProvider.Next(100) < dodgeChance)
            {
                Console.WriteLine($"{Enemy.Name} уклонился от атаки!");
            }
            else
            {
                Console.WriteLine($"{Enemy.Name} попытался уклониться, но не смог!");
            }
        }
    }

    public void ProcessRoundEffects()
    {
        Player.ProcessStatusEffects();
        Enemy.ProcessStatusEffects();
    }

    public bool IsOver()
    {
        return !Player.IsAlive || !Enemy.IsAlive;
    }

    public void RewardPlayer()
    {
        if (!Enemy.IsAlive && Player.IsAlive)
        {
            int expReward = Enemy.Level * 20 + (20 - Enemy.CurrentHealth);
            int goldReward = Enemy.Bounty;

            Player.GainExperience(expReward);
            Player.Inventory.Money.Add(goldReward);

            Console.WriteLine($"\n[ПОБЕДА!]");
            Console.WriteLine($"Получено: {expReward} опыта, {goldReward} золота");
        }
    }

    public bool GetPlayerAction(out BattleAction action)
    {
        action = BattleAction.Attack;

        Console.WriteLine("\nВыберите действие:");
        Console.WriteLine("(1) Атаковать");
        Console.WriteLine("(2) Использовать зелье");
        Console.WriteLine("(3) Уклониться");
        Console.WriteLine("(4) Убежать");
        Console.Write("> ");

        string input = Console.ReadLine() ?? "";

        switch (input)
        {
            case "1":
                action = BattleAction.Attack;
                return true;
            case "2":
                action = BattleAction.UsePotion;
                return true;
            case "3":
                action = BattleAction.Dodge;
                return true;
            case "4":
                action = BattleAction.Retreat;
                return true;
            default:
                return false;
        }
    }
}
