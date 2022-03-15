using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockCipherProject.Cipher
{
    internal class PBlock
    {
        private List<int> _table = new();

        public PBlock() {

            for (int i = 0; i < 32; i++) { 
                _table.Add(-1);
            }


            // инициализируем таблицу перестановок случайным образом
            var count = 0;
            while (count < _table.Count)
            {
                var randNum = new Random().Next(0, 32);
                try
                {
                    // проверяем есть ли данно число в таблице
                    _ = _table.First(x => x == randNum);
                }
                catch
                {
                    _table[count] = (byte)randNum;
                    count++;
                }

            }

            foreach (int i in _table) { 
                Console.Write(i + " ");
            }
            Console.WriteLine();
        }

        public string Encrypt(string text) {
            if (text.Length != 32) throw new ArgumentException();
            string result = "";

            foreach (var num in _table) { 
                result += text[num];
            }
            return result;
        }

        public string Decrypt(string text) {
            if (text.Length != 32) throw new ArgumentException();
            string result = "";

            for (int i = 0; i < text.Length; i++) { 
                var index = _table.FindIndex(x => x == i);
                result += text[index];
            }
            return result;
        }
    }
}
