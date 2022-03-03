

using System.Text;
using TranspositionCipher;


var scrambler = new Scrambler("ОИУЫНТК ");

Console.WriteLine("Файл со всеми перестановками: " + scrambler.FileName);

var encryptedText = scrambler.Encrypt(2, "У НИКИТЫ ОКУНИ");
Console.WriteLine("Пример шифрования текста: " + encryptedText);
var decriptedText = scrambler.Decrypt(2, encryptedText);
Console.WriteLine("Пример дешифрования текста: " + decriptedText);
var premutation = scrambler.GetPermutation(2);
Console.WriteLine("Строка перебора, с помощью которой совершалось шифрование/дешифрование: '" + premutation + "'");

var fileName = "DecriptedTextByBruteForceHacking.txt";
using FileStream fs = new FileStream(fileName, FileMode.Create);
Console.WriteLine("Ожидание взлома перебором...");
var text = scrambler.Break(encryptedText);
var body = Encoding.UTF8.GetBytes(text);
fs.Write(body, 0, body.Length);
fileName = Path.GetFullPath(fileName);
Console.WriteLine("Взлом завершился. Результат находится по пути: " + fileName);