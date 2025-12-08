import random

list_of_numbers = []

for i in range(random.randint(0,1000)):
    list_of_numbers.append(random.randint(0,10000))

list_len = len(list_of_numbers)
max_int = max(list_of_numbers)

print(f'Длина данного списка: {list_len}, Наибольшее число: {max_int}')

print(f'результат: {int(max_int / list_len)}')