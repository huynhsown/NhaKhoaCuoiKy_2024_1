using Dapper;
using NhaKhoaCuoiKy.Helpers;
using NhaKhoaCuoiKy.Models;
using NhaKhoaCuoiKy.Views.Service;
using NhaKhoaCuoiKy.Views.PatientForm;
using System.Data;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace NhaKhoaCuoiKy.Views
{
    public partial class Patient : Form
    {
        public Patient()
        {
            InitializeComponent();
        }

        public EventHandler popupNewPatient;
        public EventHandler addNewRecord;
        private NewPatient newPatient;
        private Validate validate = new Validate();
        private MainForm mainForm;
        PatientModel pm;
        UserModel userAccount;

        public Patient(MainForm mainForm, UserModel userAccount)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            this.userAccount = userAccount;
        }

        private void loadForm(Form form)
        {
            FormBackGround formBackGround = new FormBackGround(mainForm);
            try
            {
                using (form)
                {
                    formBackGround.Owner = mainForm;
                    formBackGround.Show();
                    form.Owner = formBackGround;
                    form.ShowDialog();
                    formBackGround.Dispose();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Đã xảy ra lỗi! Vui lòng thử lại.", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            loadForm(new NewPatient(this));

            /*            FormBackGround formBackGround = new FormBackGround(mainForm);
                        try
                        {
                            using (NewPatient newCategory = new NewPatient(this))
                            {
                                formBackGround.Owner = mainForm;
                                formBackGround.Show();
                                newCategory.Owner = formBackGround;
                                newCategory.ShowDialog();
                                formBackGround.Dispose();
                            }
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Đã xảy ra lỗi! Vui lòng thử lại.", "Thông báo",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }*/
        }

        private void Patient_Load(object sender, EventArgs e)
        {
            pm = new PatientModel();
            data_benhNhan.AllowUserToAddRows = false;
            addToDataGrid(pm.getAllPatient());
            /*            data_benhNhan.Columns[0].Width = 90;
                        DataGridViewButtonColumn btn_record = new DataGridViewButtonColumn();
                        btn_record.HeaderText = "Thêm bệnh án";
                        btn_record.Name = "btn_addRecord";
                        btn_record.Text = "Thêm";
                        btn_record.UseColumnTextForButtonValue = true;
                        data_benhNhan.Columns.Add(btn_record);

                        DataGridViewButtonColumn btn_info = new DataGridViewButtonColumn();
                        btn_info.HeaderText = "Xem thông tin";
                        btn_info.Name = "btn_editInfoPatient";
                        btn_info.Text = "Xem";
                        btn_info.UseColumnTextForButtonValue = true;
                        data_benhNhan.Columns.Add(btn_info);*/
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            if (tb_filter_search.Text.Trim().Length == 0)
            {
                MessageBox.Show("Vui long nhap thong tin", "Thong tin", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            data_benhNhan.Rows.Clear();
            data_benhNhan.Refresh();
            if (cb_filter.SelectedIndex == 0)
            {
                searchByID();
            }
            else if (cb_filter.SelectedIndex == 1)
            {
                searchByName();
            }
            else if (cb_filter.SelectedIndex == 2)
            {
                searchByPhone();
            }
        }

        public void addToDataGrid(DataTable dt)
        {
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    int maBN = Convert.ToInt32(dt.Rows[i]["MaBenhNhan"]);
                    string hoTen = Convert.ToString(dt.Rows[i]["HoVaTen"]);
                    string gioiTinh = dt.Rows[i]["GioiTinh"].ToString();
                    string ngaySinh = Convert.ToDateTime(dt.Rows[i]["NgaySinh"]).ToShortDateString();
                    int soNha = int.Parse(dt.Rows[i]["SoNha"].ToString());
                    string soDienThoai = dt.Rows[i]["SoDienThoai"].ToString();
                    string duong = dt.Rows[i]["TenDuong"].ToString();
                    string phuong = dt.Rows[i]["Phuong"].ToString();
                    string thanhPho = dt.Rows[i]["ThanhPho"].ToString();

                    string diaChi = soNha.ToString() + " " + duong + " " + phuong + " " + thanhPho;
                    data_benhNhan.Rows.Add(maBN, hoTen, soDienThoai, ngaySinh, gioiTinh, diaChi);
                }
            }
        }
        private void searchByID()
        {
            try
            {
                if (!validate.validateNumber(tb_filter_search.Text.Trim()))
                {
                    MessageBox.Show("Ma benh nhan khong hop le (Chua ky tu dac biet hoac chu cai)", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                DataTable dt = pm.getByID(int.Parse(tb_filter_search.Text.Trim()));
                addToDataGrid(dt);
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy bệnh nhân", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void searchByName()
        {
            try
            {
                DataTable dt = pm.getByName(tb_filter_search.Text.Trim());
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy bệnh nhân", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                addToDataGrid(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void searchByPhone()
        {
            try
            {
                if (!validate.validateNumber(tb_filter_search.Text.Trim()))
                {
                    MessageBox.Show("So dien thoai khong hop le (Chua ky tu dac biet hoac chu cai)", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                DataTable dt = pm.getByPhone(tb_filter_search.Text.Trim());
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy bệnh nhân", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                addToDataGrid(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void data_benhNhan_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int patientID = Convert.ToInt32(data_benhNhan.Rows[e.RowIndex].Cells[0].Value);
            if (data_benhNhan.Columns[e.ColumnIndex].Name == "col_btn_addRecord")
            {
                mainForm.openChildFormHaveData(new AddNewRecord(patientID, userAccount));
            }
            if (data_benhNhan.Columns[e.ColumnIndex].Name == "col_btn_Info")
            {
                loadForm(new EditPatient(patientID, this));
            }
            if (data_benhNhan.Columns[e.ColumnIndex].Name == "col_btn_invoice")
            {
                mainForm.openChildFormHaveData(new AddInvoice(patientID, mainForm));
            }
            if (data_benhNhan.Columns[e.ColumnIndex].Name == "col_btn_history")
            {
                mainForm.openChildFormHaveData(new HistoryForm(patientID, mainForm));
            }
        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btn_print_Click(object sender, EventArgs e)
        {
            printInvoice();
        }

        void printInvoice()
        {
            PrintDialog pd = new PrintDialog();
            PrintDocument doc = new PrintDocument();
            pd.Document = doc;
            doc.PrintPage += Doc_PrintPage;
            if (pd.ShowDialog() == DialogResult.OK)
            {
                doc.Print();
            }
        }

        void Doc_PrintPage(object sender, PrintPageEventArgs e)
        {
            // Define the dimensions and position of the rectangle
            int x = 0; // X-coordinate
            int y = 0; // Y-coordinate


            // Draw the text onto the print page
            e.Graphics.DrawString("Danh sách bệnh nhân", new Font("Times New Roman", 20, FontStyle.Bold), Brushes.Black, new Point(330, 20));
            e.Graphics.DrawString($"Ngày in: {DateTime.Now.ToString("dd/MM/yyyy HH:mm")}", new Font("Times New Roman", 12), Brushes.Black, new Point(30, 125));

            x = 30;
            y = 150;
            if (data_benhNhan.Rows.Count > 0)
            {
                DataGridViewRow headerRow = data_benhNhan.Rows[0];
                foreach (DataGridViewCell headerCell in headerRow.Cells)
                {
                    if (headerCell.Visible && !(data_benhNhan.Columns[headerCell.ColumnIndex] is DataGridViewButtonColumn))
                    {
                        Rectangle headerRect = new Rectangle(x, y, headerCell.Size.Width, headerRow.Height); 
                        if(headerCell.ColumnIndex == 1) 
                        {
                            headerRect = new Rectangle(x, y, 250, headerRow.Height);
                        }
                        e.Graphics.FillRectangle(Brushes.White, headerRect);
                        e.Graphics.DrawRectangle(Pens.Black, headerRect);
                        e.Graphics.DrawString(data_benhNhan.Columns[headerCell.ColumnIndex].HeaderText,
                            data_benhNhan.Font, Brushes.Black, headerRect, new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
                        x += headerRect.Width;
                    }
                }
                y += data_benhNhan.Rows[0].Height;

                foreach (DataGridViewRow dvr in data_benhNhan.Rows)
                {
                    x = 30;
                    foreach (DataGridViewCell cell in dvr.Cells)
                    {
                        if (cell.Visible && !(data_benhNhan.Columns[cell.ColumnIndex] is DataGridViewButtonColumn))
                        {
                            Rectangle headerRect = new Rectangle(x, y, cell.Size.Width, cell.Size.Height);
                            if (cell.ColumnIndex == 1)
                            {
                                headerRect = new Rectangle(x, y, 250, headerRow.Height);
                            }
                            e.Graphics.DrawRectangle(Pens.Black, headerRect);
                            e.Graphics.DrawString(cell.FormattedValue.ToString(),
                                data_benhNhan.Font, Brushes.Black, headerRect, new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
                            x += headerRect.Width;
                        }
                    }
                    y += dvr.Height;
                }
                y += data_benhNhan.Rows[0].Height;
            }
        }
    }
}
