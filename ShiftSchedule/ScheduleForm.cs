using Google.OrTools.LinearSolver;
using Guna.UI2.WinForms;
using NhaKhoaCuoiKy.Helpers;
using NhaKhoaCuoiKy.Models;
using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;

namespace NhaKhoaCuoiKy.ShiftSchedule
{
    public partial class ScheduleForm : Form
    {
        public ScheduleForm()
        {
            InitializeComponent();
        }

        List<EmployeeModel> employees = new List<EmployeeModel>();
        List<Guna2DataGridView> week = new List<Guna2DataGridView>();
        int weekNum;
        private void ScheduleForm_Load(object sender, EventArgs e)
        {
            DateTime inputDate = DateTime.Now;
            CultureInfo currentCulture = CultureInfo.CurrentCulture;

            // Define the first day of the current year
            DateTime firstDayOfYear = new DateTime(inputDate.Year, 1, 1);

            // Define the first day of the next year
            DateTime firstDayOfNextYear = new DateTime(inputDate.Year + 1, 1, 1);

            // Initialize a counter for the number of weeks
            int numberOfWeeks = 0;

            // Iterate through each week of the year
            for (DateTime date = firstDayOfYear; date < firstDayOfNextYear; date = date.AddDays(7))
            {
                // Check if the current week's starting day is within the current year
                if (date.Year == inputDate.Year)
                {
                    numberOfWeeks++;
                }
            }
            weekNum = currentCulture.Calendar.GetWeekOfYear(
                inputDate,
                CalendarWeekRule.FirstDay,
                DayOfWeek.Monday);
            for (int i = 1; i <= numberOfWeeks; i++)
            {
                guna2ComboBox1.Items.Add(i);
                if (weekNum == i) guna2ComboBox1.SelectedIndex = i - 1;
            }

            // Get the week number


            // Get the starting day of the week
            DateTime startOfWeek = inputDate.AddDays(-(int)inputDate.DayOfWeek).AddDays(1);
            label_timeinfo.Text = $"Năm {inputDate.Year}. Tuần thứ: {weekNum}. Bắt đầu từ ngày: {startOfWeek.ToString("dd/MM")} tới ngày {startOfWeek.AddDays(7).ToString("dd/MM")}";

            DataTable dt = EmployeeHelper.getAllGuard();
            foreach (DataRow dr in dt.Rows)
            {
                int id = Convert.ToInt32(dr["MaNhanVien"]);
                string name = Convert.ToString(dr["HoVaTen"]);
                EmployeeModel emp = new EmployeeModel(id, name);
                employees.Add(emp);
            }
            /*            data_schedule.Rows.Add("Sáng");
                        data_schedule.Rows.Add("Trưa");
                        data_schedule.Rows.Add("Chiều");*/
            loadTime(startOfWeek, startOfWeek.AddDays(6));

        }

        void loadTime(DateTime monday, DateTime sunday)
        {
            DataTable dt = CalHelper.getTime(monday, sunday);
            if (dt.Rows.Count > 0)
            {
                btn_cal.Enabled = false;
            }
            else
            {
                btn_cal.Enabled = true;
            }
            clear();
            foreach (DataRow dr in dt.Rows)
            {
                int maNhanVien = Convert.ToInt32(dr["MaNhanVien"]);
                string tenNhanVien = Convert.ToString(dr["HoVaTen"]);
                string ca = Convert.ToString(dr["Ca"]).Trim();
                Control morning = Controls.Find(ca, true).FirstOrDefault();
                if (morning is Guna2DataGridView data_moring)
                {
                    EmployeeModel emp = new EmployeeModel(maNhanVien, tenNhanVien);
                    data_moring.Rows.Add(emp.toString());
                }
            }
        }

        void clear()
        {
            for (int i = 2; i <= 8; i++)
            {
                Control morning = Controls.Find($"t{i}s", true).FirstOrDefault();
                if (morning is Guna2DataGridView data_moring)
                {
                    data_moring.Rows.Clear();
                }
                Control lunch = Controls.Find($"t{i}t", true).FirstOrDefault();
                if (lunch is Guna2DataGridView data_lunch)
                {
                    data_lunch.Rows.Clear();
                }
                Control afternoon = Controls.Find($"t{i}c", true).FirstOrDefault();
                if (afternoon is Guna2DataGridView data_afternoon)
                {
                    data_afternoon.Rows.Clear();
                }
            }
        }

        void calcSchedule()
        {
            int days = 7; // Example value, replace with your actual value
            int minResourceConstraint = employees.Count / 3; // Example value, replace with your actual value
            int maxConsecutiveDays = employees.Count - minResourceConstraint; // Example value, replace with your actual value
            double level = 6.0; // Example value, replace with your actual value

            Solver solver = Solver.CreateSolver("SCIP");

            // Generate variables
            Variable[,] var_M = new Variable[days, employees.Count];
            Variable[,] var_E = new Variable[days, employees.Count];
            Variable[,] var_L = new Variable[days, employees.Count];
            Variable[,] var_T = new Variable[days, employees.Count];

            for (int i = 0; i < days; i++)
            {
                for (int j = 0; j < employees.Count; j++)
                {
                    var_M[i, j] = solver.MakeBoolVar($"Morning_{i}_{j}");
                    var_E[i, j] = solver.MakeBoolVar($"Evening_{i}_{j}");
                    var_L[i, j] = solver.MakeBoolVar($"Leave_{i}_{j}");
                    var_T[i, j] = solver.MakeBoolVar($"Lunch_{i}_{j}");
                }
            }

            // Objective function
            Objective obj = solver.Objective();
            for (int i = 0; i < days; i++)
            {
                for (int j = 0; j < employees.Count; j++)
                {
                    obj.SetCoefficient(var_M[i, j], level); // Replace 'level' with your actual value
                    obj.SetCoefficient(var_E[i, j], level); // Replace 'level' with your actual value
                    obj.SetCoefficient(var_T[i, j], level); // Replace 'level' with your actual value
                }
            }
            obj.SetMaximization();

            // Constraints
            // Each employee should work at most one shift per day
            for (int i = 0; i < days; i++)
            {
                for (int j = 0; j < employees.Count; j++)
                {
                    Google.OrTools.LinearSolver.Constraint c = solver.MakeConstraint(1, 1);
                    c.SetCoefficient(var_M[i, j], 1);
                    c.SetCoefficient(var_E[i, j], 1);
                    c.SetCoefficient(var_L[i, j], 1);
                    c.SetCoefficient(var_T[i, j], 1);
                }
            }

            // Minimum resource constraint
            for (int i = 0; i < days; i++)
            {
                Google.OrTools.LinearSolver.Constraint c = solver.MakeConstraint(minResourceConstraint, double.PositiveInfinity);
                Google.OrTools.LinearSolver.Constraint d = solver.MakeConstraint(minResourceConstraint, double.PositiveInfinity);
                Google.OrTools.LinearSolver.Constraint e = solver.MakeConstraint(minResourceConstraint, double.PositiveInfinity);
                for (int j = 0; j < employees.Count; j++)
                {
                    c.SetCoefficient(var_M[i, j], 1);
                    d.SetCoefficient(var_E[i, j], 1);
                    e.SetCoefficient(var_T[i, j], 1);
                }
            }

            // Rest of your constraints...

            // Solve the problem
            Solver.ResultStatus status = solver.Solve();

            if (status == Solver.ResultStatus.OPTIMAL)
            {
                Console.WriteLine("Optimal");
            }
            else
            {
                Console.WriteLine("No optimal solution found.");
            }
            // Loop through each day and each employee to print their shift assignments
            for (int i = 0; i < days; i++)
            {
                for (int j = 0; j < employees.Count; j++)
                {

                    if (var_M[i, j].SolutionValue() == 1)
                    {

                        Control morning = Controls.Find($"t{i + 2}s", true).FirstOrDefault();
                        if (morning is Guna2DataGridView data_moring)
                        {
                            data_moring.Rows.Add(employees[j].toString());
                        }

                    }
                    else if (var_E[i, j].SolutionValue() == 1)
                    {
                        Control morning = Controls.Find($"t{i + 2}c", true).FirstOrDefault();
                        if (morning is Guna2DataGridView data_moring)
                        {
                            data_moring.Rows.Add(employees[j].toString());
                        }
                    }
                    else if (var_T[i, j].SolutionValue() == 1)
                    {
                        Control morning = Controls.Find($"t{i + 2}t", true).FirstOrDefault();
                        if (morning is Guna2DataGridView data_moring)
                        {
                            data_moring.Rows.Add(employees[j].toString());
                        }
                    }
                    else if (var_L[i, j].SolutionValue() == 1)
                    {
                        Console.WriteLine($"Employee {j + 1} takes leave.");
                    }
                }
            }
        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int weekNum_ = guna2ComboBox1.SelectedIndex + 1;
            if (weekNum_ < weekNum)
            {
                btn_cal.Enabled = false;
            }
            else btn_cal.Enabled = true;

            // Get the current year
            int currentYear = DateTime.Now.Year;

            // Define the first day of the year
            DateTime firstDayOfYear = new DateTime(currentYear, 1, 1);

            // Calculate the first day of the selected week
            DateTime firstDayOfWeek = firstDayOfYear.AddDays((weekNum_ - 1) * 7);

            // Find the Monday in the selected week
            DateTime mondayOfWeek = firstDayOfWeek.AddDays(-(int)firstDayOfWeek.DayOfWeek).AddDays(1);
            label_timeinfo.Text = $"Năm {DateTime.Now.Year}. Tuần thứ: {weekNum_}. Bắt đầu từ ngày: {mondayOfWeek.ToString("dd/MM")} tới ngày {mondayOfWeek.AddDays(7).ToString("dd/MM")}";
            loadTime(mondayOfWeek, mondayOfWeek.AddDays(6));
        }

        void insertData(DateTime date)
        {
            for (int i = 2; i <= 8; i++)
            {
                Control morning = Controls.Find($"t{i}s", true).FirstOrDefault();
                if (morning is Guna2DataGridView data_moring)
                {
                    foreach(DataGridViewRow dr in data_moring.Rows)
                    {
                        int maNhanVien = Convert.ToInt32(dr.Cells[0].Value.ToString().Split(" ")[0]);
                        string ca = $"t{i}s";
                        DateTime ngay = date.AddDays(i - 2);
                        CalHelper.addTime(maNhanVien, ca, ngay);
                    }
                }
                Control lunch = Controls.Find($"t{i}t", true).FirstOrDefault();
                if (lunch is Guna2DataGridView data_lunch)
                {
                    foreach(DataGridViewRow dr in data_lunch.Rows)
                    {
                        int maNhanVien = Convert.ToInt32(dr.Cells[0].Value.ToString().Split(" ")[0]);
                        string ca = $"t{i}t";
                        DateTime ngay = date.AddDays(i - 2);
                        CalHelper.addTime(maNhanVien, ca, ngay);
                    }
                }
                Control afternoon = Controls.Find($"t{i}c", true).FirstOrDefault();
                if (afternoon is Guna2DataGridView data_afternoon)
                {
                    foreach (DataGridViewRow dr in data_afternoon.Rows)
                    {
                        int maNhanVien = Convert.ToInt32(dr.Cells[0].Value.ToString().Split(" ")[0]);
                        string ca = $"t{i}c";
                        DateTime ngay = date.AddDays(i - 2);
                        CalHelper.addTime(maNhanVien, ca, ngay);
                    }
                }
            }
        }

        private void btn_cal_Click(object sender, EventArgs e)
        {
            calcSchedule();
            int weekNum_ = guna2ComboBox1.SelectedIndex + 1;
            if (weekNum_ < weekNum)
            {
                btn_cal.Enabled = false;
            }
            else btn_cal.Enabled = true;

            // Get the current year
            int currentYear = DateTime.Now.Year;

            // Define the first day of the year
            DateTime firstDayOfYear = new DateTime(currentYear, 1, 1);

            // Calculate the first day of the selected week
            DateTime firstDayOfWeek = firstDayOfYear.AddDays((weekNum_ - 1) * 7);

            // Find the Monday in the selected week
            DateTime mondayOfWeek = firstDayOfWeek.AddDays(-(int)firstDayOfWeek.DayOfWeek).AddDays(1);
            insertData(mondayOfWeek);
        }
    }
}

