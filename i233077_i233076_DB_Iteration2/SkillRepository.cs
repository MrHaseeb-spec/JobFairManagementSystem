using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    public class SkillRepository
    {
        private void sqlConnection()
        {
            //Saad
            //Data source = ALMIGHTYPUSH\SQLEXPRESS;Initial Catalog=JOBFAIR;Integrated Security=True;Encrypt=True;Trust Server Certificate=True
            //Haseeb
            //Data Source = DESKTOP - JUIJCBN\SQLEXPRESS; Initial Catalog = JOBFAIR; Integrated Security = True; Encrypt = False
         
        }
        public List<Skills> GetSkills()
        {
            var skill = new List<Skills>();
            string connectionString = "Data Source=DESKTOP-JUIJCBN\\SQLEXPRESS;Initial Catalog=JOBFAIR;Integrated Security=True;Encrypt=False";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM SKILL WHERE studentId = @studentId";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@studentId", StudentData.SelectedStudentID);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Skills skillobj = new Skills();
                        skillobj.SelectedSkillId = reader.GetInt32(reader.GetOrdinal("skillCertId"));
                        skillobj.SelectedSkillTitle = reader.GetString(reader.GetOrdinal("title"));
                        skillobj.SelectedSkillType = reader.GetString(reader.GetOrdinal("type"));
                        skillobj.SelectedSkillIssuedBy = reader.GetString(reader.GetOrdinal("issuedBy"));
                        skillobj.SelectedSkillDescription = reader.GetString(reader.GetOrdinal("description"));
                        skillobj.SelectedIssuedDate = reader.GetDateTime(reader.GetOrdinal("issuedDate")).ToString();
                        skillobj.SelectedStudentId = reader.GetString(reader.GetOrdinal("studentId"));

                        skill.Add(skillobj);
                    }
                }
            }
            return skill;
        }
    }
}