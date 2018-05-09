import random

#def outputMatrix(var):
def random_matrix(n):
    matrix = []
    supermatrix = []
    for j in range(n):
        for i in range(n):
            matrix.append( random.randint(1,10))
        supermatrix.append(matrix)
        matrix=[]
    return supermatrix

def printMatrix(matr,n):
    for i in range(n):
        print(matr[i])
    print("\n")

def multyplyMatrix(matr1,matr2,n):
    result = []
    matrixTemp = []
    for i in range(n):
        for j in range(n):
            temp = 0
            for k in range(n):
                temp += matr1[i][k]* matr2[k][j]
            matrixTemp.append(temp)
        result.append(matrixTemp)
        matrixTemp = []
    return result

def E_matrix(n):
    matrix = []
    supermatrix = []
    for j in range(n):
        for i in range(n):
            if(i==j):
                matrix.append(1)
            else:
                matrix.append(0)
        supermatrix.append(matrix)
        matrix = []
    return supermatrix

def minus(matr1,matr2,n):
    result = []
    matrixTemp = []
    for i in range(n):
        for j in range(n):
            temp = matr1[i][j]-matr2[i][j]
            matrixTemp.append(temp)
        result.append(matrixTemp)
        matrixTemp = []
    return result

def switch(matr,n,j,k):
    for i in range(n):
        temp = matr[j][n]
        matr[j][n] = matr[k][n]
        matr[k][n] = temp

def maxInColumn(matr,step,n):
    temp = -99999
    result = step
    for i in range(step,n):
        if(temp<matr[i][step]):
            temp = matr[i][step]
            result = i
    return result

def inputMatrix():
    result = []
    tempResult = []
    print("Введiть кiлькiсть рядкiв матрицi: ")
    n =int( input())
    print("Введiть кiлькiсть стовпчикiв матрицi: ")
    m = int(input())
    print("Введiть елементи матрицi: ")
    for i in range(n):
        temp = input()
        work = temp.split(" ")
        tempResult = []
        for j in range(m):
            tempResult.append(float(work[j]))
        result.append(tempResult)
    return result,n

def Gauss(A,b,n):
    result = []
    for i in range(n-1):
        if(A[i][i]==0):
            index = maxInColumn(A,i,n)
            switch(A,n,index,i)
            switch(b,1,index,i)
        if(A[i][i]==0):
            return False
        koef = 1 / A[i][i]
        for j in range(i+1,n):
            koef2 = A[j][i]
            for k in range(i,n):
                A[j][k]-=A[i][k]*koef*koef2
            b[j][0] -=b[i][0]*koef*koef2
        print(str(i+1)+"-ий крок прямого ходу")
        printMatrix(A,n)
        printMatrix(b,n)
    for i in range(n):
        x = 0
        for j in range(n-1-i,n):
            x+=result[n-j-1]*A[n-1-i][n-1-j]
        result[n-1-i] = b[n-1-i]-x
    return True


A,n = inputMatrix()
b,k = inputMatrix()
print("До:")
printMatrix(A,n)
printMatrix(b,n)
Gauss(A,b,n)
print("Пiсля:")
printMatrix(A,n)
printMatrix(b,n)