


namespace CaesarCipher
{
    /// <summary>
    /// Криптоаналитик для взлома перебором всех сдвигов.
    /// </summary>
    public class Cryptoanalyst
    {
        private readonly EncryptionDataRepository _repositry;
        public Cryptoanalyst(EncryptionDataRepository repository) {
            _repositry = repository;
        }

        public void BreakCipher() {
            var alphabet = _repositry.Alphabet;
            Console.WriteLine();
            Console.WriteLine("Работа криптоаналитика: ");
            for (int i = 1; i < alphabet.Count - 1; i++) {
                Console.Write(i + ":\t");
                MakeSortOut(i);
            }
        }
        private void MakeSortOut(int offset) {
            _repositry.Offset = offset;
            var scrambler = new Scrambler(_repositry);
            scrambler.Decrypt();
            var data = scrambler.GetEncryptData();
            Console.WriteLine(data.DecryptedText);
        }
    }
}
