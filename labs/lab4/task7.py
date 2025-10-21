user_input = str.split(input("Введите три числа: "),' ')
smallest_num = user_input[0]
if(user_input[1] < smallest_num):
    smallest_num = user_input[1]
if(user_input[2] < smallest_num):
    smallest_num = user_input[2]
print(smallest_num)