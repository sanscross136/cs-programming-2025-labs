import random

list_of_numbers = []

for i in range (5):
    list_of_numbers.append(random.randint(0,100))

print(f'Данный список: {list_of_numbers}')

for i in range(len(list_of_numbers)):
    list_of_numbers[i] **= 2

print(f'Лист после операций: {list_of_numbers}')