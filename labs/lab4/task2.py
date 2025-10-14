user_input = int(input('введите номер месяца: '))

if (user_input == 12 or user_input in range(1,3)):
    print('Зима')
elif (user_input in range(3,6)):
    print('Весна')
elif (user_input in range(6,9)):
    print('Лето')
elif (user_input in range(9,12)):
    print('Осень')
else:
    print('Ошибка, месяц введён не правильно')
