user_amount = int(input("Введите сумму покупки: "))
if user_amount < 1000:
    discount = 0
elif 1000 <= user_amount <= 5000:
    discount = 5
elif 5000 < user_amount <= 10000:
    discount = 10
else:
    discount = 15

final_price = user_amount * (100 - discount) / 100

print(f"Сумма покупки: {user_amount} руб.")
print(f"Ваша скидка: {discount}%")
print(f"К оплате: {final_price:} руб.")