namespace NhaKhoaCuoiKy.Views.PatientForm
{
    partial class AddMedicineToInvoice
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
            btn_cancel = new Guna.UI2.WinForms.Guna2Button();
            btn_add = new Guna.UI2.WinForms.Guna2Button();
            cbb_medicine = new Guna.UI2.WinForms.Guna2ComboBox();
            nud_selected = new Guna.UI2.WinForms.Guna2NumericUpDown();
            tb_amount = new Guna.UI2.WinForms.Guna2TextBox();
            ((System.ComponentModel.ISupportInitialize)nud_selected).BeginInit();
            SuspendLayout();
            // 
            // btn_cancel
            // 
            btn_cancel.BorderRadius = 15;
            btn_cancel.BorderThickness = 1;
            btn_cancel.CustomizableEdges = customizableEdges1;
            btn_cancel.DisabledState.BorderColor = Color.DarkGray;
            btn_cancel.DisabledState.CustomBorderColor = Color.DarkGray;
            btn_cancel.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btn_cancel.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btn_cancel.FillColor = Color.White;
            btn_cancel.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            btn_cancel.ForeColor = Color.Black;
            btn_cancel.Location = new Point(119, 196);
            btn_cancel.Name = "btn_cancel";
            btn_cancel.ShadowDecoration.CustomizableEdges = customizableEdges2;
            btn_cancel.Size = new Size(113, 40);
            btn_cancel.TabIndex = 16;
            btn_cancel.Text = "Hủy";
            btn_cancel.Click += btn_cancel_Click;
            // 
            // btn_add
            // 
            btn_add.BorderRadius = 15;
            btn_add.BorderThickness = 1;
            btn_add.CustomizableEdges = customizableEdges3;
            btn_add.DisabledState.BorderColor = Color.DarkGray;
            btn_add.DisabledState.CustomBorderColor = Color.DarkGray;
            btn_add.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btn_add.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btn_add.FillColor = Color.FromArgb(17, 34, 71);
            btn_add.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            btn_add.ForeColor = Color.White;
            btn_add.Location = new Point(318, 196);
            btn_add.Name = "btn_add";
            btn_add.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btn_add.Size = new Size(113, 40);
            btn_add.TabIndex = 17;
            btn_add.Text = "Thêm";
            btn_add.Click += btn_add_Click;
            // 
            // cbb_medicine
            // 
            cbb_medicine.BackColor = Color.Transparent;
            cbb_medicine.BorderColor = Color.Black;
            cbb_medicine.CustomizableEdges = customizableEdges5;
            cbb_medicine.DrawMode = DrawMode.OwnerDrawFixed;
            cbb_medicine.DropDownStyle = ComboBoxStyle.DropDownList;
            cbb_medicine.FocusedColor = Color.FromArgb(94, 148, 255);
            cbb_medicine.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            cbb_medicine.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            cbb_medicine.ForeColor = Color.Black;
            cbb_medicine.ItemHeight = 36;
            cbb_medicine.Items.AddRange(new object[] { "Chọn thuốc" });
            cbb_medicine.Location = new Point(119, 43);
            cbb_medicine.Name = "cbb_medicine";
            cbb_medicine.ShadowDecoration.CustomizableEdges = customizableEdges6;
            cbb_medicine.Size = new Size(312, 42);
            cbb_medicine.StartIndex = 0;
            cbb_medicine.TabIndex = 20;
            cbb_medicine.SelectedIndexChanged += cbb_medicine_SelectedIndexChanged;
            // 
            // nud_selected
            // 
            nud_selected.BackColor = Color.Transparent;
            nud_selected.CustomizableEdges = customizableEdges7;
            nud_selected.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            nud_selected.Location = new Point(306, 114);
            nud_selected.Name = "nud_selected";
            nud_selected.ShadowDecoration.CustomizableEdges = customizableEdges8;
            nud_selected.Size = new Size(125, 42);
            nud_selected.TabIndex = 21;
            // 
            // tb_amount
            // 
            tb_amount.CustomizableEdges = customizableEdges9;
            tb_amount.DefaultText = "";
            tb_amount.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            tb_amount.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            tb_amount.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            tb_amount.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            tb_amount.Enabled = false;
            tb_amount.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            tb_amount.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            tb_amount.ForeColor = Color.Black;
            tb_amount.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            tb_amount.Location = new Point(119, 114);
            tb_amount.Name = "tb_amount";
            tb_amount.PasswordChar = '\0';
            tb_amount.PlaceholderText = "";
            tb_amount.SelectedText = "";
            tb_amount.ShadowDecoration.CustomizableEdges = customizableEdges10;
            tb_amount.Size = new Size(144, 42);
            tb_amount.TabIndex = 22;
            // 
            // AddMedicineToInvoice
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(586, 291);
            Controls.Add(tb_amount);
            Controls.Add(nud_selected);
            Controls.Add(cbb_medicine);
            Controls.Add(btn_add);
            Controls.Add(btn_cancel);
            FormBorderStyle = FormBorderStyle.None;
            Name = "AddMedicineToInvoice";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "AddMedicineToInvoice";
            Load += AddMedicineToInvoice_Load;
            ((System.ComponentModel.ISupportInitialize)nud_selected).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2Button btn_cancel;
        private Guna.UI2.WinForms.Guna2Button btn_add;
        private Guna.UI2.WinForms.Guna2ComboBox cbb_medicine;
        private Guna.UI2.WinForms.Guna2NumericUpDown nud_selected;
        private Guna.UI2.WinForms.Guna2TextBox tb_amount;
    }
}