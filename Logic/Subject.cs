using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Assigment.DataAccess;

namespace Assigment.Logic
{
    public class Subject
    {
        public Subject(string subjectID, string subjectName)
        {
            SubjectID = subjectID;
            SubjectName = subjectName;
        }

        public Subject(string subjectName, float smallTest, float midterm, float finalExam, float aVERAGE, string status)
        {
            SubjectName = subjectName;
            SmallTest = smallTest;
            Midterm = midterm;
            FinalExam = finalExam;
            AVERAGE = aVERAGE;
            Status = status;
        }

        public Subject(string subjectID, string subjectName, float smallTest, float midterm, float finalExam, float aVERAGE, string status)
        {
            SubjectID = subjectID;
            SubjectName = subjectName;
            SmallTest = smallTest;
            Midterm = midterm;
            FinalExam = finalExam;
            AVERAGE = aVERAGE;
            Status = status;
        }

        public string SubjectID { get; set; }
        public string SubjectName { get; set; }
        public float SmallTest { get; set; }
        public float Midterm { get; set; }
        public float FinalExam { get; set; }
        public float AVERAGE { get; set; }
        public String Status { get; set; }
        public String StudentID { get; set; }
    }

    public class SubjectList
    {
        public static List<Subject> GetMark(string subID,String stuID)
        {
            List<Subject> subj = new List<Subject>();
            DataTable sub = DAO.GetMark(subID,stuID);
            foreach (DataRow dr in sub.Rows)
                subj.Add(new Subject(
                    dr["SubjectID"].ToString(),
                    dr["SubjectName"].ToString(),
                    float.Parse(dr["SmallTest"].ToString()),
                    float.Parse(dr["Midterm"].ToString()),
                    float.Parse(dr["FinalExam"].ToString()),
                    float.Parse(dr["AVERAGE"].ToString()),
                    dr["Status"].ToString()
                    ));
            return subj;
        }

        public static Subject GetSubjectNameBySubID(String subID)
        {
            List<Subject> subj = new List<Subject>();
            DataTable sub = DAO.GetSubjectNameBySubID(subID);
            foreach (DataRow dr in sub.Rows)
                subj.Add(new Subject(
                    dr["SubjectID"].ToString(),
                    dr["SubjectName"].ToString()
                    ));
            return subj[0];
        }

        public static List<Subject> GetSubjectNameByStubID(String stuID)
        {
            List<Subject> subj = new List<Subject>();
            DataTable sub = DAO.GetSubjectNameByStuID(stuID);
            foreach (DataRow dr in sub.Rows)
                subj.Add(new Subject(
                    dr["SubjectID"].ToString(),
                    dr["SubjectName"].ToString()
                    ));
            return subj;
        }
    }
}
