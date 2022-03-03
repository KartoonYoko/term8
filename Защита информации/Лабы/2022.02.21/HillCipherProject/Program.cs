



using HillCipherProject;


var scrambler = new HillCipher("атиня ", 2, "атня");

var text = scrambler.Encrypt("на");
Console.WriteLine(text);
Console.WriteLine(text.Length);
Console.WriteLine(scrambler.Decrypt(text));

// на =[3 0]
// атня = [ [0 1] [3 4]]
// перемножаем
/**
 * [0 3] = ан
 * 
 * получаем обратную
 * inverseKey = [ [-1.3 0.33] [1 0] ]
 * 
 * умножаем зашифрованную строку на обратный ключ
 */
Matrix na = new(new() { 
    new() { 3, 0 }
});
Matrix key = new(new() { 
    new() { 0, 1},
    new() { 3, 4 }
});
Matrix inverseKey = new(new() { 
    new() { -1.3, 0.33 },
    new() { 1, 0 }
});
Matrix an = new(new() { 
    new() { 0, 3 }
});

var c = inverseKey * an;
c.Print();
