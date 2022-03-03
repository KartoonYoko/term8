using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HillCipherProject
{
    /// <summary>
    /// Экземпляр для шифровки/дешифровки методом Хилла.
    /// </summary>
    internal class HillCipher
    {
        private readonly List<char> _alphabet = new();
        /// <summary>
        /// Количество символов в кусочке для шифрования.
        /// </summary>
        private int _offset;
        /// <summary>
        /// Модуль/мощность алфавита.
        /// </summary>
        private readonly int _n;
        /// <summary>
        /// Список ключей.
        /// </summary>
        private readonly Dictionary<string, Matrix> _keys = new();
        private string _currentKey;

        public HillCipher(string alphabet, int offset, string key = "") {
            _alphabet = alphabet.ToList();
            _offset = offset;
            _n = _alphabet.Count;
            if (key == "") CreateRandomKey();
            else CreateKeyFromString(key);
        }
        /// <summary>
        /// Зашифровать текст.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public string Encrypt(string text) {
            string encryptedText = "";
            for (int i = 0; i < text.Length; i += _offset) {
                Matrix c = new(_offset, 1);

                // получим срез для шифрования
                string slice;
                if (i + _offset > text.Length) slice = text.Substring(i, text.Length - 1 - i);
                else slice = text.Substring(i, _offset);
                // составим матрицу из среза текста
                for (int j = 0; j < _offset; j++) {
                    // если отрезок меньше чем нужно - добавляем последний символ из алфавита
                    if (slice.Length < _offset) {
                        for (int q = 0; q < _offset; q++) {
                            c.SetItem(_alphabet.Count - 1, q, 0);
                        }
                    } 
                    else {
                        var index = _alphabet.FindIndex(let => let == text[i + j]);
                        c.SetItem(index, j, 0);
                    }
                }

                var item = _keys[_currentKey] * c;
                var res = Matrix.Mod(item, _n);

                for (int k = 0; k < res.GetRowCount(); k++) {
                    encryptedText += _alphabet[(int)res.GetItem(k, 0)];
                }
            }

            return encryptedText;
        }
        public string Decrypt(string text) {
            string decryptedText = "";

            var keyInverseMatrix = _keys[_currentKey].GetInverseMatrix();
            for (int i = 0; i < text.Length; i += _offset) {
                Matrix c = new(_offset, 1);

                // получим срез для шифрования
                string slice = text.Substring(i, _offset);

                // составим матрицу из среза текста
                for (int j = 0; j < _offset; j++)
                {
                    var index = _alphabet.FindIndex(let => let == text[i + j]);
                    c.SetItem(index, j, 0);
                }

                var item = keyInverseMatrix * c;
                var res = Matrix.Mod(item, _n);

                for (int k = 0; k < res.GetRowCount(); k++)
                {
                    decryptedText += _alphabet[(int)res.GetItem(k, 0)];
                }
            }

            return decryptedText;
        }
        private void CreateRandomKey() {
            string key = "";
            for (int i = 0; i < _offset; i++) {
                while (true) {
                    Random rand = new();
                    var index = rand.Next(0, _alphabet.Count);
                    var ch = _alphabet[index];
                    if (key.IndexOf(ch) != -1) continue;
                    key += ch;
                    break;
                }
            }

            CreateKeyFromString(key);
        }
        /// <summary>
        /// Создать ключ-матрицу из строки.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private void CreateKeyFromString(string key) {
            if (key.Length != _offset * _offset) throw new Exception("Key should have lenght of " + _offset * _offset);
            _currentKey = key;
            _keys.TryGetValue(key, out var matrix);
            if (matrix is not null) return;

            int count = 0;
            Matrix keyMatrix = new(_offset, _offset);
            for (int i = 0; i < _offset; i++) {
                for (int j = 0; j < _offset; j++) {
                    var ch = key[count];
                    count++;

                    var num = _alphabet.FindIndex(let => let == ch);
                    keyMatrix.SetItem(num, i, j);                    
                }
            }

            _keys[key] = keyMatrix;
        }

        
    }
}
