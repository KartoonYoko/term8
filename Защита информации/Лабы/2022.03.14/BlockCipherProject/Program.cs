


using System.Text;
using BlockCipherProject.Cipher;

Console.OutputEncoding = Encoding.Unicode;
var scrambler = new Scrambler();


var text = scrambler.Encrypt("qw");
Console.WriteLine("Encrypred text: " + text);
Console.WriteLine("Decrypted text: " + scrambler.Decrypt(text));
text = scrambler.Encrypt("qq");
Console.WriteLine("Encrypred text: " + text);
Console.WriteLine("Decrypted text: " + scrambler.Decrypt(text));


