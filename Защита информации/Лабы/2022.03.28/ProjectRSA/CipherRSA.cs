



namespace ProjectRSA
{
    internal struct KeyRSA{
        /// <summary>
        /// 
        /// </summary>
        public int E { get; set; }
        /// <summary>
        /// Произведение простых чисел
        /// </summary>
        public int N { get; set; }

        public KeyRSA(int e, int n) { E = e; N = n; }
    }
    internal class CipherRSA
    {

        /// <summary>
        /// Открытый ключ
        /// </summary>
        private readonly KeyRSA _pk;
        /// <summary>
        /// Закрытый ключ
        /// </summary>
        private readonly KeyRSA _sk;
        /// <summary>
        /// Максимальное сообщение
        /// </summary>
        private readonly int _n;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p">Простое число</param>
        /// <param name="q">Простое число</param>
        public CipherRSA(int p, int q) {
            var n = p * q; _n = n;
            var eilerFunction = (q - 1) * (p - 1);
            var e = new Random().Next(3, n - 1);
            // НОД
            var gcd = MathHelp.GCD(e, eilerFunction);
            while (gcd != 1) {
                e = new Random().Next(3, n - 1);
                gcd = MathHelp.GCD(e, eilerFunction);
            };
            //e = 13; // TODO delete
            var d = MathHelp.Inverse(e, eilerFunction);
            if (d is null) throw new Exception("");

            _pk = new(e, n);
            _sk = new((int)d, n);
        }

        public int Encrypt(int message) {
            if (message > _n - 1) throw new Exception("Message should be less than " + (_n - 1));

            var c = Math.Pow(message, _pk.E) % _pk.N;
            return (int)c;
        }

        public int Decrypt(int c) { 
            var m = Math.Pow(c, _sk.E) % _sk.N;

            return (int)m;
        }

    }
}
