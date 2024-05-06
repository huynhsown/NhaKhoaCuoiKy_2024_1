using Microsoft.VisualBasic.Devices;
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

namespace NhaKhoaCuoiKy.Views.Appointment
{
    public partial class EditAppointment : Form
    {
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
        int appointmentID;
        List<EmptyScheduleModel> emptySchedules;
        List<EmptyScheduleModel> newEmptySchedules = new List<EmptyScheduleModel>();
        List<ScheduleModel> schedules;
        public EditAppointment(int appointmentID, List<EmptyScheduleModel> emptySchedules, List<ScheduleModel> schedules)
        {
            InitializeComponent();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
            this.appointmentID = appointmentID;
            this.emptySchedules = new List<EmptyScheduleModel>(emptySchedules);
            this.schedules = new List<ScheduleModel>(schedules);
        }

        private void EditAppointment_Load(object sender, EventArgs e)
        {
            loadWork();
            time_picker.CustomFormat = "HH:mm:ss";
        }

        int removeTime(DateTime start)
        {
            int index = schedules.FindIndex(a => a.Start == start);
            return index;
        }

        void loadWork()
        {
            try
            {
                DataTable dt = AppointmentHelper.getAppontmentByID(appointmentID);
                if (dt.Rows.Count == 0)
                {
                    Close();
                }
                TimeSpan lastSecond = TimeSpan.FromHours(23)
                      .Add(TimeSpan.FromMinutes(59))
                      .Add(TimeSpan.FromSeconds(0));
                TimeSpan startWork = TimeSpan.FromHours(8)
                      .Add(TimeSpan.FromMinutes(0))
                      .Add(TimeSpan.FromSeconds(0));
                TimeSpan endWork = TimeSpan.FromHours(21)
                          .Add(TimeSpan.FromMinutes(0))
                          .Add(TimeSpan.FromSeconds(0));
                tb_name.Text = dt.Rows[0][2].ToString();
                tb_phone.Text = dt.Rows[0][3].ToString();
                tb_address.Text = dt.Rows[0][4].ToString();
                time_picker.Value = Convert.ToDateTime(dt.Rows[0][5]);
                tb_time.Text = dt.Rows[0][6].ToString();
                tb_detail.Text = dt.Rows[0][7].ToString();
                int index = schedules.FindIndex(a => a.Start == time_picker.Value);
                if (index == -1) return;
                schedules.RemoveAt(index);
                DateOnly date = DateOnly.FromDateTime(time_picker.Value);
                if (schedules.Count == 0)
                {
                    newEmptySchedules.Add(new EmptyScheduleModel(DateTime.Parse(date.ToShortDateString()), DateTime.Parse(date.ToShortDateString()).Add(lastSecond)));
                    return;
                }
                for (int i = 0; i < schedules.Count; i++)
                {
                    ScheduleModel schedule = schedules[i];
                    if (i == 0 && schedule.Start.TimeOfDay != startWork)
                    {
                        newEmptySchedules.Add(new EmptyScheduleModel(DateTime.Parse(date.ToShortDateString()).Add(startWork), schedule.Start));
                    }
                    if (i < schedules.Count - 1 && schedules[i].End < schedules[i + 1].Start)
                    {
                        newEmptySchedules.Add(new EmptyScheduleModel(schedules[i].End, schedules[i + 1].Start));
                    }
                    if (i == schedules.Count - 1)
                    {
                        if (schedule.End.TimeOfDay != lastSecond)
                        {
                            newEmptySchedules.Add(new EmptyScheduleModel(schedule.End, DateTime.Parse(date.ToShortDateString()).Add(endWork)));
                        }
                    }
                }

                foreach (EmptyScheduleModel model in newEmptySchedules)
                {
                    if (model.Start.TimeOfDay <= time_picker.Value.TimeOfDay && time_picker.Value.TimeOfDay <= model.End.TimeOfDay)
                    {

                        if (model.Start.TimeOfDay < time_picker.Value.TimeOfDay)
                        {
                            DataGridViewRow dgvr1 = new DataGridViewRow();
                            dgvr1.Cells.Add(new DataGridViewTextBoxCell());
                            dgvr1.Cells.Add(new DataGridViewTextBoxCell());
                            dgvr1.Cells[0].Value = model.Start.TimeOfDay;
                            dgvr1.Cells[1].Value = time_picker.Value.TimeOfDay;
                            data_time.Rows.Add(dgvr1);
                        }

                        if (time_picker.Value.TimeOfDay < time_picker.Value.AddMinutes(Convert.ToInt32(tb_time.Text)).TimeOfDay)
                        {
                            DataGridViewRow dgvr2 = new DataGridViewRow();
                            dgvr2.Cells.Add(new DataGridViewTextBoxCell());
                            dgvr2.Cells.Add(new DataGridViewTextBoxCell());
                            dgvr2.Cells[0].Value = time_picker.Value.TimeOfDay;
                            dgvr2.Cells[1].Value = time_picker.Value.AddMinutes(Convert.ToInt32(tb_time.Text)).TimeOfDay;
                            data_time.Rows.Add(dgvr2);
                        }

                        if (time_picker.Value.AddMinutes(Convert.ToInt32(tb_time.Text)).TimeOfDay < model.End.TimeOfDay) {
                            DataGridViewRow dgvr3 = new DataGridViewRow();
                            dgvr3.Cells.Add(new DataGridViewTextBoxCell());
                            dgvr3.Cells.Add(new DataGridViewTextBoxCell());
                            dgvr3.Cells[0].Value = time_picker.Value.AddMinutes(Convert.ToInt32(tb_time.Text)).TimeOfDay;
                            dgvr3.Cells[1].Value = model.End.TimeOfDay;
                            data_time.Rows.Add(dgvr3);
                        }

                    }
                    else
                    {
                        DataGridViewRow dgvr = new DataGridViewRow();
                        dgvr.Cells.Add(new DataGridViewTextBoxCell());
                        dgvr.Cells.Add(new DataGridViewTextBoxCell());
                        dgvr.Cells[0].Value = model.Start.TimeOfDay;
                        dgvr.Cells[1].Value = model.End.TimeOfDay;
                        data_time.Rows.Add(dgvr);
                    }
                }

                foreach(DataGridViewRow dgvr in data_time.Rows)
                {
                    TimeOnly timeOnly;
                    TimeOnly.TryParse(dgvr.Cells[0].Value.ToString(), out timeOnly);
                    if(timeOnly.ToTimeSpan() == time_picker.Value.TimeOfDay)
                    {
                        dgvr.DefaultCellStyle.BackColor = Color.Yellow;
                    }
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

        bool checkEmptyTime()
        {
            foreach (EmptyScheduleModel model in newEmptySchedules)
            {
                DateTime dt = time_picker.Value;
                DateTime end = dt.AddMinutes(Convert.ToInt32(tb_time.Text));
                if (dt.TimeOfDay >= model.Start.TimeOfDay && end.TimeOfDay <= model.End.TimeOfDay)
                {
                    return true;
                }
            }
            return false;
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            string inform = string.Empty;
            if (pb_name.Visible == true) inform = "Tên khách hàng không chứa số hoặc ký tự đặc biệt";
            else if (pb_address.Visible == true) inform = "Địa chỉ không hợp lệ";
            else if (pb_phone.Visible == true) inform = "Số điện thoại chỉ chứa số";
            else if (pb_timepicker.Visible == true) inform = "Thời gian không hợp lệ";
            else if (pb_time.Visible == true) inform = "Thời gian không hợp lệ";
            if (inform != string.Empty)
            {
                MessageBox.Show(inform, "Lịch hẹn", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (!checkEmptyTime())
            {
                MessageBox.Show("Trùng với lịch trình!", "Lịch trình", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            MessageBox.Show(newEmptySchedules.Count.ToString());
            return;
            DateTime real_time = time_picker.Value;
            if (AppointmentHelper.updateAppointment(
                appointmentID, tb_name.Text.Trim(),
                tb_phone.Text.Trim(),
                tb_address.Text.Trim(),
                real_time,
                Convert.ToInt32(tb_time.Text),
                tb_detail.Text.Trim()))
            {
                MessageBox.Show("Sửa lịch hẹn thành công", "Lịch hẹn", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
            else
            {
                MessageBox.Show("Sửa lịch hẹn thất bại", "Lịch hẹn", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
