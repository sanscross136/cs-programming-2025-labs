user_input = int(input('Введите год: '))

if user_input % 4 == 0 and user_input % 100 != 0 or user_input % 400 == 0:
    print(f'{user_input} - високосный год')
else:
    print(f'{user_input} - не високосный год')