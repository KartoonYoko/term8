


namespace CaesarCipher
{
    public  class EncryptionDataHandler
    {
        private readonly EncryptionDataRepository _repository;
        public EncryptionDataHandler(EncryptionDataRepository repository) {
            _repository = repository;
        }

        public void PrintData() {
            Console.Write("Алфавит: ");
            Console.WriteLine(new String(_repository.Alphabet.ToArray()));

            Console.Write("Изначальный текст: ");
            Console.WriteLine(_repository.Text);

            Console.Write("Зашифрованный текст: ");
            Console.WriteLine(_repository.EncryptedText);

            Console.Write("Расшифрованный текст: ");
            Console.WriteLine(_repository.DecryptedText);
        }
    }
}
