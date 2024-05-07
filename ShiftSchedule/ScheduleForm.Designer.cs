namespace NhaKhoaCuoiKy.ShiftSchedule
{
    partial class ScheduleForm
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            data_schedule = new Guna.UI2.WinForms.Guna2DataGridView();
            Column8 = new DataGridViewTextBoxColumn();
            Column1 = new DataGridViewTextBoxColumn();
            Column2 = new DataGridViewTextBoxColumn();
            Column3 = new DataGridViewTextBoxColumn();
            Column4 = new DataGridViewTextBoxColumn();
            Column5 = new DataGridViewTextBoxColumn();
            Column6 = new DataGridViewTextBoxColumn();
            Column7 = new DataGridViewTextBoxColumn();
            label_timeinfo = new Label();
            guna2Button1 = new Guna.UI2.WinForms.Guna2Button();
            ((System.ComponentModel.ISupportInitialize)data_schedule).BeginInit();
            SuspendLayout();
            // 
            // data_schedule
            // 
            data_schedule.AllowUserToAddRows = false;
            data_schedule.AllowUserToDeleteRows = false;
            data_schedule.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = Color.White;
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle1.ForeColor = Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = Color.FromArgb(128, 128, 255);
            dataGridViewCellStyle1.SelectionForeColor = Color.Black;
            data_schedule.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(17, 34, 71);
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(17, 34, 71);
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            data_schedule.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            data_schedule.ColumnHeadersHeight = 100;
            data_schedule.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            data_schedule.Columns.AddRange(new DataGridViewColumn[] { Column8, Column1, Column2, Column3, Column4, Column5, Column6, Column7 });
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.White;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(128, 128, 255);
            dataGridViewCellStyle3.SelectionForeColor = Color.Black;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            data_schedule.DefaultCellStyle = dataGridViewCellStyle3;
            data_schedule.GridColor = Color.FromArgb(231, 229, 255);
            data_schedule.Location = new Point(101, 107);
            data_schedule.Name = "data_schedule";
            data_schedule.ReadOnly = true;
            data_schedule.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = Color.White;
            dataGridViewCellStyle4.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle4.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = Color.White;
            dataGridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
            data_schedule.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            data_schedule.RowHeadersVisible = false;
            data_schedule.RowHeadersWidth = 55;
            data_schedule.RowTemplate.Height = 100;
            data_schedule.Size = new Size(1013, 565);
            data_schedule.TabIndex = 8;
            data_schedule.ThemeStyle.AlternatingRowsStyle.BackColor = Color.White;
            data_schedule.ThemeStyle.AlternatingRowsStyle.Font = null;
            data_schedule.ThemeStyle.AlternatingRowsStyle.ForeColor = Color.Empty;
            data_schedule.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = Color.Empty;
            data_schedule.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = Color.Empty;
            data_schedule.ThemeStyle.BackColor = Color.White;
            data_schedule.ThemeStyle.GridColor = Color.FromArgb(231, 229, 255);
            data_schedule.ThemeStyle.HeaderStyle.BackColor = Color.FromArgb(100, 88, 255);
            data_schedule.ThemeStyle.HeaderStyle.BorderStyle = DataGridViewHeaderBorderStyle.None;
            data_schedule.ThemeStyle.HeaderStyle.Font = new Font("Segoe UI", 9F);
            data_schedule.ThemeStyle.HeaderStyle.ForeColor = Color.White;
            data_schedule.ThemeStyle.HeaderStyle.HeaightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            data_schedule.ThemeStyle.HeaderStyle.Height = 100;
            data_schedule.ThemeStyle.ReadOnly = true;
            data_schedule.ThemeStyle.RowsStyle.BackColor = Color.White;
            data_schedule.ThemeStyle.RowsStyle.BorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            data_schedule.ThemeStyle.RowsStyle.Font = new Font("Segoe UI", 9F);
            data_schedule.ThemeStyle.RowsStyle.ForeColor = Color.Transparent;
            data_schedule.ThemeStyle.RowsStyle.Height = 100;
            data_schedule.ThemeStyle.RowsStyle.SelectionBackColor = Color.FromArgb(231, 229, 255);
            data_schedule.ThemeStyle.RowsStyle.SelectionForeColor = Color.FromArgb(71, 69, 94);
            // 
            // Column8
            // 
            Column8.HeaderText = "Buổi";
            Column8.MinimumWidth = 6;
            Column8.Name = "Column8";
            Column8.ReadOnly = true;
            // 
            // Column1
            // 
            Column1.HeaderText = "Thứ hai";
            Column1.MinimumWidth = 6;
            Column1.Name = "Column1";
            Column1.ReadOnly = true;
            // 
            // Column2
            // 
            Column2.HeaderText = "Thứ ba";
            Column2.MinimumWidth = 6;
            Column2.Name = "Column2";
            Column2.ReadOnly = true;
            // 
            // Column3
            // 
            Column3.HeaderText = "Thứ tư";
            Column3.MinimumWidth = 6;
            Column3.Name = "Column3";
            Column3.ReadOnly = true;
            // 
            // Column4
            // 
            Column4.HeaderText = "Thứ năm";
            Column4.MinimumWidth = 6;
            Column4.Name = "Column4";
            Column4.ReadOnly = true;
            // 
            // Column5
            // 
            Column5.HeaderText = "Thứ sáu";
            Column5.MinimumWidth = 6;
            Column5.Name = "Column5";
            Column5.ReadOnly = true;
            // 
            // Column6
            // 
            Column6.HeaderText = "Thứ bảy";
            Column6.MinimumWidth = 6;
            Column6.Name = "Column6";
            Column6.ReadOnly = true;
            // 
            // Column7
            // 
            Column7.HeaderText = "Chủ nhật";
            Column7.MinimumWidth = 6;
            Column7.Name = "Column7";
            Column7.ReadOnly = true;
            // 
            // label_timeinfo
            // 
            label_timeinfo.AutoSize = true;
            label_timeinfo.Font = new Font("Segoe UI", 12F);
            label_timeinfo.Location = new Point(101, 28);
            label_timeinfo.Name = "label_timeinfo";
            label_timeinfo.Size = new Size(65, 28);
            label_timeinfo.TabIndex = 9;
            label_timeinfo.Text = "label1";
            // 
            // guna2Button1
            // 
            guna2Button1.CustomizableEdges = customizableEdges1;
            guna2Button1.DisabledState.BorderColor = Color.DarkGray;
            guna2Button1.DisabledState.CustomBorderColor = Color.DarkGray;
            guna2Button1.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            guna2Button1.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            guna2Button1.Font = new Font("Segoe UI", 9F);
            guna2Button1.ForeColor = Color.White;
            guna2Button1.Location = new Point(864, 28);
            guna2Button1.Name = "guna2Button1";
            guna2Button1.ShadowDecoration.CustomizableEdges = customizableEdges2;
            guna2Button1.Size = new Size(225, 56);
            guna2Button1.TabIndex = 10;
            guna2Button1.Text = "guna2Button1";
            // 
            // ScheduleForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1214, 806);
            Controls.Add(guna2Button1);
            Controls.Add(label_timeinfo);
            Controls.Add(data_schedule);
            Name = "ScheduleForm";
            Text = "ScheduleForm";
            Load += ScheduleForm_Load;
            ((System.ComponentModel.ISupportInitialize)data_schedule).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        public Guna.UI2.WinForms.Guna2DataGridView data_schedule;
        private DataGridViewTextBoxColumn Column8;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column3;
        private DataGridViewTextBoxColumn Column4;
        private DataGridViewTextBoxColumn Column5;
        private DataGridViewTextBoxColumn Column6;
        private DataGridViewTextBoxColumn Column7;
        private Label label_timeinfo;
        private Guna.UI2.WinForms.Guna2Button guna2Button1;
    }
}