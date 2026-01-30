namespace Rpg_Game;

public class Dungeon
{
    public int CurrentFloor { get; set; }
    public int CurrentRoomId { get; set; }
    private int _totalRoomsExplored;
    private const int RoomsPerFloor = 5;

    public Dungeon()
    {
        CurrentFloor = 1;
        CurrentRoomId = 0;
        _totalRoomsExplored = 0;
    }

    public Room GenerateNextRoom()
    {
        CurrentRoomId++;
        _totalRoomsExplored++;

        // Переход на следующий этаж каждые N комнат
        if (_totalRoomsExplored % RoomsPerFloor == 0)
        {
            CurrentFloor++;
            Console.WriteLine($"\n*** Вы спустились на {CurrentFloor} этаж подземелья! ***\n");
        }

        return new Room(CurrentRoomId, CurrentFloor);
    }

    public void HandleRoomEvent(Room room, Player player, RoomType roomType)
    {
        switch (roomType)
        {
            case RoomType.Battle:
                HandleBattleRoom(player);
                break;
            case RoomType.Rest:
                HandleRestRoom(player);
                break;
            case RoomType.Treasure:
                HandleTreasureRoom(player);
                break;
        }
    }

    private void HandleBattleRoom(Player player)
    {
        Console.WriteLine("\n[!] Впереди враг!");
        Enemy enemy = EnemyFactory.GenerateEnemy(CurrentFloor);
        Console.WriteLine($"Появился {enemy.Name}!");
    }

    private void HandleRestRoom(Player player)
    {
        Console.WriteLine("\nВы входите в спокойную комнату, лишенную опасности.");
        Console.WriteLine("Это хорошее место для отдыха и распределения очков характеристик.");
    }

    public void HandleTreasureRoom(Player player)
    {
        Console.WriteLine("\n[+] Вы нашли сундук!");
        int goldReward = RandomProvider.Next(10 + (CurrentFloor * 5), 30 + (CurrentFloor * 10));
        player.Inventory.Money.Add(goldReward);
        Console.WriteLine($"Получено золото: {goldReward}");

        // 50% вероятность найти предмет
        if (RandomProvider.Next(100) < 50)
        {
            int itemType = RandomProvider.Next(4);
            switch (itemType)
            {
                case 0:
                    var potion = new HealthPotion(CurrentFloor);
                    if (player.Inventory.TryAddItem(potion))
                    {
                        Console.WriteLine($"Найдено: {potion.Name}");
                    }
                    break;
                case 1:
                    var weapon = new Weapon(CurrentFloor);
                    if (player.Inventory.TryAddItem(weapon))
                    {
                        Console.WriteLine($"Найдено оружие уровня {weapon.Level}");
                    }
                    break;
                case 2:
                    var armor = new Armor(CurrentFloor);
                    if (player.Inventory.TryAddItem(armor))
                    {
                        Console.WriteLine($"Найдена броня уровня {armor.Level}");
                    }
                    break;
                case 3:
                    // Special weapon with effect
                    var specialWeapon = GenerateSpecialWeapon(CurrentFloor);
                    if (player.Inventory.TryAddItem(specialWeapon))
                    {
                        Console.WriteLine($"Найдено легендарное оружие уровня {specialWeapon.Level}!");
                    }
                    break;
            }
        }
    }

    private Weapon GenerateSpecialWeapon(int floor)
    {
        int weaponType = RandomProvider.Next(3);
        return weaponType switch
        {
            // Пылающий меч
            0 => new Weapon(floor, StatusEffectType.OnFire, 8),      
            // Ядовитый клинок
            1 => new Weapon(floor, StatusEffectType.PoisonDot, 6),   
            // Молот оглушения
            2 => new Weapon(floor, StatusEffectType.Stunned, 0),     
            // По умолчанию обычное оружие
            _ => new Weapon(floor)
        };
    }}