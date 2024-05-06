using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NhaKhoaCuoiKy.Helpers;
using System.Windows.Forms;

namespace NhaKhoaCuoiKy.Views.PatientForm
{
    public partial class HistoryForm : Form
    {
        int patientID;
        MainForm mainForm;
        public HistoryForm(int patientID, MainForm mainForm)
        {
            InitializeComponent();
            this.patientID = patientID;
            this.mainForm = mainForm;
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void HistoryForm_Load(object sender, EventArgs e)
        {
            try
            {
                loadRecord(PatientHelper.getRecordByPatient(patientID));
                loadInvoice(PatientHelper.getInvoiceByPatient(patientID));
            }
            catch (Exception ex)
            {

            }
        }

        void loadRecord(DataTable dt)
        {
            data_record.Rows.Clear();
            foreach (DataRow row in dt.Rows)
            {
                string employeeID = row["MaNhanVien"].ToString();
                string name = row["HoVaTen"].ToString();
                string recordID = row["MaBenhAn"].ToString();
                string diagnose = row["ChanDoan"].ToString();
                string date = Convert.ToDateTime(row["NgayLapBenhAn"]).ToString("dd/MM/yyyy");
                data_record.Rows.Add(employeeID, name, recordID, diagnose, date);
            }
        }

        void loadInvoice(DataTable dt)
        {
            data_invoice.Rows.Clear();
            foreach (DataRow row in dt.Rows)
            {
                string invoiceID = row["MaHoaDon"].ToString();
                string date = Convert.ToDateTime(row["NgayThamKham"]).ToString("dd/MM/yyyy");
                data_invoice.Rows.Add(invoiceID, date);
            }
        }

        private void data_record_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
            int recordID = Convert.ToInt32(data_record.Rows[e.RowIndex].Cells[2].Value);
            if (data_record.Columns[e.ColumnIndex].Name == "col_btn_record_info")
            {
                AddNewRecord addNewRecord = new AddNewRecord(patientID, recordID);
                mainForm.openChildFormHaveData(addNewRecord);
            }
            if (data_record.Columns[e.ColumnIndex].Name == "col_btn_record_delete")
            {
                DialogResult dr = MessageBox.Show("Bạn có chắc chắn muốn xóa bệnh án này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    if (PatientHelper.removeRecordByID(recordID))
                    {
                        MessageBox.Show("Bệnh án đã được xóa thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        loadRecord(PatientHelper.getRecordByPatient(patientID));
                    }
                    else
                    {
                        MessageBox.Show("Xóa bệnh án thất bại. Vui lòng thử lại sau.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void data_invoice_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex ==  -1) return;
            int invoiceID = Convert.ToInt32(data_invoice.Rows[e.RowIndex].Cells[0].Value);
            if (data_invoice.Columns[e.ColumnIndex].Name == "col_btn_invoice_info")
            {
                AddInvoice addInvoice = new AddInvoice(patientID, mainForm, true, invoiceID);
                mainForm.openChildFormHaveData(addInvoice);
            }
            if (data_invoice.Columns[e.ColumnIndex].Name == "col_btn_invoice_delete")
            {
                DialogResult dr = MessageBox.Show("Bạn có chắc chắn muốn xóa hóa đơn này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    if (PatientHelper.removeInvoiceByID(invoiceID))
                    {
                        MessageBox.Show("Hóa hơn đã được xóa thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        loadInvoice(PatientHelper.getInvoiceByPatient(patientID));
                    }
                    else
                    {
                        MessageBox.Show("Xóa hóa đơn thất bại. Vui lòng thử lại sau.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
