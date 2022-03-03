


namespace CaesarCipher
{
    /// <summary>
    /// Шифратор для шифра Цезаря.
    /// В качестве алфавита использует буквы русского алфавита и пробел.
    /// Также содержит функцию расшифровки.
    /// </summary>
    public class Scrambler
    {
        /// <summary>
        /// Алфавит шифратора.
        /// </summary>
        private readonly List<AlphabetItem> _alphabet = new();
        /// <summary>
        /// Сдвиг.
        /// </summary>
        private readonly int _offset;
        private readonly EncryptionDataRepository _encryptionDataRepository;

        public Scrambler(EncryptionDataRepository repository) {
            foreach (var item in repository.Alphabet) {
                _alphabet.Add(new(item));
            }

            _encryptionDataRepository = repository;
            _offset = repository.Offset;
            AlphabetItem.SetCounter(-1);
            foreach (AlphabetItem item in _alphabet) {
                GetMatchedLetter(item);
            }
        }

        /// <summary>
        /// Зашифровать данные.
        /// </summary>
        public void Encrypt() {
            string encryptedText = "";
            string text = _encryptionDataRepository.Text;
            for (int i = 0; i < text.Length; i++) {
                var item = _alphabet.Find(let => let.Letter == text[i]);
                encryptedText += item.MatchedLetter;
            }

            _encryptionDataRepository.EncryptedText = encryptedText;
        }
        /// <summary>
        /// Расшифровать данные.
        /// Используется обратное преобразование.
        /// </summary>
        public void Decrypt() {
            string decryptText = "";
            string text = _encryptionDataRepository.EncryptedText;
            // преобразовываем алфавит с помощью обратного преобразования
            foreach (AlphabetItem item in _alphabet) {
                GetInverseLetter(item);
            }

            for (int i = 0; i < text.Length; i++) {
                var item = _alphabet.Find(let => let.MatchedLetter == text[i]);
                decryptText += item.Letter;
            }
            _encryptionDataRepository.DecryptedText = decryptText;
        }

        public EncryptionDataRepository GetEncryptData() => _encryptionDataRepository;

        /// <summary>
        /// Остаток от деления двух чисел по правилам математики.
        /// Всегда возвращает неотрицательный остаток.
        /// </summary>
        /// <param name="x1">Делимое.</param>
        /// <param name="x2">Делитель.</param>
        /// <returns></returns>
        private static int Mod(int x1, int x2) {
            if (x1 >= 0) return x1 % x2;

            // неполное частное 
            Decimal quotient = Math.Abs((Decimal)x1 / x2);
            var incompleteQuotient = -1 * (int)Math.Ceiling(quotient);
            // остаток = число - делитель * неполное_частное
            return x1 - x2 * incompleteQuotient;
        }
        /// <summary>
        /// Найти позицию нового символа в алфавите по смещению.
        /// </summary>
        /// <param name="item"></param>
        private void GetMatchedLetter(AlphabetItem item) {
            // y = (x + b) mod N
            var dividend = item.Number + _offset;
            if (dividend > _alphabet.Count - 1) {
                dividend %= (_alphabet.Count - 1);
                dividend -= 1;
            }

            var divisor = _alphabet.Count;
            var newNumber = dividend % divisor;
            item.MatchedNumber = newNumber;
            item.MatchedLetter = _alphabet.First(let => let.Number == newNumber).Letter;
        }
        /// <summary>
        /// Обратное преобразование.
        /// Найти позицию старого символа в алфавите.
        /// </summary>
        /// <param name="item"></param>
        private void GetInverseLetter(AlphabetItem item) {
            // x = (y - b) mod N
            var y = (int)item.MatchedNumber - _offset;
            var x = Mod(y, _alphabet.Count);
            item.Number = x;
        }

        private class AlphabetItem {
            public char Letter;
            public int Number;
            public char? MatchedLetter;
            public int? MatchedNumber;

            private static int _count = -1;
            public AlphabetItem(char let, char? matchedLet = null) {
                _count++;
                Letter = let;
                Number = _count;
                MatchedLetter = matchedLet;                
            }

            public static void SetCounter(int count) => _count = count;
        }

    }

    
}
