namespace Rpg_Game;

public enum RoomType
{
    Battle,    // Боевая комната - враг
    Rest,      // Комната отдыха - никаких событий
    Treasure   // Комната с сундуком - золото и/или предметы
}

// Выход из комнаты с типом и видимостью
public class RoomExit
{
    public RoomType Type { get; set; }
    public bool IsVisible { get; set; }
    public string Description => IsVisible ? GetDescription() : "???";

    private string GetDescription() => Type switch
    {
        RoomType.Battle => "Боевая комната",
        RoomType.Rest => "Комната отдыха",
        RoomType.Treasure => "Комната с сундуком",
        _ => "Неизвестно"
    };
}

// Комната в подземелье с двумя выходами
public class Room
{
    public int Id { get; set; }
    public int Floor { get; set; }
    public RoomExit LeftExit { get; set; } = new();
    public RoomExit RightExit { get; set; } = new();

    public Room(int id, int floor)
    {
        Id = id;
        Floor = floor;
        GenerateExits();
    }

    // Генерирует два выхода с случайными типами и видимостью
    private void GenerateExits()
    {
        var types = new[] { RoomType.Battle, RoomType.Rest, RoomType.Treasure };
        
        LeftExit = new RoomExit
        {
            // случайный тип выхода
            Type = types[RandomProvider.Next(types.Length)],
            // 60% шанс, что выход будет видимым
            IsVisible = RandomProvider.Next(100) < 60
        };

        RightExit = new RoomExit
        {
            Type = types[RandomProvider.Next(types.Length)],
            IsVisible = RandomProvider.Next(100) < 60
        };
    }

    public void DisplayRoom()
    {
        Console.WriteLine($"\n=== Комната {Id} (Этаж {Floor}) ===");
        Console.WriteLine($"(1) Налево: {LeftExit.Description}");
        Console.WriteLine($"(2) Направо: {RightExit.Description}");
    }
    // Получает выбранный выход по номеру выбора
    public RoomExit GetChosenExit(int choice)
    {
        return choice == 1 ? LeftExit : RightExit;
    }
}
