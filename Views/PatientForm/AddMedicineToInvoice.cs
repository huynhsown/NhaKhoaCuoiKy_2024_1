using NhaKhoaCuoiKy.Helpers;
using NhaKhoaCuoiKy.Models;
using System.Data;
using System.Runtime.InteropServices;


namespace NhaKhoaCuoiKy.Views.PatientForm
{
    public partial class AddMedicineToInvoice : Form
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

        AddInvoice addInvoice;

        public AddMedicineToInvoice(AddInvoice addInvoice)
        {
            InitializeComponent();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
            this.addInvoice = addInvoice;
        }

        private void AddMedicineToInvoice_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = MedicineHelper.getAllMedicine();
                foreach (DataRow dr in dt.Rows)
                {
                    string id = dr["MaThuoc"].ToString();
                    string label = dr["TenThuoc"].ToString();
                    cbb_medicine.Items.Add(new ComboBoxItemString(id, label));

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

        int priceOfCur = -1;

        private void cbb_medicine_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbb_medicine.SelectedIndex == 0)
            {
                priceOfCur = -1;
                tb_amount.Text = string.Empty;
                nud_selected.Value = 0;
                return;
            }
            try
            {
                ComboBoxItemString cbi = (ComboBoxItemString)cbb_medicine.SelectedItem;
                DataTable dt = MedicineHelper.getMedicineByID(cbi.Id);
                int index = haveInGrid(cbi.Id);
                int amount = calAmoutMedicine(Convert.ToInt32(dt.Rows[0]["SoLuong"]), index);
                priceOfCur = Convert.ToInt32(dt.Rows[0]["GiaBan"]);
                tb_amount.Text = $"Số lượng: {amount}";
                nud_selected.Maximum = amount;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        int haveInGrid(string medicineID)
        {
            for(int i=0; i<addInvoice.data_medicine.Rows.Count; i++)
            {
                if (addInvoice.data_medicine.Rows[i].Cells[0].Value.ToString().Trim() == medicineID)
                {
                    return i;
                }
            }
            return -1;
        }

        int calAmoutMedicine(int db_amount, int index)
        {
            int result = db_amount;;
            if(index != -1)
            {
                int grid_amount = Convert.ToInt32(addInvoice.data_medicine.Rows[index].Cells[2].Value);
                result -= grid_amount;
            }
            return result;
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            if (cbb_medicine.SelectedIndex == 0)
            {
                priceOfCur = -1;
                MessageBox.Show("Vui lòng chọn thuốc", "Thêm thuốc", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if(nud_selected.Value == 0)
            {
                MessageBox.Show("Vui lòng chọn số lượng", "Thêm thuốc", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            ComboBoxItemString cbi = (ComboBoxItemString)cbb_medicine.SelectedItem;
            int index = haveInGrid(cbi.Id);
            if (index == -1)
            {
                addInvoice.data_medicine.Rows.Add(cbi.Id, cbi.Text, nud_selected.Value, priceOfCur * nud_selected.Value);
            }
            else
            {
                int grid_amount = Convert.ToInt32(addInvoice.data_medicine.Rows[index].Cells[2].Value);
                int amount = grid_amount + Convert.ToInt32(nud_selected.Value);
                addInvoice.data_medicine.Rows[index].SetValues(new object[] { cbi.Id, cbi.Text, amount, amount * priceOfCur });
            }
            Close();
        }
    }
}
