using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamCipherProject.Scrambling
{
    internal class KeyGenerator
    {
        private readonly List<byte> _t = new();

        public KeyGenerator() {
            // инициализация от 0 до 255
            //for (byte i = byte.MinValue; i <= byte.MaxValue; i++) {
            //    _t.Add(i);
            //}
            byte i = 0;
            while (i != 255) {
                _t.Add(i);
                i++;
            }
            _t.Add(255);
        }
        public List<byte> Generate() {

            var n = _t.Count * 2;
            var rand = new Random();
            for (int q = 0; q < n; q++) { 
                int i = rand.Next(0, _t.Count);
                int j = rand.Next(0, _t.Count);
                if ((i != j) && (_t[i] != _t[j])){
                    (_t[j], _t[i]) = (_t[i], _t[j]);
                }
            }

            return _t;
        }
    }
}
