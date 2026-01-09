TimeUnits = {'s': 1, 'm': 60, 'h': 3600, 'd': 86400}

def ConvertTime(_time, _unit):
    for unit in TimeUnits.keys():
        if unit in _time:
            base_time = int(_time.replace(unit, '')) * TimeUnits[unit]
            break
        
    return f'{base_time / TimeUnits[_unit]}{_unit}'

user_input = input('введите два  значения через пробел: ').split(' ')
print(ConvertTime(user_input[0], user_input[1]))