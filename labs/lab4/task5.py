from unittest import result


alphabet = 'abcdefghijklmnopqrstuvwxyz'
upper_alphabet = str.upper(alphabet)
numbers = '1234567890'
special_char = '@!#$%&_-'
has_alpha = False
has_upper_alpha = False
has_numbers = False
has_spec_char = False
checks_passed = 0
result_str = 'строчные буквы:заглавные буквы:числа:специальные символы'

user_input = input('Введите пароль: ')
for i in user_input:
    if(i in alphabet and has_alpha == False):
        has_alphabet = True
        checks_passed += 1
        result_str = result_str.replace('строчные буквы:', '')
    elif(i in upper_alphabet and has_upper_alpha == False):
        has_upper_alpha = True
        checks_passed += 1
        result_str = result_str.replace('заглавные буквы:', '')
    elif(i in numbers and has_numbers == False):
        has_numbers = True
        checks_passed += 1
        result_str = result_str.replace('числа:', '')
    elif(i in special_char and has_spec_char == False):
        has_spec_char = True
        checks_passed += 1
        result_str = result_str.replace('специальные символы', '')

if(checks_passed != 4):  
    current_error = 4 - checks_passed
    errors = str.split(result_str,':')
    if(current_error == 1):
        print(f'Пароль ненадёжный: отсутсвуют {errors[0]}')
    elif(current_error == 2):
        print(f'Пароль ненадёжный: отсутсвуют {errors[0]} и {errors[1]}')
    elif(current_error == 3):
        print(f'Пароль ненадёжный: отсутсвуют {errors[0]}, {errors[1]} и {errors[2]}')
    else:
        print(f'Пароль ненадёжный: отсутсвуют {errors[0]}, {errors[1]}, {errors[2]} и {errors[3]}')
else:
    print('Пароль надёжный')

    
