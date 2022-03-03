




namespace CaesarCipher
{
    /// <summary>
    /// Хранилище данных шифрования.
    /// </summary>
    public class EncryptionDataRepository
    {
        /// <summary>
        /// Исходный текст для шифрования.
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// Зашифрованный текст.
        /// </summary>
        public string EncryptedText { get; set; }
        /// <summary>
        /// Расшифрованный текст.
        /// </summary>
        public string DecryptedText { get; set; }
        /// <summary>
        /// Алфавит, используемый шифратором.
        /// </summary>
        public List<char> Alphabet { get; set; } = new();
        /// <summary>
        /// Смещение для шифратора.
        /// </summary>
        public int Offset { get; set; }
    }
}
