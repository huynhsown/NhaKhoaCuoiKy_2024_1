using NhaKhoaCuoiKy.Helpers;
using NhaKhoaCuoiKy.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NhaKhoaCuoiKy.Views.PatientForm
{
    public partial class AddServiceToInvoice : Form
    {
        AddInvoice addInvoice;
        List<ScheduleModel> schedules = new List<ScheduleModel>();
        List<EmptyScheduleModel> emptySchedules = new List<EmptyScheduleModel>();
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,     // x-coordinate of upper-left corner
            int nTopRect,      // y-coordinate of upper-left corner
            int nRightRect,    // x-coordinate of lower-right corner
            int nBottomRect,   // y-coordinate of lower-right corner
            int nWidthEllipse, // width of ellipse
            int nHeightEllipse // height of ellipse
        );
        public AddServiceToInvoice(AddInvoice addInvoice)
        {
            InitializeComponent();
            this.addInvoice = addInvoice;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }

        private void AddServiceToInvoice_Load(object sender, EventArgs e)
        {
            try
            {
                dtp_date.Value = DateTime.Today;
                dtp_start.CustomFormat = "HH:mm";
                dtp_start.Value = DateTime.Today.AddHours(8).AddMinutes(0).AddSeconds(0);
                dtp_end.CustomFormat = "HH:mm";
                dtp_end.Value = DateTime.Today.AddHours(8).AddMinutes(0).AddSeconds(0);
                DataTable dt = ServiceHelper.getAllServiceCategory();
                foreach (DataRow dr in dt.Rows)
                {
                    int id = Convert.ToInt32(dr["MaLoaiDichVu"]);
                    string label = Convert.ToString(dr["TenLoaiDichVu"]);
                    cbb_category.Items.Add(new ComboBoxItem(id, label));
                }
                DataTable doctors = EmployeeHelper.getAllDoctor();
                foreach (DataRow dr in doctors.Rows)
                {
                    int id = Convert.ToInt32(dr["MaNhanVien"]);
                    string label = "BS " + Convert.ToString(dr["HoVaTen"]);
                    cbb_doctor.Items.Add(new ComboBoxItem(id, label));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        int priceOfCur = 0;
        string unit = string.Empty;
        int warranty = 0;
        int time = 0;

        private void cbb_category_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbb_service.Items.Clear();
            cbb_service.Items.Add("-Chọn dịch vụ-");
            cbb_service.StartIndex = 0;
            priceOfCur = 0;
            if (cbb_category.SelectedIndex == 0)
            {

                return;
            }
            DataTable dt = null;
            if (cbb_category.SelectedIndex == 1)
            {
                dt = ServiceHelper.getAllService();
            }
            else
            {
                int categoryID = ((ComboBoxItem)cbb_category.SelectedItem).Id;
                dt = ServiceHelper.getServiceByCategoryID(categoryID);
            }

            foreach (DataRow dr in dt.Rows)
            {
                int id = Convert.ToInt32(dr["MaDichVu"]);
                string label = Convert.ToString(dr["TenDichVu"]);
                cbb_service.Items.Add(new ComboBoxItem(id, label));
            }
        }

        private void cbb_service_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbb_service.SelectedIndex == 0)
            {
                resetValue();
                loadTime();
                return;
            }
            try
            {
                ComboBoxItem cbi = ((ComboBoxItem)cbb_service.SelectedItem);
                DataTable dt = ServiceHelper.getServiceByID(cbi.Id);
                priceOfCur = Convert.ToInt32(dt.Rows[0]["GiaDichVu"]);
                unit = Convert.ToString(dt.Rows[0]["DonVi"]);
                warranty = Convert.ToInt32(dt.Rows[0]["BaoHanh"]);
                time = Convert.ToInt32(dt.Rows[0]["ThoiGianThucHien"]);
                loadTime();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void resetValue()
        {
            priceOfCur = 0;
            unit = string.Empty;
            warranty = 0;
            time = 0;
        }

        private void loadTime()
        {
            int time_work = time * Convert.ToInt32(nud_selected.Value);
            dtp_end.Value = dtp_start.Value.AddMinutes(time_work);
        }

        private void loadWork(DateOnly date, int doctor_id)
        {
            label_date.Text = "Lịch trình ngày: " + date.ToString("dd/M/yyyy");
            schedules.Clear();
            data_time.Rows.Clear();
            emptySchedules.Clear();
            TimeSpan lastSecond = TimeSpan.FromHours(23)
                      .Add(TimeSpan.FromMinutes(59))
                      .Add(TimeSpan.FromSeconds(0));
            TimeSpan startWork = TimeSpan.FromHours(8)
                      .Add(TimeSpan.FromMinutes(0))
                      .Add(TimeSpan.FromSeconds(0));
            TimeSpan endWork = TimeSpan.FromHours(21)
                      .Add(TimeSpan.FromMinutes(0))
                      .Add(TimeSpan.FromSeconds(0));
            try
            {
                DataTable appointment = AppointmentHelper.getAllAppointmentByDoctor(doctor_id, date);
                foreach (DataRow row in appointment.Rows)
                {
                    DateTime dt_tmp = Convert.ToDateTime(row[5]);
                    ScheduleModel temp = new ScheduleModel();
                    temp.ScheduleID = Convert.ToInt32(row[0]);
                    temp.ScheduleType = "Lịch hẹn";
                    temp.ScheduleTypeID = Convert.ToInt32(row[0]);
                    temp.Start = dt_tmp;
                    temp.End = dt_tmp.AddMinutes(Convert.ToInt32(row[6]));
                    schedules.Add(temp);
                }
                DataTable service = AppointmentHelper.getAllServiceByDoctor(doctor_id, date);
                foreach (DataRow row in service.Rows)
                {
                    DateTime dt_tmp = Convert.ToDateTime(row[2]);
                    ScheduleModel temp = new ScheduleModel();
                    temp.ScheduleID = Convert.ToInt32(row[0]);
                    temp.ScheduleType = "Dịch vụ";
                    temp.ScheduleTypeID = Convert.ToInt32(row[5]);
                    temp.Start = dt_tmp;
                    temp.End = dt_tmp.AddMinutes(Convert.ToInt32(row[4]) * Convert.ToInt32(row[3]));
                    schedules.Add(temp);
                }
                foreach(DataGridViewRow drv in addInvoice.data_service.Rows)
                {
                    DateTime dt_tmp = Convert.ToDateTime(drv.Cells[7].Value);
                    ScheduleModel temp = new ScheduleModel();
                    temp.ScheduleID = Convert.ToInt32(drv.Cells[0].Value);
                    temp.ScheduleType = "Dịch vụ";
                    temp.ScheduleTypeID = Convert.ToInt32(drv.Cells[0].Value);
                    temp.Start = dt_tmp;
                    temp.End = dt_tmp.AddMinutes(Convert.ToInt32(drv.Cells[9].Value));
                    schedules.Add(temp);
                }
                schedules = schedules.OrderBy(schedule => schedule.Start).ToList();
                if (schedules.Count == 0)
                {
                    emptySchedules.Add(new EmptyScheduleModel(DateTime.Parse(date.ToShortDateString()).Add(startWork), DateTime.Parse(date.ToShortDateString()).Add(endWork)));
                }
                else
                {
                    for (int i = 0; i < schedules.Count; i++)
                    {
                        ScheduleModel schedule = schedules[i];
                        if (i == 0 && schedule.Start.TimeOfDay != startWork)
                        {
                            emptySchedules.Add(new EmptyScheduleModel(DateTime.Parse(date.ToShortDateString()).Add(startWork), schedule.Start));
                        }
                        if (i < schedules.Count - 1 && schedules[i].End < schedules[i + 1].Start)
                        {
                            emptySchedules.Add(new EmptyScheduleModel(schedules[i].End, schedules[i + 1].Start));
                        }
                        if (i == schedules.Count - 1)
                        {
                            if (schedule.End.TimeOfDay != lastSecond)
                            {
                                emptySchedules.Add(new EmptyScheduleModel(schedule.End, DateTime.Parse(date.ToShortDateString()).Add(endWork)));
                            }
                        }
                    }
                }
                foreach (EmptyScheduleModel model in emptySchedules)
                {
                    data_time.Rows.Add(model.Start.TimeOfDay, model.End.TimeOfDay);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cbb_doctor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbb_doctor.SelectedIndex == 0)
            {
                data_time.Rows.Clear();
                return;
            }
            DateOnly date = DateOnly.Parse(dtp_date.Value.ToShortDateString());
            int doctor_id = ((ComboBoxItem)cbb_doctor.SelectedItem).Id;
            loadWork(date, doctor_id);
        }

        private void dtp_date_ValueChanged(object sender, EventArgs e)
        {
            if (cbb_doctor.SelectedIndex == 0)
            {
                return;
            }
            DateOnly date = DateOnly.Parse(dtp_date.Value.ToShortDateString());
            int doctor_id = ((ComboBoxItem)cbb_doctor.SelectedItem).Id;
            loadWork(date, doctor_id);
        }

        private void nud_selected_ValueChanged(object sender, EventArgs e)
        {
            loadTime();
        }

        private void dtp_start_ValueChanged(object sender, EventArgs e)
        {
            loadTime();
        }

        bool checkEmptyTime()
        {
            DateTime selectedDate = dtp_date.Value;
            DateTime start = selectedDate.Add(dtp_start.Value.TimeOfDay);
            DateTime end = selectedDate.Add(dtp_end.Value.TimeOfDay);
            foreach (EmptyScheduleModel model in emptySchedules)
            {
                if (start.TimeOfDay >= model.Start.TimeOfDay && end.TimeOfDay <= model.End.TimeOfDay)
                {
                    return true;
                }
            }
            return false;
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            if(cbb_category.SelectedIndex == 0)
            {
                MessageBox.Show("Vui lòng chọn loại dịch vụ", "Dịch vụ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if(cbb_service.SelectedIndex == 0)
            {
                MessageBox.Show("Vui lòng chọn dịch vụ", "Dịch vụ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if(cbb_doctor.SelectedIndex == 0)
            {
                MessageBox.Show("Vui lòng chọn bác sĩ", "Dịch vụ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if(nud_selected.Value == 0)
            {
                MessageBox.Show("Vui lòng chọn số lượng dịch vụ", "Dịch vụ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            DateTime selectedDate = dtp_date.Value;
            DateTime start = selectedDate.Add(dtp_start.Value.TimeOfDay);
            DateTime end = selectedDate.Add(dtp_end.Value.TimeOfDay);
            if (!checkEmptyTime())
            {
                MessageBox.Show("Trùng lịch của bác sĩ", "Dịch vụ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            int maDichVu = ((ComboBoxItem)cbb_service.SelectedItem).Id;
            string dichVu = ((ComboBoxItem)cbb_service.SelectedItem).Text;
            int maBacSi = ((ComboBoxItem)cbb_doctor.SelectedItem).Id;
            string tenBacSi = ((ComboBoxItem)cbb_doctor.SelectedItem).Text;
            int soLuong = Convert.ToInt32(nud_selected.Value);
            string donVi = unit;
            int baoHanh = warranty;
            int gia = priceOfCur * soLuong;
            string lichThucHien = start.ToString("dd/MM/yyyy HH:mm");
            
            addInvoice.data_service.Rows.Add(maDichVu, dichVu, tenBacSi, soLuong, donVi, baoHanh, gia, lichThucHien, "", maBacSi, time);
            Close();
        }
    }
}
