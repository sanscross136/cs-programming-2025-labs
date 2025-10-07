while(1):

    print('Введите два числа через пробел: ')  
    user_input = input().split(' ')
    sum = int(user_input[0]) + int(user_input[1])
    print(f'Сумма равна: {sum}')