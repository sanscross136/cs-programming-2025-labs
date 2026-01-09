import string

def IsPalindrome(_line):
    BaseStr = str(_line).lower()
    
    for letter in range(len(BaseStr)):
        if BaseStr[letter] in string.punctuation:
            BaseStr[letter] = ''
    BaseStr = BaseStr.replace(' ' , '')

    if BaseStr == BaseStr[::-1]:
        return 'Да'
    return 'Нет'

print(IsPalindrome(input('введите строку для  проверки: ')))
