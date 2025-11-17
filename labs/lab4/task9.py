user_time = int(input("Введите час (0-23): "))
if user_time < 0 or user_time > 23:
    print("Ошибка: введите число от 0 до 23")
elif user_time <= 5:
    print("Сейчас ночь")
elif user_time <= 11:
    print("Сейчас утро")
elif user_time <= 17:
    print("Сейчас день")
else:
    print("Сейчас вечер")
