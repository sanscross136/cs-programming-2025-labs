user_numbers = input('Введите три числа: ').split(' ')

min_number = int(user_numbers[0])
if int(user_numbers[1]) < min_number:
    min_number = user_numbers[1]
elif int(user_numbers[2]) < min_number:
    min_number = user_numbers[2]

print(f'Наименьшее число: {min_number}')