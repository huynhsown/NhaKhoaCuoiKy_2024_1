using NhaKhoaCuoiKy.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace NhaKhoaCuoiKy.Views.PatientForm
{
    public partial class AddInvoice : Form
    {
        int patientID;
        int doctor_id;
        int invoiceID = 0;
        MainForm mainForm;
        bool readOnly = false;
        public AddInvoice(int patientID, MainForm mainForm)
        {
            InitializeComponent();
            this.patientID = patientID;
            this.mainForm = mainForm;
        }

        public AddInvoice(int patientID, MainForm mainForm, bool readOnly, int invoiceID)
        {
            InitializeComponent();
            this.patientID = patientID;
            this.mainForm = mainForm;
            this.readOnly = readOnly;
            this.invoiceID = invoiceID;
            btn_addMedicine.Visible = false;
            btn_addService.Visible = false;
            btn_save.Visible = false;
            add_newInvoice.Visible = false;
            btn_print.Location = btn_addMedicine.Location;
            btn_print.Visible = true;
            data_medicine.Columns["col_btn_delete"].Visible = false;
            data_service.Columns["col_btn_delete_service"].Visible = false;
            try
            {
                DataTable medicine_tb = PatientHelper.getMedicineOfInvoice(invoiceID);
                foreach(DataRow dr in medicine_tb.Rows)
                {
                    string medicineID = dr["MaThuoc"].ToString();
                    string medicineName = dr["TenThuoc"].ToString();
                    int amount = Convert.ToInt32(dr["SoLuong"]);
                    int price = Convert.ToInt32(dr["GiaBan"]);
                    int total_price = amount * price;
                    data_medicine.Rows.Add(medicineID, medicineName, amount, total_price);
                }
                DataTable service_tb = PatientHelper.getServiceOfInvoice(invoiceID);
                foreach(DataRow dr in service_tb.Rows)
                {
                    string serviceID = dr["MaDichVu"].ToString();
                    string serviceName = dr["TenDichVu"].ToString();
                    string employeeName = dr["HoVaTen"].ToString();
                    int amount = Convert.ToInt32(dr["SoLuong"]);
                    int warranty = Convert.ToInt32(dr["BaoHanh"]);
                    int discount = Convert.ToInt32(dr["GiamGia"]);
                    int price = Convert.ToInt32(dr["GiaDichVu"]);
                    int total_price = (amount * price) / 100 *(100-discount);
                    string plan = Convert.ToDateTime(dr["NgaySuDung"]).ToString("dd/MM/yyyy HH:mm");
                    data_service.Rows.Add(serviceID, serviceName, employeeName, amount, "", warranty, total_price, plan);
                }
            }
            catch(Exception ex){
                MessageBox.Show(ex.Message);
            }
        }

        public AddInvoice(int patientID, MainForm mainForm, int doctor_id)
        {
            InitializeComponent();
            this.patientID = patientID;
            this.mainForm = mainForm;
            this.doctor_id = doctor_id;
        }

        private void AddInvoice_Load(object sender, EventArgs e)
        {

        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btn_addMedicine_Click(object sender, EventArgs e)
        {
            ViewHelper.loadForm(new AddMedicineToInvoice(this), mainForm);
        }

        private void btn_addService_Click(object sender, EventArgs e)
        {
            ViewHelper.loadForm(new AddServiceToInvoice(this), mainForm);
        }

        private void data_medicine_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (data_medicine.Columns[e.ColumnIndex].Name == "col_btn_delete")
            {
                DialogResult dr = MessageBox.Show("Xóa thuốc", "Xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    data_medicine.Rows.RemoveAt(e.RowIndex);
                }
            }
        }

        private void data_service_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (data_service.Columns[e.ColumnIndex].Name == "col_btn_delete_service")
            {
                DialogResult dr = MessageBox.Show("Xóa dịch vụ", "Xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    data_service.Rows.RemoveAt(e.RowIndex);
                }
            }
            else
            {
                MessageBox.Show(data_service.Rows[e.RowIndex].Cells[10].Value.ToString());
            }
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            invoiceID = PatientHelper.addInvoice(patientID, DateTime.Today, data_medicine, data_service);
            if (invoiceID != 0)
            {
                MessageBox.Show("Lưu hóa đơn thành công", "Hóa đơn", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btn_save.Enabled = false;
                btn_addMedicine.Enabled = false;
                btn_addService.Enabled = false;
                btn_print.Visible = true;
                add_newInvoice.Visible = true;
            }
            else
            {
                MessageBox.Show("Lưu hóa đơn thất bại", "Hóa đơn", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void add_newInvoice_Click(object sender, EventArgs e)
        {
            btn_save.Enabled = true;
            btn_addMedicine.Enabled = true;
            btn_addService.Enabled = true;
            btn_print.Visible = false;
            add_newInvoice.Visible = false;
            data_medicine.Rows.Clear();
            data_service.Rows.Clear();
            invoiceID = 0;
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
            int width = panel1.Width; // Width of the rectangle
            int height = panel1.Height; // Height of the rectangle

            // Create a solid brush with dark blue color for filling
            SolidBrush brush = new SolidBrush(Color.FromArgb(17, 34, 71));

            // Fill the rectangle with dark blue color
            e.Graphics.FillRectangle(brush, x, y, width, height);

            Bitmap bm = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.DrawToBitmap(bm, new Rectangle(0, 0, panel1.Width, panel1.Height));
            e.Graphics.DrawImage(bm, 550, 20);


            // Draw the text onto the print page
            e.Graphics.DrawString(label1.Text, new Font("Times New Roman", 20, FontStyle.Bold), Brushes.White, new Point(330, 20));
            e.Graphics.DrawString(label2.Text, new Font("Times New Roman", 12), Brushes.White, new Point(30, 20));
            e.Graphics.DrawString(label3.Text, new Font("Times New Roman", 12), Brushes.White, new Point(30, 55));
            e.Graphics.DrawString($"Mã hóa đơn: {invoiceID}", new Font("Times New Roman", 12), Brushes.White, new Point(30, 90));
            e.Graphics.DrawString($"Ngày in: {DateTime.Now.ToString("dd/MM/yyyy HH:mm")}", new Font("Times New Roman", 12), Brushes.White, new Point(30, 125));

            x = 30;
            y = 250;
            if (data_medicine.Rows.Count > 0)
            {
                DataGridViewRow headerRow = data_medicine.Rows[0];
                foreach (DataGridViewCell headerCell in headerRow.Cells)
                {
                    if (headerCell.Visible && data_medicine.Columns[headerCell.ColumnIndex].Name != "col_btn_delete")
                    {
                        Rectangle headerRect;
                        if (headerCell.ColumnIndex == 0)
                        {
                            headerRect = new Rectangle(x, y, 80, headerRow.Height);
                        }
                        else if (headerCell.ColumnIndex == 1)
                        {
                            headerRect = new Rectangle(x, y, 530, headerRow.Height);
                        }
                        else if (headerCell.ColumnIndex == 2)
                        {
                            headerRect = new Rectangle(x, y, 80, headerRow.Height);
                        }
                        else
                        {
                            headerRect = new Rectangle(x, y, 100, headerRow.Height);
                        }
                        e.Graphics.FillRectangle(Brushes.White, headerRect);
                        e.Graphics.DrawRectangle(Pens.Black, headerRect);
                        e.Graphics.DrawString(data_medicine.Columns[headerCell.ColumnIndex].HeaderText,
                            data_medicine.Font, Brushes.Black, headerRect, new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
                        x += headerRect.Width;
                    }
                }
                y += data_medicine.Rows[0].Height;

                foreach (DataGridViewRow dvr in data_medicine.Rows)
                {
                    x = 30;
                    foreach (DataGridViewCell cell in dvr.Cells)
                    {
                        if (cell.Visible && data_medicine.Columns[cell.ColumnIndex].Name != "col_btn_delete")
                        {
                            Rectangle headerRect = new Rectangle(x, y, cell.Size.Width, cell.Size.Height);
                            if (cell.ColumnIndex == 0)
                            {
                                headerRect = new Rectangle(x, y, 80, cell.Size.Height);
                            }
                            else if (cell.ColumnIndex == 1)
                            {
                                headerRect = new Rectangle(x, y, 530, headerRow.Height);
                            }
                            else if (cell.ColumnIndex == 2)
                            {
                                headerRect = new Rectangle(x, y, 80, headerRow.Height);
                            }
                            else
                            {
                                headerRect = new Rectangle(x, y, 100, headerRow.Height);
                            }
                            e.Graphics.DrawRectangle(Pens.Black, headerRect);
                            e.Graphics.DrawString(cell.FormattedValue.ToString(),
                                data_medicine.Font, Brushes.Black, headerRect, new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
                            x += headerRect.Width;
                        }
                    }
                    y += dvr.Height;
                }
                y += data_medicine.Rows[0].Height;
            }

            if (data_service.Rows.Count > 0)
            {
                x = 30;
                DataGridViewRow headerRow = data_service.Rows[0];
                foreach (DataGridViewCell headerCell in headerRow.Cells)
                {
                    if (headerCell.Visible && data_service.Columns[headerCell.ColumnIndex].Name != "col_btn_delete_service")
                    {
                        Rectangle headerRect = new Rectangle(x, y, headerCell.Size.Width, headerRow.Height);
                        if (headerCell.ColumnIndex == 0 || headerCell.ColumnIndex == 3 || headerCell.ColumnIndex == 6)
                        {
                            headerRect = new Rectangle(x, y, 80, headerRow.Height);
                        }
                        else if (headerCell.ColumnIndex == 1 || headerCell.ColumnIndex == 2)
                        {
                            headerRect = new Rectangle(x, y, 150, headerRow.Height);
                        }
                        else if (headerCell.ColumnIndex == 4)
                        {
                            headerRect = new Rectangle(x, y, 90, headerRow.Height);
                        }
                        else if (headerCell.ColumnIndex == 7)
                        {
                            headerRect = new Rectangle(x, y, 130, headerRow.Height);
                        }
                        else if (headerCell.ColumnIndex == 5)
                        {
                            headerRect = new Rectangle(x, y, 120, headerRow.Height);
                        }
                        e.Graphics.FillRectangle(Brushes.White, headerRect);
                        e.Graphics.DrawRectangle(Pens.Black, headerRect);
                        e.Graphics.DrawString(data_service.Columns[headerCell.ColumnIndex].HeaderText,
                            data_service.Font, Brushes.Black, headerRect, new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
                        x += headerRect.Width;
                    }
                }
                y += data_service.Rows[0].Height;

                foreach (DataGridViewRow dvr in data_service.Rows)
                {
                    x = 30;
                    foreach (DataGridViewCell cell in dvr.Cells)
                    {
                        if (cell.Visible && data_service.Columns[cell.ColumnIndex].Name != "col_btn_delete_service")
                        {
                            Rectangle headerRect = new Rectangle(x, y, cell.Size.Width, cell.Size.Height);
                            if (cell.ColumnIndex == 0 || cell.ColumnIndex == 3 || cell.ColumnIndex == 6)
                            {
                                headerRect = new Rectangle(x, y, 80, headerRow.Height);
                            }
                            else if (cell.ColumnIndex == 1 || cell.ColumnIndex == 2)
                            {
                                headerRect = new Rectangle(x, y, 150, headerRow.Height);
                            }
                            else if (cell.ColumnIndex == 4)
                            {
                                headerRect = new Rectangle(x, y, 90, headerRow.Height);
                            }
                            else if (cell.ColumnIndex == 7)
                            {
                                headerRect = new Rectangle(x, y, 130, headerRow.Height);
                            }
                            else if (cell.ColumnIndex == 5)
                            {
                                headerRect = new Rectangle(x, y, 120, headerRow.Height);
                            }
                            e.Graphics.DrawRectangle(Pens.Black, headerRect);
                            e.Graphics.DrawString(cell.FormattedValue.ToString(),
                                data_service.Font, Brushes.Black, headerRect, new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
                            x += headerRect.Width;
                        }
                    }
                    y += dvr.Height;
                }
            }
        }
    }
}
