using Rpg_Game;

var saveManager = new GameSaveManager();

Console.WriteLine("╔═══════════════════════════════════════╗");
Console.WriteLine("║     Добро пожаловать в Подземелье!    ║");
Console.WriteLine("║          Текстовая RPG-игра           ║");
Console.WriteLine("╚═══════════════════════════════════════╝\n");

try
{
    
    Player player;
    Dungeon dungeon;

    Console.WriteLine("╔═══════════════════════════════════════╗");
    Console.WriteLine("║  (1) Новая игра                       ║");
    Console.WriteLine("║  (2) Загрузить игру                   ║");
    Console.WriteLine("╚═══════════════════════════════════════╝");
    Console.Write("> ");

    string menuChoice = GetValidMenuChoice("12");
    Console.Clear();

    if (menuChoice == "2")
{
    var saves = saveManager.GetSaveFiles();
    if (saves.Count == 0)
    {
        Console.WriteLine("\n✗ Нет сохранённых игр!");
        player = CreateCharacter();
        dungeon = new Dungeon();
    }
    else
    {
        Console.WriteLine("\n╔═══════════════════════════════════════╗");
        Console.WriteLine("║     ЗАГРУЗИТЬ ИГРУ                    ║");
        Console.WriteLine("╚═══════════════════════════════════════╝\n");
        for (int i = 0; i < saves.Count; i++)
        {
            Console.WriteLine($"({i + 1}) {saves[i].Filename} — {saves[i].SaveTime:dd.MM.yyyy HH:mm:ss}");
        }
        Console.WriteLine("(0) Назад");
        Console.Write("\n> ");

        if (int.TryParse(Console.ReadLine() ?? "0", out int loadChoice) && loadChoice > 0 && loadChoice <= saves.Count)
        {
            if (saveManager.LoadGame(saves[loadChoice - 1].Filename, out var loadedPlayer, out var loadedDungeon))
            {
                player = loadedPlayer!;
                dungeon = loadedDungeon!;
                Console.WriteLine($"\n✓ Игра загружена! Добро пожаловать, {player.Name}!");
                System.Threading.Thread.Sleep(1500);
                Console.Clear();
            }
            else
            {
                player = CreateCharacter();
                dungeon = new Dungeon();
            }
        }
        else
        {
            player = CreateCharacter();
            dungeon = new Dungeon();
        }
    }
}
else
{
    // Создание персонажа
    player = CreateCharacter();

    // Инициализация подземелья
    dungeon = new Dungeon();
}

// Главный игровой цикл
GameLoop(player, dungeon, saveManager);

Console.WriteLine("\n╔═══════════════════════════════════════╗");
Console.WriteLine("║           Конец игры                 ║");
if (player.IsAlive)
{
    Console.WriteLine($"║ Спасибо за игру, {player.Name}! ║");
    Console.WriteLine($"║ Вы достигли этажа {dungeon.CurrentFloor}      ║");
}
else
{
    Console.WriteLine($"║ {player.Name} был повержен...      ║");
}
Console.WriteLine("╚═══════════════════════════════════════╝\n");
}
catch (Exception ex)
{
    Console.WriteLine($"\n✗ Критическая ошибка: {ex.Message}");
    Console.WriteLine("Игра будет закрыта. Нажмите любую клавишу...");
    Console.ReadLine();
}

// Функции 

static string GetValidMenuChoice(string validChoices)
{
    while (true)
    {
        string input = Console.ReadLine() ?? "";
        if (!string.IsNullOrWhiteSpace(input) && validChoices.Contains(input))
        {
            return input;
        }
        Console.WriteLine($"Некорректный выбор. Пожалуйста, введите одно из: {string.Join(", ", validChoices.ToCharArray())}");
        Console.Write("> ");
    }
}

static Player CreateCharacter()
{
    Console.WriteLine("═══ СОЗДАНИЕ ПЕРСОНАЖА ═══\n");
    Console.WriteLine("Выберите расу:");
    Console.WriteLine("  (1) Человек  - сбалансированная раса");
    Console.WriteLine("  (2) Эльф     - высокая ловкость и атака");
    Console.WriteLine("  (3) Дворф    - высокий уровень жизни и защиты");
    Console.Write("\n> ");

    string raceInput = GetValidMenuChoice("123");
    string raceKey = raceInput switch
    {
        "1" => "Human",
        "2" => "Elf",
        "3" => "Dwarf",
        _ => "Human"
    };

    Console.Write("Введите имя персонажа: ");
    string name = Console.ReadLine() ?? "";
    
    if (string.IsNullOrWhiteSpace(name))
    {
        name = "Герой";
    }

    Player player = new Player(name, raceKey, 50);
    
    Console.WriteLine("\n╔════════════════════════════════════╗");
    Console.WriteLine("║   Персонаж успешно создан!         ║");
    Console.WriteLine("╚════════════════════════════════════╝");
    
    player.DisplayStatus();
    
    Console.Write("Нажмите любую клавишу для начала игры...");
    Console.ReadLine();
    Console.Clear();

    return player;
}

static void GameLoop(Player player, Dungeon dungeon, GameSaveManager saveManager)
{
    bool continueGame = true;

    while (continueGame && player.IsAlive)
    {
        // Генерируем комнату
        Room room = dungeon.GenerateNextRoom();
        room.DisplayRoom();

        // Выбор направления
        Console.Write("\nВыберите направление (1-слева/2-справа): ");
        string input = Console.ReadLine() ?? "1";
        int choice = int.TryParse(input, out int num) && (num == 1 || num == 2) ? num : 1;
        Console.Clear();

        RoomExit exit = room.GetChosenExit(choice);

        // Обработка события в комнате
        HandleRoomEncounter(player, dungeon, exit.Type);
        Console.Write("\nНажмите любую клавишу для продолжения...");
        Console.ReadLine();
        Console.Clear();
        
        if (!player.IsAlive)
        {
            Console.WriteLine($"\n╔════════════════════════════════════╗");
            Console.WriteLine($"║       {player.Name} был повержен!  ║");
            Console.WriteLine($"╚════════════════════════════════════╝");
            continueGame = false;
            break;
        }

        // Меню после комнаты
        bool validChoice = false;
        while (!validChoice)
        {
            Console.WriteLine("\n╔════════════════════════════════════╗");
            Console.WriteLine("║     Что делать дальше?             ║");
            Console.WriteLine("║  (1) Продолжить исследование       ║");
            Console.WriteLine("║  (2) Просмотреть статус            ║");
            Console.WriteLine("║  (3) Открыть инвентарь             ║");
            Console.WriteLine("║  (4) Распределить очки статов      ║");
            Console.WriteLine("║  (5) Сохранить игру                ║");
            Console.WriteLine("║  (6) Выход в главное меню          ║");
            Console.WriteLine("╚════════════════════════════════════╝");
            Console.Write("> ");

            string menuChoice = GetValidMenuChoice("123456");

            switch (menuChoice)
            {
                case "1":
                    validChoice = true;
                    break;
                case "2":
                    Console.Clear();
                    player.DisplayStatus();
                    Console.Write("Нажмите любую клавишу для продолжения...");
                    Console.ReadLine();
                    Console.Clear();
                    break;
                case "3":
                    Console.Clear();
                    DisplayInventoryMenu(player);
                    Console.Clear();
                    break;
                case "4":
                    Console.Clear();
                    DistributeStats(player);
                    Console.Clear();
                    break;
                case "5":
                    Console.Clear();
                    SaveGameMenu(player, dungeon, saveManager);
                    Console.Clear();
                    break;
                case "6":
                    continueGame = false;
                    validChoice = true;
                    break;
            }
        }
    }
}
// Обработка события в комнате
static void HandleRoomEncounter(Player player, Dungeon dungeon, RoomType roomType)
{
    switch (roomType)
    {
        case RoomType.Battle:
            StartBattle(player, dungeon);
            break;
        case RoomType.Rest:
            Console.WriteLine("\n[*] Вы входите в гостиницу.");
            Console.WriteLine("Здесь можно отдохнуть и восстановить здоровье.");
            
            // Оплата за отдых в гостинице
            int restCost = 30 + (dungeon.CurrentFloor * 5);
            Console.WriteLine($"\nСтоимость отдыха: {restCost} золота (ваше золото: {player.Inventory.Money.Amount})");
            Console.WriteLine("\n╔════════════════════════════════════╗");
            Console.WriteLine("║     Что вы хотите сделать?         ║");
            Console.WriteLine("║  (1) Отдохнуть и восстановиться    ║");
            Console.WriteLine("║  (2) Покинуть гостиницу            ║");
            Console.WriteLine("╚════════════════════════════════════╝");
            Console.Write("> ");
            string restChoice = GetValidMenuChoice("12");
            
            if (restChoice == "1")
            {
                if (player.Inventory.Money.TrySubtract(restCost))
                {
                    player.CurrentHealth = player.MaxHealth;
                    player.ClearStatusEffects();
                    Console.WriteLine($"\n[+] HP полностью восстановлено! ({player.CurrentHealth} HP)");
                    Console.WriteLine("[+] Все эффекты состояния очищены!");
                    Console.WriteLine($"[-] Вы заплатили {restCost} золота за отдых");
                }
                else
                {
                    Console.WriteLine($"\n[!] Недостаточно денег! Нужно {restCost} золота, а у вас только {player.Inventory.Money.Amount}");
                }
            }
            else
            {
                Console.WriteLine("\nВы покидаете гостиницу.");
            }
            break;
        case RoomType.Treasure:
            dungeon.HandleTreasureRoom(player);
            break;
    }
}

static void StartBattle(Player player, Dungeon dungeon)
{
    Enemy enemy = EnemyFactory.GenerateEnemy(dungeon.CurrentFloor);
    Battle battle = new Battle(player, enemy);

    Console.WriteLine($"\n╔════════════════════════════════════╗");
    Console.WriteLine($"║        БОЙ С {enemy.Name}          ║");          
    Console.WriteLine($"╚════════════════════════════════════╝");

    while (!battle.IsOver())
    {
        Console.Clear();
        battle.DisplayBattleStatus();

        if (!battle.GetPlayerAction(out BattleAction action))
        {
            Console.WriteLine("Некорректная команда!");
            continue;
        }

        Console.Clear();
        Console.WriteLine($"=== БОЙ - {player.Name} vs {enemy.Name} ===\n");        
        // Обработка статус эффектов перед действием игрока
        battle.ProcessRoundEffects();
                bool continueAction = battle.ExecutePlayerAction(action);

        if (action == BattleAction.Retreat && !continueAction)
        {
            Console.WriteLine("✓ Вы успешно убежали из боя!");
            break;
        }

        if (!enemy.IsAlive)
        {
            battle.RewardPlayer();
            Console.Write("\nНажмите любую клавишу для продолжения...");
            Console.ReadLine();
            break;
        }

        if (continueAction)
        {
            battle.ExecuteEnemyAction();
        }

        // Обработка эффектов в конце раунда
        battle.ProcessRoundEffects();

        if (!player.IsAlive)
        {
            Console.WriteLine($"\n✗ {player.Name} был повержен!");
            Console.Write("\nНажмите любую клавишу для продолжения...");
            Console.ReadLine();
            break;
        }

        Console.Write("\nНажмите любую клавишу для следующего раунда...");
        Console.ReadLine();
    }
}

static void DisplayInventoryMenu(Player player)
{
    bool inInventory = true;
    while (inInventory)
    {
        Console.Clear();
        Console.WriteLine("\n╔════════════════════════════════════╗");
        Console.WriteLine("║         ИНВЕНТАРЬ                  ║");
        Console.WriteLine("╚════════════════════════════════════╝");
        
        player.Inventory.DisplayItemsWithIndexes();
        
        Console.WriteLine($"\nЗолото: {player.Inventory.Money.Amount}");
        Console.WriteLine($"Оружие: Уровень {player.Inventory.CurrentWeapon.Level}");
        Console.WriteLine($"Броня: Уровень {player.Inventory.CurrentArmor.Level}");
        
        Console.WriteLine("\nДействия:");
        Console.WriteLine("(1) Экипировать оружие");
        Console.WriteLine("(2) Экипировать броню");
        Console.WriteLine("(3) Использовать зелье");
        Console.WriteLine("(4) Назад");
        Console.Write("> ");

        string choice = Console.ReadLine() ?? "";
        switch (choice)
        {
            case "1":
                Console.Clear();
                EquipWeaponMenu(player);
                break;
            case "2":
                Console.Clear();
                EquipArmorMenu(player);
                break;
            case "3":
                Console.Clear();
                UsePotionMenu(player);
                break;
            case "4":
                inInventory = false;
                break;
            default:
                Console.WriteLine("Некорректная команда!");
                System.Threading.Thread.Sleep(1000);
                break;
        }
    }
}

static void EquipWeaponMenu(Player player)
{
    Console.Clear();
    var weapons = player.Inventory.Items.OfType<Weapon>().ToList();
    if (weapons.Count == 0)
    {
        Console.WriteLine("\nНет оружия в инвентаре!");
        Console.Write("Нажмите любую клавишу для возврата...");
        Console.ReadLine();
        return;
    }

    Console.WriteLine("\nВыберите оружие:");
    for (int i = 0; i < weapons.Count; i++)
    {
        Console.WriteLine($"({i + 1}) Оружие уровня {weapons[i].Level}");
    }
    // Выбор оружия
    if (int.TryParse(Console.ReadLine() ?? "0", out int idx) && idx > 0 && idx <= weapons.Count)
    {
        player.EquipWeapon(weapons[idx - 1]);
    }
}

static void EquipArmorMenu(Player player)
{
    Console.Clear();
    var armors = player.Inventory.Items.OfType<Armor>().ToList();
    if (armors.Count == 0)
    {
        Console.WriteLine("\nНет брони в инвентаре!");
        Console.Write("Нажмите любую клавишу для возврата...");
        Console.ReadLine();
        return;
    }

    Console.WriteLine("\nВыберите броню:");
    for (int i = 0; i < armors.Count; i++)
    {
        Console.WriteLine($"({i + 1}) Броня уровня {armors[i].Level}");
    }

    if (int.TryParse(Console.ReadLine() ?? "0", out int idx) && idx > 0 && idx <= armors.Count)
    {
        player.EquipArmor(armors[idx - 1]);
    }
}

static void UsePotionMenu(Player player)
{
    Console.Clear();
    var potions = player.Inventory.Items.OfType<Potion>().ToList();
    if (potions.Count == 0)
    {
        Console.WriteLine("Нет зелий в инвентаре!");
        return;
    }

    Console.WriteLine("\nВыберите зелье:");
    for (int i = 0; i < potions.Count; i++)
    {
        var potion = potions[i];
        Console.WriteLine($"({i + 1}) {potion.Name}");
    }

    if (int.TryParse(Console.ReadLine() ?? "0", out int idx) && idx > 0 && idx <= potions.Count)
    {
        var potion = potions[idx - 1];
        potion.UsePotion(player);
        player.Inventory.RemoveItem(potion);
    }
}

static void DistributeStats(Player player)
{
    Console.Clear();
    if (player.StatPoints == 0)
    {
        Console.WriteLine("\nНет очков характеристик для распределения!");
        return;
    }

    Console.WriteLine($"\n╔════════════════════════════════════╗");
    Console.WriteLine($"║ Доступно очков: {player.StatPoints,-17}║");
    Console.WriteLine($"╚════════════════════════════════════╝");
    Console.WriteLine("Сколько очков распределить:");
    Console.Write("HP (x5 за очко): ");
    int hp = int.TryParse(Console.ReadLine() ?? "0", out int h) ? h : 0;
    Console.Write("ATK (x2 за очко): ");
    int atk = int.TryParse(Console.ReadLine() ?? "0", out int a) ? a : 0;
    Console.Write("DEF (x2 за очко): ");
    int def = int.TryParse(Console.ReadLine() ?? "0", out int d) ? d : 0;
    Console.Write("AGI (x2 за очко): ");
    int agi = int.TryParse(Console.ReadLine() ?? "0", out int ag) ? ag : 0;

    try
    {
        player.DistributeStatPoints(hp, atk, def, agi);
        Console.WriteLine("✓ Очки успешно распределены!");
    }
    catch (ArgumentException ex)
    {
        Console.WriteLine($"✗ Ошибка: {ex.Message}");
    }
}

static void SaveGameMenu(Player player, Dungeon dungeon, GameSaveManager saveManager)
{
    Console.Clear();
    Console.WriteLine("\n╔════════════════════════════════════╗");
    Console.WriteLine("║         СОХРАНИТЬ ИГРУ             ║");
    Console.WriteLine("╚════════════════════════════════════╝");
    Console.Write("Введите название сохранения: ");
    string saveName = Console.ReadLine() ?? "";

    if (string.IsNullOrWhiteSpace(saveName))
    {
        Console.WriteLine("✗ Название не может быть пусто!");
        return;
    }

    if (saveManager.SaveGame(player, dungeon, saveName))
    {
        Console.WriteLine($"✓ Игра сохранена под названием '{saveName}'!");
    }
    else
    {
        Console.WriteLine("✗ Ошибка при сохранении игры!");
    }
}
