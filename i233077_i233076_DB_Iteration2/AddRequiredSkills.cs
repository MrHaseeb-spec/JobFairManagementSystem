using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project
{
    public partial class AddRequiredSkills : Form
    {
        public AddRequiredSkills()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string skill = textBox1.Text.Trim();
            if (!string.IsNullOrEmpty(skill))
            {
                RequiredSkills.SkillList.Add(skill);
                this.Close(); // Simply close and go back to PostJob
            }
            else
            {
                MessageBox.Show("Please enter a skill.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close(); // Close and return
        }
    }
}