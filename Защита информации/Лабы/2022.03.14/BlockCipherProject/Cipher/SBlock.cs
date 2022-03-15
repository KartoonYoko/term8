using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockCipherProject.Cipher
{


    internal class SBlock
    {
        /// <summary>
        /// Таблица перестановок.
        /// </summary>
        private List<int> _table = new();
        public SBlock() {

            for (int i = 0; i < 16; i++) {
                _table.Add(-1);
            }

            // инициализируем таблицу перестановок случайным образом
            var count = 0;
            while (count < _table.Count) {
                var randNum = new Random().Next(0, 16);
                try
                {
                    // проверяем есть ли данно число в таблице
                    _ = _table.First(x => x == randNum);
                }
                catch {
                    _table[count] = (byte)randNum;
                    count++;
                }
                
            }

        }



        /// <summary>
        /// Шифрует четырехбайтовое предстваление.
        /// </summary>
        /// <param name="text">Текст, содержащий четырехбитовое число</param>
        /// <returns></returns>
        public string Encrypt(string text) {
            if (text.Length != 4) throw new ArgumentException();
            var tenDigit = Convert.ToInt32(text, 2);
            return Convert.ToString(_table[tenDigit], 2).PadLeft(4, '0');
        }

        public string Decrypt(string text) {
            if (text.Length != 4) throw new ArgumentException();
            var tenDigit = Convert.ToInt32(text, 2);
            var permNum = _table.FindIndex(x => x == tenDigit);
            return Convert.ToString(permNum, 2).PadLeft(4, '0');
        }
    }
}
