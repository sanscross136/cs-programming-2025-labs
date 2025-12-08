translation_dict = {"Hello" : 'Привет', 'Bye' : "Пока"}

user_input = input('введите Слово на русском: ')

been_translated = False
for key, value in translation_dict.items():
    if(value == user_input):
        print(f"Перевод слова: {key}")
        been_translated = True
        break
if(not been_translated):
    print('Такого слова нет в словаре')

