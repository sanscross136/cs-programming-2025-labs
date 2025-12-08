user_input = input('введите значения для создания словаря пробел: ')

user_list = user_input.split(' ')

user_dict = {}

for i in user_list:
    user_dict[i] = i

print(f'Результат: {user_dict}')