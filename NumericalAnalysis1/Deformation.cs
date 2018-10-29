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
        // Size of grid
        private int nPatchX;
        private int nPatchY;
        private int szGrid;
        private int[] deltasGrids;
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

        public void SetAttribute(Image img, int gridSize)
        {
            // Set internal data matrix to image.
            szImg = new Size(img.Width, img.Height);
            szGrid = gridSize;
            nPatchX = (int)Math.Ceiling((double)szImg.Width / gridSize);
            nPatchY = (int)Math.Ceiling((double)szImg.Height / gridSize);
            deltasGrids = new int[nPatchX * nPatchY];
        }

        private void Step(Point2i ptSrc, Point2d ptDst, Point2i ptBegin, Point2i ptEnd)
        {
            int idxControlX = (int)Math.Round((double)ptBegin.X / szGrid);
            int idxControlY = (int)Math.Round((double)ptBegin.Y / szGrid);
            if (idxControlX < 0) idxControlX = 0;
            if (idxControlY < 0) idxControlX = 0;
            if (idxControlX >= nPatchX - 1) idxControlX = nPatchX - 2;
            if (idxControlY >= nPatchY - 1) idxControlY = nPatchY - 2;
            int deltaX = ptEnd.X - idxControlX * szGrid;
            int deltaY = ptEnd.Y - idxControlY * szGrid;
            if (deltaX < -szGrid) deltaX = 1 - szGrid;
            if (deltaY < -szGrid) deltaY = 1 - szGrid;
            if (deltaX > szGrid) deltaX = szGrid - 1;
            if (deltaY > szGrid) deltaY = szGrid - 1;
            

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
