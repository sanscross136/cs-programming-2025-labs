goods = {"яблоко": 100, "банан": 80, "груша": 120}

max_price = ""
for key, value in goods.items():
    if(max_price == "" or goods[max_price] < goods[key]):
        max_price = key

print(f'Максимальная цена: {max_price} - {goods[max_price]}')