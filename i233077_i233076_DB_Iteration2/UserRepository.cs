using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    public class UserRepository
    {
        // Replace with your actual SQL Server connection string
        //Saad
        //SqlConnection conn = new SqlConnection(" Data source = ALMIGHTYPUSH\\SQLEXPRESS;Initial Catalog=JOBFAIR;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");
        //Haseeb
        public List<nonStatUserData> getNotApproved()
        {
            var users = new List<nonStatUserData>();
            string connectionString = "Data Source=DESKTOP-JUIJCBN\\SQLEXPRESS;Initial Catalog=JOBFAIR;Integrated Security=True;Encrypt=False";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            string sql = "SELECT * FROM USERS WHERE isApproved = 0";
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        nonStatUserData userData = new nonStatUserData();
                        userData.SelectedUserId = reader.GetInt32(0);
                        userData.SelectedName = reader.GetString(1);
                        userData.SelectedEmail = reader.GetString(2);
                        userData.SelectedPassword = reader.GetString(3);
                        userData.SelectedPhoneNumber = reader.GetString(4);
                        userData.SelectedRole = reader.GetString(5);
                        userData.SelectedApproval = reader.GetBoolean(6);
                        userData.SelectedActive = reader.GetBoolean(7);
                        users.Add(userData);
                    }
                }
            }
            return users;
        }

        public List<nonStatUserData> getNoAdmin()
        {
            var users = new List<nonStatUserData>();
            string connectionString = "Data Source=DESKTOP-JUIJCBN\\SQLEXPRESS;Initial Catalog=JOBFAIR;Integrated Security=True;Encrypt=False";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            string sql = "SELECT * FROM USERS where role != 'Admin'";
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        nonStatUserData userData = new nonStatUserData();
                        userData.SelectedUserId = reader.GetInt32(0);
                        userData.SelectedName = reader.GetString(1);
                        userData.SelectedEmail = reader.GetString(2);
                        userData.SelectedPassword = reader.GetString(3);
                        userData.SelectedPhoneNumber = reader.GetString(4);
                        userData.SelectedRole = reader.GetString(5);
                        userData.SelectedApproval = reader.GetBoolean(6);
                        userData.SelectedActive = reader.GetBoolean(7);
                        users.Add(userData);
                    }
                }
            }
            return users;
        }

        public void updateUserStatus(int id, bool isActive, bool isApproved)
        {
            string connectionString = "Data Source=DESKTOP-JUIJCBN\\SQLEXPRESS;Initial Catalog=JOBFAIR;Integrated Security=True;Encrypt=False";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string sql = "UPDATE USERS SET isActive = @isActive, isApproved = @isApproved WHERE userId = @userId";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@isActive", isActive);
                    cmd.Parameters.AddWithValue("@isApproved", isApproved);
                    cmd.Parameters.AddWithValue("@userId", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

    }
}
