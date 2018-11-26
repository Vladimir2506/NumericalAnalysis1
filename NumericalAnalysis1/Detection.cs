using OpenCvSharp;
using DlibDotNet;
using System.Drawing.Imaging;
using System.Drawing;

namespace NumericalAnalysis1
{
    class Detection
    {
        private FrontalFaceDetector detector = null;
        private ShapePredictor landmarkModel = null;
        private static Detection instance = null;
        private Detection()
        {
            detector = Dlib.GetFrontalFaceDetector();
        }
        public static Detection GetInstance()
        {
            if (instance == null)
            {
                instance = new Detection();
            }
            return instance;
        }
        
        public void LoadModel(string modelPath)
        {
            landmarkModel = ShapePredictor.Deserialize(modelPath);
        }

        public void DetectLandmarks68(in Bitmap inputs, out Point2d[] outputs)
        {
            // Face landmarks detection using Dlib.
            outputs = new Point2d[68];
            BitmapData data = inputs.LockBits(new System.Drawing.Rectangle(0, 0, inputs.Width, inputs.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            Array2D<int> image = new Array2D<int>(inputs.Height, inputs.Width);
            unsafe
            {
                int* pData = (int*)data.Scan0;
                for (int j = 0; j < inputs.Height; ++j)
                {
                    for (int i = 0; i < inputs.Width; ++i)
                    {
                        image[j][i] = *(pData + j * inputs.Width + i);
                    }
                }
            }
            inputs.UnlockBits(data);
            Dlib.PyramidUp(image);
            DlibDotNet.Rectangle[] boundingBoxes = detector.Operator(image);
            // If multiple faces are detected, select the first one.
            DlibDotNet.Rectangle box = boundingBoxes[0];
            FullObjectDetection result = landmarkModel.Detect(image, box);
            for (uint i = 0; i < 68; i++)
            {
                outputs[i].X = result.GetPart(i).X / 2;
                outputs[i].Y = result.GetPart(i).Y / 2;
            }
        }
    }
}
