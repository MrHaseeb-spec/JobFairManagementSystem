using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project
{
    public partial class AddSkill : Form
    {
        public AddSkill()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string title = textBox1.Text.Trim();
            string type = comboBox1.Text.Trim();
            string description = textBox3.Text.Trim();
            string issuedBy = textBox4.Text.Trim();
            string issuedDate = textBox5.Text.Trim(); // Expecting a valid date string
            string studentId = StudentData.SelectedStudentID;

            string connectionString = "Data Source=DESKTOP-JUIJCBN\\SQLEXPRESS;Initial Catalog=JOBFAIR;Integrated Security=True;Encrypt=False";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string query = @"INSERT INTO SKILL (title, type, description, issuedBy, issuedDate, studentId)
                         VALUES (@title, @type, @description, @issuedBy, @issuedDate, @studentId)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@title", title);
                    cmd.Parameters.AddWithValue("@type", type);
                    cmd.Parameters.AddWithValue("@description", description);
                    cmd.Parameters.AddWithValue("@issuedBy", issuedBy);
                    cmd.Parameters.AddWithValue("@issuedDate", DateTime.Parse(issuedDate)); // Ensure correct format
                    cmd.Parameters.AddWithValue("@studentId", studentId);

                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Skill inserted successfully!");
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            StudentDashBoard dataForm = new StudentDashBoard();
            dataForm.Show();
        }
    }
}
