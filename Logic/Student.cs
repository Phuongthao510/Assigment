using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Assigment.DataAccess;

namespace Assigment.Logic
{
    public class Student
    {
        public Student(string studentID, string fullName, float smallTest, float midterm, float finalExam)
        {
            StudentID = studentID;
            FullName = fullName;
            SmallTest = smallTest;
            Midterm = midterm;
            FinalExam = finalExam;
        }

        public Student(string studentID, string fullName, bool gender, DateTime dateOFBirth, string homeTown)
        {
            StudentID = studentID;
            FullName = fullName;
            Gender = gender;
            DateOFBirth = dateOFBirth;
            HomeTown = homeTown;
        }

        public Student(string studentID, string username, string passWord, string fullName, bool gender, DateTime dateOFBirth, string homeTown, string image)
        {
            StudentID = studentID;
            Username = username;
            PassWord = passWord;
            FullName = fullName;
            Gender = gender;
            DateOFBirth = dateOFBirth;
            HomeTown = homeTown;
            Image = image;
        }

        public string StudentID { get; set; }
        public string Username { get; set; }
        public string PassWord { get; set; }
        public string FullName { get; set; }
        public bool Gender { get; set; }
        public DateTime DateOFBirth { get; set; }
        public string HomeTown { get; set; }
        public string Image { get; set; }

        public float SmallTest { get; set; }
        public float Midterm { get; set; }
        public float FinalExam { get; set; }

        public static int UpdateStudent(Student s)
        {
            return DAO.UpdateStudentProfile(s);
        }

    }
    public class StudentList
    {
        public static Student GetStudent(String username, String password)
        {
            List<Student> students = new List<Student>();
            DataTable stu = DAO.GetStudentDT(username, password);
            foreach (DataRow dr in stu.Rows)
                students.Add(new Student(
                    dr["StudentID"].ToString(),
                    dr["Username"].ToString(),
                    dr["PassWord"].ToString(),
                    dr["FullName"].ToString(),
                    Convert.ToBoolean(dr["Gender"]),
                    Convert.ToDateTime(dr["DateOFBirth"]),
                    dr["HomeTown"].ToString(),
                    dr["Image"].ToString()
                    ));
            if (students.Count <= 0)
            {
                return null;
            }
            else
            {
                return students[0];
            }
        }

        public static List<Student> GetMarkOfStudent(String subID)
        {
            List<Student> stus = new List<Student>();
            DataTable stu = DAO.GetMarkOfStudent(subID);
            foreach (DataRow dr in stu.Rows)
                stus.Add(new Student(
                    dr["StudentID"].ToString(),
                    dr["FullName"].ToString(),
                    float.Parse(dr["SmallTest"].ToString()),
                    float.Parse(dr["Midterm"].ToString()),
                    float.Parse(dr["FinalExam"].ToString())
                    ));
            return stus;
        }

        public static int CountStudent(String subID)
        {
            return DAO.CountStudent(subID);
        }
        public static int Search_CountStudent(string searchID, string searchName,String subID)
        {
            return DAO.Search_CountStudent(searchID, searchName,subID);
        }
        public static List<Student> GetStudentbyPage(String subID, int pageIndex, int pageSize)
        {
            List<Student> stus = new List<Student>();
            DataTable stu = DAO.GetMarkOfStudent_Pagger(subID, pageIndex, pageSize);
            foreach (DataRow dr in stu.Rows)
            {
                stus.Add(new Student(
                    dr["StudentID"].ToString(),
                    dr["FullName"].ToString(),
                    float.Parse(dr["SmallTest"].ToString()),
                    float.Parse(dr["Midterm"].ToString()),
                    float.Parse(dr["FinalExam"].ToString())
                    ));
            }
            return stus;
        }

        public static List<Student> Search_GetMarkOfStudent_Pagger(string searchID, string searchName, String subID, int pageIndex, int pageSize)
        {
            List<Student> stus = new List<Student>();
            DataTable stu = DAO.Search_GetMarkOfStudent_Pagger(searchID, searchName,subID, pageIndex, pageSize);
            foreach (DataRow dr in stu.Rows)
            {
                stus.Add(new Student(
                    dr["StudentID"].ToString(),
                    dr["FullName"].ToString(),
                    float.Parse(dr["SmallTest"].ToString()),
                    float.Parse(dr["Midterm"].ToString()),
                    float.Parse(dr["FinalExam"].ToString())
                    ));
            }
            return stus;
        }
    }
}
