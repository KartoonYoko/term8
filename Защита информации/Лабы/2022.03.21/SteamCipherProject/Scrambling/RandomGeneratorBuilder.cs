using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamCipherProject.Scrambling
{
    internal class RandomGeneratorBuilder
    {

        private readonly List<byte> _key;
        private readonly List<byte> _t;
        public RandomGeneratorBuilder(List<byte> key) {
            _key = key;
            _t = new(key);
        }

        public List<byte> GetGenerator() {
            byte j = 0;
            for (byte i = byte.MinValue; i < byte.MaxValue; i++)
            {
                j = (byte)((j + _t[i] + _key[i]) % 256);
                (_t[i], _t[j]) = (_t[j], _t[i]);
            }

            return _t;
        }
    }
}
