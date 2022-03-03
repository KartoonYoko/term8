import numpy as np


class HillCipherScrambler:
    ''' Шифратор по методу Шифра Хилла'''


    def __init__(self, alphabet: np.array, m: np.number, key: str="") -> None:
        ''' '''
        self.__alphabet = alphabet              # Алфавит
        self.create_key(key)                    # Ключ для шифровки/дешифровки
        self.__m = m                            # Заданный порядок (количество букв векторе, на которые будет разбит текст)
        self.__N = self.__alphabet.size         # Мощность алфавита
        
    def create_key(self, key: str) -> None:
        ''' Инициализация ключа'''
        if key == "":                           
            self.__key = self.rand_key()
        else:
            self.__key = key

    def rand_matrix_key(self, str: str) -> np.array:
        count = 0
        while count < len(str):
            slice = str[count:count+self.__m]
            count += self.__m
        for let, index in enumerate(self.__alphabet):
            count += 1

            if count == self.__m:
                count = 0
                

    @property
    def key(self):
        pass

    @key.setter
    def key():
        pass

    # Y = (X * A + B) mod N
    def encrypt():
        ''' Зашифровать текст '''
        pass
    # X = ((Y - B) * A^-1) mod N
    def decrypt():
        ''' Расшифровать текст '''
        pass


str = "abcd"

print(str[0:4])
print(str[2:5])