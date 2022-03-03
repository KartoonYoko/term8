
using CaesarCipher;

// Данные для шифрования
var repository = new EncryptionDataRepository()
{
    Offset = 3,
    Alphabet = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ ".ToList(),
    Text = "МАМА МЫЛА РАМУ"
};

var scrambler = new Scrambler(repository);

scrambler.Encrypt();
scrambler.Decrypt();

var printer = new EncryptionDataHandler(scrambler.GetEncryptData());
printer.PrintData();

var cryptoanalyst = new Cryptoanalyst(repository);

cryptoanalyst.BreakCipher();

