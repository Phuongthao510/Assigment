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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            txtPassword.PasswordChar = '*';
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            String username = txtUsername.Text.Trim().ToString();
            String password =  txtPassword.Text.Trim().ToString();
            if (StudentList.GetStudent(username, password) != null)
            {
                Student s = StudentList.GetStudent(username, password);
                frmViewProfile newform = new frmViewProfile(s);
                newform.Show();
            }
            else
            {
                MessageBox.Show("Username or password invalid!");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}
