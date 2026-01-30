using System.Text.Json;

namespace Rpg_Game;

// Менеджер сохранений и загрузок игры
public class GameSaveManager
{
    private static readonly string SavesDirectory = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
        "RpgGame",
        "Saves"
    );

    public GameSaveManager()
    {
        // Ensure saves directory exists
        if (!Directory.Exists(SavesDirectory))
        {
            Directory.CreateDirectory(SavesDirectory);
        }
    }

    // получение списка сохранений с датами
    public List<(string Filename, DateTime SaveTime)> GetSaveFiles()
    {
        if (!Directory.Exists(SavesDirectory))
            return new();

        var files = Directory.GetFiles(SavesDirectory, "*.json");
        var saves = new List<(string Filename, DateTime SaveTime)>();

        foreach (var file in files)
        {
            try
            {
                var content = File.ReadAllText(file);
                var data = JsonSerializer.Deserialize<SaveData>(content);
                if (data != null)
                {
                    saves.Add((Path.GetFileNameWithoutExtension(file), data.SaveTime));
                }
            }
            catch { /* пропуск поврежденных сохранений */ }
        }

        return saves.OrderByDescending(s => s.SaveTime).ToList();
    }

    // сохранение игры в джейсон
    public bool SaveGame(Player player, Dungeon dungeon, string saveName)
    {
        try
        {
            var saveData = new SaveData
            {
                PlayerName = player.Name,
                Race = player.Race,
                Level = player.Level,
                Experience = player.Experience,
                CurrentHealth = player.CurrentHealth,
                MaxHealth = player.MaxHealth,
                Damage = player.Damage,
                Defense = player.Defense,
                Agility = player.Agility,
                StatPoints = player.StatPoints,
                CurrentFloor = dungeon.CurrentFloor,
                CurrentRoomId = dungeon.CurrentRoomId,
                Money = player.Inventory.Money.Amount,
                WeaponLevel = player.Inventory.CurrentWeapon.Level,
                ArmorLevel = player.Inventory.CurrentArmor.Level,
                InventoryItems = SerializeInventory(player.Inventory),
                SaveTime = DateTime.Now
            };

            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            string filePath = Path.Combine(SavesDirectory, $"{saveName}.json");
            string jsonContent = JsonSerializer.Serialize(saveData, options);
            File.WriteAllText(filePath, jsonContent);

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"✗ Ошибка при сохранении игры: {ex.Message}");
            return false;
        }
    }

    // подгрузка информации из джейсона
    public bool LoadGame(string saveName, out Player? loadedPlayer, out Dungeon? loadedDungeon)
    {
        loadedPlayer = null;
        loadedDungeon = null;

        try
        {
            string filePath = Path.Combine(SavesDirectory, $"{saveName}.json");
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"✗ Сохранение '{saveName}' не найдено!");
                return false;
            }

            string jsonContent = File.ReadAllText(filePath);
            var saveData = JsonSerializer.Deserialize<SaveData>(jsonContent);

            if (saveData == null)
            {
                Console.WriteLine("✗ Ошибка при загрузке сохранения!");
                return false;
            }

            // восстановить игрока
            loadedPlayer = new Player(saveData.PlayerName, GetRaceKeyFromName(saveData.Race), saveData.Money);
            loadedPlayer.CurrentHealth = saveData.CurrentHealth;
            loadedPlayer.MaxHealth = saveData.MaxHealth;
            loadedPlayer.Experience = saveData.Experience;
            loadedPlayer.Damage = saveData.Damage;
            loadedPlayer.Defense = saveData.Defense;
            loadedPlayer.Agility = saveData.Agility;
            loadedPlayer.Level = saveData.Level;
            loadedPlayer.StatPoints = saveData.StatPoints;

            //  восстановить экипировку
            loadedPlayer.Inventory.CurrentWeapon.Level = saveData.WeaponLevel;
            loadedPlayer.Inventory.CurrentArmor.Level = saveData.ArmorLevel;

            //  восстановить инвентарь
            RestoreInventory(loadedPlayer, saveData.InventoryItems);

            // восстановить подземелье
            loadedDungeon = new Dungeon();
            loadedDungeon.CurrentFloor = saveData.CurrentFloor;
            loadedDungeon.CurrentRoomId = saveData.CurrentRoomId;

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"✗ Ошибка при загрузке игры: {ex.Message}");
            return false;
        }
    }

    // Удаляет сохранение по имени
    public bool DeleteSave(string saveName)
    {
        try
        {
            string filePath = Path.Combine(SavesDirectory, $"{saveName}.json");
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                return true;
            }
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"✗ Ошибка при удалении сохранения: {ex.Message}");
            return false;
        }
    }

    // Сохраняет предметы инвентаря в сериализуемый формат

    private List<InventoryItemData> SerializeInventory(Inventory inventory)
    {
        var items = new List<InventoryItemData>();

        foreach (var item in inventory.Items)
        {
            if (item is HealthPotion)
            {
                items.Add(new InventoryItemData { ItemType = "HealthPotion", Level = 1 });
            }
            else if (item is RegenerationPotion)
            {
                items.Add(new InventoryItemData { ItemType = "RegenerationPotion", Level = 1 });
            }
            else if (item is DamageBoostPotion)
            {
                items.Add(new InventoryItemData { ItemType = "DamageBoostPotion", Level = 1 });
            }
            else if (item is Weapon weapon)
            {
                items.Add(new InventoryItemData
                {
                    ItemType = "Weapon",
                    Level = weapon.Level,
                    EffectType = weapon.EffectType?.ToString(),
                    EffectPower = weapon.EffectPower
                });
            }
            else if (item is Armor armor)
            {
                items.Add(new InventoryItemData { ItemType = "Armor", Level = armor.Level });
            }
        }

        return items;
    }

    private void RestoreInventory(Player player, List<InventoryItemData> itemsData)
    {
        // Восстанавливает предметы инвентаря из сериализуемого формата
        foreach (var itemData in itemsData)
        {
            switch (itemData.ItemType)
            {
                case "HealthPotion":
                    player.Inventory.TryAddItem(new HealthPotion(1));
                    break;
                case "RegenerationPotion":
                    player.Inventory.TryAddItem(new RegenerationPotion(1));
                    break;
                case "DamageBoostPotion":
                    player.Inventory.TryAddItem(new DamageBoostPotion(1));
                    break;
                case "Weapon":
                    var weapon = new Weapon(itemData.Level);
                    if (!string.IsNullOrEmpty(itemData.EffectType) && 
                        Enum.TryParse<StatusEffectType>(itemData.EffectType, out var effectType))
                    {
                        weapon.EffectType = effectType;
                        weapon.EffectPower = itemData.EffectPower;
                    }
                    player.Inventory.TryAddItem(weapon);
                    break;
                case "Armor":
                    player.Inventory.TryAddItem(new Armor(itemData.Level));
                    break;
            }
        }
    }

    private static string GetRaceKeyFromName(string raceName)
    {
        return raceName switch
        {
            "Человек" => "Human",
            "Эльф" => "Elf",
            "Дворф" => "Dwarf",
            _ => "Human"
        };
    }
}
