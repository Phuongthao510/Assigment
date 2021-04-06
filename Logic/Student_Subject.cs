using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Assigment.DataAccess;

namespace Assigment.Logic
{
    public class Student_Subject
    {
        public Student_Subject(string studentID, string subjectID, float smallTest, float midterm, float finalExam)
        {
            StudentID = studentID;
            SubjectID = subjectID;
            SmallTest = smallTest;
            Midterm = midterm;
            FinalExam = finalExam;
        }

        public string StudentID { get; set; }
        public string SubjectID { get; set; }
        public float SmallTest { get; set; }
        public float Midterm { get; set; }
        public float FinalExam { get; set; }

        public static int UpdateStudentGrade(String stuID, String subID, float SmallTest, float Midterm, float FinalExam)
        {
            return DAO.UpdateStudentGrade(stuID,subID, SmallTest, Midterm, FinalExam);
        }
    }
    
}
