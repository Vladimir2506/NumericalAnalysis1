using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenCvSharp;
using System.Drawing;
using System.Drawing.Imaging;
using Point2i = OpenCvSharp.Point;
using Size = System.Drawing.Size;

namespace NumericalAnalysis1
{
    public enum InterpolateMethod{ Nearest, Bilinear, Bicubic };
    public class Interpolation
    {
        // Source image and Destination Image must be in the same size.
        // Singleton.
        private static Interpolation instance = null;
        // ARGB make up a 32bit data.
        private int[] matSrcData = null;
        // Size of image
        private Size szImg;

        private Interpolation() { }
        public static Interpolation GetInstance()
        {
            if (instance == null) instance = new Interpolation();
            return instance;
        }
        public void SetSourceImg(Image img)
        {
            // Set internal data matrix to image.
            szImg = new Size(img.Width, img.Height);
            matSrcData = new int[img.Width * img.Height];
            BitmapData srcData = ((Bitmap)img).LockBits(new Rectangle(0, 0, img.Width, img.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            unsafe
            {
                fixed (int* pMat = &matSrcData[0])
                {
                    int* pImg = (int*)srcData.Scan0;
                    for (int j = 0; j < srcData.Height; ++j)
                    {
                        for (int i = 0; i < srcData.Width; ++i)
                        {
                            *(pMat + i + j * srcData.Width) = *(pImg + i + j * srcData.Width);
                        }
                    }
                }
                    
            }
            ((Bitmap)img).UnlockBits(srcData);
        }
        public int Step(Point2d ptSrc, InterpolateMethod method)
        {
            // One step to calculate the interpolated value.
            switch(method)
            {
                case InterpolateMethod.Bicubic:
                    return Bicubic(GetNeighbourhood16(ptSrc), ptSrc);
                case InterpolateMethod.Bilinear:
                    return Bilinear(GetNeighbourhood4(ptSrc), ptSrc);
                case InterpolateMethod.Nearest:
                    return GetNeighbourhood1(ptSrc);
                default:
                    throw new ArgumentException("Invalid method.");
            }
        }
        private int Bilinear(int[] ROI, Point2d ptSrc)
        {
            // u = frac(x) v = frac(y)
            double u = ptSrc.X - (int)ptSrc.X, v = ptSrc.Y - (int)ptSrc.Y;
            double a = 0.0, r = 0.0, g = 0.0, b = 0.0;
            // Perform simple
            for(int j = 0; j < 2; ++j)
            {
                for(int i = 0; i < 2; ++i)
                {
                    int data = ROI[i + j * 2];
                    double coefficient = LinearBasis(i, u) * LinearBasis(j, v);
                    a += coefficient * ((data >> 24) & 0x000000ff);
                    r += coefficient * ((data >> 16) & 0x000000ff);
                    g += coefficient * ((data >> 8) & 0x000000ff);
                    b += coefficient * ((data >> 0) & 0x000000ff);
                }
            }
            // Restrict & Compose
            return (Restrict(a) << 24) | (Restrict(r) << 16) | (Restrict(g) << 8) | (Restrict(b) << 0);
        }
        private double LinearBasis(int k, double x)
        {
            // Linear base function
            switch(k)
            {
                case 0:
                    return 1.0 - x;
                case 1:
                    return x;
                default:
                    throw new ArgumentException("K must be 0 or 1.");
            }
        }
        private int Bicubic(int[] ROI, Point2d ptSrc)
        {
            // u = frac(x) v = frac(y)
            double u = ptSrc.X - (int)ptSrc.X, v = ptSrc.Y - (int)ptSrc.Y;
            double a = 0.0, r = 0.0, g = 0.0, b = 0.0;
            // Perform ABC'
            for(int j = 0; j < 4; ++j)
            {
                for(int i = 0; i < 4; ++i)
                {
                    int data = ROI[i + j * 4];
                    double coefficient = CubicBasis(u + 1 - i) * CubicBasis(v + 1 - j);
                    a += coefficient * ((data >> 24) & 0x000000ff);
                    r += coefficient * ((data >> 16) & 0x000000ff);
                    g += coefficient * ((data >> 8) & 0x000000ff);
                    b += coefficient * ((data >> 0) & 0x000000ff);
                }
            }
            // Restrict & Compose
            return (Restrict(a) << 24) | (Restrict(r) << 16) | (Restrict(g) << 8) | (Restrict(b) << 0);
        }
        private double CubicBasis(double x)
        {
            // Basis cubic fucntion
            x = x > 0 ? x : -x;
            if (x < 1) return 1.0 - x * x * (2.0 - x);
            else if (x < 2) return 4.0 - x * (8.0 - x * (5.0 - x));
            else return 0.0;
        }
        private bool CheckIndexValid(int X, int Y)
        {
            // utils to check in range
            return X >= 0 && X < szImg.Width && Y >= 0 && Y < szImg.Height;
        }
        private int[] GetNeighbourhood4(Point2d ptSrc)
        {
            // For bilinear 
            int X = (int)ptSrc.X, Y = (int)ptSrc.Y;
            int[] result = new int[4];
            for (int j = 0; j < 2; ++j)
            {
                for (int i = 0; i < 2; ++i)
                {
                    unchecked
                    {
                        if (CheckIndexValid(X + i, Y + j)) result[i + j * 2] = matSrcData[(X + i) + (Y + j) * szImg.Width];
                        else result[i + j * 2] = (int)0xff000000;
                    }

                }
            }
            return result;
        }
        private int[] GetNeighbourhood16(Point2d ptSrc)
        {
            // For bicubic
            int X = (int)ptSrc.X, Y = (int)ptSrc.Y;
            int[] result = new int[16];
            for (int j = 0; j < 4; ++j)
            {
                for (int i = 0; i < 4; ++i)
                {
                    unchecked
                    {
                        if (CheckIndexValid(X - 1 + i, Y - 1 + j)) result[i + j * 4] = matSrcData[(X - 1 + i) + (Y - 1 + j) * szImg.Width];
                        else result[i + j * 4] = (int)0xff000000;
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
                if (CheckIndexValid(X, Y)) result = matSrcData[X + Y * szImg.Width];
                else result = (int)0xff000000;
            }
            return result;
        }
        private int Restrict(double x)
        {
            // Restrict to one byte
            x = x < 255.0 ? x : 255.0;
            x = x > 0.0 ? x : 0.0;
            return (int)x;
        }
    }
}
