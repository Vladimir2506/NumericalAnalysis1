using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using OpenCvSharp;
using System.Threading;
using OpenCvSharp.Extensions;
using Point2i = OpenCvSharp.Point;
using Cuda = OpenCvSharp.Cuda;
using System.Net;
using System.Drawing.Imaging;

namespace NumericalAnalysis1
{
    public partial class MainForm : Form
    {
        private string strDataPath = "./data/";
        private int nImageOri = 1;
        private int nImageGuide = 2;

        private Point2f[] ptsLandmarkSrc;
        private Point2f[] ptsLandmarkGuide;
        private Point2f[] ptsLandmarkDst;
        private Point2f[] ptsLandmarkMean;
        private Point2f[] ptsConvertSrc;
        private Point2f[] ptsConvertGuide;

        private int nLandmarks = 68;
        private int[] idxs = { 38 - 1, 43 - 1, 48 - 1, 54 - 1 };
        private char[] chSeps = {' ', '\r', '\n' };
        private double[] dMeanLandmarkXs = 
        {
            0.000213256, 0.0752622, 0.18113, 0.29077, 0.393397, 0.586856, 0.689483, 0.799124,
            0.904991, 0.98004, 0.490127, 0.490127, 0.490127, 0.490127, 0.36688, 0.426036,
            0.490127, 0.554217, 0.613373, 0.121737, 0.187122, 0.265825, 0.334606, 0.260918,
            0.182743, 0.645647, 0.714428, 0.793132, 0.858516, 0.79751, 0.719335, 0.254149,
            0.340985, 0.428858, 0.490127, 0.551395, 0.639268, 0.726104, 0.642159, 0.556721,
            0.490127, 0.423532, 0.338094, 0.290379, 0.428096, 0.490127, 0.552157, 0.689874,
            0.553364, 0.490127, 0.42689
        };
        private double[] dMeanLandmarkYs =
        {
            0.106454, 0.038915, 0.0187482, 0.0344891, 0.0773906, 0.0773906, 0.0344891,
            0.0187482, 0.038915, 0.106454, 0.203352, 0.307009, 0.409805, 0.515625, 0.587326,
            0.609345, 0.628106, 0.609345, 0.587326, 0.216423, 0.178758, 0.179852, 0.231733,
            0.245099, 0.244077, 0.231733, 0.179852, 0.178758, 0.216423, 0.244077, 0.245099,
            0.780233, 0.745405, 0.727388, 0.742578, 0.727388, 0.745405, 0.780233, 0.864805,
            0.902192, 0.909281, 0.902192, 0.864805, 0.784792, 0.778746, 0.785343, 0.778746,
            0.784792, 0.824182, 0.831803, 0.824182
        };
        public MainForm()
        {
            InitializeComponent();
            ptsLandmarkSrc = new Point2f[nLandmarks];
            ptsLandmarkDst = new Point2f[nLandmarks];
            ptsLandmarkGuide = new Point2f[nLandmarks];
            ptsLandmarkMean = new Point2f[68 - 17];
            ptsConvertSrc = new Point2f[68 - 17];
            ptsConvertGuide = new Point2f[68 - 17];
           
            for(int i = 0;i < 68 - 17; ++i)
            {
                ptsLandmarkMean[i].X = (float)dMeanLandmarkXs[i];
                ptsLandmarkMean[i].Y = (float)dMeanLandmarkYs[i];
            }
            
        }

        private void OnLoad(object sender, EventArgs e)
        {
            /*Bitmap bmp = new Bitmap("./data/test.png");
            System.Drawing.Size size = bmp.Size;
            pbSrc.Image = bmp;
            Interpolation interpolater = Interpolation.GetInstance();
            interpolater.SetSourceImg(bmp);
            Bitmap bmp2 = new Bitmap(size.Width * 2, size.Height * 2);
            BitmapData data2 = bmp2.LockBits(new Rectangle(0, 0, bmp2.Width, bmp2.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            unsafe
            {
                int* pData = (int*)data2.Scan0;
                for(int j = 0; j < bmp2.Height; ++j)
                {
                    for(int i = 0; i < bmp2.Width; ++i)
                    {
                        *(pData + i + j * bmp2.Width) = interpolater.Step(new Point2d(i / 2.0, j / 2.0), InterpolateMethod.Bicubic);
                    }
                }
            }
            bmp2.UnlockBits(data2);
            pbDst.Image = bmp2;
            bmp2.Save("./data/testBicubic.png");*/
            Bitmap bmpSrc = new Bitmap("./data/8.jpg");
            Bitmap bmpDst = (Bitmap)bmpSrc.Clone();
            Point2i[] mrk1 = LoadFacialLandmarks(8);
            Point2i[] mrk2 = LoadFacialLandmarks(9);
            Point2i[] mrk22 = new Point2i[17], mrk11 = new Point2i[17];
            for (int i = 0; i < 17; ++i)
            {
                mrk11[i] = mrk1[i];
                mrk22[i] = mrk2[i];
            }
            Interpolation intp = Interpolation.GetInstance();
            intp.SetSourceImg(bmpSrc);
            BitmapData data = bmpDst.LockBits(new Rectangle(0, 0, bmpSrc.Width, bmpSrc.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            Alignment algn = Alignment.GetInstance();
            algn.EstimateLeastSquare(mrk11, mrk22);
            //mrk2 = algn.ApplyTransform(mrk1);
            /*DeformTPS tps = DeformTPS.GetInstance();
            tps.SetAttribute(68);
            tps.Estimate(mrk2, mrk1);
            unsafe
            {
                int* p = (int*)data.Scan0;
                for (int j = 0; j < bmpDst.Height; ++j)
                {
                    for (int i = 0; i < bmpDst.Width; ++i)
                    {
                        * (p + i + j * bmpDst.Width) = intp.Step(tps.Step(new Point2i(i, j)), InterpolateMethod.Nearest);
                    }
                }
            }*/
            
            DeformBspline bsp = DeformBspline.GetInstance();
            bsp.SetAttribute(bmpSrc, 25);
            unsafe
            {
                int* p = (int*)data.Scan0;
                for (int k = 0; k < 68; ++k)
                {
                    Point2i[] boundaries = bsp.Displace(mrk1[k], mrk2[k]);
                    for (int j = boundaries[0].Y; j < boundaries[1].Y; ++j)
                    {
                        for (int i = boundaries[0].X; i < boundaries[1].X; ++i)
                        {
                            *(p + i + j * bmpDst.Width) = intp.Step(bsp.Step(new Point2i(i, j)), InterpolateMethod.Bicubic);
                        }
                    }
                }
            }
            bmpDst.UnlockBits(data);
            pbSrc.Image = bmpSrc;
            pbDst.Image = bmpDst;
        }

        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            Cv2.DestroyAllWindows();
        }

        private void DisposeMats() 
        {
            
        }

        private Point2i[] LoadFacialLandmarks(int nID)
        {
            string strLandmarkFileName = strDataPath + nID.ToString() + ".txt";
            if (File.Exists(strLandmarkFileName))
            {
                Point2i[] ptsLandmarks = new Point2i[nLandmarks];
                FileStream fsRead = new FileStream(strLandmarkFileName, FileMode.Open);
                long nLen = fsRead.Length;
                byte[] buffer = new byte[nLen];
                fsRead.Read(buffer, 0, buffer.Length);
                string strEntire = Encoding.UTF8.GetString(buffer);
                string[] strPoints = strEntire.Split(chSeps);
                fsRead.Close();
                for (int k = 0; k < nLandmarks; ++k)
                {
                    ptsLandmarks[k].X = (int)Convert.ToSingle(strPoints[2 * k]);
                    ptsLandmarks[k].Y = (int)Convert.ToSingle(strPoints[2 * k + 1]);
                }
                return ptsLandmarks;
            }
            else
            {
                return new Point2i[]{ };
            }
        }
    }
}
