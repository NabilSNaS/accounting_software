using Accounting1.BLL;
using Accounting1.DAL;
using DGVPrinterHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Forms;

namespace Accounting1.UI
{
    public partial class frmPurchaseAndSales : Form
    {
        public frmPurchaseAndSales()
        {
            InitializeComponent();
        }


        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        DeaCustDAL dcDAL = new DeaCustDAL();
        productsDAL pDAL = new productsDAL();

        userDAL uDAL = new userDAL();
        transactionDAL tDAL = new transactionDAL();
        transactionDetailDAL tdDAL = new transactionDetailDAL();

        DataTable transactionDT = new DataTable();


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pnlCalculation_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblPaidAmount_Click(object sender, EventArgs e)
        {

        }

        private void lblGrandTotal_Click(object sender, EventArgs e)
        {

        }

        private void lblVat_Click(object sender, EventArgs e)
        {

        }

        private void lblDiscount_Click(object sender, EventArgs e)
        {

        }

        private void frmPurchaseAndSales_Load(object sender, EventArgs e)
        {
            string type = frmUserDashboard.transactionType;
            lblTop.Text = type;
            transactionDT.Columns.Add("Product Name");
             transactionDT.Columns.Add("Rate");
             transactionDT.Columns.Add("Quantity");
             transactionDT.Columns.Add("Total");

        
        
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text;
            if (keyword == "")
            {
                txtName.Text = "";
                txtEmail.Text = "";
                txtContact.Text = "";
                txtAddress.Text = "";
                return;
            }

            DeaCustBLL dc = dcDAL.SearchDealerCustomerForTransaction(keyword);
            txtName.Text = dc.name;
            txtEmail.Text = dc.email;
            txtContact.Text = dc.contact;
            txtAddress.Text = dc.address;
        }

        private void txtSearchProduct_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtSearchProduct.Text;

            if(keyword=="")
            {
                txtProductName.Text = "";
                txtInventory.Text = "";
                txtRate.Text = "";
                txtqty.Text = "";
                return;
            }

            productsBLL p = pDAL.GetProductsForTransaction(keyword);
            txtProductName.Text = p.name;
            txtInventory.Text = p.qty.ToString();
            txtRate.Text = p.rate.ToString();
        }

        private void btnADD_Click(object sender, EventArgs e)
        {
            string productName = txtProductName.Text;
            decimal Rate = decimal.Parse(txtRate.Text);
            decimal qty = decimal.Parse(txtqty.Text);
            decimal Total = Rate * qty;
            decimal subTotal = decimal.Parse(txtSubTotal.Text);
            subTotal = subTotal + Total;


            if(productName==""){
                MessageBox.Show("Select the product first.Try Again.");

            }
            else{
                transactionDT.Rows.Add(productName, Rate, qty,Total);
                dgvAddedProducts.DataSource = transactionDT;
                txtSubTotal.Text = subTotal.ToString();
                txtSearchProduct.Text = "";
                txtProductName.Text = "";
                txtInventory.Text = "0.00";
                txtRate.Text = "0.00";
                txtqty.Text = "0.00";

            }
        }

        private void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            string value = txtDiscount.Text;
            if (value == "")
            {
                MessageBox.Show("Please Add Discount First");
            }
            else
            {   decimal subTotal = decimal.Parse(txtSubTotal.Text);
                decimal discount = decimal.Parse(txtDiscount.Text);
                decimal grandTotal = ((100 - discount) / 100) * subTotal;
                txtGrandTotal.Text = grandTotal.ToString();
            }
        }

        private void txtVat_TextChanged(object sender, EventArgs e)
        {
            string check = txtGrandTotal.Text;
            if (check == "")
            {
                MessageBox.Show("Calculate the discount and set the Grand Total First.");
            }
            else
            {    decimal previousGT = decimal.Parse(txtGrandTotal.Text);
                decimal vat = decimal.Parse(txtVat.Text);
                decimal grandTotalWithVat = (((100 + vat) / 100) * previousGT);
                txtGrandTotal.Text = grandTotalWithVat.ToString();
            }
        }

        private void txtPaidAmount_TextChanged(object sender, EventArgs e)
        {
            decimal grandTotal = decimal.Parse(txtGrandTotal.Text);
            decimal paidAmount = decimal.Parse(txtPaidAmount.Text);
            decimal returnAmount = paidAmount - grandTotal;


            txtReturnAmount.Text = returnAmount.ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            transactionsBLL transaction = new transactionsBLL();
            transaction.type = lblTop.Text;

            string deaCustName = txtName.Text;
            DeaCustBLL dc = dcDAL.GetDeacustIDFromName(deaCustName);

            transaction.dea_cust_id = dc.id;
            transaction.grandTotal = Math.Round(decimal.Parse(txtGrandTotal.Text),2);
            transaction.transaction_date = DateTime.Now;
            transaction.tax = decimal.Parse(txtVat.Text);
            transaction.discount = decimal.Parse(txtDiscount.Text);

           // string username = frmLogin.loggedIn;
           // userBLL u=uDAL.GetIDFromUsername(username);

            transaction.added_by = 1;
            transaction.transactionDetails = transactionDT;

            bool success = false;
             using(TransactionScope scope = new TransactionScope())
            {

                int transactionID= -1;
                bool w =tDAL.Insert_Transaction(transaction,out transactionID);

                for (int i = 0; i < transactionDT.Rows.Count; i++)
                {
                    transactionDetailBLL transactionDetail = new transactionDetailBLL();
                    string productName = transactionDT.Rows[i][0].ToString();
                    productsBLL p = pDAL.GetProductIDFromName(ProductName);


                  transactionDetail.product_id = p.id;
                  transactionDetail.rate = decimal.Parse(transactionDT.Rows[i][1].ToString());
                  transactionDetail.qty = decimal.Parse(transactionDT.Rows[i][2].ToString());
                  transactionDetail.total = Math.Round(decimal.Parse(transactionDT.Rows[i][3].ToString()),2);
                  transactionDetail.dea_cust_id = dc.id;
                  transactionDetail.added_date = DateTime.Now;
                  transactionDetail.added_by = 1;

                  string transactionType = lblTop.Text;

                  bool x=false;
                    if(transactionType == "Purchase")
                    {
                         x = pDAL.IncreaseProduct(transactionDetail.product_id,transactionDetail.qty);
                    }
                    else if (transactionType == "Sales")
                    {
                         x = pDAL.DecreaseProduct(transactionDetail.product_id, transactionDetail.qty);
                    }
                  bool y = tdDAL.InsertTransactionDetail(transactionDetail);
                  success = y && w && x;
                 
                }
               
                if (success == true)
                {
                    scope.Complete();

                    DGVPrinter printer = new DGVPrinter();

                    printer.Title = "\r\n\r\n\r\n ANYSTORE PVT.LTD.\r\n";
                    printer.SubTitle = "Dhaka, Bangladesh\r\n Phone: 0134567890 \r\n\r\n";
                    printer.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
                    printer.PageNumbers = true;
                    printer.PageNumberInHeader = false;
                    printer.PorportionalColumns = true;
                    printer.HeaderCellAlignment = StringAlignment.Near;
                    printer.Footer = "Discount:" + txtDiscount.Text + "%\r\n" +"VAT: "+txtVat.Text+"%\r\n"+ "Grand Total:" +txtGrandTotal.Text+ "\r\n\r\n"+"Thank you for doing business with us.";
                    printer.FooterSpacing = 15;
                    printer.PrintDataGridView(dgvAddedProducts); 


                    
                    MessageBox.Show("Transaction Completed Successfully");
                    dgvAddedProducts.DataSource = null;
                    dgvAddedProducts.Rows.Clear();
                    txtSearch.Text = "";
                    txtName.Text = "";
                    txtEmail.Text = "";
                    txtContact.Text = "";
                    txtAddress.Text = "";
                    txtSearchProduct.Text = "";
                    txtProductName.Text = "";
                    txtInventory.Text = "0";
                    txtRate.Text = "0";
                    txtqty.Text = "0";
                    txtSubTotal.Text = "0";
                    txtDiscount.Text = "0";
                    txtVat.Text = "0";
                    txtGrandTotal.Text = "0";
                    txtPaidAmount.Text = "0";
                    txtReturnAmount.Text = "0";


                }
                else
                {
                    MessageBox.Show("Transaction Failed");
                }

                 
            }

       
                    
                }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {

        }


       // public bool y { get; set; }
    }


        }

    

