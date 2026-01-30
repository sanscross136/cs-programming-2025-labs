public bool SaveGame(Player player, Dungeon dungeon, string saveName)
{
    try {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        string filePath = Path.Combine(SavesDirectory, $"{saveName}.json");
        string jsonContent = JsonSerializer.Serialize(saveData, options);
        File.WriteAllText(filePath, jsonContent);

        return true;
    }
    catch (Exception ex) {
        Console.WriteLine($"✗ Ошибка при сохранении игры: {ex.Message}");
        return false;
    }
}

public bool LoadGame(string saveName, out Player? loadedPlayer,  out Dungeon? loadedDungeon)
{
    loadedPlayer = null;
    loadedDungeon = null;

    try{
        string filePath = Path.Combine(SavesDirectory, $"{saveName}.json");
        if (!File.Exists(filePath)) {
            Console.WriteLine($"✗ Сохранение '{saveName}' не найдено!");
            return false;
        }
        string jsonContent = File.ReadAllText(filePath);
        var saveData = JsonSerializer.Deserialize<SaveData>(jsonContent);
        if (saveData == null){
            Console.WriteLine("✗ Ошибка при загрузке сохранения!");
            return false;
        }
        loadedPlayer = new Player(saveData.PlayerName, GetRaceKeyFromName(saveData.Race), saveData.Money);
        loadedPlayer.Inventory.CurrentWeapon.Level = saveData.WeaponLevel;
        loadedPlayer.Inventory.CurrentArmor.Level = saveData.ArmorLevel;
        RestoreInventory(loadedPlayer, saveData.InventoryItems);
        loadedDungeon = new Dungeon();
        loadedDungeon.CurrentFloor = saveData.CurrentFloor;
        loadedDungeon.CurrentRoomId = saveData.CurrentRoomId;
        return true;
    }
    catch (Exception ex){
        Console.WriteLine($"✗ Ошибка при загрузке игры: {ex.Message}");
        return false;
    }
}
