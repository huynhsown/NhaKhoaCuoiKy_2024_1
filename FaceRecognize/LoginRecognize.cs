using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Face;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using NhaKhoaCuoiKy.Helpers;
using NhaKhoaCuoiKy.Views.LogIn;
using System.Data;

namespace NhaKhoaCuoiKy.FaceRecognize
{
    public partial class LoginRecognize : Form
    {
        Image<Bgr, Byte> currentFrame;
        VideoCapture grabber;
        CascadeClassifier face;
        Image<Gray, byte> result, TrainedFace = null;
        Image<Gray, byte> gray = null;
        List<Image<Gray, byte>> trainingImages = new List<Image<Gray, byte>>();
        List<int> labels = new List<int>();
        Image<Gray, Byte> resultImage;
        int NumLabels;
        EigenFaceRecognizer recognizer;
        bool haveFace = false;
        bool isUpdate = false;
        bool isExist = false;
        int employeeID;
        UserManagement userManagement;
        public LoginRecognize(int employeeID, UserManagement userManagement)
        {
            InitializeComponent();
            face = new CascadeClassifier("haarcascade_frontalface_default.xml");
            {
                TrainedData();
                this.employeeID = employeeID;
                this.userManagement = userManagement;
                textBox1.Text = employeeID.ToString();
                textBox1.Enabled = false;
                DataTable faces = FaceRecognizeHelper.getAllFaceByEmployeeID(employeeID);
                if (faces.Rows.Count > 0)
                {
                    isUpdate = true;
                    foreach (DataRow dr in faces.Rows)
                    {
                        byte[] imageData = (byte[])dr["Anh"];

                        MemoryStream ms = new System.IO.MemoryStream(imageData);
                        Bitmap bm = new Bitmap(ms);
                        Image img = (Image)bm;
                        imageBox1.Image = img;

                    }
                }
            }
        }

        void TrainedData()
        {
            DataTable faces = FaceRecognizeHelper.getAllFace();
            if (faces.Rows.Count > 0)
            {
                haveFace = true;
                NumLabels = faces.Rows.Count;
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

        private void LoginRecognize_Load(object sender, EventArgs e)
        {
            grabber = new VideoCapture();
            grabber.QueryFrame();
            Application.Idle += new EventHandler(FrameGrabber);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            grabber = new VideoCapture();
            grabber.QueryFrame();
            Application.Idle += new EventHandler(FrameGrabber);
            button1.Enabled = false;
        }

        void FrameGrabber(object sender, EventArgs e)
        {
            if (grabber.QueryFrame != null)
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
                    if (haveFace)
                    {
                        var result_t = recognizer.Predict(result);

                        if (result_t.Label > 1)
                        {
                            CvInvoke.PutText(currentFrame, result_t.Label.ToString(), new Point(face.X - 2, face.Y - 2), FontFace.HersheyComplex, 1.0, new Bgr(Color.Orange).MCvScalar);
                            CvInvoke.Rectangle(currentFrame, face, new Bgr(Color.Green).MCvScalar, 2);
                            isExist = true;
                        }
                        else
                        {
                            CvInvoke.PutText(currentFrame, "Unknown", new Point(face.X - 2, face.Y - 2), FontFace.HersheyComplex, 1.0, new Bgr(Color.Orange).MCvScalar);
                            CvInvoke.Rectangle(currentFrame, face, new Bgr(Color.Green).MCvScalar, 2);
                            isExist = false;
                        }
                    }
                }
            }
            imageBoxFrameGrabber.Image = currentFrame.ToBitmap();
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Please input your name to add face");
            }
            else
            {
                gray = grabber.QueryFrame().ToImage<Gray, Byte>().Resize(320, 240, Inter.Cubic);
                Rectangle[] faces = face.DetectMultiScale(gray, 1.1, 3, Size.Empty, Size.Empty);

                if (faces.Length > 0)
                {
                    trainingImages.Add(resultImage);
                    labels.Add(Convert.ToInt32(textBox1.Text));
                    /*File.WriteAllText(Application.StartupPath + "/TrainedFaces/TrainedLabels.txt", trainingImages.Count.ToString() + "%");

                    for (int i = 0; i < trainingImages.Count; i++)
                    {
                        trainingImages[i].Save(Application.StartupPath + "/TrainedFaces/face" + i + ".bmp");
                        File.AppendAllText(Application.StartupPath + "/TrainedFaces/TrainedLabels.txt", labels[i] + "%");

                    }*/
                   
                    if (!isUpdate)
                    {
                        if (isExist)
                        {
                            MessageBox.Show("Khuôn mặt đã tồn tại trong cơ sở dữ liệu", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else if (FaceRecognizeHelper.addFace(resultImage, Convert.ToInt32(textBox1.Text)))
                        {
                            imageBox1.Image = resultImage.ToBitmap();
                            MessageBox.Show("Thêm nhận diện khuôn mặt cho " + employeeID.ToString() + "thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Application.Idle -= FrameGrabber;
                            if (grabber != null)
                            {
                                grabber.Stop();
                                grabber.Dispose();
                            }
                            Close();
                        }
                    }
                    else
                    {
                        if (FaceRecognizeHelper.updateFace(resultImage, Convert.ToInt32(textBox1.Text)))
                        {
                            MessageBox.Show("Thay đổi nhận diện khuôn mặt cho " + employeeID.ToString() + "thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Application.Idle -= FrameGrabber;
                            if (grabber != null)
                            {
                                grabber.Stop();
                                grabber.Dispose();
                            }
                            Close();
                        }
                    }
                }
            }
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            Application.Idle -= FrameGrabber;
            if (grabber != null)
            {
                grabber.Stop();
                grabber.Dispose();
            }
            Close();
        }
    }
}
