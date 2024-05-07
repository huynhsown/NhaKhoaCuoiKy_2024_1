using Google.OrTools.LinearSolver;
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

        private void ScheduleForm_Load(object sender, EventArgs e)
        {
            DateTime inputDate = DateTime.Now;

            CultureInfo currentCulture = CultureInfo.CurrentCulture;

            // Get the week number
            var weekNum = currentCulture.Calendar.GetWeekOfYear(
                inputDate,
                CalendarWeekRule.FirstDay,
                DayOfWeek.Monday);

            // Get the starting day of the week
            var startOfWeek = inputDate.AddDays(-(int)inputDate.DayOfWeek);
            label_timeinfo.Text = $"Năm {inputDate.Year}. Tuần thứ: {weekNum}. Bắt đầu từ ngày: {startOfWeek.ToString("dd/MM")} tới ngày {startOfWeek.AddDays(7).ToString("dd/MM")}";

            DataTable dt = EmployeeHelper.getAllGuard();
            foreach (DataRow dr in dt.Rows)
            {
                int id = Convert.ToInt32(dr["MaNhanVien"]);
                string name = Convert.ToString(dr["HoVaTen"]);
                EmployeeModel emp = new EmployeeModel(id, name);
                employees.Add(emp);
            }
            data_schedule.Rows.Add("Sáng");
            data_schedule.Rows.Add("Trưa");
            data_schedule.Rows.Add("Chiều");
            calcSchedule();

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
                string sang = "";
                string trua = "";
                string chieu = "";
                for (int j = 0; j < employees.Count; j++)
                {
                    if (var_M[i, j].SolutionValue() == 1)
                    {
                        sang += "Nhân viên: " + employees[j].toString() + '\n';
                    }
                    else if (var_E[i, j].SolutionValue() == 1)
                    {
                        chieu += "Nhân viên: " + employees[j].toString() + '\n';
                    }
                    else if (var_T[i, j].SolutionValue() == 1)
                    {
                        trua += "Nhân viên: " + employees[j].toString() + '\n';
                    }
                    else if (var_L[i, j].SolutionValue() == 1)
                    {
                        Console.WriteLine($"Employee {j + 1} takes leave.");
                    }
                }
                data_schedule.Rows[0].Cells[$"Column{i + 1}"].Value = sang;
                data_schedule.Rows[1].Cells[$"Column{i + 1}"].Value = trua;
                data_schedule.Rows[2].Cells[$"Column{i + 1}"].Value = chieu;
            }
        }
    }
}

