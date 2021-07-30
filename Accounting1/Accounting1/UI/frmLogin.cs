﻿using Accounting1.BLL;
using Accounting1.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Accounting1.UI
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        loginBLL l = new loginBLL();
        loginDAL dal = new loginDAL();
       public static string loggedIn;

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pboxClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            l.username = txtUsername.Text.Trim();
            l.password = txtPassword.Text.Trim();
            l.user_type = cmbUserType.Text.Trim();

            bool success = dal.loginCheck(l);
            if(success==true)
            {
                //MessageBox.Show("Login Successful");
                loggedIn = l.user_type;

                switch(l.user_type)
                {
                    case "Admin":
                        {
                            frmAdminDashboard admin= new frmAdminDashboard();
                            admin.Show();
                            this.Hide();
                        }
                        break;

                    case "User":
                            {
                                frmUserDashboard user = new frmUserDashboard();
                                user.Show();
                                this.Hide();
                            }
                        break;

                    default:
                        {
                            MessageBox.Show("Invalid User Type.");
                        }
                        break;
                }
            }
            else
            {
                MessageBox.Show("Login Failed.Try Again");
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
