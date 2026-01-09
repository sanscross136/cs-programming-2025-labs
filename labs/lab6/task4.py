def AddMatrices(_n, _mat1, _mat2):
    N = int(_n)
    if N <= 2:
        return "Error!"
    
    Result = []
    for i in range(N):
        Row = []
        for J in range(N):
            Row.append(int(_mat1[i][J]) + int(_mat2[I][J]))
        Result.append(Row)
    return Result

UserInput = input('введите крч данные').strip().split()
N = UserInput[0]

Mat1 = []
Mat2 = []

for i in range(int(N)):
    Mat1.append(input().strip().split())

for i in range(int(N)):
    Mat2.append(input().strip().split())

Answer = AddMatrices(N, Mat1, Mat2)

if Answer == "Error!":
    print("Error!")
else:
    for Row in Answer:
        print(" ".join(map(str, Row)))