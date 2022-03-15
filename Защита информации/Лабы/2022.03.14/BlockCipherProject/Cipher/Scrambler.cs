using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockCipherProject.Cipher
{
    internal class Scrambler
    {
        private readonly PBlock _firstPBlock;
        private readonly PBlock _secondPBlock;
        private readonly List<SBlock> _SBlocks = new();
        public Scrambler() { 
            
            _firstPBlock = new PBlock();
            _secondPBlock = new PBlock();
            for (int i = 0; i < 8; i++) {
                _SBlocks.Add(new SBlock());
            }
        }

        public string Encrypt(string text) {
            var bytes = GetBytes32(text);

            var encrypted = _firstPBlock.Encrypt(bytes);

            string result = "";
            for (int i = 0; i < encrypted.Length; i += 4) {
                result += _SBlocks[i/8].Encrypt(encrypted.Substring(i, 4));
            }
            encrypted = result;
            encrypted = _secondPBlock.Encrypt(encrypted);
            return GetStringFromBytes32(encrypted);
        }
        public string Decrypt(string text) {
            var bytes = GetBytes32(text);
            var decrypted = _secondPBlock.Decrypt(bytes);

            string result = "";
            for (int i = 0; i < decrypted.Length; i += 4)
            {
                var item = decrypted.Substring(i, 4);
                result += _SBlocks[i / 8].Decrypt(item);
            }
            decrypted = result;
            decrypted = _firstPBlock.Decrypt(decrypted);
            return GetStringFromBytes32(decrypted);
        }
        private string GetBytes32(string text) {
            var bytes = Encoding.Unicode.GetBytes(text);
            string result = "";
            foreach (var b in bytes) {
                result += Convert.ToString(b, 2).PadLeft(8, '0');
            }
            return result;
        }
        private string GetStringFromBytes32(string text) {
            if (text.Length != 32) throw new ArgumentException();
            var arr = new byte[4];
            for (int i = 0; i < text.Length; i += 8) {
                var strByte = text.Substring(i, 8);
                arr[i/8] = Convert.ToByte(strByte, 2);
            }
            return Encoding.Unicode.GetString(arr);
        }
    }
}
