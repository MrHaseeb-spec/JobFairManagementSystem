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
    public partial class AcademicRecord : Form
    {
        public AcademicRecord()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string gpa = textBox1.Text.Trim();
            string semester = comboBox1.Text.Trim();
            string degree = comboBox2.Text.Trim();
            string studentId = StudentData.SelectedStudentID;

            string connectionString = "Data Source=DESKTOP-JUIJCBN\\SQLEXPRESS;Initial Catalog=JOBFAIR;Integrated Security=True;Encrypt=False";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"INSERT INTO ACADEMIC_RECORD (gpa, currentSemester, degreeProgram, studentId)
                                 VALUES (@gpa, @semester, @degree, @studentId)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@gpa", float.Parse(gpa));
                    cmd.Parameters.AddWithValue("@semester", semester);
                    cmd.Parameters.AddWithValue("@degree", degree);
                    cmd.Parameters.AddWithValue("@studentId", studentId);

                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Academic Record saved successfully!");
                this.Hide();
                StudentDashBoard dashboard = new StudentDashBoard();
                dashboard.Show();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            StudentDashBoard dashboard = new StudentDashBoard();
            dashboard.Show();
        }
    }
}
