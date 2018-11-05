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
        private int idxImgSrc = 0;
        private int idxImgGuide = 0;
        private int totalImgs = 0;
        private Point2d[] mrkSrc = null;
        private Point2d[] mrkGuide = null;
        private Point2d[] mrkGuide2 = null;
        private bool srcMrkShown = false;
        private bool guideMrkShown = false;
        private bool srcImgChanged = false;
        private bool guideImgChanged = false;
        private DeformBspline bsp = null;
        private DeformTPS tps = null;
        private Interpolation intp = null;
        private Alignment algn = null;
        private int landmarks = 68;
        private char[] separators = { ' ', '\r', '\n' };
        private Bitmap imgSrc = null;
        private Bitmap imgGuide = null;
        private Bitmap imgDst = null;
        private InterpolateMethod methodIntp;
        private DeformMethod methodDeform;
        private int nAlign = 68;
        private Point2d[] algn1 = null, algn2 = null;

        public MainForm()
        {
            InitializeComponent();
            while (File.Exists(string.Format("./data/{0}.jpg", totalImgs + 1))) totalImgs++;
            intp = Interpolation.GetInstance();
            bsp = DeformBspline.GetInstance();
            tps = DeformTPS.GetInstance();
            algn = Alignment.GetInstance();
        }

        private void InitData()
        {
            pbDst.Image = new Bitmap("./data/test.jpg");
            idxImgSrc = 1;
            idxImgGuide = 1;
            srcImgChanged = true;
            guideImgChanged = true;
            srcMrkShown = false;
            guideMrkShown = false;
            rbNearest.Checked = true;
            rbBsp.Checked = true;
            methodIntp = InterpolateMethod.Nearest;
            methodDeform = DeformMethod.BSpline;
            algn1 = new Point2d[nAlign];
            algn2 = new Point2d[nAlign];
        }

        private void UpdateData()
        {
            if (srcImgChanged)
            {
                string pathImgSrc = string.Format("./data/{0}.jpg", idxImgSrc);
                string pathMrkSrc = string.Format("./data/{0}.txt", idxImgSrc);
                imgSrc = new Bitmap(pathImgSrc);
                mrkSrc = LoadFacialLandmarks(pathMrkSrc);
                pbSrc.Image = imgSrc;
                imgDst = (Bitmap)imgSrc.Clone();
                srcImgChanged = false;
                srcMrkShown = false;
            }
            if (guideImgChanged)
            {
                string pathImgGuide = string.Format("./data/{0}.jpg", idxImgGuide);
                string pathMrkGuide = string.Format("./data/{0}.txt", idxImgGuide);
                imgGuide = new Bitmap(pathImgGuide);
                mrkGuide = LoadFacialLandmarks(pathMrkGuide);
                pbGuide.Image = imgGuide;
                guideImgChanged = false;
                guideMrkShown = false;
            }
            if (idxImgSrc == 1) btnSrcLeft.Enabled = false;
            else btnSrcLeft.Enabled = true;
            if (idxImgSrc == totalImgs) btnSrcRight.Enabled = false;
            else btnSrcRight.Enabled = true;
            if (idxImgGuide == 1) btnGuideLeft.Enabled = false;
            else btnGuideLeft.Enabled = true;
            if (idxImgGuide == totalImgs) btnGuideRight.Enabled = false;
            else btnGuideRight.Enabled = true;
            if (srcMrkShown) btnSrcMrk.Text = "隐藏关键点";
            else btnSrcMrk.Text = "显示关键点";
            if (guideMrkShown) btnGuideMrk.Text = "隐藏关键点";
            else btnGuideMrk.Text = "显示关键点";
        }

        private void OnLoad(object sender, EventArgs e)
        {
            InitData();
            UpdateData();
        }

        private void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            Cv2.DestroyAllWindows();
        }

        private void DisposeMats()
        {

        }

        private Point2d[] LoadFacialLandmarks(string path)
        {
            string strLandmarkFileName = path;
            if (File.Exists(strLandmarkFileName))
            {
                Point2d[] ptsLandmarks = new Point2d[landmarks];
                FileStream fsRead = new FileStream(strLandmarkFileName, FileMode.Open);
                long nLen = fsRead.Length;
                byte[] buffer = new byte[nLen];
                fsRead.Read(buffer, 0, buffer.Length);
                string strEntire = Encoding.UTF8.GetString(buffer);
                string[] strPoints = strEntire.Split(separators);
                fsRead.Close();
                for (int k = 0; k < landmarks; ++k)
                {
                    ptsLandmarks[k].X = Convert.ToDouble(strPoints[2 * k]);
                    ptsLandmarks[k].Y = Convert.ToDouble(strPoints[2 * k + 1]);
                }
                return ptsLandmarks;
            }
            else
            {
                return new Point2d[] { };
            }
        }

        private void OnbtnSrcMrkClick(object sender, EventArgs e)
        {
            if (srcMrkShown)
            {
                pbSrc.Image = imgSrc;
                srcMrkShown = false;
            }
            else
            {
                Bitmap marked = (Bitmap)imgSrc.Clone();
                var g = Graphics.FromImage(marked);
                foreach (var mrk in mrkSrc)
                {
                    g.FillEllipse(new SolidBrush(Color.FromKnownColor(KnownColor.LightGreen)), (float)mrk.X, (float)mrk.Y, 6, 6);
                }
                g.Dispose();
                pbSrc.Image = marked;
                srcMrkShown = true;
            }
            UpdateData();
        }

        private void OnbtnGuideMrkClick(object sender, EventArgs e)
        {
            if (guideMrkShown)
            {
                pbGuide.Image = imgGuide;
                guideMrkShown = false;
            }
            else
            {
                Bitmap marked = (Bitmap)imgGuide.Clone();
                var g = Graphics.FromImage(marked);
                foreach (var mrk in mrkGuide)
                {
                    g.FillEllipse(new SolidBrush(Color.FromKnownColor(KnownColor.LightGreen)), (float)mrk.X, (float)mrk.Y, 6, 6);
                }
                g.Dispose();
                pbGuide.Image = marked;
                guideMrkShown = true;
            }
            UpdateData();
        }

        private void OnbtnSrcLeftClick(object sender, EventArgs e)
        {
            --idxImgSrc;
            srcImgChanged = true;
            UpdateData();
        }

        private void OnbtnSrcRightClick(object sender, EventArgs e)
        {
            ++idxImgSrc;
            srcImgChanged = true;
            UpdateData();
        }

        private void OnbtnGuideLeftClick(object sender, EventArgs e)
        {
            --idxImgGuide;
            guideImgChanged = true;
            UpdateData();
        }

        private void OnbtnGuideRightClick(object sender, EventArgs e)
        {
            ++idxImgGuide;
            guideImgChanged = true;
            UpdateData();
        }

        private void OnrbNearestCheck(object sender, EventArgs e)
        {
            if (rbNearest.Checked) methodIntp = InterpolateMethod.Nearest;
        }

        private void OnrbBilinearCheck(object sender, EventArgs e)
        {
            if (rbBilinear.Checked) methodIntp = InterpolateMethod.Bilinear;
        }

        private void OnBicubicCheck(object sender, EventArgs e)
        {
            if (rbBicubic.Checked) methodIntp = InterpolateMethod.Bicubic;
        }

        private void OnrbBspCheck(object sender, EventArgs e)
        {
            if (rbBsp.Checked) methodDeform = DeformMethod.BSpline;
        }

        private void OnrbTpsCheck(object sender, EventArgs e)
        {
            if (rbTps.Checked) methodDeform = DeformMethod.TPSpline;
        }

        private void PaintBorderlessGroupBox(object sender, PaintEventArgs p)
        {
            GroupBox box = (GroupBox)sender;
            p.Graphics.Clear(SystemColors.Control);
            p.Graphics.DrawString(box.Text, box.Font, Brushes.MidnightBlue, 0, 0);
        }

        private void OnbtnExecuteClick(object sender, EventArgs e)
        {
            if (idxImgSrc == idxImgGuide)
            {
                pbDst.Image = imgSrc;
                return;
            }
            for (int k = 0; k < nAlign; ++k)
            {
                algn1[k] = mrkSrc[k];
                algn2[k] = mrkGuide[k];
            }
            intp.SetSourceImg(imgSrc);
            bsp.SetAttribute(imgSrc, 15);
            tps.SetAttribute(landmarks);
            algn.EstimateLeastSquare(algn2, algn1);
            mrkGuide2 = algn.ApplyTransform(mrkGuide);
            BitmapData dst = imgDst.LockBits(new Rectangle(0, 0, imgDst.Width, imgDst.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            unsafe
            {
                if (methodDeform == DeformMethod.BSpline)
                {
                    for(int i = 0; i < landmarks; ++i)
                    {
                        bsp.Displace(mrkSrc[i], mrkGuide2[i]);
                    }
                }
                if (methodDeform == DeformMethod.TPSpline)
                {
                    tps.Estimate(mrkGuide2, mrkSrc);
                }
                int* pdst = (int*)dst.Scan0;
                for(int j = 0; j < dst.Height; ++j)
                {
                    for (int i = 0; i < dst.Width; ++i)
                    {
                        if (methodDeform == DeformMethod.TPSpline)
                        {
                            *(pdst + i + j * dst.Width) = intp.Step(tps.Step(new Point2d(i, j)), methodIntp);
                        }
                        if (methodDeform == DeformMethod.BSpline)
                        {
                            *(pdst + i + j * dst.Width) = intp.Step(bsp.Step(new Point2d(i, j)), methodIntp);
                        }
                    }
                }
            }
            imgDst.UnlockBits(dst);
            var g = Graphics.FromImage(imgDst);
            foreach (var mrk in mrkGuide2)
            {
                g.FillEllipse(new SolidBrush(Color.FromKnownColor(KnownColor.Yellow)), (float)mrk.X, (float)mrk.Y, 6, 6);
            }
            foreach (var mrk in mrkSrc)
            {
                g.FillEllipse(new SolidBrush(Color.FromKnownColor(KnownColor.Blue)), (float)mrk.X, (float)mrk.Y, 6, 6);
            }
            g.Dispose();
            pbDst.Image = imgDst;
            
        }
    }
}