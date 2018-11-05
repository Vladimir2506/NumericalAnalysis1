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

        private Alignment() { }

        public static Alignment GetInstance()
        {
            if(instance == null)
            {
                instance = new Alignment();
            }
            return instance;
        }

        public void Reset()
        {
            tsfmMatrix = new double[3, 2];
        }

        public void EstimateLeastSquare(Point2d[] srcs, Point2d[] dsts)
        {
            tsfmMatrix = new double[3, 2];
            if (srcs.Length != dsts.Length)
            {
                throw new ArgumentException("Points do not match!");
            }
            int rows = srcs.Length;
            // Preparation
            double[,] A = new double[rows, 3], b = new double[rows, 2];
            for(int i = 0; i < rows; ++i)
            {
                A[i, 0] = srcs[i].X;
                A[i, 1] = srcs[i].Y;
                A[i, 2] = 1;
                b[i, 0] = dsts[i].X;
                b[i, 1] = dsts[i].Y;
            }
            // 1. Get A'
            double[,] AT = Utils.Transpose(A);
            // 2. Get A'A and A'b
            double[,] ATA = Utils.MatMul(AT, A);
            double[,] ATb = Utils.MatMul(AT, b);
            // 3. Solve (A'A)x = (A')b
            double[,] augmented = new double[3, 5];
            for(int i = 0; i < 3; ++i)
            {
                augmented[i, 0] = ATA[i, 0];
                augmented[i, 1] = ATA[i, 1];
                augmented[i, 2] = ATA[i, 2];
                augmented[i, 3] = ATb[i, 0];
                augmented[i, 4] = ATb[i, 1];
            }
            Utils.SolveLinearEqn(augmented);
            // 4. Get result
            for(int i = 0; i < 3; ++i)
            {
                tsfmMatrix[i, 0] = augmented[i, 3];
                tsfmMatrix[i, 1] = augmented[i, 4];
            }
            
        }

        public Point2d[] ApplyTransform(Point2d[] points)
        {
            Point2d[] results = new Point2d[points.Length];
            for(int k = 0; k < points.Length; ++k)
            {
                double[,] result = new double[1, 3];
                result[0, 0] = points[k].X;
                result[0, 1] = points[k].Y;
                result[0, 2] = 1;
                result = Utils.MatMul(result, tsfmMatrix);
                results[k].X = result[0, 0];
                results[k].Y = result[0, 1];
            }
            return results;
        }
    }
}
