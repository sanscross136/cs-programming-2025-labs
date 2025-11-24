import string

special_chars = set(string.punctuation) 
user_input = input("Введите пароль: ") 
mistakes = []

if len(user_input) < 8:
    mistakes.append("длина менее 8 символов")

if not any(char.isupper() for char in user_input): 
    mistakes.append("отсутствуют заглавные буквы")
    
if not any(char.islower() for char in user_input): 
    mistakes.append("отсутствуют строчные буквы")

if not any(char.isdigit() for char in user_input): 
    mistakes.append("отсутствуют числа")

if not any(char in special_chars for char in user_input): 
    mistakes.append("отсутствуют специальные символы")

if mistakes:
    error_list = ", ".join(mistakes)
    print(f"Пароль ненадежный: {error_list}") 
else:
    print("Пароль надежный")
