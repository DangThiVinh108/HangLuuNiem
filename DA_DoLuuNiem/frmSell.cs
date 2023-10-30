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
    public partial class frmSell : Form
    {
        private DataServices dsSell;
        private DataTable dtSell;
        public frmSell()
        {
            InitializeComponent();
        }

        private void frmSell_Load(object sender, EventArgs e)
        {
            //1. Truy vấn dũ liệu bảng người dùng và đưa vào cbotblUsers
            string sSql = "Select * from tblUsers Order By FullName";
            DataServices dsUser = new DataServices();
            DataTable dtUser = dsUser.RunQuery(sSql);
            //Đưa vào cbotblUsers
            cboUsers.DataSource = dtUser;
            cboUsers.DisplayMember = "FullName";
            cboUsers.ValueMember = "UserID";
            //2. Truy vấn dữ liệu bảng đồ lưu niệm và đưa vào cboSouvenir
            sSql = "Select * from tblSouvenir  Order By SouvenirName";
            DataServices dsSouvenir = new DataServices();
            DataTable dtSouvenir = dsSouvenir.RunQuery(sSql);
            //Đưa vào cbotblSuppliers
            cboSouvenirs.DataSource = dtSouvenir;
            cboSouvenirs.DisplayMember = "SouvenirName";
            cboSouvenirs.ValueMember = "SouvenirID";

            //3. Truy vấn dữ liệu bảng nhà cung cấp và đưa vào cbotblSuppliers
            sSql = "Select * from tblCustomers  Order By FullName";
            DataServices dsCustomer = new DataServices();
            DataTable dtCustomer = dsCustomer.RunQuery(sSql);
            //Đưa vào cbotblSuppliers
            cboCustomer.DataSource = dtCustomer;
            cboCustomer.DisplayMember = "FullName";
            cboCustomer.ValueMember = "CustomerID";
            //Gọi hàm hiển thị lên lưới
            // Display();
            SetControls(true);
        }
        private void Display()
        {
            dsSell = new DataServices();
            string sSql = "Select * From tblSell Order BY SellDate";
            dtSell = dsSell.RunQuery(sSql);
            dataGridView1.DataSource = dtSell;
        }
        private void SetControls(bool edit)
        {
            //các textbox và combobox
            cboUsers.Enabled = edit;
            cboCustomer.Enabled = edit;
            dtSellDate.Enabled = edit;
            txtDescription1.Enabled = edit;
            cboSouvenirs.Enabled = edit;
            cboSouvenirs.SelectedIndex = -1;
            cboCustomer.SelectedIndex = -1;
            cboUsers.SelectedIndex = -1;
            txtQuantity.Enabled = edit;
            txtSellingPrice.Enabled = edit;

            //các nút ấn
            btnNew.Enabled = !edit;
            btnSave.Enabled = edit;

        }

        
    }
}
