using System;
using OpenCvSharp;
using System.Drawing;
using Point2i = OpenCvSharp.Point;
using Size = System.Drawing.Size;

namespace NumericalAnalysis1
{
    public enum DeformMethod { BSpline,TPSpline };
    public class DeformBspline
    {
        // Size of image.
        private Size szImg;
        // Attributes of grid.
        private int nControlX = 0;
        private int nControlY = 0;
        private int szGrid;
        private double[,] deltasGrids;
        // Exponential decay based adjustment.
        private const double decayRate = 0.1;
        private const int maxSteps = 5;

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

        public void Displace(Image img, Point2d[] srcs, Point2d[] dsts, int gridSize)
        {
            // Get the optimal Bspline deform grids.
            szImg = new Size(img.Width, img.Height);
            if (srcs.Length != dsts.Length)
            {
                throw new ArgumentException("Points do not match.");
            }
            int controlPts = srcs.Length;
            // Construct displacment-delta grids.
            szGrid = gridSize;
            nControlX = (int)Math.Ceiling((double)szImg.Width / gridSize);
            nControlY = (int)Math.Ceiling((double)szImg.Height / gridSize);
            deltasGrids = new double[nControlX * nControlY, 2];
            Vec2d[] targetDisplacemet = new Vec2d[controlPts];
            for (int i = 0; i < controlPts; ++i)
            {
                targetDisplacemet[i].Item0 = dsts[i].X - srcs[i].X;
                targetDisplacemet[i].Item1 = dsts[i].Y - srcs[i].Y;
            }
            // Iterative displacement with decay.
            for (int iter = 0; iter < maxSteps; ++iter)
            {
                for (int k = 0; k < controlPts; ++k)
                {
                    Point2d pt = srcs[k];
                    int x0 = (int)(pt.X / szGrid);
                    int y0 = (int)(pt.Y / szGrid);
                    double u = pt.X / szGrid - x0;
                    double v = pt.Y / szGrid - y0;
                    for (int j = -1; j < 3; ++j)
                    {
                        for (int i = -1; i < 3; ++i)
                        {
                            int x = x0 + i;
                            int y = y0 + j;
                            if (x > nControlX - 1 || x < 0 || y > nControlY - 1 || y < 0) continue;
                            double coe = BsplineBasis3(j, v) * BsplineBasis3(i, u);
                            double decay = Math.Pow(decayRate, iter);
                            double dx = coe * targetDisplacemet[k].Item0 * decay;
                            double dy = coe * targetDisplacemet[k].Item1 * decay;
                            if (dx < -szGrid) dx = -szGrid;
                            if (dy < -szGrid) dy = -szGrid;
                            if (dx > szGrid - 1) dx = szGrid - 1;
                            if (dy > szGrid - 1) dy = szGrid - 1;
                            deltasGrids[y * nControlX + x, 0] += dx;
                            deltasGrids[y * nControlX + x, 1] += dy;
                        }
                    }
                }
            }
            
        }

        public Point2d Step(Point2d ptSrc)
        {
            // The atomic operation from one point to another controlled by grids nearby.
            Point2d ptDst = new Point2d();
            Point2i[] controlIdxs = new Point2i[4];
            // Compute the nearest control point index.
            controlIdxs[1].X = (int)ptSrc.X / szGrid;
            controlIdxs[1].Y = (int)ptSrc.Y / szGrid;
            // Expand to the 16-elems neighbourhood indices.
            for (int k = 0; k < 4; ++k)
            {
                controlIdxs[k].X = controlIdxs[1].X + (k - 1);
                controlIdxs[k].Y = controlIdxs[1].Y + (k - 1);
            }
            for (int k = 0; k < 4; ++k)
            {
                // Avoid Index out of range.
                ref int idxX = ref controlIdxs[k].X;
                ref int idxY = ref controlIdxs[k].Y;
                if (idxX < 0) idxX = 0;
                if (idxY < 0) idxY = 0;
                if (idxX >= nControlX) idxX = nControlX - 1;
                if (idxY >= nControlY) idxY = nControlY - 1;
            }
            double u = ptSrc.X / szGrid - controlIdxs[1].X;
            double v = ptSrc.Y / szGrid - controlIdxs[1].Y;
            for (int j = 0; j < 4; ++j)
            {
                for (int i = 0; i < 4; ++i)
                {
                    // Get control points displacement and sum together by coefficient.
                    double dx = deltasGrids[controlIdxs[i].X + controlIdxs[j].Y * nControlX, 0];
                    double dy = deltasGrids[controlIdxs[i].X + controlIdxs[j].Y * nControlX, 1];
                    double coefficient = BsplineBasis3(i, u) * BsplineBasis3(j, v);
                    ptDst.X += coefficient * dx;
                    ptDst.Y += coefficient * dy;
                }
            }
            ptDst.X = ptSrc.X - ptDst.X;
            ptDst.Y = ptSrc.Y - ptDst.Y;
            return ptDst;
        }

        private double BsplineBasis3(int k, double t)
        {
            // 3rd Order BSpline basis function.
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

    public class DeformTPS
    {
        // Num of control points.
        private int controlPts = 0;
        // Matrix of TPS equation.
        private Vec2d[] weights = null;
        private Point2d[] targets = null;
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
        
        public Point2d Step(Point2d ptSrc)
        {
            // The atomic operation of TPS.
            Point2d ptDst = new Point2d();
            // Implement the sum operation,
            // where weights = [w1, ..., wn, a1, ax, ay].
            for(int k = 0; k < controlPts; ++k)
            {
                double basis = TPSRadialBasis(ptSrc, targets[k]);
                ptDst.X += weights[k].Item0 * basis;
                ptDst.Y += weights[k].Item1 * basis; 
            }
            // Format: ax ay a1
            ptDst.X += weights[controlPts + 1].Item0 * ptSrc.X + weights[controlPts + 2].Item0 * ptSrc.Y + weights[controlPts].Item0;
            ptDst.Y += weights[controlPts + 1].Item1 * ptSrc.X + weights[controlPts + 2].Item1 * ptSrc.Y + weights[controlPts].Item1;
            return ptDst;
        }

        public void Estimate(Point2d[] srcs, Point2d[] dsts)
        {
            // Get the TPS deform funtion.
            if (srcs.Length != dsts.Length)
            {
                throw new ArgumentException("Control points do not match!");
            }
            controlPts = srcs.Length;
            targets = new Point2d[controlPts];
            weights = new Vec2d[controlPts + 3];
            srcs.CopyTo(targets, 0);
            // Solve equation.
            int maxRows = controlPts + 3, maxCols = controlPts + 5;
            double[,] augmented = new double[maxRows, maxCols];
            // Prepare.
            for(int j = 0; j < controlPts; ++j)
            {
                for(int i = j + 1; i < controlPts; ++i)
                {
                    augmented[j, i] = TPSRadialBasis(targets[j], targets[i]);
                    augmented[i, j] = augmented[j, i];
                }
            }
            for (int k = 0; k < controlPts; ++k)
            {
                augmented[k, controlPts] = 1;
                augmented[controlPts, k] = 1;
                augmented[k, controlPts + 1] = targets[k].X;
                augmented[controlPts + 1, k] = targets[k].X;
                augmented[k, controlPts + 2] = targets[k].Y;
                augmented[controlPts + 2, k] = targets[k].Y;
            }
            for(int k = 0; k < controlPts; ++k)
            {
                augmented[k, controlPts + 3] = dsts[k].X;
                augmented[k, controlPts + 4] = dsts[k].Y;
            }
            // Solve.
            Utils.SolveLinearEqn(augmented);
            // Get result.
            for (int j = 0; j < maxRows; ++j)
            {
                weights[j].Item0 = augmented[j, controlPts + 3];
                weights[j].Item1 = augmented[j, controlPts + 4];
            }
        }

        private double TPSRadialBasis(Point2d pt1, Point2d pt2)
        {
            // Radial basis function of TPS.
            double result = 0.0;
            double dx = pt1.X - pt2.X, dy = pt1.Y - pt2.Y;
            double r2 = dx * dx + dy * dy;
            if (r2 < 1e-12) result = 0.0;
            else result = r2 * Math.Log(r2);
            return result;
        }
    }
}
