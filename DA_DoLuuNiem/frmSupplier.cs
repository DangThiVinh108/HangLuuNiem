using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DA_DoLuuNiem
{
    public partial class frmSupplier : Form
    {
        private string conStr = @"Data Source = DESKTOP-GP47KE0;Initial Catalog = DoLuuNiem; Integrated Security = True";

        //khai bao bien de ket noi toi CSDL
        private SqlConnection mySqlConnection;

        //Khai bao bien de truy van va cap nhat du lieu
        private SqlDataAdapter mySqlDataAdapter;
        //Khai báo biến để lưu bảng dữ liệu Customer trong CSDL
        private DataTable dtSupplier;
        //private SqlConnection mySqlConnection;
        //private SqlCommand sqlCommand;
        //Khai báo biến để kiểm tra đã chọn nút <Thêm mới> hay <Sửa>
        private bool modeNew;
        //Khai báo biên lưu lại Phone
        private string _Phone;
        public frmSupplier()
        {
            InitializeComponent();
        }

        private void frmSupplier_Load(object sender, EventArgs e)
        {
            mySqlConnection = new SqlConnection(conStr);
            mySqlConnection.Open();
            //truy vấn dữ liệu lên lưới
            Display();
            SetControls(false);
        }
        private void Display()
        {
            //truy vấn dữ liệu
            string sSql = "Select * From tblSuppliers Order By SupplierID";
            mySqlDataAdapter = new SqlDataAdapter(sSql, mySqlConnection);
            //lệnh này quan trong - dùng cho update
            SqlCommandBuilder mySqlCommandBuilder = new SqlCommandBuilder(mySqlDataAdapter);
            //truy vấn vào DataTable
            dtSupplier = new DataTable();
            mySqlDataAdapter.Fill(dtSupplier);
            //Chuyen dữ liệu lên lưới
            dataGridView1.DataSource = dtSupplier;
        }
        private void SetControls(bool edit)
        {
            txtCompanyName.Enabled = edit;
            txtContactName.Enabled = edit;
            txtAddress.Enabled = edit;
            txtPhone.Enabled = edit;
            txtEmail.Enabled = edit;
            txtDescription.Enabled = edit;

            btnNew.Enabled = !edit;
            btnEdit.Enabled = !edit;
            btnDelete.Enabled = !edit;

            btnSave.Enabled = edit;
            btnCancel.Enabled = edit;
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            //lấy dòng hiện thời
            int r = e.RowIndex;
            txtCompanyName.Text = dataGridView1.Rows[r].Cells[1].Value.ToString();
            txtContactName.Text = dataGridView1.Rows[r].Cells[2].Value.ToString();
            txtAddress.Text = dataGridView1.Rows[r].Cells[3].Value.ToString();
            txtPhone.Text = dataGridView1.Rows[r].Cells[4].Value.ToString();
            txtEmail.Text = dataGridView1.Rows[r].Cells[5].Value.ToString();
            txtDescription.Text = dataGridView1.Rows[r].Cells[6].Value.ToString();

            //lưu lại Phone để kiểm tra sửa trùng Phone
            _Phone = txtPhone.Text;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            modeNew = true;
            SetControls(true);

            //Xóa trắng các textBox
            txtCompanyName.Clear();
            txtContactName.Clear();
            txtAddress.Clear();
            txtPhone.Clear();
            txtEmail.Clear();
            txtDescription.Clear();

            //Chuyển con trỏ về txtCompanyName để nhập
            txtCompanyName.Focus();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            modeNew = false;
            SetControls(true);
            //Chuyển con trỏ về txtCompanyName để nhập
            txtCompanyName.Focus();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //Hiển thị hộp thoại xác nhận chắc chắn xóa không?
            DialogResult dr;
            dr = MessageBox.Show("Chắc chắn xoá dữ liệu không ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.No) return;

            //Lấy dòng dữ liệu hiện thời đã chọn trên lưới
            int r = dataGridView1.CurrentRow.Index;
            //xóa dòng đã chọn trong dtCustomer
            dtSupplier.Rows[r].Delete();
            //cập nhật lại bảng Customers trong CSDL
            mySqlDataAdapter.Update(dtSupplier);

            //Hiển thị lại dữ liệu đã xóa
            Display();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtCompanyName.Text.Trim() == "")
            {
                MessageBox.Show("Đề nghị nhập tên nhà cung cấp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCompanyName.Focus();
                return;
            }
            if (txtCompanyName.Text.Trim() == "")
            {
                MessageBox.Show("Đề nghị nhập số điện thoại của nhà cung cấp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCompanyName.Focus();
                return;
            }
            //
            string sSql;
            //Kiểm tra dữ liệu trùng khi <thêm mới> hoặc <sửa> tên nhà xuất bản
            if ((modeNew == true) || ((modeNew == false) && (txtCompanyName.Text != _Phone)))
            {
                //truy vấn dữ liệu và kiểm tra trùng
                sSql = "Select * from tblSuppliers Where Phone = '" + txtCompanyName.Text + "'";
                SqlDataAdapter mySqlDataAdapter1 = new SqlDataAdapter(sSql, mySqlConnection);
                DataTable dtSearch = new DataTable();
                mySqlDataAdapter1.Fill(dtSearch);
                //dataGridView1.DataSource = dtSearch;
                if (dtSearch.Rows.Count > 0)
                {
                    MessageBox.Show("Đã trùng số điện thoại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCompanyName.Focus();
                    return;
                }
            }

            if (modeNew == true)
            {
                DataRow myDataRow = dtSupplier.NewRow();
                myDataRow["CompanyName"] = txtCompanyName.Text;
                myDataRow["ContactName"] = txtContactName.Text;
                myDataRow["Address"] = txtAddress.Text;
                myDataRow["Phone"] = txtPhone.Text;
                myDataRow["Email"] = txtEmail.Text;
                myDataRow["Description"] = txtDescription.Text;
                //them dòng đã gán dữ liệu vào dtCustomer
                dtSupplier.Rows.Add(myDataRow);
                //cập nhật lại dữ liệu trong bảng Custumers của CSDL
                mySqlDataAdapter.Update(dtSupplier);
            }
            else
            {
                //Sửa dữ liệu
                //1. Lấy dòng hiện thời cần sửa
                int r = dataGridView1.CurrentRow.Index;
                DataRow myDataRow = dtSupplier.Rows[r];
                //2. gán lại dữ liệu
                myDataRow["CompanyName"] = txtCompanyName.Text;
                myDataRow["ContactName"] = txtContactName.Text;
                myDataRow["Address"] = txtAddress.Text;
                myDataRow["Phone"] = txtPhone.Text;
                myDataRow["Email"] = txtEmail.Text;
                myDataRow["Description"] = txtDescription.Text;
                //3. cập nhật lại dữ liệu trong bảng Custumers của CSDL
                mySqlDataAdapter.Update(dtSupplier);
            }
            //Hiển thị lại dữ liệu sau khi thêm mới hoặc sửa
            Display();

            SetControls(false);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            SetControls(false);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string search = txtSearch.Text.Trim();
            //truy vấn dữ liệu
            string sSql = "Select * From tblSuppliers where CompanyName like N'%" + search + "%' or  Phone like N'%" + search + "%' Order By SupplierID";
            mySqlDataAdapter = new SqlDataAdapter(sSql, mySqlConnection);
            //lệnh này quan trong - dùng cho update
            SqlCommandBuilder mySqlCommandBuilder = new SqlCommandBuilder(mySqlDataAdapter);
            //truy vấn vào DataTable
            dtSupplier = new DataTable();
            mySqlDataAdapter.Fill(dtSupplier);
            //Chuyen dữ liệu lên lưới
            dataGridView1.DataSource = dtSupplier;
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyValue == 13) || (e.KeyValue == 40)) btnSearch.Focus();
        }

        private void btnSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 38) txtSearch.Focus();
        }
    }
}
