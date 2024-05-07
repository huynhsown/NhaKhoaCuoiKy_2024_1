using Google.OrTools.LinearSolver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhaKhoaCuoiKy.ShiftSchedule
{
    internal class CalcSchedule
    {
        public static void calcSchedule()
        {
            int days = 7; // Example value, replace with your actual value
            int employees = 7; // Example value, replace with your actual value
            int minResourceConstraint = employees / 3; // Example value, replace with your actual value
            int maxConsecutiveDays = employees - minResourceConstraint; // Example value, replace with your actual value
            double level = 6.0; // Example value, replace with your actual value

            Solver solver = Solver.CreateSolver("SCIP");

            // Generate variables
            Variable[,] var_M = new Variable[days, employees];
            Variable[,] var_E = new Variable[days, employees];
            Variable[,] var_L = new Variable[days, employees];
            Variable[,] var_T = new Variable[days, employees];

            for (int i = 0; i < days; i++)
            {
                for (int j = 0; j < employees; j++)
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
                for (int j = 0; j < employees; j++)
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
                for (int j = 0; j < employees; j++)
                {
                    Constraint c = solver.MakeConstraint(1, 1);
                    c.SetCoefficient(var_M[i, j], 1);
                    c.SetCoefficient(var_E[i, j], 1);
                    c.SetCoefficient(var_L[i, j], 1);
                    c.SetCoefficient(var_T[i, j], 1);
                }
            }

            // Minimum resource constraint
            for (int i = 0; i < days; i++)
            {
                Constraint c = solver.MakeConstraint(minResourceConstraint, double.PositiveInfinity);
                Constraint d = solver.MakeConstraint(minResourceConstraint, double.PositiveInfinity);
                Constraint e = solver.MakeConstraint(minResourceConstraint, double.PositiveInfinity);
                for (int j = 0; j < employees; j++)
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
            int[] sang = new int[days];
            int[] chieu = new int[days];
            int[] trua = new int[days];
            int[] sum = new int[days];
            for (int i = 0; i < days; i++)
            {
                Console.WriteLine($"Day {i + 1}:");
                for (int j = 0; j < employees; j++)
                {
                    if (var_M[i, j].SolutionValue() == 1)
                    {
                        Console.WriteLine($"Employee {j + 1} works in the morning.");
                        sang[i]++;
                        sum[i]++;
                    }
                    else if (var_E[i, j].SolutionValue() == 1)
                    {
                        Console.WriteLine($"Employee {j + 1} works in the evening.");
                        chieu[i]++;
                        sum[i]++;
                    }
                    else if (var_T[i, j].SolutionValue() == 1)
                    {
                        Console.WriteLine($"Employee {j + 1} works in the lunch.");
                        trua[i]++;
                        sum[i]++;
                    }
                    else if (var_L[i, j].SolutionValue() == 1)
                    {
                        Console.WriteLine($"Employee {j + 1} takes leave.");
                    }
                }
                Console.WriteLine();
            }

            MessageBox.Show("Morning shifts each day:" + string.Join(", ", sang));
            MessageBox.Show("Evening shifts each day:" + string.Join(", ", chieu));
            MessageBox.Show("Lunch shifts each day:" + string.Join(", ", trua));
            MessageBox.Show("Total shifts each day:" + string.Join(", ", sum));
        }
    }
}
