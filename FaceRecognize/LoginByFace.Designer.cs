namespace NhaKhoaCuoiKy.FaceRecognize
{
    partial class LoginByFace
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginByFace));
            imageBoxFrameGrabber = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)imageBoxFrameGrabber).BeginInit();
            SuspendLayout();
            // 
            // imageBoxFrameGrabber
            // 
            imageBoxFrameGrabber.Location = new Point(54, 37);
            imageBoxFrameGrabber.Name = "imageBoxFrameGrabber";
            imageBoxFrameGrabber.Size = new Size(491, 423);
            imageBoxFrameGrabber.SizeMode = PictureBoxSizeMode.StretchImage;
            imageBoxFrameGrabber.TabIndex = 1;
            imageBoxFrameGrabber.TabStop = false;
            // 
            // LoginByFace
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(595, 495);
            Controls.Add(imageBoxFrameGrabber);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "LoginByFace";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "NHA KHOA FS";
            Load += LoginByFace_Load;
            ((System.ComponentModel.ISupportInitialize)imageBoxFrameGrabber).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox imageBoxFrameGrabber;
    }
}