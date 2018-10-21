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
    enum InterpolateMethod{ Nearest, Bilinear, Bicubic };
    class Interpolation
    {
        private static Interpolation instance = null;
        // ARGB make up a 32bit data
        int[,] matSrcData = null;
        int[,] matDstData = null;
        private Size szImg;

        private Interpolation() { }
        public static Interpolation GetInstance()
        {
            if (instance == null) instance = new Interpolation();
            return instance;
        }
        public void SetSourceImg(Image img)
        {
            szImg = new Size(img.Width, img.Height);
            matSrcData = new int[img.Width, img.Height];
            matDstData = new int[img.Width, img.Height];
            
            for (int i = 0; i < img.Width; ++i)
            {
                for(int j = 0; j < img.Height; ++j)
                {
                    matDstData[i, j] = matSrcData[i, j] = ((Bitmap)img).GetPixel(i, j).ToArgb();
                }
            }
            
        }
        public Image GetDestinationImg()
        {
            Bitmap result = new Bitmap(szImg.Width, szImg.Height);
            for (int i = 0; i < result.Width; ++i)
            {
                for (int j = 0; j < result.Height; ++j)
                {
                    result.SetPixel(i, j, Color.FromArgb(matDstData[i, j]));
                }
            }
            return result;
        }
        public void Step(Point2d ptSrc, Point2i ptDst, InterpolateMethod method)
        {
            
        }
        private int Bicubic(int[,] ROI, Point2d ptSrc)
        {
            // u = frac(x) v = frac(y)
            double u = ptSrc.X - (int)ptSrc.X, v = ptSrc.Y - (int)ptSrc.Y;
            double a = 0.0, r = 0.0, g = 0.0, b = 0.0;
            // Perform ABC'
            for(int i = 0; i < 4; ++i)
            {
                for(int j = 0; j < 4; ++j)
                {
                    double coefficient = CubicBase(u + 1 - i) * CubicBase(v + 1 - j);
                    a += coefficient * ((ROI[i, j] >> 24) & 0x000000ff);
                    r += coefficient * ((ROI[i, j] >> 16) & 0x000000ff);
                    g += coefficient * ((ROI[i, j] >> 8) & 0x000000ff);
                    b += coefficient * ((ROI[i, j] >> 0) & 0x000000ff);
                }
            }
            // Restrict to [0, 255]
            a = Restrict(a);
            r = Restrict(r);
            g = Restrict(g);
            b = Restrict(b);
            // Compose
            return ((int)a << 24) | ((int)r << 16) | ((int)g << 8) | ((int)b << 0);
        }
        private double CubicBase(double x)
        {
            // Base cubic fucntion
            x = x > 0 ? x : -x;
            if (x < 1) return 1.0 - x * x * (2.0 - x);
            else if (x < 2) return 4.0 - x * (8.0 + x * (5.0 - x));
            else return 0.0;
        }
        private bool CheckIndexValid(int X, int Y)
        {
            // utils to check in range
            return X >= 0 && X < szImg.Width && Y >= 0 && Y < szImg.Height;
        }
        private int[,] GetNeighbourhood4(Point2d ptSrc)
        {
            // For bilinear 
            int X = (int)ptSrc.X, Y = (int)ptSrc.Y;
            int[,] result = new int[2, 2];
            for(int i = 0; i < 2; ++i)
            {
                for(int j = 0; j < 2; ++j)
                {
                    unchecked
                    {
                        if (CheckIndexValid(X + i, Y + j)) result[i, j] = matDstData[X + i, Y + j];
                        else result[i, j] = (int)0xff000000;
                    }
                    
                }
            }
            return result;
        }
        private int[,] GetNeighbourhood16(Point2d ptSrc)
        {
            // For bicubic
            int X = (int)ptSrc.X, Y = (int)ptSrc.Y;
            int[,] result = new int[4, 4];
            for (int i = 0; i < 4; ++i)
            {
                for (int j = 0; j < 4; ++j)
                {
                    unchecked
                    {
                        if (CheckIndexValid(X - 1 + i, Y - 1 + j)) result[i, j] = matDstData[X - 1 + i, Y - 1 + j];
                        else result[i, j] = (int)0xff000000;
                    }
                    
                }
            }
            return result;
        }
        private int GetNeighbourhood1(Point2d ptSrc)
        {
            // For nearest
            int X = (int)ptSrc.X, Y = (int)ptSrc.Y;
            int result;
            unchecked
            {
                if (CheckIndexValid(X, Y)) result = matDstData[X, Y];
                else result = (int)0xff000000;
            }
            return result;
        }
        private int Restrict(double x)
        {
            x = x < 255.0 ? x : 255.0;
            x = x > 0.0 ? x : 0.0;
            return (int)x;
        }
    }
}
