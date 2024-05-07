namespace NhaKhoaCuoiKy.Views.LogIn
{
    partial class UserManagement
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserManagement));
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            btn_refresh = new Guna.UI2.WinForms.Guna2Button();
            btn_search = new Guna.UI2.WinForms.Guna2Button();
            guna2GroupBox2 = new Guna.UI2.WinForms.Guna2GroupBox();
            data_account = new Guna.UI2.WinForms.Guna2DataGridView();
            tb_filter_search = new Guna.UI2.WinForms.Guna2TextBox();
            cb_filter = new Guna.UI2.WinForms.Guna2ComboBox();
            MaBenhNhan = new DataGridViewTextBoxColumn();
            Column1 = new DataGridViewTextBoxColumn();
            Column2 = new DataGridViewTextBoxColumn();
            Column3 = new DataGridViewTextBoxColumn();
            col_btn_add = new DataGridViewButtonColumn();
            col_btn_addFace = new DataGridViewButtonColumn();
            col_btn_delete = new DataGridViewButtonColumn();
            guna2GroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)data_account).BeginInit();
            SuspendLayout();
            // 
            // btn_refresh
            // 
            btn_refresh.BorderRadius = 5;
            btn_refresh.CustomizableEdges = customizableEdges1;
            btn_refresh.DisabledState.BorderColor = Color.DarkGray;
            btn_refresh.DisabledState.CustomBorderColor = Color.DarkGray;
            btn_refresh.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btn_refresh.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btn_refresh.FillColor = Color.FromArgb(17, 34, 71);
            btn_refresh.Font = new Font("Segoe UI", 9F);
            btn_refresh.ForeColor = Color.White;
            btn_refresh.Image = (Image)resources.GetObject("btn_refresh.Image");
            btn_refresh.ImageAlign = HorizontalAlignment.Left;
            btn_refresh.Location = new Point(684, 12);
            btn_refresh.Name = "btn_refresh";
            btn_refresh.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btn_refresh.Size = new Size(153, 36);
            btn_refresh.TabIndex = 29;
            btn_refresh.Text = "Làm mới";
            btn_refresh.Click += btn_refresh_Click;
            // 
            // btn_search
            // 
            btn_search.BorderRadius = 5;
            btn_search.CustomizableEdges = customizableEdges3;
            btn_search.DisabledState.BorderColor = Color.DarkGray;
            btn_search.DisabledState.CustomBorderColor = Color.DarkGray;
            btn_search.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btn_search.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btn_search.FillColor = Color.FromArgb(17, 34, 71);
            btn_search.Font = new Font("Segoe UI", 10F);
            btn_search.ForeColor = Color.White;
            btn_search.Image = Properties.Resources.icons8_search_500;
            btn_search.ImageAlign = HorizontalAlignment.Left;
            btn_search.Location = new Point(511, 12);
            btn_search.Name = "btn_search";
            btn_search.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btn_search.Size = new Size(154, 36);
            btn_search.TabIndex = 25;
            btn_search.Text = "Tìm";
            btn_search.Click += btn_search_Click;
            // 
            // guna2GroupBox2
            // 
            guna2GroupBox2.BorderColor = Color.Black;
            guna2GroupBox2.BorderRadius = 12;
            guna2GroupBox2.Controls.Add(data_account);
            guna2GroupBox2.CustomBorderColor = Color.Transparent;
            guna2GroupBox2.CustomizableEdges = customizableEdges5;
            guna2GroupBox2.FillColor = Color.Transparent;
            guna2GroupBox2.Font = new Font("Segoe UI", 9F);
            guna2GroupBox2.ForeColor = Color.Transparent;
            guna2GroupBox2.Location = new Point(19, 79);
            guna2GroupBox2.Name = "guna2GroupBox2";
            guna2GroupBox2.ShadowDecoration.CustomizableEdges = customizableEdges6;
            guna2GroupBox2.Size = new Size(1158, 732);
            guna2GroupBox2.TabIndex = 26;
            guna2GroupBox2.Text = "guna2GroupBox2";
            // 
            // data_account
            // 
            data_account.AllowUserToAddRows = false;
            data_account.AllowUserToDeleteRows = false;
            data_account.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(192, 192, 255);
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle1.ForeColor = Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = Color.FromArgb(192, 192, 255);
            dataGridViewCellStyle1.SelectionForeColor = Color.Black;
            data_account.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(17, 34, 71);
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(17, 34, 71);
            dataGridViewCellStyle2.SelectionForeColor = Color.White;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            data_account.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            data_account.ColumnHeadersHeight = 46;
            data_account.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            data_account.Columns.AddRange(new DataGridViewColumn[] { MaBenhNhan, Column1, Column2, Column3, col_btn_add, col_btn_addFace, col_btn_delete });
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.FromArgb(192, 192, 255);
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(192, 192, 255);
            dataGridViewCellStyle3.SelectionForeColor = Color.Black;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            data_account.DefaultCellStyle = dataGridViewCellStyle3;
            data_account.GridColor = Color.FromArgb(231, 229, 255);
            data_account.Location = new Point(40, 36);
            data_account.Name = "data_account";
            data_account.ReadOnly = true;
            data_account.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = Color.White;
            dataGridViewCellStyle4.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle4.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = Color.White;
            dataGridViewCellStyle4.SelectionForeColor = Color.Black;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
            data_account.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            data_account.RowHeadersVisible = false;
            data_account.RowHeadersWidth = 55;
            data_account.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            data_account.RowTemplate.Height = 29;
            data_account.ScrollBars = ScrollBars.Vertical;
            data_account.Size = new Size(1080, 591);
            data_account.TabIndex = 6;
            data_account.ThemeStyle.AlternatingRowsStyle.BackColor = Color.White;
            data_account.ThemeStyle.AlternatingRowsStyle.Font = null;
            data_account.ThemeStyle.AlternatingRowsStyle.ForeColor = Color.Empty;
            data_account.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = Color.Empty;
            data_account.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = Color.Empty;
            data_account.ThemeStyle.BackColor = Color.White;
            data_account.ThemeStyle.GridColor = Color.FromArgb(231, 229, 255);
            data_account.ThemeStyle.HeaderStyle.BackColor = Color.FromArgb(100, 88, 255);
            data_account.ThemeStyle.HeaderStyle.BorderStyle = DataGridViewHeaderBorderStyle.None;
            data_account.ThemeStyle.HeaderStyle.Font = new Font("Segoe UI", 9F);
            data_account.ThemeStyle.HeaderStyle.ForeColor = Color.White;
            data_account.ThemeStyle.HeaderStyle.HeaightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            data_account.ThemeStyle.HeaderStyle.Height = 46;
            data_account.ThemeStyle.ReadOnly = true;
            data_account.ThemeStyle.RowsStyle.BackColor = Color.White;
            data_account.ThemeStyle.RowsStyle.BorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            data_account.ThemeStyle.RowsStyle.Font = new Font("Segoe UI", 9F);
            data_account.ThemeStyle.RowsStyle.ForeColor = Color.Transparent;
            data_account.ThemeStyle.RowsStyle.Height = 29;
            data_account.ThemeStyle.RowsStyle.SelectionBackColor = Color.FromArgb(231, 229, 255);
            data_account.ThemeStyle.RowsStyle.SelectionForeColor = Color.FromArgb(71, 69, 94);
            data_account.CellContentClick += data_account_CellContentClick;
            // 
            // tb_filter_search
            // 
            tb_filter_search.CustomizableEdges = customizableEdges7;
            tb_filter_search.DefaultText = "";
            tb_filter_search.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            tb_filter_search.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            tb_filter_search.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            tb_filter_search.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            tb_filter_search.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            tb_filter_search.Font = new Font("Segoe UI", 9F);
            tb_filter_search.ForeColor = Color.Black;
            tb_filter_search.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            tb_filter_search.Location = new Point(208, 12);
            tb_filter_search.Name = "tb_filter_search";
            tb_filter_search.PasswordChar = '\0';
            tb_filter_search.PlaceholderText = "Nhập ...";
            tb_filter_search.SelectedText = "";
            tb_filter_search.ShadowDecoration.CustomizableEdges = customizableEdges8;
            tb_filter_search.Size = new Size(297, 36);
            tb_filter_search.TabIndex = 24;
            // 
            // cb_filter
            // 
            cb_filter.BackColor = Color.Transparent;
            cb_filter.CustomizableEdges = customizableEdges9;
            cb_filter.DrawMode = DrawMode.OwnerDrawFixed;
            cb_filter.DropDownStyle = ComboBoxStyle.DropDownList;
            cb_filter.FocusedColor = Color.FromArgb(94, 148, 255);
            cb_filter.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            cb_filter.Font = new Font("Segoe UI", 10F);
            cb_filter.ForeColor = Color.Black;
            cb_filter.ItemHeight = 30;
            cb_filter.Items.AddRange(new object[] { "Mã nhân viên", "Tên", "Số điện thoại" });
            cb_filter.Location = new Point(19, 12);
            cb_filter.Name = "cb_filter";
            cb_filter.ShadowDecoration.CustomizableEdges = customizableEdges10;
            cb_filter.Size = new Size(183, 36);
            cb_filter.StartIndex = 0;
            cb_filter.TabIndex = 23;
            // 
            // MaBenhNhan
            // 
            MaBenhNhan.HeaderText = "Mã nhân viên";
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
            Column2.HeaderText = "Chức vụ";
            Column2.MinimumWidth = 6;
            Column2.Name = "Column2";
            Column2.ReadOnly = true;
            // 
            // Column3
            // 
            Column3.HeaderText = "Tình trạng";
            Column3.MinimumWidth = 6;
            Column3.Name = "Column3";
            Column3.ReadOnly = true;
            // 
            // col_btn_add
            // 
            col_btn_add.FillWeight = 70F;
            col_btn_add.HeaderText = "Thêm tài khoản";
            col_btn_add.MinimumWidth = 6;
            col_btn_add.Name = "col_btn_add";
            col_btn_add.ReadOnly = true;
            col_btn_add.Text = "Thêm";
            col_btn_add.UseColumnTextForButtonValue = true;
            // 
            // col_btn_addFace
            // 
            col_btn_addFace.FillWeight = 70F;
            col_btn_addFace.HeaderText = "Khuôn mặt";
            col_btn_addFace.MinimumWidth = 6;
            col_btn_addFace.Name = "col_btn_addFace";
            col_btn_addFace.ReadOnly = true;
            col_btn_addFace.Resizable = DataGridViewTriState.True;
            col_btn_addFace.SortMode = DataGridViewColumnSortMode.Automatic;
            col_btn_addFace.Text = "Thêm";
            col_btn_addFace.UseColumnTextForButtonValue = true;
            // 
            // col_btn_delete
            // 
            col_btn_delete.FillWeight = 70F;
            col_btn_delete.HeaderText = "Xóa tài khoản";
            col_btn_delete.MinimumWidth = 6;
            col_btn_delete.Name = "col_btn_delete";
            col_btn_delete.ReadOnly = true;
            col_btn_delete.Text = "Xóa";
            col_btn_delete.UseColumnTextForButtonValue = true;
            // 
            // UserManagement
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1196, 759);
            Controls.Add(btn_refresh);
            Controls.Add(btn_search);
            Controls.Add(guna2GroupBox2);
            Controls.Add(tb_filter_search);
            Controls.Add(cb_filter);
            Name = "UserManagement";
            Text = "UserManagement";
            Load += UserManagement_Load;
            guna2GroupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)data_account).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2Button btn_refresh;
        private Guna.UI2.WinForms.Guna2Button btn_search;
        private Guna.UI2.WinForms.Guna2GroupBox guna2GroupBox2;
        private Guna.UI2.WinForms.Guna2TextBox tb_filter_search;
        private Guna.UI2.WinForms.Guna2ComboBox cb_filter;
        public Guna.UI2.WinForms.Guna2DataGridView data_account;
        private DataGridViewTextBoxColumn MaBenhNhan;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column3;
        private DataGridViewButtonColumn col_btn_add;
        private DataGridViewButtonColumn col_btn_addFace;
        private DataGridViewButtonColumn col_btn_delete;
    }
}