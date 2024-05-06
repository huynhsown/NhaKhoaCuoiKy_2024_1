using Guna.UI2.WinForms;
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
    public partial class CreateAppointment : Form
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

        List<EmptyScheduleModel> emptySchedules;
        public EventHandler createEditSuccees;
        public DateTime date { get; set; }
        int mode;
        int doctor_id;
        public CreateAppointment(List<EmptyScheduleModel> emptySchedules, DateTime date, int mode, int doctor_id)
        {
            InitializeComponent();
            this.emptySchedules = emptySchedules;
            this.date = date;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
            this.mode = mode;
            this.doctor_id = doctor_id;
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void CreateAppointment_Load(object sender, EventArgs e)
        {
            foreach (EmptyScheduleModel model in emptySchedules)
            {
                data_time.Rows.Add(model.Start.TimeOfDay, model.End.TimeOfDay);
            }
            time_picker.CustomFormat = "HH:mm:ss";

        }

        bool checkEmptyTime()
        {
            foreach (EmptyScheduleModel model in emptySchedules)
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

        private void btn_createAppointment_Click(object sender, EventArgs e)
        {
            List<PictureBox> pictureBoxes = new List<PictureBox>()
            {
                pb_name, pb_address, pb_phone, pb_timepicker, pb_time
            };
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
            DateTime real_time = date.Add(time_picker.Value.TimeOfDay);
            if(AppointmentHelper.createAppointment(
                doctor_id, tb_name.Text.Trim(),
                tb_phone.Text.Trim(),
                tb_address.Text.Trim(),
                real_time,
                Convert.ToInt32(tb_time.Text),
                tb_detail.Text.Trim()))
            {
                MessageBox.Show("Tạo lịch hẹn thành công", "Lịch hẹn", MessageBoxButtons.OK, MessageBoxIcon.Information);
                createEditSuccees?.Invoke(sender, e);
                Close();
            }
            else
            {
                MessageBox.Show("Tạo lịch hẹn thất bại", "Lịch hẹn", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        private void warningValidate(PictureBox picbox, Guna2TextBox tb, bool check)
        {
            picbox.Visible = (check || tb.Text.Length == 0) ? false : true;
            tb.BorderColor = (check || tb.Text.Length == 0) ? Color.Black : Color.Red;
            tb.BorderThickness = (check || tb.Text.Length == 0) ? 1 : 3;
        }

        private void tb_name_TextChanged(object sender, EventArgs e)
        {
            warningValidate(pb_name, tb_name, ValidateVNI.validateNameVNI(tb_name.Text));
        }

        private void tb_phone_TextChanged(object sender, EventArgs e)
        {
            warningValidate(pb_phone, tb_phone, ValidateVNI.validateNumber(tb_phone.Text));
        }

        private void time_picker_ValueChanged(object sender, EventArgs e)
        {
            TimeSpan startWork = TimeSpan.FromHours(8)
                      .Add(TimeSpan.FromMinutes(0))
                      .Add(TimeSpan.FromSeconds(0));
            TimeSpan endWord = TimeSpan.FromHours(21)
                      .Add(TimeSpan.FromMinutes(0))
                      .Add(TimeSpan.FromSeconds(1));
            bool check = time_picker.Value.TimeOfDay < startWork || time_picker.Value.TimeOfDay >= endWord;
            pb_timepicker.Visible = (check) ? true : false;
            time_picker.BorderColor = check ? Color.Red : Color.Transparent;
            time_picker.BorderThickness = check ? 3 : 1;
        }

        private void tb_time_TextChanged(object sender, EventArgs e)
        {
            warningValidate(pb_time, tb_time, ValidateVNI.validateNumber(tb_time.Text));
        }
    }
}
