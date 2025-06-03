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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Windows.Forms.VisualStyles;
using System.Runtime.Remoting.Messaging;
using System.Collections;

namespace Project
{
    public partial class StudentDashBoard : Form
    {
        public StudentDashBoard()
        {
            InitializeComponent();
            StudentInfo();
            SkillShow();
            AcademicShow();
        }
        private void sqlConnection()
        {
            //Saad
            //Data source = ALMIGHTYPUSH\SQLEXPRESS;Initial Catalog=JOBFAIR;Integrated Security=True;Encrypt=True;Trust Server Certificate=True
            //Haseeb
            //Data Source = DESKTOP - JUIJCBN\SQLEXPRESS; Initial Catalog = JOBFAIR; Integrated Security = True; Encrypt = False
            SqlConnection conn = new SqlConnection("Data Source = DESKTOP - JUIJCBN\\SQLEXPRESS; Initial Catalog = JOBFAIR; Integrated Security = True; Encrypt = False");
            conn.Open();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            StudentDashBoard dashboard = new StudentDashBoard();
            dashboard.Show();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            JobViewstd JobList = new JobViewstd();
            JobList.Show();

        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            ScheduleInterviewStd interview = new ScheduleInterviewStd();
            interview.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            ViewJobFairEvents events = new ViewJobFairEvents();
            events.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.Hide();
            ReviewStd review = new ReviewStd();
            review.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login login = new Login();
            login.Show();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            AddSkill skill = new AddSkill();
            skill.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            AcademicRecord record = new AcademicRecord();
            record.Show();

        }

        private void StudentInfo()
        {
            textBox1.Text = StudentData.SelectedStudentID;
            textBox2.Text = UserData.SelectedName;
            textBox3.Text = StudentData.SelectedStudentStatus;
            textBox4.Text = UserData.SelectedEmail;
            textBox5.Text = StudentData.SelectedBatchYear.ToString();
            textBox6.Text = UserData.SelectedPhoneNumber;

        }
        private void StudentDashBoard_Load(object sender, EventArgs e)
        {
            // Example of fetching and printing student info
            SqlConnection conn = new SqlConnection("Data Source=DESKTOP-JUIJCBN\\SQLEXPRESS;Initial Catalog=JOBFAIR;Integrated Security=True;Encrypt=False");
            conn.Open();
            

            conn.Close();
            
        }

        
        private void SkillShow()
        {
            DataTable Skills = new DataTable();
            Skills.Columns.Clear();
            Skills.Columns.Add("SkillCertID", typeof(int));
            Skills.Columns.Add("Title", typeof(string));
            Skills.Columns.Add("Type", typeof(string));
            Skills.Columns.Add("Issued By", typeof(string));
            Skills.Columns.Add("Description", typeof(string));
            Skills.Columns.Add("Issued Date", typeof(DateTime));

            var repo = new SkillRepository();
            var skillObj = repo.GetSkills();


            foreach (var skill in skillObj)
            {
                var row = Skills.NewRow();
                row["SkillCertID"] = skill.SelectedSkillId;
                row["Title"] = skill.SelectedSkillTitle;
                row["Type"] = skill.SelectedSkillType;
                row["Issued By"] = skill.SelectedSkillIssuedBy;
                row["Description"] = skill.SelectedSkillDescription;
                row["Issued Date"] = skill.SelectedIssuedDate;
                Skills.Rows.Add(row);

            }
            this.dataGridView1.DataSource = Skills;

        }

        private void AcademicShow()
        {
            DataTable Records = new DataTable();
            Records.Columns.Clear();
            Records.Columns.Add("Academic Record ID", typeof(int));
            Records.Columns.Add("Current Semester", typeof(string));
            Records.Columns.Add("GPA", typeof(float));
            Records.Columns.Add("Degree Program", typeof(string));
            
            var repo = new AcademyRecordRepository();
            var recordObj = repo.GetRecords();


            foreach (var record in recordObj)
            {
                var row = Records.NewRow();
                row["Academic Record ID"] = record.SelectedAcademicId;
                row["Current Semester"] = record.SelectedAcademicSemester;
                row["GPA"] = record.SelectedAcademicGPA;
                row["Degree Program"] = record.SelectedAcademicDegreePrgram;

                Records.Rows.Add(row);

            }
            this.dataGridView2.DataSource = Records;
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
