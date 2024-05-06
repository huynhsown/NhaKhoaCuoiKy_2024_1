namespace NhaKhoaCuoiKy.Views
{
    partial class ListItemService
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            lb_tenDichvu = new Label();
            lb_maDichVu = new Label();
            lb_giaDichVu = new Label();
            lb_giamGia = new Label();
            lb_donVi = new Label();
            btn_edit = new Guna.UI2.WinForms.Guna2Button();
            SuspendLayout();
            // 
            // lb_tenDichvu
            // 
            lb_tenDichvu.AutoSize = true;
            lb_tenDichvu.BackColor = Color.Transparent;
            lb_tenDichvu.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            lb_tenDichvu.ForeColor = Color.Black;
            lb_tenDichvu.Location = new Point(15, 9);
            lb_tenDichvu.Name = "lb_tenDichvu";
            lb_tenDichvu.Size = new Size(113, 28);
            lb_tenDichvu.TabIndex = 0;
            lb_tenDichvu.Text = "Ten Dich Vu";
            // 
            // lb_maDichVu
            // 
            lb_maDichVu.AutoSize = true;
            lb_maDichVu.ForeColor = Color.Black;
            lb_maDichVu.Location = new Point(18, 42);
            lb_maDichVu.Name = "lb_maDichVu";
            lb_maDichVu.Size = new Size(81, 20);
            lb_maDichVu.TabIndex = 1;
            lb_maDichVu.Text = "Mã dịch vụ";
            // 
            // lb_giaDichVu
            // 
            lb_giaDichVu.AutoSize = true;
            lb_giaDichVu.ForeColor = Color.Black;
            lb_giaDichVu.Location = new Point(237, 42);
            lb_giaDichVu.Name = "lb_giaDichVu";
            lb_giaDichVu.Size = new Size(31, 20);
            lb_giaDichVu.TabIndex = 2;
            lb_giaDichVu.Text = "Giá";
            // 
            // lb_giamGia
            // 
            lb_giamGia.AutoSize = true;
            lb_giamGia.ForeColor = Color.Black;
            lb_giamGia.Location = new Point(429, 42);
            lb_giamGia.Name = "lb_giamGia";
            lb_giamGia.Size = new Size(44, 20);
            lb_giamGia.TabIndex = 3;
            lb_giamGia.Text = "Giảm";
            // 
            // lb_donVi
            // 
            lb_donVi.AutoSize = true;
            lb_donVi.ForeColor = Color.Black;
            lb_donVi.Location = new Point(596, 42);
            lb_donVi.Name = "lb_donVi";
            lb_donVi.Size = new Size(52, 20);
            lb_donVi.TabIndex = 4;
            lb_donVi.Text = "Đơn vị";
            // 
            // btn_edit
            // 
            btn_edit.BorderRadius = 15;
            btn_edit.CustomizableEdges = customizableEdges1;
            btn_edit.DisabledState.BorderColor = Color.DarkGray;
            btn_edit.DisabledState.CustomBorderColor = Color.DarkGray;
            btn_edit.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btn_edit.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btn_edit.FillColor = Color.FromArgb(17, 34, 71);
            btn_edit.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            btn_edit.ForeColor = Color.White;
            btn_edit.Location = new Point(774, 19);
            btn_edit.Name = "btn_edit";
            btn_edit.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btn_edit.Size = new Size(111, 34);
            btn_edit.TabIndex = 5;
            btn_edit.Text = "Xem và sửa";
            btn_edit.Click += btn_edit_Click;
            // 
            // ListItemService
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(btn_edit);
            Controls.Add(lb_donVi);
            Controls.Add(lb_giamGia);
            Controls.Add(lb_giaDichVu);
            Controls.Add(lb_maDichVu);
            Controls.Add(lb_tenDichvu);
            Name = "ListItemService";
            Size = new Size(900, 75);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lb_tenDichvu;
        private Label lb_maDichVu;
        private Label lb_giaDichVu;
        private Label lb_giamGia;
        private Label lb_donVi;
        private Guna.UI2.WinForms.Guna2Button btn_edit;
    }
}
