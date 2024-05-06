using NhaKhoaCuoiKy.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhaKhoaCuoiKy.Helpers
{
    public class ViewHelper
    {
        public static void loadForm(Form form, MainForm mainForm)
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
    }
}
