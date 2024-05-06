using Guna.UI2.WinForms;
using NhaKhoaCuoiKy.dbs;
using NhaKhoaCuoiKy.Views.Employee;
using NhaKhoaCuoiKy.Views.Employee.Medicines;
using NhaKhoaCuoiKy.Views.Appointment;
using NhaKhoaCuoiKy.Views.Service;
using NhaKhoaCuoiKy.Models;
using NhaKhoaCuoiKy.Views.LogIn;
using System.Text;

namespace NhaKhoaCuoiKy.Views
{
    public partial class MainForm : Form
    {
        UserModel userAccount;
        LoginForm loginForm;
        public MainForm(UserModel userAccount, LoginForm loginForm)
        {
            InitializeComponent();
            this.userAccount = userAccount;
            this.loginForm = loginForm;
        }

        private Form currentForm;
        private Patient patient = new Patient();
        private Servicee service;
        private Doctor doctor = new Doctor();
        private Guard guard = new Guard();
        private Nurse nurse = new Nurse();
        private Medicine medicine = new Medicine();
        private UserManagement userManagement;

        private AppointMent newAppointment;
        private void MainForm_Load(object sender, EventArgs e)
        {
            panel_btn_employee.AutoSize = true;
            if (userAccount.decentralization != 0)
            {
                btn_employee.Visible = false;
                btn_employee.Enabled = false;
                btn_service.Visible = false;
                btn_service.Enabled = false;
            }
        }

        public void openChildForm(Form childForm)
        {
            if (currentForm != null) currentForm.Close();
            currentForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelMain.Controls.Add(childForm);
            panelMain.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();

        }

        public void openChildFormHaveData(Form childForm)
        {
            currentForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelMain.Controls.Add(childForm);
            panelMain.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        #region View
        private void btn_home_Click(object sender, EventArgs e)
        {
            closeAllPanel();
        }

        private void btn_home_MouseEnter(object sender, EventArgs e)
        {
            buttonMouseEnter(btn_home);
        }

        private void btn_home_MouseLeave(object sender, EventArgs e)
        {
            buttonMouseLeave(btn_home);
        }

        private void btn_patient_MouseEnter(object sender, EventArgs e)
        {
            buttonMouseEnter(btn_patient);
        }

        private void btn_patient_MouseLeave(object sender, EventArgs e)
        {
            buttonMouseLeave(btn_patient);
        }

        private void btn_patient_Click(object sender, EventArgs e)
        {
            closeAllPanel();
            patient?.Close();
            openChildForm(patient = new Patient(this, userAccount));
        }

        private void btn_employee_MouseEnter(object sender, EventArgs e)
        {
            buttonMouseEnter(btn_employee);
        }

        private void btn_employee_MouseLeave(object sender, EventArgs e)
        {
            if (!panel_btn_employee.Visible)
            {
                buttonMouseLeave(btn_employee);
            }
        }

        private void btn_employee_Click(object sender, EventArgs e)
        {
            if (!panel_btn_employee.Visible)
            {
                buttonMouseEnter(btn_employee);
                panel_btn_employee.Visible = true;
                return;
            }
            closeAllPanel();
            btn_employee.FillColor = Color.Transparent;

        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void closeAllPanel()
        {
            List<Panel> panelList = new List<Panel>() { panel_btn_employee };
            List<Guna2Button> guna2Buttons = new List<Guna2Button>()
            { btn_home,
                btn_logout,
                btn_employee,
                btn_patient
            };
            foreach (Panel p in panelList)
            {
                p.Visible = false;
            }
            foreach (Guna2Button button in guna2Buttons)
            {
                buttonMouseLeave(button);
            }
        }

        private void buttonMouseEnter(Guna2Button btn)
        {
            btn.FillColor = Color.DarkGray;
        }

        private void buttonMouseLeave(Guna2Button btn)
        {
            btn.FillColor = Color.Transparent;
        }

        private void btn_service_Click(object sender, EventArgs e)
        {
            closeAllPanel();
            service?.Close();
            openChildForm(service = new Servicee(this, userAccount));
        }

        private void btn_employee_Doctor_Click(object sender, EventArgs e)
        {
            closeAllPanel();
            doctor?.Close();
            openChildForm(doctor = new Doctor(this));

        }

        private void btn_protector_Click(object sender, EventArgs e)
        {
            closeAllPanel();
            guard?.Close();
            openChildForm(guard = new Guard(this));
        }

        private void btn_nurse_Click(object sender, EventArgs e)
        {
            closeAllPanel();
            nurse?.Close();
            openChildForm(nurse = new Nurse(this));

        }

        private void btn_medicine_Click(object sender, EventArgs e)
        {
            closeAllPanel();
            medicine?.Close();
            openChildForm(medicine = new Medicine(this, userAccount));

        }
        private void btn_appointment_Click(object sender, EventArgs e)
        {
            if (userAccount.decentralization == 1)
            {
                SetAppointment sa = new SetAppointment(userAccount.employeeID, this);
                sa.btn_back.Visible = false;
                sa.btn_back.Enabled = false;
                closeAllPanel();
                openChildForm(sa);
                return;
            }
            closeAllPanel();
            newAppointment?.Close();
            openChildForm(newAppointment = new AppointMent(this));
        }

        private void btn_logout_MouseEnter(object sender, EventArgs e)
        {
            btn_logout.FillColor = Color.Red;
        }

        private void btn_logout_MouseLeave(object sender, EventArgs e)
        {
            buttonMouseLeave(btn_logout);
        }

        private void btn_logout_Click(object sender, EventArgs e)
        {
            loginForm.Owner = null;
            Close();
            loginForm.removeInfoWhenLogout();
            loginForm.Show();
        }

        private void btn_user_Click(object sender, EventArgs e)
        {
            if (userAccount.decentralization == 0)
            {
                closeAllPanel();
                userManagement?.Close();
                openChildForm(userManagement = new UserManagement(this, userAccount));
            }
        }
        #endregion
    }
}
