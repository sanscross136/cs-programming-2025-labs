user_input = float(input("Введите сумму покупки: "))
sale = 0.00
if(user_input <1000):
    sale = 0.0
elif(user_input >= 1000 and user_input < 5000):
    sale = 0.05
elif(user_input >= 5000 and user_input <= 10000):
    sale = 0.1
elif(user_input > 10000):
    sale = 0.15
print(f'Ваша скидка: {int(sale * 100)}%')
print(f'К оплате: {user_input * (1-sale)}')