using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NCAAProject
{
    public partial class MainForm : Form
    {

        private CoachForm coachForm;
        
        //private Form form2;
        //private Form form3;
        //private Form form4;

        public MainForm()
        {
            InitializeComponent();

            // Create and initialize the panel
            panel1 = new Panel();
            panel1.Dock = DockStyle.Fill;

            // Add the panel to the form
            Controls.Add(panel1);
        }


        private void button1_Click(object sender, EventArgs e)
        {
            // create instance of Form1
            CoachForm form = new CoachForm();

            // add Form1 to the panel
            //form1.TopLevel = false;
            //form1.Dock = DockStyle.Fill;
            //form1.Parent = panel1;
            //form1.AutoScroll = true;
            //form1.FormBorderStyle = FormBorderStyle.None;
            //panel1.Controls.Clear();
            //panel1.Controls.Add(form);
            form.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // create instance of Form1
            TourneyForm form = new TourneyForm();

            // display Form1
            form.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // create instance of Form1
            CoachForm form1 = new CoachForm();

            // display Form1
            form1.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // create instance of Form1
            CoachForm form1 = new CoachForm();

            // display Form1
            form1.Show();
        }


    }
}
