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
    public enum DeformMethod { Water,Bspline,TPSpline };
    public class Deformation
    {
        // ARGB make up a 32bit data.
        private int[,] matSrcData = null;
        // Size of image
        private Size szImg;
        // Singleton.
        private static Deformation instance = null;
        private Deformation() { }

        public static Deformation GetInstance()
        {
            if (instance == null)
            {
                instance = new Deformation();
            }
            return instance;
        }

        public void SetSourceImg(Image img)
        {
            // Set internal data matrix to image.
            szImg = new Size(img.Width, img.Height);
            matSrcData = new int[img.Width, img.Height];

            for (int i = 0; i < img.Width; ++i)
            {
                for (int j = 0; j < img.Height; ++j)
                {
                    matSrcData[i, j] = ((Bitmap)img).GetPixel(i, j).ToArgb();
                }
            }

        }

        private Point2i ApplyBspline(Point2i[] ptsControl,int order, double t)
        {
            Point2i result = new Point2i();
            BsplineBasis basis;
            double x = 0.0, y = 0.0;
            switch (order)
            {
                case 1:
                    basis = BsplineBasis1;
                    break;
                case 2:
                    basis = BsplineBasis2;
                    break;
                case 3:
                    basis = BsplineBasis3;
                    break;
                default:
                    throw new ArgumentException("Invalid order.");
            }
            for (int i = 0; i < order + 1; ++i)
            {
                x += basis(i, t) * ptsControl[i].X;
                y += basis(i, t) * ptsControl[i].Y;
            }
            result.X = (int)x;
            result.Y = (int)y;
            return result;
        }

        private delegate double BsplineBasis(int k, double t);

        private double BsplineBasis3(int k, double t)
        {
            // Order 3 Bspline basis function
            if (t > 1.0 || t < 0.0) return 0.0;
            switch (k)
            {
                case 0:
                    return (1.0 - t) * (1.0 - t) * (1.0 - t) / 6.0;
                case 1:
                    return t * t * (0.5 * t - 1.0) + 4.0 / 6.0;
                case 2:
                    return t * (t * (-0.5 * t + 0.5) + 0.5) + 1.0 / 6.0;
                case 3:
                    return t * t * t / 6.0;
                default:
                    return 0.0;
            }
        }
        private double BsplineBasis2(int k, double t)
        {
            // Order 2 Bspline basis function
            if (t > 1.0 || t < 0.0) return 0.0;
            switch (k)
            {
                case 0:
                    return (1.0 - t) * (1.0 - t) / 2.0;
                case 1:
                    return t * (1.0 - t) + 1.0 / 2.0;
                case 2:
                    return t * t / 2.0;
                default:
                    return 0;
            }
        }
        private double BsplineBasis1(int k, double t)
        {
            // Order 1 Bspline basis function
            if (t > 1.0 || t < 0.0) return 0.0;
            switch (k)
            {
                case 0:
                    return 1.0 - t;
                case 1:
                    return t;
                default:
                    return 0.0;
            }
        }
    }
    public class Solver
    {
    }

}
