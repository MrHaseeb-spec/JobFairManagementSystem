using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;        //Including it mandatory
using System.Collections;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Project
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Loginbutton_Click(object sender, EventArgs e)
        {
            string email = textBox1.Text.Trim();
            string password = textBox2.Text.Trim();

            string connectionString = "Data Source=DESKTOP-JUIJCBN\\SQLEXPRESS;Initial Catalog=JOBFAIR;Integrated Security=True;Encrypt=False";

            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            string query = "SELECT * FROM Users WHERE Email = @Email AND Password = @Password";

            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Email", email);
            cmd.Parameters.AddWithValue("@Password", password);

            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                int userId = reader.GetInt32(reader.GetOrdinal("userId"));
                string name = reader.GetString(reader.GetOrdinal("name"));
                string emailFromDb = reader.GetString(reader.GetOrdinal("email"));
                string passwordFromDb = reader.GetString(reader.GetOrdinal("password"));
                string phoneNumber = reader.GetString(reader.GetOrdinal("phoneNumber"));
                string role = reader.GetString(reader.GetOrdinal("role"));
                bool isApproved = reader.GetBoolean(reader.GetOrdinal("isApproved"));
                bool isActive = reader.GetBoolean(reader.GetOrdinal("isActive"));

                UserData.SelectedUserId = userId;
                UserData.SelectedName = name;
                UserData.SelectedEmail = emailFromDb;
                UserData.SelectedPassword = passwordFromDb;
                UserData.SelectedPhoneNumber = phoneNumber;
                UserData.SelectedRole = role;
                UserData.SelectedApproval = isApproved;
                UserData.SelectedActive = isActive;

                if (!UserData.SelectedActive)
                {
                    reader.Close();
                    MessageBox.Show("Account not active!");
                }

                else if (!UserData.SelectedApproval)
                {
                    reader.Close();
                    MessageBox.Show("Account not approved!");
                }

                else if (UserData.SelectedRole == "Student")
                {
                    reader.Close();
                    string checkStudentQuery = "SELECT * FROM STUDENT WHERE userId = @userId";

                    cmd = new SqlCommand(checkStudentQuery, conn);
                    cmd.Parameters.AddWithValue("@userId", UserData.SelectedUserId);  // Assuming UserData.UserId holds the logged-in user's ID

                    reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        string studentId = reader.GetString(reader.GetOrdinal("studentId"));
                        int batchYear = reader.GetInt32(reader.GetOrdinal("batchYear"));
                        string status = reader.GetString(reader.GetOrdinal("status"));

                        StudentData.SelectedStudentID = studentId;
                        StudentData.SelectedBatchYear = batchYear;
                        StudentData.SelectedStudentStatus = status;
                        reader.Close();
                        this.Hide();
                        StudentDashBoard dashboard = new StudentDashBoard();
                        dashboard.Show();
                    }
                    else
                    {
                        reader.Close();
                        this.Hide();
                        DataFormStudent dataForm = new DataFormStudent();
                        dataForm.Show();
                    }
                }

                else if (UserData.SelectedRole == "Recruiter")
                {
                    reader.Close();
                    string checkStudentQuery = "SELECT * FROM RECRUITER WHERE userId = @userId";

                    cmd = new SqlCommand(checkStudentQuery, conn);
                    cmd.Parameters.AddWithValue("@userId", UserData.SelectedUserId);  // Assuming UserData.UserId holds the logged-in user's ID

                    reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        int recruiterId = reader.GetInt32(reader.GetOrdinal("recruiterId"));
                        string designation = reader.GetString(reader.GetOrdinal("designation"));
                        int companyID = reader.GetInt32(reader.GetOrdinal("companyId"));

                        RecruiterData.SelectedRecruiterID= recruiterId;
                        RecruiterData.SelectedRecruiterDesignation = designation;
                        RecruiterData.SelectedCompanyID = companyID;

                        reader.Close();
                        this.Hide();
                        RecruiterDashBoard dashboard = new RecruiterDashBoard();
                        dashboard.Show();
                    }
                    else
                    {
                        reader.Close();
                        this.Hide();
                        DataFormRecruiter dataForm = new DataFormRecruiter();
                        dataForm.Show();
                    }
                }

                else if (UserData.SelectedRole == "Admin")
                {
                    reader.Close();
                    string checkStudentQuery = "SELECT * FROM ADMIN WHERE userId = @userId";

                    cmd = new SqlCommand(checkStudentQuery, conn);
                    cmd.Parameters.AddWithValue("@userId", UserData.SelectedUserId);

                    reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        int adminId = reader.GetInt32(reader.GetOrdinal("adminId"));
                        string status = reader.GetString(reader.GetOrdinal("status"));

                        AdminData.SelectedAdminId = adminId;
                        AdminData.SelectedAdminStatus = status;

                        reader.Close();
                        this.Hide();
                        AdminDashboard dashboard = new AdminDashboard();
                        dashboard.Show();
                    }
                    else
                    {
                        reader.Close();
                        MessageBox.Show("Admin record not found.");
                    }
                }

                else if (UserData.SelectedRole == "BoothCoordinator")
                {
                    reader.Close();
                    string checkBoothCoordinatorQuery = "SELECT * FROM BOOTHCOORDINATOR WHERE userId = @userId";

                    cmd = new SqlCommand(checkBoothCoordinatorQuery, conn);
                    cmd.Parameters.AddWithValue("@userId", UserData.SelectedUserId);

                    reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        int boothCoordinatorId = reader.GetInt32(reader.GetOrdinal("boothCoordinatorId"));
                        TimeSpan shiftStart = reader.GetTimeSpan(reader.GetOrdinal("shiftStartTime"));
                        TimeSpan shiftEnd = reader.GetTimeSpan(reader.GetOrdinal("shiftEndTime"));

                        BoothCoordinatorData.SelectedCoordinatorId = boothCoordinatorId;
                        BoothCoordinatorData.ShiftStartTime = shiftStart;
                        BoothCoordinatorData.ShiftEndTime = shiftEnd;

                        reader.Close();
                        this.Hide();
                        BoothCoordinatorDashboard dashboard = new BoothCoordinatorDashboard();
                        dashboard.Show();
                    }
                    else
                    {
                        reader.Close();
                        this.Hide();
                        DataFormBoothCoordinator dashboard = new DataFormBoothCoordinator();
                        dashboard.Show();
                    }
                }

            }
            else
            {
                reader.Close();
                MessageBox.Show("Invalid email or password!");
                textBox1.Clear();
                textBox2.Clear();
            }
        }

        private void SignUplinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            signUp signup = new signUp();
            signup.Show();

        }
    }
}
