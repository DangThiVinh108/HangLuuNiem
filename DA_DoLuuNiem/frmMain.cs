using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DA_DoLuuNiem
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void mnuCustomers_Click(object sender, EventArgs e)
        {
            
            frmCustomer frmCustomer = new frmCustomer();
            frmCustomer.MdiParent = this;
            frmCustomer.Show();
        }

        private void mnuSuppliers_Click(object sender, EventArgs e)
        {
            frmSupplier frmSupplier = new frmSupplier();
            frmSupplier.ShowDialog();
        }

        private void mnuUsers_Click(object sender, EventArgs e)
        {
            frmUsers frmUsers = new frmUsers();
            frmUsers.ShowDialog();
        }

        private void mnuSouvenir_Click(object sender, EventArgs e)
        {
            frmSouvenir frmSouvenir = new frmSouvenir();
            frmSouvenir.ShowDialog();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {

        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.ExitThread();        }

        private void mnuOrders_Click(object sender, EventArgs e)
        {
            frmOrders frmOrders = new frmOrders();
            frmOrders.ShowDialog();
        }

        private void mnuSell_Click(object sender, EventArgs e)
        {
            frmSell frmSell = new frmSell();
            frmSell.ShowDialog();
        }

        private void mnuListOrders_Click(object sender, EventArgs e)
        {
            frmListOrders frmListOrders = new frmListOrders();
            frmListOrders.ShowDialog();
        }
    }
}
