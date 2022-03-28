



using ProjectRSA;



var scrumbler = new CipherRSA(3, 7);


var number = 5;
var encrypted = scrumbler.Encrypt(number);
var decrypted = scrumbler.Decrypt(encrypted);

Console.WriteLine(number);
Console.WriteLine(encrypted);
Console.WriteLine(decrypted);

//Console.WriteLine(Math.Pow(22, 23));

//var a = Math.Pow(22, 23);
//var c  = a % 143;
//Console.WriteLine(a);
//Console.WriteLine(c);