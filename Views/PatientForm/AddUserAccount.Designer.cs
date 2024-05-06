namespace NhaKhoaCuoiKy.Views.PatientForm
{
    partial class AddUserAccount
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges11 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges12 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            pb_name = new PictureBox();
            label1 = new Label();
            tb_name = new Guna.UI2.WinForms.Guna2TextBox();
            cbb_decentralization = new Guna.UI2.WinForms.Guna2ComboBox();
            label2 = new Label();
            pictureBox1 = new PictureBox();
            label3 = new Label();
            tb_password = new Guna.UI2.WinForms.Guna2TextBox();
            pb_confirm = new PictureBox();
            label4 = new Label();
            tb_confirm = new Guna.UI2.WinForms.Guna2TextBox();
            btn_add = new Guna.UI2.WinForms.Guna2Button();
            btn_cancel = new Guna.UI2.WinForms.Guna2Button();
            ((System.ComponentModel.ISupportInitialize)pb_name).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pb_confirm).BeginInit();
            SuspendLayout();
            // 
            // pb_name
            // 
            pb_name.Image = Properties.Resources.icons8_warning_96;
            pb_name.Location = new Point(316, 77);
            pb_name.Name = "pb_name";
            pb_name.Size = new Size(25, 25);
            pb_name.SizeMode = PictureBoxSizeMode.StretchImage;
            pb_name.TabIndex = 34;
            pb_name.TabStop = false;
            pb_name.Visible = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(107, 47);
            label1.Name = "label1";
            label1.Size = new Size(110, 20);
            label1.TabIndex = 33;
            label1.Text = "Tên đăng nhập:";
            // 
            // tb_name
            // 
            tb_name.BorderColor = Color.Black;
            tb_name.BorderRadius = 15;
            tb_name.BorderThickness = 0;
            tb_name.CustomizableEdges = customizableEdges1;
            tb_name.DefaultText = "";
            tb_name.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            tb_name.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            tb_name.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            tb_name.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            tb_name.FillColor = Color.Gainsboro;
            tb_name.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            tb_name.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            tb_name.ForeColor = Color.Black;
            tb_name.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            tb_name.Location = new Point(107, 70);
            tb_name.Name = "tb_name";
            tb_name.PasswordChar = '\0';
            tb_name.PlaceholderForeColor = Color.Gray;
            tb_name.PlaceholderText = "";
            tb_name.SelectedText = "";
            tb_name.ShadowDecoration.CustomizableEdges = customizableEdges2;
            tb_name.Size = new Size(203, 36);
            tb_name.TabIndex = 32;
            tb_name.TextChanged += tb_name_TextChanged;
            // 
            // cbb_decentralization
            // 
            cbb_decentralization.BackColor = Color.Transparent;
            cbb_decentralization.BorderRadius = 15;
            cbb_decentralization.CustomizableEdges = customizableEdges3;
            cbb_decentralization.DrawMode = DrawMode.OwnerDrawFixed;
            cbb_decentralization.DropDownStyle = ComboBoxStyle.DropDownList;
            cbb_decentralization.FocusedColor = Color.FromArgb(94, 148, 255);
            cbb_decentralization.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            cbb_decentralization.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            cbb_decentralization.ForeColor = Color.Black;
            cbb_decentralization.ItemHeight = 30;
            cbb_decentralization.Items.AddRange(new object[] { "-Chọn quyền-", "0", "1", "2", "3" });
            cbb_decentralization.Location = new Point(439, 70);
            cbb_decentralization.Name = "cbb_decentralization";
            cbb_decentralization.ShadowDecoration.CustomizableEdges = customizableEdges4;
            cbb_decentralization.Size = new Size(203, 36);
            cbb_decentralization.StartIndex = 0;
            cbb_decentralization.TabIndex = 35;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(439, 47);
            label2.Name = "label2";
            label2.Size = new Size(88, 20);
            label2.TabIndex = 36;
            label2.Text = "Phân quyền:";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.icons8_warning_96;
            pictureBox1.Location = new Point(316, 178);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(25, 25);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 39;
            pictureBox1.TabStop = false;
            pictureBox1.Visible = false;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(107, 148);
            label3.Name = "label3";
            label3.Size = new Size(73, 20);
            label3.TabIndex = 38;
            label3.Text = "Mật khẩu:";
            // 
            // tb_password
            // 
            tb_password.BorderColor = Color.Black;
            tb_password.BorderRadius = 15;
            tb_password.BorderThickness = 0;
            tb_password.CustomizableEdges = customizableEdges5;
            tb_password.DefaultText = "";
            tb_password.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            tb_password.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            tb_password.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            tb_password.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            tb_password.FillColor = Color.Gainsboro;
            tb_password.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            tb_password.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            tb_password.ForeColor = Color.Black;
            tb_password.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            tb_password.Location = new Point(107, 171);
            tb_password.Name = "tb_password";
            tb_password.PasswordChar = '*';
            tb_password.PlaceholderForeColor = Color.Gray;
            tb_password.PlaceholderText = "";
            tb_password.SelectedText = "";
            tb_password.ShadowDecoration.CustomizableEdges = customizableEdges6;
            tb_password.Size = new Size(203, 36);
            tb_password.TabIndex = 37;
            // 
            // pb_confirm
            // 
            pb_confirm.Image = Properties.Resources.icons8_warning_96;
            pb_confirm.Location = new Point(648, 174);
            pb_confirm.Name = "pb_confirm";
            pb_confirm.Size = new Size(25, 25);
            pb_confirm.SizeMode = PictureBoxSizeMode.StretchImage;
            pb_confirm.TabIndex = 42;
            pb_confirm.TabStop = false;
            pb_confirm.Visible = false;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(439, 144);
            label4.Name = "label4";
            label4.Size = new Size(137, 20);
            label4.TabIndex = 41;
            label4.Text = "Xác nhận mật khẩu:";
            // 
            // tb_confirm
            // 
            tb_confirm.BorderColor = Color.Black;
            tb_confirm.BorderRadius = 15;
            tb_confirm.BorderThickness = 0;
            tb_confirm.CustomizableEdges = customizableEdges7;
            tb_confirm.DefaultText = "";
            tb_confirm.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            tb_confirm.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            tb_confirm.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            tb_confirm.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            tb_confirm.FillColor = Color.Gainsboro;
            tb_confirm.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            tb_confirm.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            tb_confirm.ForeColor = Color.Black;
            tb_confirm.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            tb_confirm.Location = new Point(439, 167);
            tb_confirm.Name = "tb_confirm";
            tb_confirm.PasswordChar = '*';
            tb_confirm.PlaceholderForeColor = Color.Gray;
            tb_confirm.PlaceholderText = "";
            tb_confirm.SelectedText = "";
            tb_confirm.ShadowDecoration.CustomizableEdges = customizableEdges8;
            tb_confirm.Size = new Size(203, 36);
            tb_confirm.TabIndex = 40;
            tb_confirm.TextChanged += tb_confirm_TextChanged;
            // 
            // btn_add
            // 
            btn_add.BackColor = Color.Transparent;
            btn_add.BorderRadius = 15;
            btn_add.BorderThickness = 1;
            btn_add.CustomizableEdges = customizableEdges9;
            btn_add.DisabledState.BorderColor = Color.DarkGray;
            btn_add.DisabledState.CustomBorderColor = Color.DarkGray;
            btn_add.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btn_add.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btn_add.FillColor = Color.FromArgb(17, 34, 71);
            btn_add.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            btn_add.ForeColor = Color.White;
            btn_add.Location = new Point(429, 262);
            btn_add.Name = "btn_add";
            btn_add.ShadowDecoration.CustomizableEdges = customizableEdges10;
            btn_add.Size = new Size(113, 36);
            btn_add.TabIndex = 44;
            btn_add.Text = "Thêm";
            btn_add.Click += btn_add_Click;
            // 
            // btn_cancel
            // 
            btn_cancel.BorderRadius = 15;
            btn_cancel.BorderThickness = 1;
            btn_cancel.CustomizableEdges = customizableEdges11;
            btn_cancel.DisabledState.BorderColor = Color.DarkGray;
            btn_cancel.DisabledState.CustomBorderColor = Color.DarkGray;
            btn_cancel.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btn_cancel.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btn_cancel.FillColor = Color.White;
            btn_cancel.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            btn_cancel.ForeColor = Color.Black;
            btn_cancel.Location = new Point(211, 262);
            btn_cancel.Name = "btn_cancel";
            btn_cancel.ShadowDecoration.CustomizableEdges = customizableEdges12;
            btn_cancel.Size = new Size(113, 36);
            btn_cancel.TabIndex = 43;
            btn_cancel.Text = "Hủy";
            btn_cancel.Click += btn_cancel_Click;
            // 
            // AddUserAccount
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(763, 324);
            Controls.Add(btn_add);
            Controls.Add(btn_cancel);
            Controls.Add(pb_confirm);
            Controls.Add(label4);
            Controls.Add(tb_confirm);
            Controls.Add(pictureBox1);
            Controls.Add(label3);
            Controls.Add(tb_password);
            Controls.Add(label2);
            Controls.Add(cbb_decentralization);
            Controls.Add(pb_name);
            Controls.Add(label1);
            Controls.Add(tb_name);
            FormBorderStyle = FormBorderStyle.None;
            Name = "AddUserAccount";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "AddUserAccount";
            ((System.ComponentModel.ISupportInitialize)pb_name).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pb_confirm).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pb_name;
        private Label label1;
        private Guna.UI2.WinForms.Guna2TextBox tb_name;
        private Guna.UI2.WinForms.Guna2ComboBox cbb_decentralization;
        private Label label2;
        private PictureBox pictureBox1;
        private Label label3;
        private Guna.UI2.WinForms.Guna2TextBox tb_password;
        private PictureBox pb_confirm;
        private Label label4;
        private Guna.UI2.WinForms.Guna2TextBox tb_confirm;
        private Guna.UI2.WinForms.Guna2Button btn_add;
        private Guna.UI2.WinForms.Guna2Button btn_cancel;
    }
}