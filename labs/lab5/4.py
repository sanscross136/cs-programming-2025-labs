user_input = input('введите значения для сортировки через пробел: ')

user_tuple = tuple(user_input.split(' '))

try:
    for i in user_tuple:
        int(i)
    user_tuple = tuple(sorted(user_tuple))
    print(f'результат: {user_tuple}')
except:
    print('Среди введёных значений есть не число, сортировка не производится')
    print(f'результат: {user_tuple}')
