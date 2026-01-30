using System.Text.Json.Serialization;

namespace Rpg_Game;

/// <summary>
/// Serializable data structure for saving player game state to JSON
/// </summary>
public class SaveData
{
    [JsonPropertyName("playerName")]
    public string PlayerName { get; set; } = "";

    [JsonPropertyName("race")]
    public string Race { get; set; } = "";

    [JsonPropertyName("level")]
    public int Level { get; set; }

    [JsonPropertyName("experience")]
    public int Experience { get; set; }

    [JsonPropertyName("currentHealth")]
    public int CurrentHealth { get; set; }

    [JsonPropertyName("maxHealth")]
    public int MaxHealth { get; set; }

    [JsonPropertyName("damage")]
    public int Damage { get; set; }

    [JsonPropertyName("defense")]
    public int Defense { get; set; }

    [JsonPropertyName("agility")]
    public int Agility { get; set; }

    [JsonPropertyName("statPoints")]
    public int StatPoints { get; set; }

    [JsonPropertyName("currentFloor")]
    public int CurrentFloor { get; set; }

    [JsonPropertyName("currentRoomId")]
    public int CurrentRoomId { get; set; }

    [JsonPropertyName("money")]
    public int Money { get; set; }

    [JsonPropertyName("weaponLevel")]
    public int WeaponLevel { get; set; }

    [JsonPropertyName("armorLevel")]
    public int ArmorLevel { get; set; }

    [JsonPropertyName("inventoryItems")]
    public List<InventoryItemData> InventoryItems { get; set; } = new();

    [JsonPropertyName("saveTime")]
    public DateTime SaveTime { get; set; } = DateTime.Now;
}

/// <summary>
/// Represents an inventory item in serializable format
/// </summary>
public class InventoryItemData
{
    [JsonPropertyName("itemType")]
    public string ItemType { get; set; } = ""; // "HealthPotion", "Weapon", "Armor", etc.

    [JsonPropertyName("level")]
    public int Level { get; set; }

    [JsonPropertyName("effectType")]
    public string? EffectType { get; set; }

    [JsonPropertyName("effectPower")]
    public int EffectPower { get; set; }
}
