using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    public class AcademyRecordRepository
    {
        public List<AcademyRecord> GetRecords()
        {
            var records = new List<AcademyRecord>();
            string connectionString = "Data Source=DESKTOP-JUIJCBN\\SQLEXPRESS;Initial Catalog=JOBFAIR;Integrated Security=True;Encrypt=False";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM ACADEMIC_RECORD WHERE studentId = @studentId";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@studentId", StudentData.SelectedStudentID);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        AcademyRecord recordobj = new AcademyRecord();
                        recordobj.SelectedAcademicId = reader.GetInt32(reader.GetOrdinal("recordId"));
                        recordobj.SelectedAcademicGPA = (float)reader.GetDouble(reader.GetOrdinal("gpa"));
                        recordobj.SelectedAcademicSemester = reader.GetString(reader.GetOrdinal("currentSemester"));
                        recordobj.SelectedAcademicDegreePrgram = reader.GetString(reader.GetOrdinal("degreeProgram"));
                        recordobj.SelectedStudentId = reader.GetString(reader.GetOrdinal("studentId"));

                        records.Add(recordobj);
                    }
                }
            }


            return records;
        }
    }


}

