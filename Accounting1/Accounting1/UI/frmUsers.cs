using Accounting1.BLL;
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
    public partial class frmUsers : Form
    {
        public frmUsers()
        {
            InitializeComponent();
        }

        userBLL u = new userBLL();
        userDAL dal = new userDAL();
        private void lblTop_Click(object sender, EventArgs e)
        {

        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtAddress_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblEMail_Click(object sender, EventArgs e)
        {

        }

        private void lblGender_Click(object sender, EventArgs e)
        {

        }

        private void lblAddress_Click(object sender, EventArgs e)
        {

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string keywords = txtSearch.Text;
            if (keywords != null)
            {
                DataTable dt = dal.Search(keywords);
                dataGridView1.DataSource = dt;

            }
            else
            {
                DataTable dt = dal.Select();
                dataGridView1.DataSource = dt;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
           

            u.first_name = txtFirstName.Text;
            u.last_name = txtLastName.Text;
            u.email = txtEMail.Text;
            u.username = txtUsername.Text;
            u.password = txtPassword.Text;
            u.contact = txtContact.Text;
            u.address = txtAddress.Text;
            u.gender = cmbGender.Text;
            u.user_type = cmbUserType.Text;
            u.added_date = DateTime.Now;

           // string loggedUser = frmLogin.loggedIn;
           // userBLL usr = dal.GetIDFromUsername(loggedUser);

            u.added_by = 1;//usr.id;

            //Inserting data into Database
            bool success = dal.Insert(u);
            if(success==true)
            {
                MessageBox.Show("user successfully created.");
                clear();
            }
            else
            {
                MessageBox.Show("Failed to Add new user");
            }

            DataTable dt = dal.Select();
            dataGridView1.DataSource = dt;

        }

        private void frmUsers_Load(object sender, EventArgs e)
        {
            DataTable dt = dal.Select();
            dataGridView1.DataSource = dt;
        }

        private void clear()
        {
            txtUserId.Text = "";
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtEMail.Text = "";
            txtUsername.Text = "";
            txtPassword.Text = "";
            txtContact.Text = "";
            txtAddress.Text = "";
            cmbGender.Text = "";
            cmbUserType.Text = "";


        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rowIndex = e.RowIndex;
            txtUserId.Text = dataGridView1.Rows[rowIndex].Cells[0].Value.ToString();
            txtFirstName.Text = dataGridView1.Rows[rowIndex].Cells[1].Value.ToString();
            txtLastName.Text = dataGridView1.Rows[rowIndex].Cells[2].Value.ToString();
            txtEMail.Text = dataGridView1.Rows[rowIndex].Cells[3].Value.ToString();
            txtUsername.Text = dataGridView1.Rows[rowIndex].Cells[4].Value.ToString();
            txtPassword.Text = dataGridView1.Rows[rowIndex].Cells[5].Value.ToString();
            txtContact.Text = dataGridView1.Rows[rowIndex].Cells[6].Value.ToString();
            txtAddress.Text = dataGridView1.Rows[rowIndex].Cells[7].Value.ToString();
            cmbGender.Text = dataGridView1.Rows[rowIndex].Cells[8].Value.ToString();
            cmbUserType.Text = dataGridView1.Rows[rowIndex].Cells[9].Value.ToString();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            u.id = Convert.ToInt32(txtUserId.Text);
            u.first_name = txtFirstName.Text;
            u.last_name = txtLastName.Text;
            u.email = txtEMail.Text;
            u.username = txtUsername.Text;
            u.password=txtPassword.Text;
            u.contact = txtContact.Text;
            u.address = txtAddress.Text;
            u.gender = cmbGender.Text;
            u.user_type = cmbUserType.Text;
            u.added_date = DateTime.Now;
            u.added_by = 1;
            bool success = dal.Update(u);
            if (success == true)
            {
                MessageBox.Show("User successfully updated");
                clear();
            }
            else
            {
                MessageBox.Show("Failed to update user");
            }
            DataTable dt = dal.Select();
            dataGridView1.DataSource = dt;


        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            u.id = Convert.ToInt32(txtUserId.Text);
            bool success = dal.Delete(u);
            if (success == true)
            {
                MessageBox.Show("User deleted successfully");
                clear();
            }
            else
            {
                MessageBox.Show("Failed  to delete user");
            }
            DataTable dt = dal.Select();
            dataGridView1.DataSource = dt;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
