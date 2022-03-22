
using System.Text;

namespace StreamCipherProject.Scrambling
{
    internal class Scrambler
    {
        /// <summary>
        /// Случайный ключ.
        /// </summary>
        private List<byte> _key = new();

        public Scrambler() {

            KeyGenerator keyGenerator = new();
            _key = keyGenerator.Generate();
            
        }

        /// <summary>
        /// Изменить ключ.
        /// </summary>
        public void RefreshKey() {
            KeyGenerator keyGenerator = new();
            _key = keyGenerator.Generate();
        }

        public string Encrypt(string text) {
            var gamma = CreateGamma(text);
            var bytes = GetBytesFromString(text);
            List<byte> encryptedBytes = new();
            for (int i = 0; i < bytes.Count; i++) {
                encryptedBytes.Add((byte)(bytes[i] ^ gamma[i]));
            }
            string encrypted = GetStringFromBytes(encryptedBytes);
            return encrypted;   
        }
        public string Decrypt(string text) {
            var gamma = CreateGamma(text);
            var bytes = GetBytesFromString(text);
            List<byte> decryptedBytes = new();
            for (int i = 0; i < bytes.Count; i++)
            {
                decryptedBytes.Add((byte)(bytes[i] ^ gamma[i]));
            }
            string decrypted = GetStringFromBytes(decryptedBytes);
            return decrypted;
        }


        private List<byte> GetBytesFromString(string text)
        {
            var result = Encoding.Unicode.GetBytes(text).ToList();
            return result.Where(x => x != 0).ToList();
        }
        private string GetStringFromBytes(List<byte> bytes)
        {
            List<byte> temp = new();
            bytes.ForEach(x => {
                temp.Add(x);
                temp.Add(0);
            });
            return Encoding.Unicode.GetString(temp.ToArray());
        }
        private List<byte> CreateGamma(string text) {
            RandomGeneratorBuilder rgd = new(_key);
            var generator = rgd.GetGenerator();
            List<byte> _gamma = new();
            List<byte> generatorTemp = new(generator);
            int i = 0, j = 0;
            for (int k = 0; k < text.Length; k++) {
                i = (i + 1) % 256;
                j = (j + generatorTemp[i]) % 256;
                (generatorTemp[i], generatorTemp[j]) = (generatorTemp[j], generatorTemp[i]);
                var t = (generatorTemp[i] + generatorTemp[j]) % 256;
                _gamma.Add((byte)t);
            }
            return _gamma;
        }
    }
}
