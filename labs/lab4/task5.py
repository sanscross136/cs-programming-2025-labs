import string

def check_letters(value):
    has_lowercase = any(c.islower() for c in value)
    has_uppercase = any(c.isupper() for c in value)
    has_number = any(c.isdigit() for c in value)
    has_special_char = any(c in string.punctuation for c in value)
    
    return has_uppercase and has_number and has_special_char and has_lowercase

user_input = input()
if(check_letters(user_input) == True):
    print('Пароль сильный')
else: print('Пароль слабый')
