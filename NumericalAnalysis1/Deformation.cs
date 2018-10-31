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
        // Size of image.
        private Size szImg;
        // Attributes of grid.
        private int nControlX = 0;
        private int nControlY = 0;
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
            // Determine the control grids.
            szImg = new Size(img.Width, img.Height);
            szGrid = gridSize;
            nControlX = (int)Math.Ceiling((double)szImg.Width / gridSize);
            nControlY = (int)Math.Ceiling((double)szImg.Height / gridSize);
            deltasGrids = new int[nControlX * nControlY];
        }

        public Point2i[] Displace(Point2i ptBegin, Point2i ptEnd)
        {
            // Determine the control point displacement and the region to deform.
            int idxControlX = (int)Math.Round((double)ptBegin.X / szGrid);
            int idxControlY = (int)Math.Round((double)ptBegin.Y / szGrid);
            // Make sure idxControl is in the map.
            if (idxControlX < 0) idxControlX = 0;
            if (idxControlY < 0) idxControlX = 0;
            // nControl - 1 may be out of the map.
            if (idxControlX >= nControlX - 1) idxControlX = nControlX - 2;
            if (idxControlY >= nControlY - 1) idxControlY = nControlY - 2;
            int deltaX = ptEnd.X - idxControlX * szGrid;
            int deltaY = ptEnd.Y - idxControlY * szGrid;
            // Regularize the displacement into 1 grid.
            if (deltaX < -szGrid) deltaX = 1 - szGrid;
            if (deltaY < -szGrid) deltaY = 1 - szGrid;
            if (deltaX > szGrid) deltaX = szGrid - 1;
            if (deltaY > szGrid) deltaY = szGrid - 1;
            // Store delta.
            deltasGrids[idxControlX + idxControlY * nControlX] = ((deltaY << 16) & 0xffff) | ((deltaX << 0) & 0xffff);
            // Determine region of the map.
            Point2i ptLeftUp = new Point2i(Math.Max(idxControlX - 2, 0) * szGrid, Math.Max(idxControlY - 2, 0) * szGrid);
            Point2i ptRightDown = new Point2i(Math.Min(idxControlX + 2, nControlX) * szGrid, Math.Min(idxControlY + 2, nControlY) * szGrid);
            return new Point2i[] { ptLeftUp, ptRightDown };
        }

        public Point2d Step(Point2i ptSrc)
        {
            // The atomic operation from one point to another controlled by grids nearby.
            Point2d ptDst = new Point2d();
            Point2i[] controlIdxs = new Point2i[4];
            // Compute the nearest control point index.
            controlIdxs[1].X = ptSrc.X / szGrid;
            controlIdxs[1].Y = ptSrc.Y / szGrid;
            // Expand to the 16-elems neighbourhood indices.
            for (int k = 0; k < 4; ++k)
            {
                controlIdxs[k].X = controlIdxs[1].X + (k - 1);
                controlIdxs[k].Y = controlIdxs[1].Y + (k - 1);
            }
            for (int k = 0; k < 4; ++k)
            {
                // Avoid Index out of range.
                RestrictIdxToGrid(controlIdxs[k]);
            }
            double u = (double)ptSrc.X / szGrid - controlIdxs[1].X;
            double v = (double)ptSrc.Y / szGrid - controlIdxs[1].Y;
            unsafe
            {
                fixed(int* pGridBase = &deltasGrids[0])
                {
                    for (int j = 0; j < 3; ++j)
                    {
                        for (int i = 0; i < 3; ++i)
                        {
                            // Get control points displacement and sum together by coefficient
                            int* pGrid = pGridBase + controlIdxs[i].X + controlIdxs[j].Y * nControlX;
                            double coefficient = BsplineBasis3(i, u) * BsplineBasis3(j, v);
                            ptDst.X += coefficient * ((*pGrid >> 0) & 0xffff);
                            ptDst.Y += coefficient * ((*pGrid >> 16) & 0xffff);
                        }
                    }
                }
            }
            // Inverse approximation.
            ptDst.X = ptSrc.X - ptDst.X;
            ptDst.Y = ptSrc.Y - ptDst.Y;
            return ptDst;
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

        private void RestrictIdxToGrid(Point2i ptIdx)
        {
            ref int idxX = ref ptIdx.X;
            ref int idxY = ref ptIdx.Y;
            if (idxX < 0) idxX = 0;
            if (idxY < 0) idxY = 0;
            if (idxX >= nControlX) idxX = nControlX - 1;
            if (idxY >= nControlY) idxY = nControlY - 1;   
        }
    }

    public class DeformTPS
    {
        // Num of control points.
        private int nControlPoints = 0;
        // Matrix of TPS equation.
        private Vec2d[] weights = null;
        private Point2i[] targets = null;
        // Singleton.
        private static DeformTPS instance = null;
        private DeformTPS() { }

        public static DeformTPS GetInstance()
        {
            if (instance == null)
            {
                instance = new DeformTPS();
            }
            return instance;
        }

        public void SetAttribute(int points)
        {
            nControlPoints = points;
            targets = new Point2i[nControlPoints];
        }
        
        public Point2d Step(Point2i ptSrc)
        {
            // The atomic operation of TPS.
            Point2d ptDst = new Point2d();
            // Implement the sum operation
            // where weights = [w1, ..., wn, a1, ax, ay]
            for(int k = 0; k < nControlPoints; ++k)
            {
                double basis = TPSRadialBasis(ptSrc, targets[k]);
                ptDst.X += weights[k].Item0 * basis;
                ptDst.Y += weights[k].Item1 * basis; 
            }
            // ax ay a1
            ptDst.X += weights[nControlPoints + 1].Item0 * ptSrc.X + weights[nControlPoints + 2].Item0 * ptSrc.Y + weights[nControlPoints].Item0;
            ptDst.Y += weights[nControlPoints + 1].Item1 * ptSrc.X + weights[nControlPoints + 2].Item1 * ptSrc.Y + weights[nControlPoints].Item1;
            return ptDst;
        }

        public void Estimate(Point2i[] srcs, Point2i[] dsts)
        {
            // Get the TPS deform funtion.
            if (srcs.Length != nControlPoints || dsts.Length != nControlPoints)
            {
                throw new ArgumentException("Control points do not match!");
            }
            weights = new Vec2d[nControlPoints + 3];
            srcs.CopyTo(targets, 0);
            // Solve equation.
            int maxRows = nControlPoints + 3, maxCols = nControlPoints + 5;
            double[,] augmented = new double[maxRows, maxCols];
            // Prepare.
            for(int j = 0; j < nControlPoints; ++j)
            {
                for(int i = j + 1; i < nControlPoints; ++i)
                {
                    augmented[j, i] = TPSRadialBasis(targets[j], targets[i]);
                    augmented[i, j] = augmented[j, i];
                }
            }
            for (int k = 0; k < nControlPoints; ++k)
            {
                augmented[k, nControlPoints] = 1;
                augmented[nControlPoints, k] = 1;
                augmented[k, nControlPoints + 1] = targets[k].X;
                augmented[nControlPoints + 1, k] = targets[k].X;
                augmented[k, nControlPoints + 2] = targets[k].Y;
                augmented[nControlPoints + 2, k] = targets[k].Y;
            }
            for(int k = 0; k < nControlPoints; ++k)
            {
                augmented[k, nControlPoints + 3] = dsts[k].X;
                augmented[k, nControlPoints + 4] = dsts[k].Y;
            }
            // Solve.
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
            // Get result.
            for (int j = 0; j < maxRows; ++j)
            {
                weights[j].Item0 = augmented[j, nControlPoints + 3];
                weights[j].Item1 = augmented[j, nControlPoints + 4];
            }
            // utils.Print("C:\\Users\\dell1\\Desktop\\a.csv", augmented);
        }

        private double TPSRadialBasis(Point2i pt1, Point2i pt2)
        {
            // Radial basis function of TPS.
            double result = 0.0;
            double dx = pt1.X - pt2.X, dy = pt1.Y - pt2.Y;
            double r2 = dx * dx + dy * dy;
            if (r2 < 1e-12) result = 0.0;
            else result = r2 * Math.Log(r2);
            return result;
        }

        private void SwapRows(double[,] mat, int r1, int r2)
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
