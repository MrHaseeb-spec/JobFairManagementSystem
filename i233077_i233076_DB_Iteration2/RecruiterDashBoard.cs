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
    public partial class RecruiterDashBoard : Form
    {
        public RecruiterDashBoard()
        {
            InitializeComponent();
            RecruiterInfo();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            ScheduleInterview dashboard = new ScheduleInterview();
            dashboard.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            Hiring dashboard = new Hiring();
            dashboard.Show();
        }
        private void RecruiterInfo()
        {
            textBox1.Text = RecruiterData.SelectedRecruiterID.ToString();
            textBox2.Text = UserData.SelectedName;
            textBox3.Text = RecruiterData.SelectedRecruiterDesignation;
            textBox4.Text = UserData.SelectedEmail;
            textBox5.Text = RecruiterData.SelectedCompanyID.ToString();
            textBox6.Text = UserData.SelectedPhoneNumber;
            string query = "Select * from COMPANY where companyId = @companyId";
            string connectionString = "Data Source=DESKTOP-JUIJCBN\\SQLEXPRESS;Initial Catalog=JOBFAIR;Integrated Security=True;Encrypt=False";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@companyId", RecruiterData.SelectedCompanyID);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    string name = reader.GetString(reader.GetOrdinal("name"));
                    string sector = reader.GetString(reader.GetOrdinal("sector"));
                    string description = reader.GetString(reader.GetOrdinal("description"));
                    string contactemail = reader.GetString(reader.GetOrdinal("contactemail"));
                    string contactPhone = reader.GetString(reader.GetOrdinal("contactPhone"));

                    RecruiterData.SelectedCompanyName = name;
                    RecruiterData.SelectedCompanySector = sector;
                    RecruiterData.SelectedCompanyDescription = description;
                    RecruiterData.SelectedCompanyEmail = contactemail;
                    RecruiterData.SelectedCompanyContact = contactPhone;


                }
                textBox7.Text = RecruiterData.SelectedCompanyDescription;
                textBox8.Text = RecruiterData.SelectedCompanySector;
                textBox9.Text = RecruiterData.SelectedCompanyName;
                textBox10.Text = RecruiterData.SelectedCompanyEmail;
                textBox11.Text = RecruiterData.SelectedCompanyContact;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            RecruiterDashBoard dashboard = new RecruiterDashBoard();
            dashboard.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (RecruiterData.SelectedCompanyID == 0)
            {
                this.Hide();
                CompanyDataForm dashboard = new CompanyDataForm();
                dashboard.Show();
            }
            else
            {
                MessageBox.Show("Recruiter already has a company");
                this.Hide();
                RecruiterDashBoard dashboard = new RecruiterDashBoard();
                dashboard.Show();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            PostJob dashboard = new PostJob();
            dashboard.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            ReviewApplication dashboard = new ReviewApplication();
            dashboard.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        { 
            this.Hide();
            Login login = new Login();
            login.Show();

        }
    }
}
