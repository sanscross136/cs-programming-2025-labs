def errors_check(value):
    if(value.isdigit() == False):
        print('Ошибка: возраст должен быть числом')
        return False
    elif(int(value) < 1):
        print('Ошибка: возраст должен быть не меньше 1')
        return False
    elif(int(value) > 22):
        print('Ошибка: возраст должен быть не больше 22')
        return False
    else: return True

def calculate_age(value):
    if(errors_check(value) == False):
        return
    if(int(value) >= 2):
        print(21 + (int(value) - 2) * 4)
    else: print(11.5)

dog_age = input('Введите возраст собаки (В годах): ')
calculate_age(dog_age)