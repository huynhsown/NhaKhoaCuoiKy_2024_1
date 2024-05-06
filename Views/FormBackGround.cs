using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NhaKhoaCuoiKy.Views
{
    public partial class FormBackGround : Form
    {

        private MainForm mainForm;
        public FormBackGround()
        {
            InitializeComponent();
        }

        public FormBackGround(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
        }

        private void FormBackGround_Load_1(object sender, EventArgs e)
        {
            this.Size = new Size(mainForm.Size.Width-10, mainForm.Size.Height-5); 
            this.Location = new Point(mainForm.Location.X+5, mainForm.Location.Y);
        }
    }
}
