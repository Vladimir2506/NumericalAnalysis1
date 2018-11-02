using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;
using System.Drawing;
using Point2i = OpenCvSharp.Point;
using Size = System.Drawing.Size;

namespace NumericalAnalysis1
{
    public class Alignment
    {
        private double[,] tsfmMatrix = null;

        //Singleton.
        private static Alignment instance = null;

        private Alignment()
        {
            tsfmMatrix = new double[3, 3];
        }

        public static Alignment GetInstance()
        {
            if(instance == null)
            {
                instance = new Alignment();
            }
            return instance;
        }

        public void EstimateLeastSquare(Point2i[] srcs, Point2i[] dsts)
        {
            if(srcs.Length != dsts.Length)
            {
                throw new ArgumentException("Points do not match!");
            }
            int rows = srcs.Length;
            // Preparation
            double[,] A = new double[rows, 3], b = new double[rows, 3];
            for(int i = 0; i < rows; ++i)
            {
                A[i, 0] = srcs[i].X;
                A[i, 1] = srcs[i].Y;
                A[i, 2] = 1;
                b[i, 0] = dsts[i].X;
                b[i, 1] = dsts[i].Y;
                b[i, 2] = 1;
            }
            // 1. Get A'
            double[,] AT = Utils.Transpose(A);
            // 2. Get A'A and A'b
            double[,] ATA = Utils.MatMul(AT, A);
            double[,] ATb = Utils.MatMul(AT, b);
            // 3. Solve (A'A)x = (A')b
            double[,] augmented = new double[3, 6];
            for(int i = 0; i < 3; ++i)
            {
                augmented[i, 0] = ATA[i, 0];
                augmented[i, 1] = ATA[i, 1];
                augmented[i, 2] = ATA[i, 2];
                augmented[i, 3] = ATb[i, 0];
                augmented[i, 4] = ATb[i, 1];
                augmented[i, 5] = ATb[i, 2];
            }
            Utils.SolveLinearEqn(augmented);
            // 4. Get result
            for(int i = 0; i < 3; ++i)
            {
                tsfmMatrix[i, 0] = 0;// augmented[i, 3];
                tsfmMatrix[i, 1] = 0;// augmented[i, 4];
                tsfmMatrix[i, 2] = augmented[i, 5];
            }
            tsfmMatrix[0, 0] = 1;
            tsfmMatrix[1, 1] = 1;
        }

        public Point2i[] ApplyTransform(Point2i[] points)
        {
            Point2i[] results = new Point2i[points.Length];
            for(int k = 0; k < points.Length; ++k)
            {
                double[,] result = new double[1, 3];
                result[0, 0] = points[k].X;
                result[0, 1] = points[k].Y;
                result[0, 2] = 1;
                result = Utils.MatMul(result, tsfmMatrix);
                results[k].X = (int)result[0, 0];
                results[k].Y = (int)result[0, 1];
            }
            return results;
        }
    }
}
