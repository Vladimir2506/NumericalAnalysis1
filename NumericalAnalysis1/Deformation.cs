using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Imaging;
using OpenCvSharp;
using System.Drawing;
using Point2i = OpenCvSharp.Point;
using Size = System.Drawing.Size;

namespace NumericalAnalysis1
{
    public enum DeformMethod { Water,Bspline,TPSpline };
    public class DeformBspline
    {
        // Size of image
        private Size szImg;
        private Size szGrid;
        // Singleton.
        private static DeformBspline instance = null;
        private DeformBspline() { }

        public static DeformBspline GetInstance()
        {
            if (instance == null)
            {
                instance = new DeformBspline();
            }
            return instance;
        }

        public void SetAttribute(Image img, Size gridSize)
        {
            // Set internal data matrix to image.
            szImg = new Size(img.Width, img.Height);
            szGrid = gridSize;
        }

        private void Step(Point2i ptSrc)
        {

        }

        private Point2i Bspline3(Point2i[] ptsControl, double t)
        {
            Point2i result = new Point2i();
            double x = 0.0, y = 0.0;
            for (int i = 0; i < 4; ++i)
            {
                x += BsplineBasis3(i, t) * ptsControl[i].X;
                y += BsplineBasis3(i, t) * ptsControl[i].Y;
            }
            result.X = (int)x;
            result.Y = (int)y;
            return result;
        }

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
       
    }

}
