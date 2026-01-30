namespace Rpg_Game;

public record RaceStats(int MinHp, int MaxHp, int MinAtk, int MaxAtk, int MinDef, int MaxDef, int MinAgi, int MaxAgi, string Name);

public abstract class Creature
{
    protected List<StatusEffect> _statusEffects = new();

    public string Name { get; init; }
    public int MaxHealth { get; set; }
    public int CurrentHealth { get; set; }
    public int Damage { get; set; }
    public int Defense { get; set; }
    public int Agility { get; set; }
    public int Height { get; protected set; }
    public int Weight { get; protected set; }
    public int Level { get; set; }
    public IReadOnlyList<StatusEffect> StatusEffects => _statusEffects;

        protected Creature(string name, int health, int damage, int defense, int agility, int height, int weight, int level = 1)
        {
            Name = name;
            MaxHealth = health;
            CurrentHealth = health;
            Damage = damage;
            Defense = defense;
            Agility = agility;
            Height = height;
            Weight = weight;
            Level = level;
        }

        public bool IsAlive => CurrentHealth > 0;

        public virtual void TakeDamage(int damage)
        {
            int actualDamage = Math.Max(0, damage - Defense / 2);
            CurrentHealth = Math.Max(0, CurrentHealth - actualDamage);
            Console.WriteLine($"{Name} получил {actualDamage} урона. Осталось: {CurrentHealth}");
        }

        public virtual void Heal(uint amount)
        {
            if (!IsAlive) return;
            CurrentHealth = Math.Min(MaxHealth, CurrentHealth + (int)amount);
        }

        public void AddStatusEffect(StatusEffect effect)
        {
            //  избегаем дублирования эффектов
            if (HasStatusEffect(effect.Type))
            {
                Console.WriteLine($"{Name} уже под влиянием {effect.Name}!");
                return;
            }
            _statusEffects.Add(effect);
            Console.WriteLine($"{Name} получил эффект: {effect.Name} (длительность: {effect.Duration} ходов)");
        }

        public bool HasStatusEffect(StatusEffectType type)
        {
            return _statusEffects.Any(e => e.Type == type);
        }

        public void ProcessStatusEffects()
        {
            if (_statusEffects.Count == 0) return;

            Console.WriteLine($"\n[Эффекты {Name}]");

            for (int i = _statusEffects.Count - 1; i >= 0; i--)
            {
                var effect = _statusEffects[i];

                switch (effect.Type)
                {
                    case StatusEffectType.Regeneration:
                        Heal((uint)effect.Power);
                        Console.WriteLine($"  {Name} восстановил {effect.Power} HP (регенерация)");
                        break;

                    case StatusEffectType.OnFire:
                        Console.WriteLine($"  {Name} горит!");
                        TakeDamage(effect.Power);
                        break;

                    case StatusEffectType.PoisonDot:
                        Console.WriteLine($"  {Name} отравлен!");
                        TakeDamage(effect.Power);
                        break;

                    case StatusEffectType.DamageBoost:
                        Console.WriteLine($"  {Name} получает бонус к урону +{effect.Power}");
                        break;

                    case StatusEffectType.Stunned:
                        Console.WriteLine($"  {Name} оглушен и не может атаковать!");
                        break;
                }

                effect.Duration--;
                if (effect.Duration <= 0)
                {
                    _statusEffects.RemoveAt(i);
                    Console.WriteLine($"  Эффект {effect.Name} закончился");
                }
            }
        }

        public int GetDamageBonus()
        {
            var boostEffect = _statusEffects.FirstOrDefault(e => e.Type == StatusEffectType.DamageBoost);
            return boostEffect?.Power ?? 0;
        }

        public bool IsStunned()
        {
            return _statusEffects.Any(e => e.Type == StatusEffectType.Stunned);
        }

        public void ClearStatusEffects()
        {
            _statusEffects.Clear();
        }

        public abstract void Attack(Creature target);
    }

    public class Weapon : Item
    {
        public override int Level { get; set; }
        public StatusEffectType? EffectType { get; set; }
        public int EffectPower { get; set; }

        public Weapon(int level = 1, StatusEffectType? effectType = null, int effectPower = 0)
        {
            Level = level;
            EffectType = effectType;
            EffectPower = effectPower;
        }

        public void ApplyWeaponEffect(Creature target)
        {
            if (EffectType == null) return;

            var effect = new StatusEffect(EffectType.Value, 2 + Level, EffectPower + (Level * 2));
            target.AddStatusEffect(effect);
        }
    }

    public class Armor : Item
    {
        public override int Level { get; set; }
        public Armor(int level = 1) => Level = level;
    }

    public class Player : Creature
    {
        public int Experience { get; set; }
        public int StatPoints { get; set; }
        public string Race { get; set; }
        public Inventory Inventory { get; private set; }
        public Coins Money;
        
        private const int ExpPerLevel = 100;

        private static readonly Dictionary<string, RaceStats> AvailableRaces = new()
        {
            {"Human", new RaceStats(80, 120, 10, 15, 5, 10, 10, 15, "Человек")},
            {"Elf", new RaceStats(70, 100, 12, 18, 3, 8, 15, 20, "Эльф")},
            {"Dwarf", new RaceStats(100, 150, 15, 20, 10, 15, 8, 12, "Дворф")}
        };

        public Player(string name, string raceKey, int moneyAmount = 20) 
            : base(name, GenerateStats(raceKey).Hp, GenerateStats(raceKey).Atk, GenerateStats(raceKey).Def, GenerateStats(raceKey).Agi, 170, 70) 
        {
            Race = GetRaceName(raceKey);
            Experience = 0;
            StatPoints = 0;
            Money = new Coins(moneyAmount);
            Inventory = new Inventory(moneyAmount, new Armor(1), new Weapon(1), null);
        }
        
        private static (int Hp, int Atk, int Def, int Agi) GenerateStats(string raceKey)
        {
            if (!AvailableRaces.TryGetValue(raceKey, out var stats))
            {
                throw new ArgumentException("Неверно указана раса");
            }
            int hp = RandomProvider.Next(stats.MinHp, stats.MaxHp + 1);
            int atk = RandomProvider.Next(stats.MinAtk, stats.MaxAtk + 1);
            int def = RandomProvider.Next(stats.MinDef, stats.MaxDef + 1);
            int agi = RandomProvider.Next(stats.MinAgi, stats.MaxAgi + 1);
            return (hp, atk, def, agi);
        }

        private static string GetRaceName(string raceKey)
        {
            return raceKey switch
            {
                "Human" => "Человек",
                "Elf" => "Эльф",
                "Dwarf" => "Дворф",
                _ => "Неизвестная раса"
            };
        }

        public override void Attack(Creature target)
        {
            if (IsStunned())
            {
                Console.WriteLine($"{Name} оглушен и не может атаковать!");
                return;
            }

            int baseDamage = Damage + Inventory.CurrentWeapon.Level + GetDamageBonus();
            int variance = RandomProvider.Next(-2, 3);
            int damage = Math.Max(1, baseDamage + variance);
            bool isCritical = RandomProvider.Next(100) < (Agility * 2);
            
            if (isCritical)
            {
                damage = (int)(damage * 1.5);
                Console.WriteLine($"[КРИТ!] Игрок {Name} наносит мощный удар {target.Name}! ({damage} урона)");
            }
            else
            {
                Console.WriteLine($"Игрок {Name} атакует {target.Name}! ({damage} урона)");
            }
            target.TakeDamage(damage);

            //  Применение эффекта оружия
            Inventory.CurrentWeapon.ApplyWeaponEffect(target);
        }

        public void GainExperience(int amount)
        {
            Experience += amount;
            Console.WriteLine($"{Name} получил {amount} опыта.");
            CheckLevelUp();
        }

        private void CheckLevelUp()
        {
            while (Experience >= ExpPerLevel)
            {
                Level++;
                Experience -= ExpPerLevel;
                StatPoints += 5;
                Console.WriteLine($"**НОВЫЙ УРОВЕНЬ! {Name} достиг уровня {Level}! (+5 очков характеристик)**");
            }
        }

        public void DistributeStatPoints(int hpBonus, int atkBonus, int defBonus, int agiBonus)
        {
            int totalPoints = hpBonus + atkBonus + defBonus + agiBonus;
            if (totalPoints > StatPoints)
                throw new ArgumentException($"Недостаточно очков. Доступно: {StatPoints}, запрашивается: {totalPoints}");

            MaxHealth += hpBonus * 5;
            CurrentHealth = Math.Min(CurrentHealth + hpBonus * 5, MaxHealth);
            Damage += atkBonus * 2;
            Defense += defBonus * 2;
            Agility += agiBonus * 2;
            StatPoints -= totalPoints;

            Console.WriteLine($"Распределено очков - HP: +{hpBonus * 5}, ATK: +{atkBonus * 2}, DEF: +{defBonus * 2}, AGI: +{agiBonus * 2}");
        }

        public void EquipWeapon(Weapon weapon)
        {
            Inventory.CurrentWeapon = weapon;
            Console.WriteLine($"Экипировано оружие уровня {weapon.Level}");
        }

        public void EquipArmor(Armor armor)
        {
            Inventory.CurrentArmor = armor;
            Console.WriteLine($"Экипирована броня уровня {armor.Level}");
        }

        public void DisplayStatus()
        {
            Console.WriteLine($"\n=== Статус персонажа ===");
            Console.WriteLine($"Имя: {Name}");
            Console.WriteLine($"Уровень: {Level}");
            Console.WriteLine($"HP: {CurrentHealth}/{MaxHealth}");
            Console.WriteLine($"Атака: {Damage} (+ {Inventory.CurrentWeapon.Level} от оружия)");
            Console.WriteLine($"Защита: {Defense} (+ {Inventory.CurrentArmor.Level} от брони)");
            Console.WriteLine($"Ловкость: {Agility}");
            Console.WriteLine($"Опыт: {Experience}/{ExpPerLevel}");
            Console.WriteLine($"Очки характеристик: {StatPoints}");
            Console.WriteLine($"Золото: {Inventory.Money.Amount}");
            Console.WriteLine($"Предметов в инвентаре: {Inventory.Items.Count}/20\n");
        }
    }

    public class Enemy : Creature
    {
        public int Bounty { get; }

        public Enemy(string name, int health, int damage, int defense, int agility, int level) 
            : base(name, health, damage, defense, agility, 0, 0, level)
        {
            Bounty = level * 10;
        }

        public override void Attack(Creature target)
        {
            if (IsStunned())
            {
                Console.WriteLine($"{Name} оглушен и не может атаковать!");
                return;
            }

            int variance = RandomProvider.Next(-1, 2);
            int damage = Math.Max(1, Damage + variance + GetDamageBonus());
            Console.WriteLine($"Враг {Name} атакует {target.Name}! ({damage} урона)");
            target.TakeDamage(damage);

            //  Применение эффекта оружия с вероятностью 30%
            if (RandomProvider.Next(100) < 30)
            {
                int effectType = RandomProvider.Next(2);
                StatusEffectType? type = effectType switch
                {
                    0 => StatusEffectType.OnFire,
                    1 => StatusEffectType.PoisonDot,
                    _ => null
                };

                if (type.HasValue)
                {
                    var effect = new StatusEffect(type.Value, 2, 5 + (Level * 2));
                    target.AddStatusEffect(effect);
                }
            }
        }
    }

    public static class EnemyFactory
    {
        private static readonly string[] EnemyNames = 
        {
            "Гоблин", "Орк", "Слизень", "Скелет", "Зомби", "Летучая мышь", 
            "Паук", "Волк", "Привидение", "Демон"
        };

        public static Enemy GenerateEnemy(int floor)
        {
            int level = 1 + (floor / 3);
            int health = 20 + (level * 15);
            int damage = 5 + (level * 3);
            int defense = 2 + (level * 1);
            int agility = 3 + (level * 2);
            
            var random = new Random();
            string name = EnemyNames[random.Next(EnemyNames.Length)];
            return new Enemy($"{name} (ур. {level})", health, damage, defense, agility, level);
        }
    }