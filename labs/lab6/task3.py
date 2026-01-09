def IsPrime(_value):
    if _value <= 1:
        return False

    limit = int(_value ** 0.5) + 1
    for i in range(2, limit):
        if _value % i == 0:
            return False
    return True

def PrintAllPrimes(_lowerLimit , _upperLimit):
    if int(_lowerLimit) < 0 or int(_upperLimit) <= int(_lowerLimit):
        print('Error!')
        return
    IsPrimeFound = False
    CompletedStr = ''
    for  digit in range(int(_lowerLimit), int(_upperLimit)):
        if IsPrime(digit):
            CompletedStr += str(digit) + ' '
            IsPrimeFound = True
    if(IsPrimeFound == False):
        print('Error!')
        return
    print(CompletedStr)

user_input = input('введите два  значения через пробел: ').split(' ')
PrintAllPrimes(user_input[0], user_input[1])