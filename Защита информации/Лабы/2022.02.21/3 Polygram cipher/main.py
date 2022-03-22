# Полиграммный шифр Хилла
# Бизяев К.К.
# ИВТ-18

from array import array
import numpy as np
import string

def check_common_divisors(alphabet:str, key)-> bool:
    a = len(alphabet)
    b = int(get_key_det(key))
    for i in range(1, min(a, b)+1):
        if a % i == b % i == 0:
            return False
    return True

def get_key_inv(key):
    return np.linalg.inv(key)

def get_key_det(key):
    return np.linalg.det(key)

def convert_vector_to_message(alphabet:str, message_vector):
    message_str = ""
    for s in message_vector.flatten():
        message_str += alphabet[s]
    return message_str

def get_user_message():
    message = input("Введите ваше сообщение: ").upper()
    return message

def get_random_encryption_key(alphabet: str):    
    module = len(alphabet)
    while True:
        key = np.random.randint(0, module, (3, 3))        
        key_det = get_key_det(key)
        if key_det != 0 and check_common_divisors(alphabet, key) and get_multiplicative_inverse(key_det, module) != 0:
            return key     

def get_decryption_key(alphabet:str, key):
    module = len(alphabet)    
    key_det = get_key_det(key)
    key_adjoint = get_adjoint_matrix(key, key_det, module)    
    inverse = get_multiplicative_inverse(key_det, module)    
    decryption_key = inverse * key_adjoint % module
    return decryption_key

def get_adjoint_matrix(key, key_det, module):
    # Обратная матрица = присоединённая матрица/определитель матрицы
    key_iverted = get_key_inv(key)
    key_adjoint = key_iverted * key_det % module        
    key_adjoint = np.around(key_adjoint)
    key_adjoint = key_adjoint.astype(int)
    return key_adjoint

# Обратное число по модулю
def get_multiplicative_inverse(number, module):    
    inverse = 0
    while(inverse < module):
        result = (number * inverse) % module
        if result == 1:
            break
        else:
            inverse += 1
            if inverse == module:            
                return 0 # нет обратного числа
    return inverse
    
def encrypt(alphabet, key, message):   
    module = len(alphabet)
    return np.dot(key, message) % module    

def decrypt(alphabet, decryption_key, encrypted_message):
    module = len(alphabet)
    return np.dot(decryption_key, encrypted_message) % module    

def convert_message_to_vector(alphabet:str, message:str, key):    
    module = len(alphabet)
    row_len = key.shape[0]

    temp_list1 = [0,0,0]
    temp_list2 = []
    for i in range(len(message)):
        j = i % row_len
        if (i > 0 and i % row_len == 0):
            temp_list2.append(temp_list1)
            temp_list1 = [0, 0, 0]
        temp_list1[j] = alphabet.find(message[i])
    temp_list2.append(temp_list1)

    message_vector = np.array(temp_list2)
    message_vector = np.reshape(message_vector,(message_vector.shape[1],message_vector.shape[0]))
    message_vector = message_vector % module
    return message_vector

def print_separator():
    print("-" * 50)    

def main():

    ALPHABET = string.ascii_uppercase # + " " # можно прибавить пробел к алфавиту
              
    key = np.array([[6,24,1],[13,16,10],[20,17,15]], dtype=int)  # пример из википедии    
    key = get_random_encryption_key(ALPHABET)
    MESSAGE = 'ACT' # пример из википедии
    MESSAGE = get_user_message()

    key_str = convert_vector_to_message(ALPHABET, key)
    key_inv = get_key_inv(key)    
    decryption_key = get_decryption_key(ALPHABET, key)              
    message_vector = convert_message_to_vector(ALPHABET, MESSAGE, key)
    encrypted_message_vector = encrypt(ALPHABET, key, message_vector)    
    encrypted_message = convert_vector_to_message(ALPHABET, encrypted_message_vector) 
    decrypted_message_vector = decrypt(ALPHABET, decryption_key, encrypted_message_vector)    
    decrypted_message = convert_vector_to_message(ALPHABET, decrypted_message_vector) 

    print_separator()
    print(f"Алфавит: {ALPHABET}")
    print_separator()
    print(f"Ключ шифрования (строка): [{key_str}]")
    print(f"Ключ шифрования (матрица):\n {key}")        
    print_separator()
    print(f"Ключ расшифрования (строка): [{convert_vector_to_message(ALPHABET, decryption_key)}]")
    print(f"Ключ расшифрования (матрица):\n {decryption_key}")     
    print_separator()
    print(f"Сообщение (строка): {MESSAGE}")
    print(f"Сообщение (матрица):\n {message_vector}")
    print_separator()
    print(f"Зашифрованное сообщение (строка): [{encrypted_message}]")
    print(f"Зашифрованное сообщение (матрица):\n {encrypted_message_vector}")                
    print_separator()
    print(f"Расшифрованное сообщение (строка): [{decrypted_message}]")
    print(f"Расшифрованное сообщение (матрица):\n {decrypted_message_vector}")    
    print_separator()

    print(np.dot(key, decryption_key))
    print(np.dot(key, get_key_inv(key)))

if __name__ == "__main__":
    main()
