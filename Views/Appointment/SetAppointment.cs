using NhaKhoaCuoiKy.Helpers;
using NhaKhoaCuoiKy.Models;
using NhaKhoaCuoiKy.Views.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Xml.Schema;

namespace NhaKhoaCuoiKy.Views.Appointment
{
    public partial class SetAppointment : Form
    {
        public SetAppointment()
        {
            InitializeComponent();
        }

        private MainForm mainForm;
        private int doctor_id;
        List<ScheduleModel> schedules = new List<ScheduleModel>();
        List<EmptyScheduleModel> emptySchedules = new List<EmptyScheduleModel>();
        private CreateAppointment createAppointment;

        public SetAppointment(int doctor_id, MainForm mainForm)
        {
            InitializeComponent();
            this.doctor_id = doctor_id;
            this.mainForm = mainForm;
            guna2Button1.Text = doctor_id.ToString();
        }

        private void SetAppointment_Load(object sender, EventArgs e)
        {
            DateOnly today = DateOnly.Parse(DateTime.Now.ToShortDateString());
            loadWork(today);
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            DateOnly picker = DateOnly.Parse(monthCalendar.SelectionRange.Start.ToShortDateString());
            DateOnly today = DateOnly.Parse(DateTime.Now.ToShortDateString());
            loadWork(picker);
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

        private void loadWork(DateOnly date)
        {
            schedule_label.Text = "Lịch trình ngày: " + monthCalendar.SelectionRange.Start.ToString("dd/M/yyyy");
            data_bacsi.Rows.Clear();
            schedules.Clear();
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
                schedules = schedules.OrderBy(schedule => schedule.Start).ToList();
                if (schedules.Count == 0)
                {
                    emptySchedules.Add(new EmptyScheduleModel(DateTime.Parse(date.ToShortDateString()).Add(startWork), DateTime.Parse(date.ToShortDateString()).Add(endWork)));
                    return;
                }
                for (int i = 0; i < schedules.Count; i++)
                {
                    ScheduleModel schedule = schedules[i];
                    data_bacsi.Rows.Add(i + 1, schedule.ScheduleType, schedule.Start.ToString("HH:mm:ss"), schedule.End.ToString("HH:mm:ss"), schedule.ScheduleTypeID);
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            createAppointment?.Dispose();
            loadForm(createAppointment = new CreateAppointment(emptySchedules, monthCalendar.SelectionRange.Start, 0, doctor_id));
        }

        private void data_bacsi_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int col = e.ColumnIndex;
            int row = e.RowIndex;
            if (data_bacsi.Columns[col].Name == "col_btn_detail")
            {
                loadForm(new EditAppointment(Convert.ToInt32(data_bacsi.Rows[row].Cells[4].Value), emptySchedules, schedules));
            }
            if (data_bacsi.Columns[col].Name == "col_btn_delete")
            {
                if (data_bacsi.Rows[row].Cells[1].Value.ToString() == "Lịch hẹn")
                {
                    DialogResult dr = MessageBox.Show("Bạn có chắc xóa lịch hẹn này", "Lịch hẹn", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == DialogResult.Yes)
                    {

                    }
                }
            }
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            DateOnly picker = DateOnly.Parse(monthCalendar.SelectionRange.Start.ToShortDateString());
            DateOnly today = DateOnly.Parse(DateTime.Now.ToShortDateString());
            if(picker.Day < today.Day)
            {
                MessageBox.Show("Không thể đặt lịch", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            loadForm(createAppointment = new CreateAppointment(emptySchedules, monthCalendar.SelectionRange.Start, 0, doctor_id));
        }
    }
}
