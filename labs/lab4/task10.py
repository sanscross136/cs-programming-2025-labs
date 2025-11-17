def is_prime(n):
    if n <= 1:
        return False

    limit = int(n ** 0.5) + 1
    for i in range(2, limit):
        if n % i == 0:
            return False
    return True

user_input = input("Введите число: ")
try:
    num = int(user_input)    
    if is_prime(num):
        print(f"{num} — простое число.")
    else:
        print(f"{num} — не является простым числом.")
except:
    print("Ошибка: нужно ввести целое число.")
