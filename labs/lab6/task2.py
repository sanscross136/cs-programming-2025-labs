def CalculateVklad(_deposit, _years):
    if int(_deposit) < 30000:
        return 0.0
    _years = int(_years)
    if _years <= 3:
        Rate = 0.03
    elif _years <= 6:
        Rate = 0.05
    else:
        Rate = 0.02
    AddRate = (int(_deposit) // 10000) * 0.003
    TotallRate = min(Rate + AddRate, 0.05)
    _profit = int(_deposit) * (1 + TotallRate) ** _years - int(_deposit)
    return round(_profit, 2)

user_input = input('введите сумму вклада и количество лет через пробел: ').split(' ')
print(CalculateVklad(user_input[0], user_input[1]))