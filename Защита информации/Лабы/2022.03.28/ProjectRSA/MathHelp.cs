


namespace ProjectRSA
{
    internal static class MathHelp
    {

        private static int Min(int x, int y)
        {
            return x < y ? x : y;
        }

        private static int Max(int x, int y)
        {
            return x > y ? x : y;
        }
        /// <summary>
        /// Наибольщий общий делитель
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static int GCD(int a, int b)
        {
            if (a == 0)
            {
                return b;
            }
            else
            {
                var min = Min(a, b);
                var max = Max(a, b);
                //вызываем метод с новыми аргументами
                return GCD(max - min, min);
            }
        }

        /// <summary>
        /// Обратное по модулю число
        /// </summary>
        /// <returns></returns>
        public static int? Inverse(int a, int n) {
            if (a % n == 0)
            {
                return null;
            }
            else {
                var result = 1;
                while (true) {
                    if ((result * a) % n == 1) { 
                        return result;
                    }
                    result++;
                }
            }
        }

    }
}
