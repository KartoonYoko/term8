

using System.Text;
using StreamCipherProject.Scrambling;


Scrambler scrambler = new();

var text = scrambler.Encrypt("my text qwelmsdfkm p a;sldka;lkd;K:LK:LKSADK:SLA");

Console.WriteLine(text);
Console.WriteLine(scrambler.Decrypt(text));