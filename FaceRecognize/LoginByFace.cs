using Emgu.CV.Face;
using Emgu.CV.Structure;
using Emgu.CV;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV.CvEnum;
using Emgu.CV.Util;
using NhaKhoaCuoiKy.Helpers;
using NhaKhoaCuoiKy.Models;
using System.Timers;
using NhaKhoaCuoiKy.Views.LogIn;

namespace NhaKhoaCuoiKy.FaceRecognize
{
    public partial class LoginByFace : Form
    {
        Image<Bgr, Byte> currentFrame;
        VideoCapture grabber;
        CascadeClassifier face;
        Image<Gray, byte> result, TrainedFace = null;
        Image<Gray, byte> gray = null;
        List<Image<Gray, byte>> trainingImages = new List<Image<Gray, byte>>();
        List<int> labels = new List<int>();
        List<string> NamePersons = new List<string>();
        Image<Gray, Byte> resultImage;
        int ContTrain, NumLabels, t;
        EigenFaceRecognizer recognizer;
        string name, names = null;
        bool haveFace = false;
        UserModel userAccount;
        private System.Windows.Forms.Timer timer;
        int count = 0;
        LoginForm loginForm;
        public LoginByFace(LoginForm loginForm)
        {
            InitializeComponent();
            face = new CascadeClassifier("haarcascade_frontalface_default.xml");
            this.loginForm = loginForm;
            loginForm.Hide();
            TrainedData();
        }

        void TrainedData()
        {
            DataTable faces = FaceRecognizeHelper.getAllFace();
            if (faces.Rows.Count > 0)
            {
                haveFace = true;
                NumLabels = faces.Rows.Count;
                ContTrain = NumLabels;
                trainingImages.Clear();
                labels.Clear();

                /*                for (int tf = 1; tf < NumLabels + 1; tf++)
                                {
                                    string LoadFaces = "face" + (tf - 1) + ".bmp";
                                    trainingImages.Add(new Image<Gray, byte>(Application.StartupPath + "/TrainedFaces/" + LoadFaces));
                                    labels.Add(Convert.ToInt32(Labels[tf]));
                                }*/

                foreach (DataRow dr in faces.Rows)
                {
                    byte[] imageData = (byte[])dr["Anh"];

                    MemoryStream ms = new System.IO.MemoryStream(imageData);
                    Bitmap bm = new Bitmap(ms);
                    Image<Gray, byte> trainedFace = bm.ToImage<Gray, byte>();
                    trainingImages.Add(trainedFace);
                    int label = Convert.ToInt32(dr["MaNhanVien"]);
                    labels.Add(label);
                }

                recognizer = new EigenFaceRecognizer(NumLabels);
                int width = 200;
                int height = 200;

                // Resize each training image to the specified width and height
                for (int i = 0; i < trainingImages.Count; i++)
                {
                    trainingImages[i] = trainingImages[i].Resize(width, height, Inter.Cubic);
                }
                Mat[] imagesArray = new Mat[trainingImages.Count];

                // Convert each image to a Mat and add it to the array
                for (int i = 0; i < trainingImages.Count; i++)
                {
                    imagesArray[i] = trainingImages[i].Mat;
                }
                IInputArrayOfArrays inputArrayOfArrays = new VectorOfMat(imagesArray);

                IInputArray personsLabelsArray = new VectorOfInt(labels.ToArray());
                recognizer.Train(inputArrayOfArrays, personsLabelsArray);
            }
        }

        private void LoginByFace_Load(object sender, EventArgs e)
        {
            grabber = new VideoCapture();
            grabber.QueryFrame();
            Application.Idle += new EventHandler(FrameGrabber);
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 100; // Đặt khoảng thời gian cho timer ở đây (ví dụ: 5000ms = 5 giây)
            timer.Tick += new EventHandler(Timer_Tick);
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            count++;
            if (count > 20)
            {
                Application.Idle -= FrameGrabber;
                timer.Tick -= Timer_Tick;
                if (grabber != null)
                {
                    grabber.Stop();
                    grabber.Dispose();
                }
                Close();
                return;
            }
        }

        void FrameGrabber(object sender, EventArgs e)
        {
            if (grabber.QueryFrame() != null)
            {
                currentFrame = grabber.QueryFrame().ToImage<Bgr, Byte>();
                gray = currentFrame.Convert<Gray, Byte>();

                Rectangle[] faces = face.DetectMultiScale(gray, 1.1, 3, Size.Empty, Size.Empty);

                foreach (var face in faces)
                {
                    CvInvoke.Rectangle(currentFrame, face, new Bgr(Color.Red).MCvScalar, 2);

                    resultImage = currentFrame.Convert<Gray, Byte>();
                    resultImage.ROI = face;
                    result = currentFrame.Copy(face).Convert<Gray, byte>().Resize(200, 200, Inter.Cubic);

                    if (count > 10)
                    {
                        if (haveFace)
                        {
                            var result_t = recognizer.Predict(result);

                            if (result_t.Label > 0 && result_t.Label != 1)
                            {
                                CvInvoke.PutText(currentFrame, result_t.Label.ToString(), new Point(face.X - 2, face.Y - 2), FontFace.HersheyComplex, 1.0, new Bgr(Color.Orange).MCvScalar);
                                CvInvoke.Rectangle(currentFrame, face, new Bgr(Color.Green).MCvScalar, 2);
                                int employeeID = result_t.Label;
                                DataTable dt = UserHelper.getUserByEmployeeID(employeeID);
                                if (dt.Rows.Count > 0)
                                {
                                    string username = dt.Rows[0]["TenDangNhap"].ToString();
                                    string password = dt.Rows[0]["MatKhau"].ToString();
                                    int decentralization = Convert.ToInt32(dt.Rows[0]["Quyen"]);
                                    loginForm.userAccount = new UserModel(username, password, decentralization, employeeID);
                                    Application.Idle -= FrameGrabber;
                                    timer.Tick -= Timer_Tick;
                                    if (grabber != null)
                                    {
                                        grabber.Stop();
                                        grabber.Dispose();
                                    }
                                    Close();
                                    return;
                                }
                            }
                            else
                            {
                                CvInvoke.PutText(currentFrame, "Unknown", new Point(face.X - 2, face.Y - 2), FontFace.HersheyComplex, 1.0, new Bgr(Color.Orange).MCvScalar);
                                CvInvoke.Rectangle(currentFrame, face, new Bgr(Color.Green).MCvScalar, 2);
                                Application.Idle -= FrameGrabber;
                                timer.Tick -= Timer_Tick;
                                if (grabber != null)
                                {
                                    grabber.Stop();
                                    grabber.Dispose();
                                }

                                Close();
                                return;
                            }
                        }
                    }

                }
            }
            imageBoxFrameGrabber.Image = currentFrame.ToBitmap();
        }
    }
}
