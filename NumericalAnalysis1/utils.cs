using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace NumericalAnalysis1
{
    public static class Utils
    {
        public static void Print(string path, double[,] mat)
        {
            FileStream fs = new FileStream(path, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            for (int i = 0; i < mat.GetLength(0); i++)
            {
                for (int j = 0; j < mat.GetLength(1); j++)
                {
                    sw.Write(mat[i, j]);
                    sw.Write(',');
                }
                sw.Write("\r\n");
            }
            sw.Close();
            fs.Close();
        }
        public static void SolveLinearEqn(double[,] augmented)
        {
            int maxRows = augmented.GetLength(0), maxCols = augmented.GetLength(1);
            // Step 1: Upper triangle matrix.
            for (int i = 0; i < maxRows; ++i)
            {
                for (int k = i + 1; Math.Abs(augmented[i, i]) < 1e-12; ++k)
                {
                    SwapRows(augmented, i, k);
                }
                for (int j = i + 1; j < maxRows; ++j)
                {
                    double factor = augmented[j, i] / augmented[i, i];
                    for (int k = i; k < maxCols; ++k)
                    {
                        augmented[j, k] -= factor * augmented[i, k];
                    }
                }
            }
            // Step 2: Diagnol line normalization.
            for (int i = 0; i < maxRows; ++i)
            {
                for (int j = i + 1; j < maxCols; ++j)
                {
                    augmented[i, j] /= augmented[i, i];
                }
                augmented[i, i] = 1.0;
            }
            // Step 3: Identity matrix.
            for (int i = maxRows - 2; i >= 0; --i)
            {
                for (int j = i; j >= 0; --j)
                {
                    double factor = augmented[j, i + 1];
                    for (int k = i + 1; k < maxCols; ++k)
                    {
                        augmented[j, k] -= factor * augmented[i + 1, k];
                    }
                }
            }
        }
        public static void SwapRows(double[,] mat, int r1, int r2)
        {
            if (r1 == r2) return;
            int cols = mat.GetLength(1);
            for (int k = 0; k < cols; ++k)
            {
                double tmp = mat[r1, k];
                mat[r1, k] = mat[r2, k];
                mat[r2, k] = tmp;
            }
        }
    }
}
