using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HillCipherProject
{
    /// <summary>
    /// Сущность для работы с матрицами.
    /// </summary>
    internal class Matrix
    {
        private List<List<double>> _matrix;

        public Matrix(List<List<double>> m) { 
            _matrix = m;
        }
        /// <summary>
        /// Создает нулевую матрицу размера row x col.
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        public Matrix(int row, int col) {
            _matrix = new();
            for (int i = 0; i < row; i++) {
                _matrix.Add(new());
            }
            foreach (var item in _matrix) {
                for (int i = 0; i < col; i++) { 
                    item.Add(0);
                }
            }
        }

        /// <summary>
        /// Возвращает количесвто строк в матрице.
        /// </summary>
        /// <returns></returns>
        public int GetRowCount() => _matrix.Count;
        /// <summary>
        /// Возвращает количество столбцов в матрице.
        /// </summary>
        /// <returns></returns>
        public int GetColumnCount() {
            if (_matrix.Count == 0) return 0;
            return _matrix[0].Count;
        }
        /// <summary>
        /// Сохраняет элемент в матрице. Отсчет строк/столбцов начинается с нуля.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        public void SetItem(double item, int row, int col) { 
            _matrix[row][col] = item;
        }
        public double GetItem(int row, int col) => _matrix[row][col];

        /// <summary>
        /// Транспонировать матрицу.
        /// </summary>
        public void Transpose() {
            List<List<double>> buffer = new();
            for (int i = 0; i < GetRowCount(); i++)
            {
                buffer.Add(new());
                for (int j = 0; j < GetColumnCount(); j++)
                {
                    buffer[i].Add(_matrix[j][i]);
                }
            }
            _matrix = buffer;
        }

        /// <summary>
        /// Найти детерминант.
        /// </summary>
        /// <returns></returns>
        public double FindDeterminant() {
            double sum = 0;
            if (GetColumnCount() == 1) return _matrix[0][0];

            for (int i = 0; i < GetColumnCount(); i++) {
                Matrix buf = new(GetRowCount() - 1, GetColumnCount() - 1);

                if ((i != 0) && (i != GetColumnCount() - 1))
                {
                    for (int i1 = 0; i1 < buf.GetRowCount(); i1++)
                    {
                        for (int j1 = 0; j1 < i; j1++)
                        {
                            buf.SetItem(GetItem(i1 + 1, j1), i1, j1);
                        }
                    }
                    for (int i1 = 0; i1 < buf.GetRowCount(); i1++)
                    {
                        for (int j1 = i; j1 < buf.GetColumnCount(); j1++)
                        {
                            buf.SetItem(GetItem(i1 + 1, j1 + 1), i1, j1);
                        }
                    }
                }
                else if (i != 0)
                {
                    for (int i1 = 0; i1 < buf.GetRowCount(); i1++)
                    {
                        for (int j1 = 0; j1 < buf.GetColumnCount(); j1++)
                        {
                            buf.SetItem(GetItem(i1 + 1, j1), i1, j1);
                        }
                    }
                }
                else
                {
                    for (int i1 = 0; i1 < buf.GetRowCount(); i1++)
                    {
                        for (int j1 = 0; j1 < buf.GetColumnCount(); j1++)
                        {
                            buf.SetItem(GetItem(i1 + 1, j1 + 1), i1, j1);
                        }
                    }
                }

                sum += _matrix[0][i] * Math.Pow(-1, i) * buf.FindDeterminant();
            }

            return sum;
        }
        /// <summary>
        /// Возвращает обратную матрицу к текущей.
        /// </summary>
        /// <returns></returns>
        public Matrix GetInverseMatrix() {
            if (FindDeterminant() == 0) throw new Exception("Determinant equals zero.");
            if (GetColumnCount() != GetRowCount()) throw new Exception("Can not get inverse matrix to not square one.");

            var result = new Matrix(GetRowCount(), GetColumnCount());
            for (int i = 0; i < GetRowCount(); i++)
                for (int j = 0; j < GetRowCount(); j++)
                {
                    result.SetItem(FindAlgebraicAdjunct(i, j), i, j);
                }
            result.Transpose();

            result *= (1 / FindDeterminant());
            return result;
        }

        /// <summary>
        /// Умножение матрицы на число.
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        public static Matrix operator *(Matrix matrix, double num) {
            var result = new Matrix(matrix.GetRowCount(), matrix.GetColumnCount());
		    for (int i = 0; i< matrix.GetRowCount(); i++)
			    for (int j = 0; j< matrix.GetColumnCount(); j++) {
				    result.SetItem(matrix.GetItem(i, j) * num, i, j);
			    }
		    return result;
	    }
        /// <summary>
        /// Умножение матриц.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        /// <exception cref="Exception">Количество строк левой матрицы должны быть равны количеству столбцов правой.</exception>
        public static Matrix operator *(Matrix left, Matrix right) {
            if (left.GetColumnCount() != right.GetRowCount()) throw new Exception("Row count of left matrix doen't equal to column of left matrix.");
            
            var result = new Matrix(left.GetRowCount(), right.GetColumnCount());

            for (int i = 0; i < result.GetRowCount(); i++) {
                for (int j = 0; j < result.GetColumnCount(); j++) {
                    for (int k = 0; k < left.GetColumnCount(); k++) {
                        var item = result.GetItem(i, j) + (left.GetItem(i, k) * right.GetItem(k, j));
                        result.SetItem(item, i, j);
                    }
                }
            }

            return result;
        }
        public static Matrix operator /(Matrix matrix, double num) {
            var result = new Matrix(matrix.GetRowCount(), matrix.GetColumnCount());
            for (int i = 0; i < matrix.GetRowCount(); i++)
                for (int j = 0; j < matrix.GetColumnCount(); j++)
                {
                    result.SetItem(matrix.GetItem(i, j) / num, i, j);
                }
            return result;
        }
        public static Matrix Mod(Matrix matrix, double num) {
            var result = new Matrix(matrix.GetRowCount(), matrix.GetColumnCount());
            for (int i = 0; i < matrix.GetRowCount(); i++)
                for (int j = 0; j < matrix.GetColumnCount(); j++)
                {
                    var item = Mod(matrix.GetItem(i, j), num);
                    result.SetItem(item, i, j);
                }
            return result;
        }
        /// <summary>
        /// Остаток от деления двух чисел по правилам математики.
        /// Всегда возвращает неотрицательный остаток.
        /// </summary>
        /// <param name="x1">Делимое.</param>
        /// <param name="x2">Делитель.</param>
        /// <returns></returns>
        private static double Mod(double x1, double x2)
        {
            if (x1 >= 0) return x1 % x2;

            // неполное частное 
            var quotient = Math.Abs(x1 / x2);
            var incompleteQuotient = -1 * (int)Math.Ceiling(quotient);
            // остаток = число - делитель * неполное_частное
            return x1 - x2 * incompleteQuotient;
        }

        public void Print() {
            foreach (var row in _matrix) {
                foreach (var col in row) { 
                    Console.Write(col + " ");
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Нахождение алгебраического дополнения для элемента row x col.
        /// </summary>
        /// <returns></returns>
        private double FindAlgebraicAdjunct(int row, int col) {
            var buf = new Matrix(GetRowCount() - 1, GetColumnCount() - 1);
            double result;
            int i1 = 0, j1 = 0;
            for (int i = 0; i < GetRowCount(); i++)
                for (int j = 0; j < GetRowCount(); j++)
                {
                    if ((i != row) && (j != col))
                    {
                        buf.SetItem(GetItem(i, j), i1, j1);
                        if (j1 == GetColumnCount() - 2)
                        {
                            j1 = 0;
                            if (i1 == GetRowCount() -2) i1 = 0; else i1++;
                        }
                        else j1++;
                    }
                }
            result = buf.FindDeterminant() * Math.Pow(-1, (row + col));
            return result;
        }

    }
}
