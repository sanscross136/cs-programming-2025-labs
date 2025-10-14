user_input = input('Введите число: ')
sum_of_numbers = 0
for i in user_input:
    sum_of_numbers += int(i)
if int(user_input[-1]) % 2 == 0 and sum_of_numbers % 3 == 0 and sum_of_numbers != 0:
    print('Число делится на 6')
else: print('Число не делится на 6')
    
