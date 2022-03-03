

using System.IO;
using System.Text.Json;
using System.Text;

namespace TranspositionCipher
{
    internal class Scrambler
    {

        private readonly string _fileName;
        private readonly char[] _alphabet;
        private readonly Dictionary<int, string> _permutations;
        public Scrambler(string alphabet, string filename = "Permutation.txt") {
            _alphabet = alphabet.ToArray();

            // создаем перестановки и записываем в файл
            var initilizerPermutations = new Permutations(alphabet);
            var permutations = initilizerPermutations.GetPermutationsSortList();
            _permutations = new();
            for (int i = 0; i < permutations.Count; i++)
            {
                _permutations[i] = permutations[i];
            }

            _fileName = Path.GetFullPath(filename);
            using FileStream stream = new(_fileName, FileMode.Create);

            for(int i = 0; i< _permutations.Count; i++) {
                var str = i + ")\t" + _permutations[i] + "\n";
                byte[] body = Encoding.UTF8.GetBytes(str);
                stream.Write(body, 0, body.Length);
            }
        }
        /// <summary>
        /// Зашифровать
        /// </summary>
        /// <param name="num">Номер перестановки</param>
        /// <param name="text">Текст для шифрования</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public string Encrypt(int num, string text) {
            if (num < 0 || num > _permutations.Count - 1) throw new ArgumentException("Uncorrect number of permutation.");

            _permutations.TryGetValue(num, out var permutation);
            var arr = permutation.ToArray();
            string res = "";
            foreach (var ch in text) {
                var index = Array.IndexOf(_alphabet, ch);
                res += arr[index];
            }

            return res;
            
        }
        /// <summary>
        /// Расшифровать
        /// </summary>
        /// <param name="num">Номер перестановки</param>
        /// <param name="text">Текст для расшифрования</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public string Decrypt(int num, string text) {
            if (num < 0 || num > _permutations.Count - 1) throw new ArgumentException("Uncorrect number of permutation.");

            _permutations.TryGetValue(num, out var permutation);
            string res = "";
            var arr = permutation.ToArray();
            foreach (var ch in text) {
                var index = Array.IndexOf(arr, ch);
                res += _alphabet[index];
            }
            return res;

        }
        /// <summary>
        /// Взлом перебором
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public string Break(string text) {
            string res = "";
            for (int i = 0; i < _permutations.Count; i++) { 
                res += i + ")\t" + Decrypt(i, text) + "\n";
            }

            return res;
        }
        public string GetPermutation(int num) => _permutations[num];
        public string FileName => _fileName;
    }
}
