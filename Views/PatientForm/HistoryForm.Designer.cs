namespace NhaKhoaCuoiKy.Views.PatientForm
{
    partial class HistoryForm
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle7 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle8 = new DataGridViewCellStyle();
            panel1 = new Panel();
            btn_back = new Guna.UI2.WinForms.Guna2CircleButton();
            pictureBox1 = new PictureBox();
            label2 = new Label();
            label1 = new Label();
            label3 = new Label();
            data_record = new Guna.UI2.WinForms.Guna2DataGridView();
            MaBenhNhan = new DataGridViewTextBoxColumn();
            Column1 = new DataGridViewTextBoxColumn();
            Column2 = new DataGridViewTextBoxColumn();
            Column3 = new DataGridViewTextBoxColumn();
            Column4 = new DataGridViewTextBoxColumn();
            col_btn_record_info = new DataGridViewButtonColumn();
            col_btn_record_delete = new DataGridViewButtonColumn();
            data_invoice = new Guna.UI2.WinForms.Guna2DataGridView();
            dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            col_btn_invoice_info = new DataGridViewButtonColumn();
            col_btn_invoice_delete = new DataGridViewButtonColumn();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)data_record).BeginInit();
            ((System.ComponentModel.ISupportInitialize)data_invoice).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(17, 34, 71);
            panel1.Controls.Add(btn_back);
            panel1.Controls.Add(pictureBox1);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(label3);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1214, 163);
            panel1.TabIndex = 7;
            // 
            // btn_back
            // 
            btn_back.DisabledState.BorderColor = Color.DarkGray;
            btn_back.DisabledState.CustomBorderColor = Color.DarkGray;
            btn_back.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btn_back.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btn_back.FillColor = Color.White;
            btn_back.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            btn_back.ForeColor = Color.White;
            btn_back.Image = Properties.Resources.icons8_back_100__1_;
            btn_back.Location = new Point(28, 27);
            btn_back.Name = "btn_back";
            btn_back.ShadowDecoration.CustomizableEdges = customizableEdges1;
            btn_back.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            btn_back.Size = new Size(40, 40);
            btn_back.TabIndex = 7;
            btn_back.Click += btn_back_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.dental2;
            pictureBox1.Location = new Point(847, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(298, 129);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 6;
            pictureBox1.TabStop = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label2.ForeColor = Color.White;
            label2.Location = new Point(175, 30);
            label2.Name = "label2";
            label2.Size = new Size(203, 22);
            label2.TabIndex = 3;
            label2.Text = "Sở Y tế: Thành phố UTE";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Times New Roman", 20F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = Color.White;
            label1.Location = new Point(435, 17);
            label1.Name = "label1";
            label1.Size = new Size(376, 38);
            label1.TabIndex = 5;
            label1.Text = "LỊCH SỬ PHÒNG NHA";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Times New Roman", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label3.ForeColor = Color.White;
            label3.Location = new Point(175, 81);
            label3.Name = "label3";
            label3.Size = new Size(156, 22);
            label3.TabIndex = 4;
            label3.Text = "Phòng nha: FSUNI";
            // 
            // data_record
            // 
            data_record.AllowUserToAddRows = false;
            data_record.AllowUserToDeleteRows = false;
            data_record.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(192, 192, 255);
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = Color.FromArgb(192, 192, 255);
            dataGridViewCellStyle1.SelectionForeColor = Color.Black;
            data_record.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(17, 34, 71);
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(17, 34, 71);
            dataGridViewCellStyle2.SelectionForeColor = Color.White;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            data_record.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            data_record.ColumnHeadersHeight = 46;
            data_record.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            data_record.Columns.AddRange(new DataGridViewColumn[] { MaBenhNhan, Column1, Column2, Column3, Column4, col_btn_record_info, col_btn_record_delete });
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.FromArgb(192, 192, 255);
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle3.ForeColor = Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(192, 192, 255);
            dataGridViewCellStyle3.SelectionForeColor = Color.Black;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            data_record.DefaultCellStyle = dataGridViewCellStyle3;
            data_record.GridColor = Color.FromArgb(231, 229, 255);
            data_record.Location = new Point(28, 198);
            data_record.Name = "data_record";
            data_record.ReadOnly = true;
            data_record.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = Color.White;
            dataGridViewCellStyle4.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle4.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = Color.White;
            dataGridViewCellStyle4.SelectionForeColor = Color.Black;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
            data_record.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            data_record.RowHeadersVisible = false;
            data_record.RowHeadersWidth = 55;
            data_record.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            data_record.RowTemplate.Height = 29;
            data_record.ScrollBars = ScrollBars.Vertical;
            data_record.Size = new Size(783, 541);
            data_record.TabIndex = 8;
            data_record.ThemeStyle.AlternatingRowsStyle.BackColor = Color.White;
            data_record.ThemeStyle.AlternatingRowsStyle.Font = null;
            data_record.ThemeStyle.AlternatingRowsStyle.ForeColor = Color.Empty;
            data_record.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = Color.Empty;
            data_record.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = Color.Empty;
            data_record.ThemeStyle.BackColor = Color.White;
            data_record.ThemeStyle.GridColor = Color.FromArgb(231, 229, 255);
            data_record.ThemeStyle.HeaderStyle.BackColor = Color.FromArgb(100, 88, 255);
            data_record.ThemeStyle.HeaderStyle.BorderStyle = DataGridViewHeaderBorderStyle.None;
            data_record.ThemeStyle.HeaderStyle.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            data_record.ThemeStyle.HeaderStyle.ForeColor = Color.White;
            data_record.ThemeStyle.HeaderStyle.HeaightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            data_record.ThemeStyle.HeaderStyle.Height = 46;
            data_record.ThemeStyle.ReadOnly = true;
            data_record.ThemeStyle.RowsStyle.BackColor = Color.White;
            data_record.ThemeStyle.RowsStyle.BorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            data_record.ThemeStyle.RowsStyle.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            data_record.ThemeStyle.RowsStyle.ForeColor = Color.Transparent;
            data_record.ThemeStyle.RowsStyle.Height = 29;
            data_record.ThemeStyle.RowsStyle.SelectionBackColor = Color.FromArgb(231, 229, 255);
            data_record.ThemeStyle.RowsStyle.SelectionForeColor = Color.FromArgb(71, 69, 94);
            data_record.CellContentClick += data_record_CellContentClick;
            // 
            // MaBenhNhan
            // 
            MaBenhNhan.HeaderText = "Mã bác sĩ";
            MaBenhNhan.MinimumWidth = 6;
            MaBenhNhan.Name = "MaBenhNhan";
            MaBenhNhan.ReadOnly = true;
            // 
            // Column1
            // 
            Column1.HeaderText = "Họ tên";
            Column1.MinimumWidth = 6;
            Column1.Name = "Column1";
            Column1.ReadOnly = true;
            // 
            // Column2
            // 
            Column2.HeaderText = "Mã bệnh án";
            Column2.MinimumWidth = 6;
            Column2.Name = "Column2";
            Column2.ReadOnly = true;
            // 
            // Column3
            // 
            Column3.HeaderText = "Chẩn đoán";
            Column3.MinimumWidth = 6;
            Column3.Name = "Column3";
            Column3.ReadOnly = true;
            // 
            // Column4
            // 
            Column4.HeaderText = "Ngày lập";
            Column4.MinimumWidth = 6;
            Column4.Name = "Column4";
            Column4.ReadOnly = true;
            // 
            // col_btn_record_info
            // 
            col_btn_record_info.FillWeight = 70F;
            col_btn_record_info.HeaderText = "Thông tin";
            col_btn_record_info.MinimumWidth = 6;
            col_btn_record_info.Name = "col_btn_record_info";
            col_btn_record_info.ReadOnly = true;
            col_btn_record_info.Text = "Xem";
            col_btn_record_info.UseColumnTextForButtonValue = true;
            // 
            // col_btn_record_delete
            // 
            col_btn_record_delete.FillWeight = 40F;
            col_btn_record_delete.HeaderText = "Xóa";
            col_btn_record_delete.MinimumWidth = 6;
            col_btn_record_delete.Name = "col_btn_record_delete";
            col_btn_record_delete.ReadOnly = true;
            col_btn_record_delete.Text = "Xóa";
            col_btn_record_delete.UseColumnTextForButtonValue = true;
            // 
            // data_invoice
            // 
            data_invoice.AllowUserToAddRows = false;
            data_invoice.AllowUserToDeleteRows = false;
            data_invoice.AllowUserToResizeRows = false;
            dataGridViewCellStyle5.BackColor = Color.FromArgb(192, 192, 255);
            dataGridViewCellStyle5.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle5.ForeColor = Color.Black;
            dataGridViewCellStyle5.SelectionBackColor = Color.FromArgb(192, 192, 255);
            dataGridViewCellStyle5.SelectionForeColor = Color.Black;
            data_invoice.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = Color.FromArgb(17, 34, 71);
            dataGridViewCellStyle6.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle6.ForeColor = Color.White;
            dataGridViewCellStyle6.SelectionBackColor = Color.FromArgb(17, 34, 71);
            dataGridViewCellStyle6.SelectionForeColor = Color.White;
            dataGridViewCellStyle6.WrapMode = DataGridViewTriState.True;
            data_invoice.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            data_invoice.ColumnHeadersHeight = 46;
            data_invoice.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            data_invoice.Columns.AddRange(new DataGridViewColumn[] { dataGridViewTextBoxColumn1, dataGridViewTextBoxColumn2, col_btn_invoice_info, col_btn_invoice_delete });
            dataGridViewCellStyle7.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = Color.FromArgb(192, 192, 255);
            dataGridViewCellStyle7.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle7.ForeColor = Color.Black;
            dataGridViewCellStyle7.SelectionBackColor = Color.FromArgb(192, 192, 255);
            dataGridViewCellStyle7.SelectionForeColor = Color.Black;
            dataGridViewCellStyle7.WrapMode = DataGridViewTriState.False;
            data_invoice.DefaultCellStyle = dataGridViewCellStyle7;
            data_invoice.GridColor = Color.FromArgb(231, 229, 255);
            data_invoice.Location = new Point(831, 198);
            data_invoice.Name = "data_invoice";
            data_invoice.ReadOnly = true;
            data_invoice.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle8.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = Color.White;
            dataGridViewCellStyle8.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle8.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = Color.White;
            dataGridViewCellStyle8.SelectionForeColor = Color.Black;
            dataGridViewCellStyle8.WrapMode = DataGridViewTriState.True;
            data_invoice.RowHeadersDefaultCellStyle = dataGridViewCellStyle8;
            data_invoice.RowHeadersVisible = false;
            data_invoice.RowHeadersWidth = 55;
            data_invoice.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            data_invoice.RowTemplate.Height = 29;
            data_invoice.ScrollBars = ScrollBars.Vertical;
            data_invoice.Size = new Size(371, 541);
            data_invoice.TabIndex = 11;
            data_invoice.ThemeStyle.AlternatingRowsStyle.BackColor = Color.White;
            data_invoice.ThemeStyle.AlternatingRowsStyle.Font = null;
            data_invoice.ThemeStyle.AlternatingRowsStyle.ForeColor = Color.Empty;
            data_invoice.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = Color.Empty;
            data_invoice.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = Color.Empty;
            data_invoice.ThemeStyle.BackColor = Color.White;
            data_invoice.ThemeStyle.GridColor = Color.FromArgb(231, 229, 255);
            data_invoice.ThemeStyle.HeaderStyle.BackColor = Color.FromArgb(100, 88, 255);
            data_invoice.ThemeStyle.HeaderStyle.BorderStyle = DataGridViewHeaderBorderStyle.None;
            data_invoice.ThemeStyle.HeaderStyle.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            data_invoice.ThemeStyle.HeaderStyle.ForeColor = Color.White;
            data_invoice.ThemeStyle.HeaderStyle.HeaightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            data_invoice.ThemeStyle.HeaderStyle.Height = 46;
            data_invoice.ThemeStyle.ReadOnly = true;
            data_invoice.ThemeStyle.RowsStyle.BackColor = Color.White;
            data_invoice.ThemeStyle.RowsStyle.BorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            data_invoice.ThemeStyle.RowsStyle.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            data_invoice.ThemeStyle.RowsStyle.ForeColor = Color.Transparent;
            data_invoice.ThemeStyle.RowsStyle.Height = 29;
            data_invoice.ThemeStyle.RowsStyle.SelectionBackColor = Color.FromArgb(231, 229, 255);
            data_invoice.ThemeStyle.RowsStyle.SelectionForeColor = Color.FromArgb(71, 69, 94);
            data_invoice.CellContentClick += data_invoice_CellContentClick;
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewTextBoxColumn1.HeaderText = "Mã hóa đơn";
            dataGridViewTextBoxColumn1.MinimumWidth = 6;
            dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            dataGridViewTextBoxColumn2.HeaderText = "Ngày tạo";
            dataGridViewTextBoxColumn2.MinimumWidth = 6;
            dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // col_btn_invoice_info
            // 
            col_btn_invoice_info.FillWeight = 70F;
            col_btn_invoice_info.HeaderText = "Thông tin";
            col_btn_invoice_info.MinimumWidth = 6;
            col_btn_invoice_info.Name = "col_btn_invoice_info";
            col_btn_invoice_info.ReadOnly = true;
            col_btn_invoice_info.Text = "Xem";
            col_btn_invoice_info.UseColumnTextForButtonValue = true;
            // 
            // col_btn_invoice_delete
            // 
            col_btn_invoice_delete.FillWeight = 40F;
            col_btn_invoice_delete.HeaderText = "Xóa";
            col_btn_invoice_delete.MinimumWidth = 6;
            col_btn_invoice_delete.Name = "col_btn_invoice_delete";
            col_btn_invoice_delete.ReadOnly = true;
            col_btn_invoice_delete.Text = "Xóa";
            col_btn_invoice_delete.UseColumnTextForButtonValue = true;
            // 
            // HistoryForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1214, 806);
            Controls.Add(data_invoice);
            Controls.Add(data_record);
            Controls.Add(panel1);
            Name = "HistoryForm";
            Text = "HistoryForm";
            Load += HistoryForm_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)data_record).EndInit();
            ((System.ComponentModel.ISupportInitialize)data_invoice).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private PictureBox pictureBox1;
        private Label label2;
        private Label label1;
        private Label label3;
        private Guna.UI2.WinForms.Guna2CircleButton btn_back;
        public Guna.UI2.WinForms.Guna2DataGridView data_record;
        public Guna.UI2.WinForms.Guna2DataGridView data_invoice;
        private DataGridViewTextBoxColumn MaBenhNhan;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column3;
        private DataGridViewTextBoxColumn Column4;
        private DataGridViewButtonColumn col_btn_record_info;
        private DataGridViewButtonColumn col_btn_record_delete;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewButtonColumn col_btn_invoice_info;
        private DataGridViewButtonColumn col_btn_invoice_delete;
    }
}