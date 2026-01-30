namespace Rpg_Game;

public class Inventory
{
    // Максимальная вместимость инвентаря
    private readonly List<Item> _items = new(20);
    // Только для чтения список предметов
    public IReadOnlyList<Item> Items => _items;

    public Coins Money { get; private set; }
    public Armor CurrentArmor { get; set; }
    public Weapon CurrentWeapon { get; set; }

    public Inventory(int moneyAmount, Armor armor, Weapon weapon, Item[]? items)
    {
        Money = new Coins(moneyAmount);
        CurrentArmor = armor;
        CurrentWeapon = weapon;

        if (items is not null)
        {
            if (items.Length > 20)
                throw new ArgumentException($"Массив предметов не может содержать более 20 элементов. В массиве: {items?.Length ?? 0}. элементов");

            _items.AddRange(items);
        }
    }

    public bool TryAddItem(Item item)
    {
        if (_items.Count >= 20) {

            Console.WriteLine("В инвентаре нет места");            
            return false;
        }
        _items.Add(item);
        return true;
    }

    public bool RemoveItem(Item item)
    {
        return _items.Remove(item);
    }

    public bool RemoveItemAt(int index)
    {
        if (index < 0 || index >= _items.Count) return false;
        _items.RemoveAt(index);
        return true;
    }

    public void DisplayItemsWithIndexes()
    {
        if (_items.Count == 0)
        {
            Console.WriteLine("Инвентарь пуст.");
            return;
        }

        Console.WriteLine("Ваш инвентарь:");
        for (int i = 0; i < _items.Count; i++)
        {
            Item item = _items[i];
            // Определение места предмета в списске, в зависимости от его типа
            string itemName = item switch
            {
                Potion p => p.Name,
                Armor _  => $"Доспех (уровень {item.Level})",
                Weapon _ => $"Оружие (уровень {item.Level})",
                _        => "Неизвестный предмет"
            };
            Console.WriteLine($"[{i}] {itemName}");
        }
    }

    
}
