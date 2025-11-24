import random

list_of_numbers = []

for i in range(10):
    list_of_numbers.append(random.randint(0,10))

print(f'Данный список: {list_of_numbers}')

for i in range(len(list_of_numbers)):
    if list_of_numbers[i] == 3:
        list_of_numbers[i] = 30

print(f'Лист после операций: {list_of_numbers}')