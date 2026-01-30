namespace Rpg_Game;

// безопасный для потоков генератор случайных чисел
public static class RandomProvider
{
    // блокировка для обеспечения потокобезопасности
    private static readonly object _lock = new object();
    private static readonly Random _random = new Random();

    // возвращает случайное целое число в диапазоне [minValue, maxValue)
    public static int Next(int minValue, int maxValue)
    {
        lock (_lock)
        {
            return _random.Next(minValue, maxValue);
        }
    }

    // возвращает случайное целое число в диапазоне [0, maxValue)
    public static int Next(int maxValue)
    {
        lock (_lock)
        {
            return _random.Next(maxValue);
        }
    }

    // возвращает случайное вещественное число между 0.0 и 1.0
    public static double NextDouble()
    {
        lock (_lock)
        {
            return _random.NextDouble();
        }
    }
}
