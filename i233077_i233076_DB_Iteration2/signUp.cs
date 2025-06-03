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
    public partial class signUp : Form
    {
        public signUp()
        {
            InitializeComponent();
        }
        private void sqlConnection()
        {
            //Saad
            //SqlConnection conn = new SqlConnection(" Data source = ALMIGHTYPUSH\\SQLEXPRESS;Initial Catalog=JOBFAIR;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");
            //Haseeb
            SqlConnection conn = new SqlConnection("Data Source = DESKTOP - JUIJCBN\\SQLEXPRESS; Initial Catalog = JOBFAIR; Integrated Security = True; Encrypt = False");

            conn.Open();
        }

        private void next(object sender, EventArgs e)
        {
            string name = nameTextBox.Text.Trim();
            string email = emailTextBox.Text.Trim();
            string password = passwordTextBox.Text.Trim();
            string role = roleComboBox.SelectedItem?.ToString();
            string phoneNumber = phoneNumberTextBox.Text.Trim();

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(role) || string.IsNullOrEmpty(phoneNumber))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }
            if (role == "Student")
            {
                if (!email.StartsWith("i2") || !email.EndsWith("@isb.nu.edu.pk"))
                {
                    MessageBox.Show("Student email must start with 'i2' and end with '@isb.nu.edu.pk'.");
                    return;
                }
                string middlePart = email.Substring(2, email.Length - 2 - "@isb.nu.edu.pk".Length);
                foreach (char c in middlePart)
                {
                    if (!char.IsDigit(c))
                    {
                        MessageBox.Show("The part after 'i2' and before '@isb.nu.edu.pk' must contain only digits.");
                        return;
                    }
                }
            }
            if (!long.TryParse(phoneNumber, out _))
            {
                MessageBox.Show("Phone number must contain digits only.");
                return;
            }

            string connectionString = "Data Source=DESKTOP-JUIJCBN\\SQLEXPRESS;Initial Catalog=JOBFAIR;Integrated Security=True;Encrypt=False";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            string checkQuery = "SELECT * FROM USERS WHERE Email= @email";
            SqlCommand cmd = new SqlCommand(checkQuery, conn);
            cmd.Parameters.AddWithValue("@email", email);
            SqlDataReader reader = cmd.ExecuteReader();
            if(reader.HasRows)
            {
                MessageBox.Show("An account with this email already exists!");
                reader.Close();
                this.Hide();
                Login go_login = new Login();
                go_login.Show();
            }
            reader.Close();
            string insertQuery = "INSERT INTO USERS (name, email, password, phoneNumber, role, isApproved, isActive) VALUES (@name, @email, @password, @phoneNumber, @role, 0, 0)";
            cmd = new SqlCommand(insertQuery, conn);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@password", password);
            cmd.Parameters.AddWithValue("@phoneNumber", phoneNumber);
            cmd.Parameters.AddWithValue("@role", role);

            cmd.ExecuteNonQuery();
            MessageBox.Show("Signup successful! Awaiting approval.");
            reader.Close();
            this.Hide();
            Login loginForm = new Login();
            loginForm.Show();
                
        }

        private void loginlinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            Login login = new Login();
            login.Show();
        }
    }
}
