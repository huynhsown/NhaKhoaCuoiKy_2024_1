namespace NhaKhoaCuoiKy.FaceRecognize
{
    partial class LoginRecognize
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            imageBoxFrameGrabber = new PictureBox();
            imageBox1 = new PictureBox();
            btn_add = new Guna.UI2.WinForms.Guna2Button();
            textBox1 = new Guna.UI2.WinForms.Guna2TextBox();
            btn_cancel = new Guna.UI2.WinForms.Guna2Button();
            ((System.ComponentModel.ISupportInitialize)imageBoxFrameGrabber).BeginInit();
            ((System.ComponentModel.ISupportInitialize)imageBox1).BeginInit();
            SuspendLayout();
            // 
            // imageBoxFrameGrabber
            // 
            imageBoxFrameGrabber.Location = new Point(35, 27);
            imageBoxFrameGrabber.Name = "imageBoxFrameGrabber";
            imageBoxFrameGrabber.Size = new Size(330, 344);
            imageBoxFrameGrabber.SizeMode = PictureBoxSizeMode.StretchImage;
            imageBoxFrameGrabber.TabIndex = 0;
            imageBoxFrameGrabber.TabStop = false;
            // 
            // imageBox1
            // 
            imageBox1.Location = new Point(392, 27);
            imageBox1.Name = "imageBox1";
            imageBox1.Size = new Size(190, 190);
            imageBox1.TabIndex = 1;
            imageBox1.TabStop = false;
            // 
            // btn_add
            // 
            btn_add.BackColor = Color.Transparent;
            btn_add.BorderRadius = 15;
            btn_add.BorderThickness = 1;
            btn_add.CustomizableEdges = customizableEdges5;
            btn_add.DisabledState.BorderColor = Color.DarkGray;
            btn_add.DisabledState.CustomBorderColor = Color.DarkGray;
            btn_add.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btn_add.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btn_add.FillColor = Color.FromArgb(17, 34, 71);
            btn_add.Font = new Font("Segoe UI", 10F);
            btn_add.ForeColor = Color.White;
            btn_add.Location = new Point(392, 284);
            btn_add.Name = "btn_add";
            btn_add.ShadowDecoration.CustomizableEdges = customizableEdges6;
            btn_add.Size = new Size(190, 36);
            btn_add.TabIndex = 46;
            btn_add.Text = "Thêm nhận diện";
            btn_add.Click += btn_add_Click;
            // 
            // textBox1
            // 
            textBox1.BorderColor = Color.Black;
            textBox1.BorderRadius = 15;
            textBox1.BorderThickness = 0;
            textBox1.CustomizableEdges = customizableEdges7;
            textBox1.DefaultText = "";
            textBox1.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            textBox1.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            textBox1.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            textBox1.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            textBox1.FillColor = Color.Gainsboro;
            textBox1.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            textBox1.Font = new Font("Segoe UI", 9F);
            textBox1.ForeColor = Color.Black;
            textBox1.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            textBox1.Location = new Point(392, 233);
            textBox1.Name = "textBox1";
            textBox1.PasswordChar = '\0';
            textBox1.PlaceholderForeColor = Color.Black;
            textBox1.PlaceholderText = "";
            textBox1.SelectedText = "";
            textBox1.ShadowDecoration.CustomizableEdges = customizableEdges8;
            textBox1.Size = new Size(190, 36);
            textBox1.TabIndex = 47;
            // 
            // btn_cancel
            // 
            btn_cancel.BorderRadius = 15;
            btn_cancel.BorderThickness = 1;
            btn_cancel.CustomizableEdges = customizableEdges9;
            btn_cancel.DisabledState.BorderColor = Color.DarkGray;
            btn_cancel.DisabledState.CustomBorderColor = Color.DarkGray;
            btn_cancel.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btn_cancel.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btn_cancel.FillColor = Color.White;
            btn_cancel.Font = new Font("Segoe UI", 10F);
            btn_cancel.ForeColor = Color.Black;
            btn_cancel.Location = new Point(392, 335);
            btn_cancel.Name = "btn_cancel";
            btn_cancel.ShadowDecoration.CustomizableEdges = customizableEdges10;
            btn_cancel.Size = new Size(190, 36);
            btn_cancel.TabIndex = 48;
            btn_cancel.Text = "Hủy";
            btn_cancel.Click += btn_cancel_Click;
            // 
            // LoginRecognize
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(636, 428);
            Controls.Add(btn_cancel);
            Controls.Add(textBox1);
            Controls.Add(btn_add);
            Controls.Add(imageBox1);
            Controls.Add(imageBoxFrameGrabber);
            FormBorderStyle = FormBorderStyle.None;
            Name = "LoginRecognize";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "LoginRecognize";
            Load += LoginRecognize_Load;
            ((System.ComponentModel.ISupportInitialize)imageBoxFrameGrabber).EndInit();
            ((System.ComponentModel.ISupportInitialize)imageBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox imageBoxFrameGrabber;
        private PictureBox imageBox1;
        private Guna.UI2.WinForms.Guna2Button btn_add;
        private Guna.UI2.WinForms.Guna2TextBox textBox1;
        private Button button1;
        private Guna.UI2.WinForms.Guna2Button btn_cancel;
    }
}