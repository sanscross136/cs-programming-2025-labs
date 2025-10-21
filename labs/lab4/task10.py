user_input = input('Введите число: ')
user_num = 0
operation_result = 'составное'
try:
    user_num = int(user_input)
    for i in range(2,user_num):
        if(user_num % i == 0):
         operation_result = 'простое'
    print(f'{user_num} - {operation_result} число')
except:
    print(f'введено не число, ваш ввод {user_input}')
