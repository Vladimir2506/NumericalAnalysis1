using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using OpenCvSharp;
using System.Drawing.Imaging;

namespace NumericalAnalysis1
{
    public partial class MainForm : Form
    {
        // Indices of test samples.
        private int idxImgSrc = 0;
        private int idxImgGuide = 0;
        private int totalImgs = 0;
        // Core landmarks.
        private Point2d[] mrkSrc = null;
        private Point2d[] mrkGuide = null;
        private Bitmap imgSrc = null;
        private Bitmap imgGuide = null;
        private Bitmap imgDst = null;
        // Logic Controls.
        private bool srcMrkShown = false;
        private bool guideMrkShown = false;
        private bool srcImgChanged = false;
        private bool guideImgChanged = false;
        private bool mineSrc = false;
        private bool mineGuide = false;
        private DeformBspline bsp = null;
        private DeformTPS tps = null;
        private Interpolation intp = null;
        private Alignment algn = null;
        private Detection det = null;
        private int landmarks = 68;
        private InterpolateMethod methodIntp;
        private DeformMethod methodDeform;
        // Pre-alignment.
        private int nAlign = 68;
        private Point2d[] algn1 = null, algn2 = null;

        public MainForm()
        {
            InitializeComponent();
            // Init all methods.
            while (File.Exists(string.Format("./data/{0}.jpg", totalImgs + 1))) totalImgs++;
            intp = Interpolation.GetInstance();
            bsp = DeformBspline.GetInstance();
            tps = DeformTPS.GetInstance();
            algn = Alignment.GetInstance();
            det = Detection.GetInstance();
        }

        private void InitData()
        {
            // Set the initial data.
            det.LoadModel("./model/DetectorModel.dat");
            pbDst.Image = new Bitmap("./data/test.jpg");
            idxImgSrc = 1;
            idxImgGuide = 1;
            srcImgChanged = true;
            guideImgChanged = true;
            srcMrkShown = false;
            guideMrkShown = false;
            mineSrc = false;
            mineGuide = false;
            rbNearest.Checked = true;
            rbBsp.Checked = true;
            methodIntp = InterpolateMethod.Nearest;
            methodDeform = DeformMethod.BSpline;
            algn1 = new Point2d[nAlign];
            algn2 = new Point2d[nAlign];
        }

        private void UpdateData()
        {
            // When UI need to change...
            if (srcImgChanged)
            {
                if (!mineSrc)
                {
                    string pathImgSrc = string.Format("./data/{0}.jpg", idxImgSrc);
                    string pathMrkSrc = string.Format("./data/{0}.txt", idxImgSrc);
                    mrkSrc = Utils.LoadFacialLandmarks(pathMrkSrc, landmarks);
                    imgSrc = new Bitmap(pathImgSrc);
                }
                else
                {
                    mrkSrc = null;
                }
                pbSrc.Image = imgSrc;
                imgDst = (Bitmap)imgSrc.Clone();
                srcImgChanged = false;
                srcMrkShown = false;
            }
            if (guideImgChanged)
            {
                if (!mineGuide)
                {
                    string pathImgGuide = string.Format("./data/{0}.jpg", idxImgGuide);
                    string pathMrkGuide = string.Format("./data/{0}.txt", idxImgGuide);
                    imgGuide = new Bitmap(pathImgGuide);
                    mrkGuide = Utils.LoadFacialLandmarks(pathMrkGuide, landmarks);
                }
                else
                {
                    mrkGuide = null;
                }
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
            // Load to initialize.
            InitData();
            UpdateData();
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
                if (mrkSrc == null)
                {
                    MessageBox.Show("源图无关键点！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
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
                if (mrkGuide == null)
                {
                    MessageBox.Show("导向图无关键点！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                Bitmap marked = (Bitmap)imgGuide.Clone();
                var g = Graphics.FromImage(marked);
                foreach (var mrk in mrkGuide)
                {
                    g.FillEllipse
                        (
                            new SolidBrush(Color.FromKnownColor(KnownColor.LightGreen)), 
                            (float)mrk.X, 
                            (float)mrk.Y, 
                            6, 
                            6
                        );
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
            mineSrc = false;
            srcImgChanged = true;
            UpdateData();
        }

        private void OnbtnSrcRightClick(object sender, EventArgs e)
        {
            ++idxImgSrc;
            mineSrc = false;
            srcImgChanged = true;
            UpdateData();
        }

        private void OnbtnGuideLeftClick(object sender, EventArgs e)
        {
            --idxImgGuide;
            mineGuide = false;
            guideImgChanged = true;
            UpdateData();
        }

        private void OnbtnGuideRightClick(object sender, EventArgs e)
        {
            ++idxImgGuide;
            mineGuide = false;
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

        private void OnbtnSaveClick(object sender, EventArgs e)
        {
            Bitmap tmp = (Bitmap)imgDst.Clone();
            imgDst = (Bitmap)pbDst.Image;
            using (var saveFileDlg = new SaveFileDialog())
            {
                saveFileDlg.Title = "保存目标图片";
                saveFileDlg.RestoreDirectory = true;
                saveFileDlg.Filter = "位图 (.bmp)|*.bmp|联合图像专家组 (.jpg)|*.jpg|便携式网络图形 (.png)|*.png";
                if (saveFileDlg.ShowDialog() == DialogResult.OK)
                {
                    string fileName = saveFileDlg.FileName;
                    string[] seps = fileName.Split(new char[] { '.' }, 2);
                    string fileExtName = seps[1];
                    switch (fileExtName)
                    {
                        case "jpg":
                            imgDst.Save(fileName, ImageFormat.Jpeg);
                            break;
                        case "bmp":
                            imgDst.Save(fileName, ImageFormat.Bmp);
                            break;
                        case "png":
                            imgDst.Save(fileName, ImageFormat.Png);
                            break;
                        default:
                            break;
                    }
                }
            }
            imgDst = tmp;
        }

        private void OnbtnSrcDetectClick(object sender, EventArgs e)
        {
            det.DetectLandmarks68(in imgSrc, out mrkSrc);
        }

        private void OnbtnGuideDetectClick(object sender, EventArgs e)
        {
            det.DetectLandmarks68(in imgGuide, out mrkGuide);
        }

        private void OmbtnLoadImgSrcClick(object sender, EventArgs e)
        {
            using (var openFileDlg = new OpenFileDialog())
            {
                openFileDlg.Title = "加载源图片";
                openFileDlg.RestoreDirectory = true;
                openFileDlg.Filter = "位图 (.bmp)|*.bmp|联合图像专家组 (.jpg)|*.jpg|便携式网络图形 (.png)|*.png";
                if (openFileDlg.ShowDialog() == DialogResult.OK)
                {
                    string fileName = openFileDlg.FileName;
                    imgSrc = (Bitmap)Image.FromFile(fileName);
                    mineSrc = true;
                    srcImgChanged = true;
                    UpdateData();
                }
            }
        }

        private void OmbtnLoadImgGuideClick(object sender, EventArgs e)
        {
            using (var openFileDlg = new OpenFileDialog())
            {
                openFileDlg.Title = "加载导向图片";
                openFileDlg.RestoreDirectory = true;
                openFileDlg.Filter = "位图 (.bmp)|*.bmp|联合图像专家组 (.jpg)|*.jpg|便携式网络图形 (.png)|*.png";
                if (openFileDlg.ShowDialog() == DialogResult.OK)
                {
                    string fileName = openFileDlg.FileName;
                    imgGuide = (Bitmap)Image.FromFile(fileName);
                    mineGuide = true;
                    guideImgChanged = true;
                    UpdateData();
                }
            }
        }

        private void OnbtnLoadMrkSrcClick(object sender, EventArgs e)
        {
            using (var openFileDlg = new OpenFileDialog())
            {
                openFileDlg.Title = "加载源图片关键点";
                openFileDlg.Filter = "文本文件(*.txt)|*.txt";
                openFileDlg.RestoreDirectory = true;
                if (openFileDlg.ShowDialog() == DialogResult.OK)
                {
                    string fileName = openFileDlg.FileName;
                    mrkSrc = Utils.LoadFacialLandmarks(fileName, 68);
                }
            }
        }

        private void OnbtnLoadMrkGuideClick(object sender, EventArgs e)
        {
            using (var openFileDlg = new OpenFileDialog())
            {
                openFileDlg.Title = "加载导向图片关键点";
                openFileDlg.Filter = "文本文件(*.txt)|*.txt";
                openFileDlg.RestoreDirectory = true;
                if (openFileDlg.ShowDialog() == DialogResult.OK)
                {
                    string fileName = openFileDlg.FileName;
                    mrkGuide = Utils.LoadFacialLandmarks(fileName, 68);
                }
            }
        }

        private void OnbtnSaveSrcMrkClick(object sender, EventArgs e)
        {
            if (mrkSrc == null)
            {
                MessageBox.Show("源图无关键点！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            using (var saveFileDlg = new SaveFileDialog())
            {
                saveFileDlg.Title = "保存源图片关键点";
                saveFileDlg.Filter = "文本文件(*.txt)|*.txt";
                saveFileDlg.RestoreDirectory = true;
                if (saveFileDlg.ShowDialog() == DialogResult.OK)
                {
                    string fileName = saveFileDlg.FileName;
                    Utils.SaveFacialLandmarks(fileName, mrkSrc);
                }
            }
        }

        private void OnbtnSaveMrkGuideClick(object sender, EventArgs e)
        {
            if (mrkGuide == null)
            {
                MessageBox.Show("导向图无关键点！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            using (var saveFileDlg = new SaveFileDialog())
            {
                saveFileDlg.Title = "保存导向图片关键点";
                saveFileDlg.Filter = "文本文件(*.txt)|*.txt";
                saveFileDlg.RestoreDirectory = true;
                if (saveFileDlg.ShowDialog() == DialogResult.OK)
                {
                    string fileName = saveFileDlg.FileName;
                    Utils.SaveFacialLandmarks(fileName, mrkGuide);
                }
            }
        }

        private void OnbtnExecuteClick(object sender, EventArgs e)
        {
            // Apply transforms.
            if (mrkSrc == null)
            {
                MessageBox.Show("源图无关键点！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (mrkGuide == null)
            {
                MessageBox.Show("导向图无关键点！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // If landmarks are identical, return identity mapping.
            double sigmaDeltas = 0.0;
            for (int k = 0; k < landmarks; ++k)
            {
                sigmaDeltas += mrkSrc[k].DistanceTo(mrkGuide[k]);
            }
            if (sigmaDeltas < 1e-12)
            {
                pbDst.Image = imgSrc;
                return;
            }
            // Set up pre-alignment and interpolation.
            for (int k = 0; k < landmarks; ++k)
            {
                algn1[k] = mrkSrc[k];
                algn2[k] = mrkGuide[k];
            }
            intp.SetSourceImg(imgSrc);
            algn.EstimateLeastSquare(algn2, algn1);
            Point2d[] mrkGuide2 = algn.ApplyTransform(mrkGuide);
            // Set up deformation.
            if (methodDeform == DeformMethod.BSpline)
            {
                bsp.Displace(imgSrc, mrkSrc, mrkGuide2, 20);
            }
            if (methodDeform == DeformMethod.TPSpline)
            {
                tps.Estimate(mrkGuide2, mrkSrc);
            }
            // Speed up interpolation.
            BitmapData dst = imgDst.LockBits
                (
                    new Rectangle(0, 0, imgDst.Width, imgDst.Height), 
                    ImageLockMode.WriteOnly, 
                    PixelFormat.Format32bppArgb
                );
            unsafe
            { 
                int* pdst = (int*)dst.Scan0;
                for(int j = 0; j < dst.Height; ++j)
                {
                    for (int i = 0; i < dst.Width; ++i)
                    {
                        // Step by step.
                        if (methodDeform == DeformMethod.TPSpline)
                        {
                            *(pdst + i + j * dst.Width) = intp.Step
                                (
                                    tps.Step(new Point2d(i, j)), 
                                    methodIntp
                                );
                        }
                        if (methodDeform == DeformMethod.BSpline)
                        {
                            *(pdst + i + j * dst.Width) = intp.Step
                                (
                                    bsp.Step(new Point2d(i, j)), 
                                    methodIntp
                                );
                        }
                    }
                }
            }
            imgDst.UnlockBits(dst);
            pbDst.Image = imgDst;
        }
    }
}
