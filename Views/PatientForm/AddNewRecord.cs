using Guna.UI2.WinForms;
using NhaKhoaCuoiKy.Helpers;
using NhaKhoaCuoiKy.Models;
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

namespace NhaKhoaCuoiKy.Views.PatientForm
{
    public partial class AddNewRecord : Form
    {
        int patienID;
        int recordID;
        UserModel userAccount;
        string nameOfUser = string.Empty;
        public AddNewRecord(int patienID, UserModel userAccount)
        {
            InitializeComponent();
            this.patienID = patienID;
            this.userAccount = userAccount;
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dtp_date_ValueChanged(object sender, EventArgs e)
        {
            DateTime today = DateTime.Now;
            DateTime dtp_pick = dtp_date.Value;
            int age = today.Year - dtp_pick.Year;
            tb_age.Text = age.ToString();
        }

        private void AddNewRecord_Load(object sender, EventArgs e)
        {
            panel_patienInfo.Location = new Point(21, 115);
            panel_medicalhistory.Location = new Point(21, 330);
            panel_reason.Location = new Point(21, 755); // Khoảng cách = 20
            panel_diagnose.Location = new Point(21, 1025);
            panel_plan.Location = new Point(21, 1435);
            panel_abstract.Location = new Point(21, 1850);
            loadPatientInfomation();
            try
            {
                DataTable dt = EmployeeHelper.getDoctorByID(userAccount.employeeID);
                nameOfUser = dt.Rows[0]["HoVaTen"].ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void loadPatientInfomation()
        {
            try
            {
                DataTable dt = PatientHelper.getByID(patienID);
                if (dt.Rows.Count != 1)
                {
                    MessageBox.Show("Không thể thêm bệnh án", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                    return;
                }
                string name = Convert.ToString(dt.Rows[0]["HoVaTen"]).Trim();
                DateTime dob = Convert.ToDateTime(dt.Rows[0]["NgaySinh"]);
                string h_number = dt.Rows[0]["SoNha"].ToString();
                string s_name = dt.Rows[0]["TenDuong"].ToString();
                string ward = dt.Rows[0]["Phuong"].ToString();
                string city = dt.Rows[0]["ThanhPho"].ToString();
                string address = h_number + ", " + s_name + ", " + ward + ", " + city;
                string phone = dt.Rows[0]["SoDienThoai"].ToString();
                string gender = dt.Rows[0]["GioiTinh"].ToString().Trim();
                Image img;
                using (MemoryStream ms = new MemoryStream((byte[])dt.Rows[0]["Anh"]))
                {
                    img = Image.FromStream(ms);
                }
                pb_patientImg.Image = img;
                tb_name.Text = name;
                tb_address.Text = address;
                tb_phone.Text = phone;
                if (gender == "Nữ") rbt_female.Checked = true;
                else if (gender == "Nam") rbt_male.Checked = true;
                dtp_date.Value = dob;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR::" + ex.Message);
            }
        }

        private void btn_createAppointment_Click(object sender, EventArgs e)
        {
            try
            {
                string DentalDisease = tb_DentalDisease.Text.Trim();
                string OtherDisease = tb_OtherDisease.Text.Trim();
                string Symptoms = tb_Symptoms.Text.Trim();
                string Result = tb_Result.Text.Trim();
                string Diagnosis = tb_Diagnosis.Text.Trim();
                string TreatmentMethod = tb_TreatmentMethod.Text.Trim();
                string NextAppointment = tb_NextAppointment.Text.Trim();
                if (DentalDisease.Length == 0 ||
                    OtherDisease.Length == 0 ||
                    Symptoms.Length == 0 ||
                    Result.Length == 0 ||
                    Diagnosis.Length == 0 ||
                    TreatmentMethod.Length == 0 ||
                    NextAppointment.Length == 0)
                {
                    MessageBox.Show("Vui lòng điền đầy đủ thông tin vào các trường cần thiết.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    return;
                }
                DateTime now = DateTime.Now;
                recordID = PatientHelper.addNewRecord(patienID, 1, DentalDisease, OtherDisease, Symptoms, Result, Diagnosis, TreatmentMethod, NextAppointment, now);
                if (recordID != 0)
                {
                    MessageBox.Show("Thêm bệnh án thành công", "Bệnh án", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    label4.Text = $"Số lưu trữ: {recordID}";
                    btn_print.Visible = true;
                    btn_new.Visible = true;
                    btn_print.Location = new Point(btn_print.Location.X, btn_createAppointment.Location.Y);
                    btn_new.Location = new Point(btn_new.Location.X, btn_createAppointment.Location.Y);
                    btn_createAppointment.Visible = false;
                    tb_DentalDisease.Enabled = false;
                    tb_Diagnosis.Enabled = false;
                    tb_NextAppointment.Enabled = false;
                    tb_Result.Enabled = false;
                    tb_Symptoms.Enabled = false;
                    tb_TreatmentMethod.Enabled = false;
                    tb_OtherDisease.Enabled = false;
                }
                else
                {
                    MessageBox.Show("Thêm bệnh án thất bại", "Bệnh án", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_print_Click(object sender, EventArgs e)
        {
            printRecord();
        }

        void printRecord()
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

        int currentPage = 1;
        void Doc_PrintPage(object sender, PrintPageEventArgs e)
        {

            if (currentPage == 1)
            {
                e.Graphics.DrawString(label1.Text, new Font("Times New Roman", 18, FontStyle.Bold), Brushes.Black, new Point(350, 23));
                e.Graphics.DrawString(label2.Text, new Font("Times New Roman", 10), Brushes.Black, new Point(30, 20));
                e.Graphics.DrawString(label3.Text, new Font("Times New Roman", 10), Brushes.Black, new Point(30, 50));
                e.Graphics.DrawString($"Số lưu trữ: {recordID}", new Font("Times New Roman", 10), Brushes.Black, new Point(690, 20));
                e.Graphics.DrawString($"Ngày in: {DateTime.Now.ToString("dd/MM/yyyy HH:mm")}", new Font("Times New Roman", 10), Brushes.Black, new Point(690, 50));

                #region Patient Infomation
                e.Graphics.DrawRectangle(new Pen(Brushes.Black, 1), new Rectangle(30, 80, 790, 170));
                e.Graphics.DrawString(label5.Text, new Font("Times New Roman", 10, FontStyle.Bold), Brushes.Black, new Point(35, 85));
                e.Graphics.DrawString($"1. Họ và tên: {tb_name.Text}", new Font("Times New Roman", 10), Brushes.Black, new Point(35, 120));
                e.Graphics.DrawString($"2. Ngày sinh: {dtp_date.Value.ToShortDateString()}", new Font("Times New Roman", 10), Brushes.Black, new Point(350, 120));
                e.Graphics.DrawString($"Tuổi: {tb_age.Text}", new Font("Times New Roman", 10), Brushes.Black, new Point(550, 120));

                string gender = (rbt_female.Checked) ? "Nữ" : "Nam";
                e.Graphics.DrawString($"3. Giới tính: {gender}", new Font("Times New Roman", 10), Brushes.Black, new Point(35, 155));
                e.Graphics.DrawString($"4. Địa chỉ: {tb_address.Text}", new Font("Times New Roman", 10), Brushes.Black, new Point(350, 155));

                e.Graphics.DrawString($"5. Số điện thoại: {tb_phone.Text}", new Font("Times New Roman", 10), Brushes.Black, new Point(35, 190));

                e.Graphics.DrawString($"Ảnh:", new Font("Times New Roman", 10), Brushes.Black, new Point(650, 110));
                Bitmap scaledImage = new Bitmap(120, 200);
                Rectangle imageRect = new Rectangle(650, 130, 100, 100);
                using (Graphics g = Graphics.FromImage(scaledImage))
                {
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    g.DrawImage(pb_patientImg.Image, new Rectangle(0, 0, 120, 200));
                }
                e.Graphics.DrawImage(scaledImage, imageRect);
                #endregion

                #region Medical History
                e.Graphics.DrawRectangle(new Pen(Brushes.Black, 1), new Rectangle(30, 265, 790, 300));
                e.Graphics.DrawString("II. TIỀN SỬ BỆNH LÝ:", new Font("Times New Roman", 10, FontStyle.Bold), Brushes.Black, new Point(35, 270));
                e.Graphics.DrawString($"1. Các bệnh lý nha khoa trước đây:", new Font("Times New Roman", 10), Brushes.Black, new Point(35, 300));

                Rectangle rect = new Rectangle(35, 320, 780, 100);
                e.Graphics.DrawRectangle(new Pen(Brushes.Black, 1), rect);
                e.Graphics.DrawString(tb_DentalDisease.Text,
                                   new Font("Times New Roman", 10), Brushes.Black, rect, new StringFormat { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Near });

                e.Graphics.DrawString($"2. Các bệnh lý đang / đã mắc phải:", new Font("Times New Roman", 10), Brushes.Black, new Point(35, 440));
                rect = new Rectangle(35, 460, 780, 100);
                e.Graphics.DrawRectangle(new Pen(Brushes.Black, 1), rect);
                e.Graphics.DrawString(tb_OtherDisease.Text,
                                   new Font("Times New Roman", 10), Brushes.Black, rect, new StringFormat { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Near });

                #endregion

                #region Reason For Examination
                e.Graphics.DrawRectangle(new Pen(Brushes.Black, 1), new Rectangle(30, 585, 790, 150));
                e.Graphics.DrawString("III. LÝ DO THĂM KHÁM:", new Font("Times New Roman", 10, FontStyle.Bold), Brushes.Black, new Point(35, 590));
                e.Graphics.DrawString($"1. Các triệu chứng hay vấn đề mà bệnh nhân gặp phải:", new Font("Times New Roman", 10), Brushes.Black, new Point(35, 610));
                rect = new Rectangle(35, 630, 780, 100);
                e.Graphics.DrawRectangle(new Pen(Brushes.Black, 1), rect);
                e.Graphics.DrawString(tb_Symptoms.Text,
                                   new Font("Times New Roman", 10), Brushes.Black, rect, new StringFormat { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Near });

                #endregion

                #region Examination and Diagnosis
                e.Graphics.DrawRectangle(new Pen(Brushes.Black, 1), new Rectangle(30, 755, 790, 300));
                e.Graphics.DrawString("IV. KHÁM VÀ CHẨN ĐOÁN:", new Font("Times New Roman", 10, FontStyle.Bold), Brushes.Black, new Point(35, 760));
                e.Graphics.DrawString($"1. Kết quả khám:", new Font("Times New Roman", 10), Brushes.Black, new Point(35, 790));
                rect = new Rectangle(35, 810, 780, 100);
                e.Graphics.DrawRectangle(new Pen(Brushes.Black, 1), rect);
                e.Graphics.DrawString(tb_Result.Text,
                                   new Font("Times New Roman", 10),
                                   Brushes.Black, rect,
                                   new StringFormat { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Near });

                e.Graphics.DrawString($"2. Chẩn đoán:", new Font("Times New Roman", 10), Brushes.Black, new Point(35, 930));
                rect = new Rectangle(35, 950, 780, 100);
                e.Graphics.DrawRectangle(new Pen(Brushes.Black, 1), rect);
                e.Graphics.DrawString(tb_Diagnosis.Text,
                                   new Font("Times New Roman", 10),
                                   Brushes.Black, rect,
                                   new StringFormat { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Near });

                #endregion
                e.HasMorePages = true;
                currentPage = 2;
            }
            else
            {
                #region Treatment Plan
                e.Graphics.DrawRectangle(new Pen(Brushes.Black, 1), new Rectangle(30, 45, 790, 300));
                e.Graphics.DrawString("V. KẾ HOẠCH ĐIỀU TRỊ:", new Font("Times New Roman", 10, FontStyle.Bold), Brushes.Black, new Point(35, 50));
                e.Graphics.DrawString($"1. Phương pháp điều trị:", new Font("Times New Roman", 10), Brushes.Black, new Point(35, 80));
                Rectangle rect = new Rectangle(35, 100, 780, 100);
                e.Graphics.DrawRectangle(new Pen(Brushes.Black, 1), rect);
                e.Graphics.DrawString(tb_TreatmentMethod.Text,
                                   new Font("Times New Roman", 10),
                                   Brushes.Black, rect,
                                   new StringFormat { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Near });
                e.Graphics.DrawString($"2. Lịch trình dự kiến cho các cuộc hẹn hoặc quy trình nha khoa tiếp theo:", new Font("Times New Roman", 10), Brushes.Black, new Point(35, 220));
                rect = new Rectangle(35, 240, 780, 100);
                e.Graphics.DrawRectangle(new Pen(Brushes.Black, 1), rect);
                e.Graphics.DrawString(tb_NextAppointment.Text,
                                   new Font("Times New Roman", 10),
                                   Brushes.Black, rect,
                                   new StringFormat { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Near });

                #endregion

                e.Graphics.DrawString($"Bác sĩ: {nameOfUser}", new Font("Times New Roman", 10, FontStyle.Italic), Brushes.Black, new Point(600, 360));
                e.Graphics.DrawString($"Ngày lập bệnh án: {DateTime.Today.ToShortDateString()}", new Font("Times New Roman", 10, FontStyle.Italic), Brushes.Black, new Point(600, 380));
                e.HasMorePages = false;
                currentPage = 1;
            }
        }

        private void btn_new_Click(object sender, EventArgs e)
        {
            btn_createAppointment.Visible = true;
            btn_new.Visible = false;
            btn_print.Visible = false;
            tb_DentalDisease.Text = string.Empty;
            tb_Diagnosis.Text = string.Empty;
            tb_NextAppointment.Text = string.Empty;
            tb_Result.Text = string.Empty;
            tb_Symptoms.Text = string.Empty;
            tb_TreatmentMethod.Text = string.Empty;
            tb_OtherDisease.Text = string.Empty;
            tb_DentalDisease.Enabled = true;
            tb_Diagnosis.Enabled = true;
            tb_NextAppointment.Enabled = true;
            tb_Result.Enabled = true;
            tb_Symptoms.Enabled = true;
            tb_TreatmentMethod.Enabled = true;
            tb_OtherDisease.Enabled = true;

            label4.Text = $"Số lưu trữ:";
        }
    }
}
