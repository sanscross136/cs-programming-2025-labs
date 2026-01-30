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
    public int Defense { get; set; }}

public class InventoryItemData
{
    [JsonPropertyName("itemType")]
    public string ItemType { get; set; } = "";

    [JsonPropertyName("level")]
    public int Level { get; set; }

    [JsonPropertyName("effectType")]
    public string? EffectType { get; set; }

    [JsonPropertyName("effectPower")]
    public int EffectPower { get; set; }
}
