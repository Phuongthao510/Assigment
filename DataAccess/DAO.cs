using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Assigment.DataAccess
{
    public class DAO
    {
        public static SqlConnection GetConnection()
        {
            string ConnectionString = @"server=localhost; database=StudentTest9; user=sa; password=123";
            return new SqlConnection(ConnectionString);
        }

        public static DataTable GetDataBySQL(string sql)
        {
            SqlCommand command = new SqlCommand(sql, GetConnection());
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            return ds.Tables[0];////
        }

        public static DataTable GetDataBySQL2(string sql, params SqlParameter[] parameters)
        {
            SqlCommand command = new SqlCommand(sql, GetConnection());
            command.Parameters.AddRange(parameters);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            return ds.Tables[0];
        }

        public static DataTable GetStudentDT(String username, String password)
        {
            string sql = "SELECT * FROM Student WHERE Username = @username AND [PassWord] = @pass";
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@username", SqlDbType.VarChar);
            param[0].Value = username;
            param[1] = new SqlParameter("@pass", SqlDbType.VarChar);
            param[1].Value = password;

            return GetDataBySQL2(sql, param);

        }

        public static DataTable GetTeacherAcc(String username, String password)
        {
            string sql = "SELECT * FROM Teacher WHERE Username = @username AND [PassWord] = @pass";
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@username", SqlDbType.VarChar);
            param[0].Value = username;
            param[1] = new SqlParameter("@pass", SqlDbType.VarChar);
            param[1].Value = password;

            return GetDataBySQL2(sql, param);

        }


        public static int ExecuteSQL(string sql, params SqlParameter[] parameters)
        {
            SqlCommand command = new SqlCommand(sql, GetConnection());
            command.Parameters.AddRange(parameters);
            command.Connection.Open();
            int count = command.ExecuteNonQuery();
            command.Connection.Close();
            return count;
        }

        public static int UpdateStudentProfile(Logic.Student s)

        {
            string sql = "UPDATE dbo.Student SET FullName=@name, Gender=@gender, DateOFBirth=@dob, HomeTown=@home WHERE StudentID = @stuID";
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@name", SqlDbType.NVarChar);
            param[0].Value = s.FullName;
            param[1] = new SqlParameter("@gender", SqlDbType.Bit);
            param[1].Value = s.Gender;
            param[2] = new SqlParameter("@dob", SqlDbType.DateTime);
            param[2].Value = s.DateOFBirth;
            param[3] = new SqlParameter("@home", SqlDbType.VarChar);
            param[3].Value = s.HomeTown;
            param[4] = new SqlParameter("@stuID", SqlDbType.VarChar);
            param[4].Value = s.StudentID;
            return ExecuteSQL(sql, param);
        }

        public static DataTable GetMark(String stuID)
        {
            string sql = @"WITH r AS (
	                        SELECT ss.StudentID,su.SubjectName,ss.SmallTest,ss.Midterm,ss.FinalExam,ROUND(
	                        (CASE WHEN ss.SmallTest IS NOT NULL AND ss.Midterm IS NOT NULL AND ss.FinalExam IS NOT NULL THEN
	                        SUM(ss.SmallTest*0.3+ss.Midterm*0.3+ss.FinalExam*0.4)
	                        ELSE -1 END),1) [AVERAGE]
	                        FROM dbo.[Subject] AS su JOIN dbo.Student_Subject AS ss ON ss.SubjectID = su.SubjectID WHERE ss.StudentID = @stuID
	                        GROUP BY ss.StudentID,su.SubjectName,ss.SmallTest,ss.Midterm,ss.FinalExam)
                            SELECT *,(CASE WHEN [AVERAGE] >= 5 AND FinalExam>=4 THEN 'Pass' 
	                        WHEN [AVERAGE] < 5 AND [AVERAGE]>=0 OR FinalExam <4 THEN 'Not Pass' 
	                        ELSE 'Not Yet' END) [Status]  FROM r";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@stuID", SqlDbType.VarChar);
            param[0].Value = stuID;
            return GetDataBySQL2(sql, param);
        }

        public static DataTable GetMark(string subID,String stuID)
        {
            string sql = @"WITH r AS (
	                        SELECT ss.StudentID,ss.SubjectID,su.SubjectName,ss.SmallTest,ss.Midterm,ss.FinalExam,ROUND(
	                        (CASE WHEN ss.SmallTest IS NOT NULL AND ss.Midterm IS NOT NULL AND ss.FinalExam IS NOT NULL THEN
	                        SUM(ss.SmallTest*0.3+ss.Midterm*0.3+ss.FinalExam*0.4)
	                        ELSE -1 END),1) [AVERAGE]
	                        FROM dbo.[Subject] AS su JOIN dbo.Student_Subject AS ss ON ss.SubjectID = su.SubjectID WHERE ss.StudentID = @stuID
	                        GROUP BY ss.StudentID,ss.SubjectID,su.SubjectName,ss.SmallTest,ss.Midterm,ss.FinalExam)
                            SELECT *,(CASE WHEN [AVERAGE] >= 5 AND FinalExam>=4 THEN 'Pass' 
	                        WHEN [AVERAGE] < 5 AND [AVERAGE]>=0 OR FinalExam <4 THEN 'Not Pass' 
	                        ELSE 'Not Yet' END) [Status]  FROM r";
            int count = 1;
            if (!subID.Equals("-1"))
            {
                sql += " where SubjectID = @subID";
                ++count;
            }
            SqlParameter[] param = new SqlParameter[count];
            param[0] = new SqlParameter("@stuID", SqlDbType.VarChar);
            param[0].Value = stuID;
            if (!subID.Equals("-1"))
            {
                param[1] = new SqlParameter("@subID", SqlDbType.VarChar);
                param[1].Value = subID;
            }
            return GetDataBySQL2(sql, param);
        }
        public static DataTable GetSubjectNameByStuID(String stuID)
        {
            string sql = @"SELECT ss.SubjectID, s.SubjectName FROM dbo.Subject s JOIN dbo.Student_Subject ss ON ss.SubjectID = s.SubjectID WHERE StudentID = @stuID";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@stuID", SqlDbType.VarChar);
            param[0].Value = stuID;
            return GetDataBySQL2(sql, param);
        }
        //web
        public static DataTable GetSubjectNameBySubID(String subID)
        {
            string sql = @"SELECT s.SubjectID, s.SubjectName FROM Teacher AS t JOIN Subject AS s ON t.SubjectID = s.SubjectID WHERE s.SubjectID = @subID";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@subID", SqlDbType.VarChar);
            param[0].Value = subID;
            return GetDataBySQL2(sql, param);
        }

        public static DataTable GetMarkOfStudent(String subID)
        {
            string sql = @"SELECT stu.StudentID, stu.FullName, ss.SmallTest,ss.Midterm,ss.FinalExam FROM dbo.Student AS stu JOIN dbo.Student_Subject AS ss ON stu.StudentID = ss.StudentID WHERE ss.SubjectID = @subID";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@subID", SqlDbType.VarChar);
            param[0].Value = subID;
            return GetDataBySQL2(sql, param);
        }

        public static DataTable GetMarkOfStudent_Pagger(String subID, int pageIndex, int pagrSize)
        {
            string sql = @"WITH r AS (SELECT ROW_NUMBER() OVER (ORDER BY stu.StudentID) rownum, 
                            stu.StudentID, stu.FullName, ss.SmallTest,ss.Midterm,ss.FinalExam FROM dbo.Student AS stu JOIN dbo.Student_Subject AS ss 
                            ON stu.StudentID = ss.StudentID WHERE ss.SubjectID = @subID)
                            SELECT * FROM r WHERE r.rownum >= (@pageIndex - 1) * @pageSize + 1 AND r.rownum <= @pageIndex * @pageSize";
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@subID", SqlDbType.VarChar);
            param[0].Value = subID;
            param[1] = new SqlParameter("@pageIndex", SqlDbType.Int);
            param[1].Value = pageIndex;
            param[2] = new SqlParameter("@pageSize", SqlDbType.Int);
            param[2].Value = pagrSize;
            return GetDataBySQL2(sql, param);
        }
        public static int CountStudent(String subID)
        {
            string sql = @"WITH r AS (SELECT stu.StudentID, stu.FullName, ss.SmallTest,ss.Midterm,ss.FinalExam FROM dbo.Student AS stu JOIN dbo.Student_Subject AS ss 
                            ON stu.StudentID = ss.StudentID WHERE ss.SubjectID = @subID)
                            SELECT COUNT(*) total FROM r";
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@subID", SqlDbType.VarChar);
            param[0].Value = subID;
            DataTable dt = GetDataBySQL2(sql, param);
            return Convert.ToInt32(dt.Rows[0][0]);
        }
        public static int UpdateStudentGrade(String stuID, String subID, float SmallTest, float Midterm, float FinalExam)

        {
            string sql = "UPDATE dbo.Student_Subject SET SmallTest = @SmallTest, Midterm = @Midterm,FinalExam =@FinalExam WHERE StudentID=@stuID AND SubjectID =@subID";
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@SmallTest", SqlDbType.Float);
            param[0].Value = SmallTest;
            param[1] = new SqlParameter("@Midterm", SqlDbType.Float);
            param[1].Value = Midterm;
            param[2] = new SqlParameter("@FinalExam", SqlDbType.Float);
            param[2].Value = FinalExam;
            param[3] = new SqlParameter("@stuID", SqlDbType.VarChar);
            param[3].Value = stuID;
            param[4] = new SqlParameter("@subID", SqlDbType.VarChar);
            param[4].Value = subID;
            return ExecuteSQL(sql, param);
        }

        //Them
        public static DataTable Search_GetMarkOfStudent_Pagger(string searchID, string searchName,String subID, int pageIndex, int pagrSize)
        {
            string sql = @"WITH r AS (SELECT ROW_NUMBER() OVER (ORDER BY stu.StudentID) rownum, 
                            stu.StudentID, stu.FullName, ss.SmallTest,ss.Midterm,ss.FinalExam FROM dbo.Student AS stu JOIN dbo.Student_Subject AS ss 
                            ON stu.StudentID = ss.StudentID WHERE ss.SubjectID = @subID and stu.StudentID like @searchID and stu.FullName like @searchName)
                            SELECT * FROM r WHERE r.rownum >= (@pageIndex - 1) * @pageSize + 1 AND r.rownum <= @pageIndex * @pageSize";
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@subID", SqlDbType.VarChar);
            param[0].Value = subID;
            param[1] = new SqlParameter("@searchID", SqlDbType.VarChar);
            param[1].Value = "%"+searchID+"%";
            param[2] = new SqlParameter("@searchName", SqlDbType.VarChar);
            param[2].Value = "%"+searchName+"%";
            param[3] = new SqlParameter("@pageIndex", SqlDbType.Int);
            param[3].Value = pageIndex;
            param[4] = new SqlParameter("@pageSize", SqlDbType.Int);
            param[4].Value = pagrSize;
            return GetDataBySQL2(sql, param);
        }
        public static int Search_CountStudent(string searchID, string searchName, String subID)
        {
            string sql = @"WITH r AS (SELECT stu.StudentID, stu.FullName, ss.SmallTest,ss.Midterm,ss.FinalExam FROM dbo.Student AS stu JOIN dbo.Student_Subject AS ss 
                            ON stu.StudentID = ss.StudentID WHERE ss.SubjectID = @subID and stu.StudentID like @searchID and stu.FullName like @searchName)
                            SELECT COUNT(*) total FROM r";
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@subID", SqlDbType.VarChar);
            param[0].Value = subID;
            param[1] = new SqlParameter("@searchID", SqlDbType.VarChar);
            param[1].Value = "%" + searchID + "%";
            param[2] = new SqlParameter("@searchName", SqlDbType.VarChar);
            param[2].Value = "%" + searchName + "%";
            DataTable dt = GetDataBySQL2(sql, param);
            return Convert.ToInt32(dt.Rows[0][0]);
        }
    }
}
