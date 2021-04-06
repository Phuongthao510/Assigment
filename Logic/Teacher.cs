using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Assigment.DataAccess;

namespace Assigment.Logic
{
    public class Teacher
    {
        public Teacher(string teacherID, string username, string passWord, string subjectID)
        {
            TeacherID = teacherID;
            Username = username;
            PassWord = passWord;
            SubjectID = subjectID;
        }

        public string TeacherID { get; set; }
        public string Username { get; set; }
        public string PassWord { get; set; }
        public string SubjectID { get; set; }
    }

    public class TeacherList
    {
        public static Teacher GetTeacher(String username, String password)
        {
            List<Teacher> teachers = new List<Teacher>();
            DataTable stu = DAO.GetTeacherAcc(username, password);
            foreach (DataRow dr in stu.Rows)
                teachers.Add(new Teacher(
                    dr["TeacherID"].ToString(),
                    dr["Username"].ToString(),
                    dr["PassWord"].ToString(),
                    dr["SubjectID"].ToString()
                    ));
            if (teachers.Count <= 0)
            {
                return null;
            }
            else
            {
                return teachers[0];
            }
        }
    }
    
}
