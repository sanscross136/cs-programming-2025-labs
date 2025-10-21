from unittest import result


user_input = int(input('Введите час (0–23): '))
operation_result = "О как"
if(user_input >=0 and user_input <=5):
    operation_result = 'Ночь'
elif(user_input >= 6 and user_input <= 11):
    operation_result = 'Утро'
elif(user_input >= 12 and user_input <=17):
    operation_result = 'День'
elif(user_input >= 18 and user_input <= 23):
    operation_result = 'Вечер'
print(f'Сейчас {operation_result}')