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
    public partial class CompanyDataForm : Form
    {
        public CompanyDataForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text.Trim();
            string description = textBox3.Text.Trim();
            string sector = textBox4.Text.Trim();
            string email = textBox2.Text.Trim();
            string phone = textBox5.Text.Trim();


            string connectionString = "Data Source=DESKTOP-JUIJCBN\\SQLEXPRESS;Initial Catalog=JOBFAIR;Integrated Security=True;Encrypt=False";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlTransaction transaction = conn.BeginTransaction();
                try
                {
                    int companyId;

                    // 1. Check if the company already exists
                    string checkQuery = "SELECT companyId FROM COMPANY WHERE name = @name";
                    SqlCommand checkCmd = new SqlCommand(checkQuery, conn, transaction);
                    checkCmd.Parameters.AddWithValue("@name", name);

                    object result = checkCmd.ExecuteScalar();

                    if (result != null)
                    {
                        companyId = Convert.ToInt32(result); // Company exists
                    }
                    else
                    {
                        // 2. Insert new company
                        string insertCompanyQuery = @"INSERT INTO COMPANY (name, sector, description, contactEmail, contactPhone)
                                              VALUES (@name, @sector, @desc, @mail, @phone);
                                              SELECT SCOPE_IDENTITY();";

                        SqlCommand insertCmd = new SqlCommand(insertCompanyQuery, conn, transaction);
                        insertCmd.Parameters.AddWithValue("@name", name);
                        insertCmd.Parameters.AddWithValue("@sector", sector);
                        insertCmd.Parameters.AddWithValue("@desc", description);
                        insertCmd.Parameters.AddWithValue("@mail", email);
                        insertCmd.Parameters.AddWithValue("@phone", phone);

                        companyId = Convert.ToInt32(insertCmd.ExecuteScalar());
                    }

                    // 3. Update recruiter's companyId
                    string updateRecruiterQuery = @"UPDATE RECRUITER SET companyId = @companyId WHERE userId = @userId";
                    SqlCommand updateCmd = new SqlCommand(updateRecruiterQuery, conn, transaction);
                    updateCmd.Parameters.AddWithValue("@companyId", companyId);
                    updateCmd.Parameters.AddWithValue("@userId", UserData.SelectedUserId);

                    updateCmd.ExecuteNonQuery();

                    transaction.Commit();
                    MessageBox.Show("Company details added successfully!");
                    this.Hide();
                    RecruiterDashBoard dashboard = new RecruiterDashBoard();
                    dashboard.Show();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            this.Hide();
            RecruiterDashBoard dashboard = new RecruiterDashBoard();
            dashboard.Show();
        }
    }
}
