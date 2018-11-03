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
        Point2i[] mrkSrc = null;
        Point2i[] mrkGuide = null;
        private bool srcMrkShown = false;
        private bool guideMrkShown = false;
        private bool srcImgChanged = false;
        private bool guideImgChanged = false;
        DeformBspline bsp = null;
        DeformTPS tps = null;
        Interpolation intp = null;
        Alignment algn = null;
        private int landmarks = 68;
        private char[] separators = {' ', '\r', '\n' };
        Bitmap imgSrc = null;
        Bitmap imgGuide = null;
        Bitmap imgDst = null;
        private InterpolateMethod method;

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
            method = InterpolateMethod.Nearest;
        }

        private void UpdateData()
        {
            if (srcMrkShown) btnSrcMrk.Text = "隐藏关键点";
            else btnSrcMrk.Text = "显示关键点";
            if (guideMrkShown) btnGuideMrk.Text = "隐藏关键点";
            else btnGuideMrk.Text = "显示关键点";
            if (srcImgChanged)
            {
                string pathImgSrc = string.Format("./data/{0}.jpg", idxImgSrc);
                string pathMrkSrc = string.Format("./data/{0}.txt", idxImgSrc);
                imgSrc = new Bitmap(pathImgSrc);
                mrkSrc = LoadFacialLandmarks(pathMrkSrc);
                pbSrc.Image = imgSrc;
                imgDst = (Bitmap)imgSrc.Clone();
                srcImgChanged = false;
            }
            if (guideImgChanged)
            {
                string pathImgGuide = string.Format("./data/{0}.jpg", idxImgGuide);
                string pathMrkGuide = string.Format("./data/{0}.txt", idxImgGuide);
                imgGuide = new Bitmap(pathImgGuide);
                mrkGuide = LoadFacialLandmarks(pathMrkGuide);
                pbGuide.Image = imgGuide;
                guideImgChanged = false;
            }
            if (idxImgSrc == 1) btnSrcLeft.Enabled = false;
            else btnSrcLeft.Enabled = true;
            if (idxImgSrc == totalImgs) btnSrcRight.Enabled = false;
            else btnSrcRight.Enabled = true;
            if (idxImgGuide == 1) btnGuideLeft.Enabled = false;
            else btnGuideLeft.Enabled = true;
            if (idxImgGuide == totalImgs) btnGuideRight.Enabled = false;
            else btnGuideRight.Enabled = true;
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

        private Point2i[] LoadFacialLandmarks(string path)
        {
            string strLandmarkFileName = path;
            if (File.Exists(strLandmarkFileName))
            {
                Point2i[] ptsLandmarks = new Point2i[landmarks];
                FileStream fsRead = new FileStream(strLandmarkFileName, FileMode.Open);
                long nLen = fsRead.Length;
                byte[] buffer = new byte[nLen];
                fsRead.Read(buffer, 0, buffer.Length);
                string strEntire = Encoding.UTF8.GetString(buffer);
                string[] strPoints = strEntire.Split(separators);
                fsRead.Close();
                for (int k = 0; k < landmarks; ++k)
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

        private void OnbtnSrcMrkClick(object sender, EventArgs e)
        {
            if(srcMrkShown)
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
                    g.FillEllipse(new SolidBrush(Color.FromKnownColor(KnownColor.Aquamarine)), mrk.X, mrk.Y, 5, 5);
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
                    g.FillEllipse(new SolidBrush(Color.FromKnownColor(KnownColor.GhostWhite)), mrk.X, mrk.Y, 5, 5);
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
            if (rbNearest.Checked) method = InterpolateMethod.Nearest;
        }

        private void OnrbBilinearCheck(object sender, EventArgs e)
        {
            if (rbBilinear.Checked) method = InterpolateMethod.Bilinear;
        }

        private void OnBicubicCheck(object sender, EventArgs e)
        {
            if (rbBicubic.Checked) method = InterpolateMethod.Bicubic;
        }
    }
}
