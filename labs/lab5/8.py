import random

Winner_dict = {
    'Ножницы': ['Бумага', 'режут бумагу', 'Ящерица', 'обезглавливают ящерицу'],
    'Бумага': ['Камень', 'покрывает камень', 'Спок', 'подставляет Спока'],
    'Камень': ['Ножницы', 'разбивает ножницы', 'Ящерица', 'давит ящерицу'],
    'Ящерица': ['Спок', 'отравляет Спока', 'Бумага', 'съедает бумагу'],
    'Спок': ['Ножницы', 'ломает ножницы', 'Камень', 'испаряет камень']
}

print('Доступные варианты: Камень Ножницы Бумага Ящерица Спок')
user_input = input('Введите свой вариант: ')

bot_choise = list(Winner_dict.keys())[random.randint(0,4)]
print(f'Бот выбрал: {bot_choise}')

if(bot_choise in Winner_dict[user_input]):
    win_phrase_index = Winner_dict[user_input].index(bot_choise) + 1
    print(f'{user_input} {Winner_dict[user_input][win_phrase_index]}')
    print("Вы победили!")
elif(user_input in Winner_dict[bot_choise]):
    win_phrase_index = Winner_dict[bot_choise].index(user_input) + 1
    print(f'{bot_choise} {Winner_dict[bot_choise][win_phrase_index]}')
    print("Вы проиграли!")
else: 
    print('Ничья')