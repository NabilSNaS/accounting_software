using Accounting1.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Accounting1
{
    public partial class frmUserDashboard : Form
    {
        public frmUserDashboard()
        {
            InitializeComponent();
        }

        public static string transactionType;
        private void frmUserDashboard_FormClosed(object sender, FormClosedEventArgs e)
        {
            frmLogin login = new frmLogin();
            login.Show();
            this.Hide();
        }

        private void frmUserDashboard_Load(object sender, EventArgs e)
        {
            lblloggedinuser.Text = frmLogin.loggedIn;
        }

        private void dealerAndCustomerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDeaCust DeaCust = new frmDeaCust();
            DeaCust.Show();
        }

        private void purchaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            transactionType = "Purchase";
            frmPurchaseAndSales purchase = new frmPurchaseAndSales();
            purchase.Show();
           
           
        }

        private void saToolStripMenuItem_Click(object sender, EventArgs e)
        {
            transactionType = "Sales";
            frmPurchaseAndSales sales = new frmPurchaseAndSales();
            sales.Show();
            
        }

        private void inventoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            transactionType = "Inventory";
            frmInventory inventory = new frmInventory();
            inventory.Show();
        }

        
    }
}
