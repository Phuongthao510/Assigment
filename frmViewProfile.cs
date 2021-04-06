using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Assigment.Logic;

namespace Assigment
{
    public partial class frmViewProfile : Form
    {
        public frmViewProfile()
        {
            InitializeComponent();
        }

            public frmViewProfile(Student s)
        {
            InitializeComponent();
            dgvCourse.AutoGenerateColumns = false;
            pnlCourse.Visible = false;
            pbImage.Image = Image.FromFile(s.Image);
            txtName.Text = s.FullName;
            txtsID.Text = s.StudentID;
            txtHomeTown.Text = s.HomeTown;
            if (s.Gender)
            {
                rbtnMale.Checked = true;
            }
            else
            {
                rbtnFemale.Checked = true;
            }
            dtpDoB.Value = s.DateOFBirth;

            cbSubject.DisplayMember = "SubjectName";
            cbSubject.ValueMember = "SubjectID";
            List<Subject> list = SubjectList.GetSubjectNameByStubID(txtsID.Text);
            list.Insert(0, new Subject("-1", "-----All-----"));
            cbSubject.DataSource = list;

            
            dgvCourse.Columns.Add("subjcol", "Subject Name");
            dgvCourse.Columns["subjcol"].DataPropertyName = "SubjectName";

            dgvCourse.Columns.Add("Smallcol", "Small Test");
            dgvCourse.Columns["Smallcol"].DataPropertyName = "SmallTest";

            dgvCourse.Columns.Add("Midtermcol", "Midterm");
            dgvCourse.Columns["Midtermcol"].DataPropertyName = "Midterm";

            dgvCourse.Columns.Add("FinalExamcol", "Final Exam");
            dgvCourse.Columns["FinalExamcol"].DataPropertyName = "FinalExam";

            dgvCourse.Columns.Add("Averagecol", "Average");
            dgvCourse.Columns["Averagecol"].DataPropertyName = "AVERAGE";

            dgvCourse.Columns.Add("Statuscol", "Status");
            dgvCourse.Columns["Statuscol"].DataPropertyName = "Status";

            LoadData();
            //dgvCourse.DataSource = SubjectList.GetMark(s.StudentID);
            for (int i=0; i< dgvCourse.ColumnCount; i++)
            {
                dgvCourse.Columns[i].HeaderCell.Style.Font = new Font("Verdana", 8, FontStyle.Bold);
            }
        }
        private void LoadData()
        {
            string subID = cbSubject.SelectedValue.ToString();
            string stuID = txtsID.Text;
            dgvCourse.DataSource = SubjectList.GetMark(subID,stuID);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (pnlCourse.Visible)
            {
                pnlCourse.Visible = false;
            }
            else
            {
                pnlCourse.Visible = true;
            }
            
        }

        private void frmViewProfile_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            String name = txtName.Text;
            bool gender;
            if (rbtnMale.Checked)
            {
                gender = true;
            }
            else
            {
                gender = false;
            }
            DateTime DoB = dtpDoB.Value;
            String home = txtHomeTown.Text;
            String stuID = txtsID.Text;
            if (DateTime.Now.Year - DoB.Year < 18)
            {
                MessageBox.Show("Date of birth invalid!");
            }
            else
            {
                Student s = new Student(stuID, name, gender, DoB, home);
                if (Student.UpdateStudent(s) > 0)
                {
                    MessageBox.Show("Update successfully!");
                }
            }
        }

        private void cbSubject_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
    }
}
