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
    public partial class frmCustomer : Form
    {
        private string conStr = @"Data Source = DESKTOP-GP47KE0;Initial Catalog = DoLuuNiem; Integrated Security = True";

        //khai bao bien de ket noi toi CSDL
        private SqlConnection mySqlConnection;

        //Khai bao bien de truy van va cap nhat du lieu
        private SqlDataAdapter mySqlDataAdapter;
        //Khai báo biến để lưu bảng dữ liệu Customer trong CSDL
        private DataTable dtCustomer ;

        //Khai báo biến để kiểm tra đã chọn nút <Thêm mới> hay <Sửa>
        private bool modeNew;

        //Khai báo biên lưu lại Phone
        private string _Phone;
        public frmCustomer()
        {
            InitializeComponent();
        }

        
        private void frmCustomer_Load(object sender, EventArgs e)
        {
            //Kết nối tới CSDL
            mySqlConnection = new SqlConnection(conStr);
            mySqlConnection.Open();

            //Truy vấn dữ liệu lên lưới
            Display();

            //Đặt trạng thái các điều khiển trên Form
            SetControls(false);
        }
        private void Display()
        {
            //truy vấn dữ liệu
            string sSql = "Select * From tblCustomers Order By CustomerID";
            mySqlDataAdapter = new SqlDataAdapter(sSql, mySqlConnection);
            //lệnh này quan trong - dùng cho update
            SqlCommandBuilder mySqlCommandBuilder = new SqlCommandBuilder(mySqlDataAdapter);
            //truy vấn vào DataTable
            dtCustomer = new DataTable();
            mySqlDataAdapter.Fill(dtCustomer);
            //Chuyen dữ liệu lên lưới
            dataGridView1.DataSource = dtCustomer;

           
        }
        private void SetControls(bool edit)
        {
            txtFullName.Enabled = edit;
            txtAddress.Enabled = edit;
            txtPhone.Enabled = edit;
            txtEmail.Enabled = edit;
            txtDescription.Enabled = edit;

            btnNew.Enabled = !edit; // edit:false ---> !edit: true
            btnEdit.Enabled = !edit;
            btnDelete.Enabled = !edit;

            btnSave.Enabled = edit;
            btnCancel.Enabled = edit;
        }
        
        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            //lấy dòng hiện thời
            int r = e.RowIndex;
            var xx = dataGridView1.Rows[r];
            txtFullName.Text = dataGridView1.Rows[r].Cells[1].Value.ToString();// lấy dữ liệu ở ô thứ 1 của dòng r và gán cho điều khiển txtfullname
            txtAddress.Text = dataGridView1.Rows[r].Cells[2].Value.ToString();
            txtPhone.Text = dataGridView1.Rows[r].Cells[3].Value.ToString();
            txtEmail.Text = dataGridView1.Rows[r].Cells[4].Value.ToString();
            txtDescription.Text = dataGridView1.Rows[r].Cells[5].Value.ToString();

            //lưu lại Phone để kiểm tra sửa trùng Phone
            _Phone = txtPhone.Text;
        }
        private void btnNew_Click(object sender, EventArgs e)
        {
            modeNew = true;
            SetControls(true);

            //Xóa trắng các textBox
            txtFullName.Clear();
            txtAddress.Clear();
            txtPhone.Clear();
            txtEmail.Clear();
            txtDescription.Clear();

            //Chuyển con trỏ về txtFullName để nhập
            txtFullName.Focus();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            modeNew = false;
            SetControls(true);
            //Chuyển con trỏ về txtFullName để nhập
            txtFullName.Focus();
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
            dtCustomer.Rows[r].Delete();
            //cập nhật lại bảng Customers trong CSDL
            mySqlDataAdapter.Update(dtCustomer);

            //Hiển thị lại dữ liệu đã xóa
            Display();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtFullName.Text.Trim() == "")       
            {
                MessageBox.Show("Đề nghị nhập tên người dùng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtFullName.Focus();
                return;
            }
            if (txtPhone.Text.Trim() == "")
            {
                MessageBox.Show("Đề nghị nhập số điện thoại của khách hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPhone.Focus();
                return;
            }
            //
            string sSql;
            //Kiểm tra dữ liệu trùng khi <thêm mới> hoặc <sửa> tên khach hang
            if ((modeNew == true) || ((modeNew == false) && (txtPhone.Text != _Phone)))
            {
                //truy vấn dữ liệu và kiểm tra trùng
                sSql = "Select Phone from tblCustomers Where Phone = '" + txtPhone.Text + "'";
                SqlDataAdapter mySqlDataAdapter1 = new SqlDataAdapter(sSql, mySqlConnection);
                DataTable dtSearch = new DataTable();
                mySqlDataAdapter1.Fill(dtSearch);// truy vấn dữ liệu từ sql lên dtSearch
                if (dtSearch.Rows.Count > 0)
                {
                    MessageBox.Show("Đã trùng số điện thoại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPhone.Focus();
                    return;
                }
            }

            if (modeNew == true)
            {
                DataRow myDataRow = dtCustomer.NewRow();
                myDataRow["FullName"] = txtFullName.Text;
                myDataRow["Address"] = txtAddress.Text;
                myDataRow["Phone"] = txtPhone.Text;
                myDataRow["Email"] = txtEmail.Text;
                myDataRow["Description"] = txtDescription.Text;
                //them dòng đã gán dữ liệu vào dtCustomer
                dtCustomer.Rows.Add(myDataRow);
                //cập nhật lại dữ liệu trong bảng Custumers của CSDL
                mySqlDataAdapter.Update(dtCustomer);
            }
            else
            {
                //Sửa dữ liệu
                //1. Lấy dòng hiện thời cần sửa
                int r = dataGridView1.CurrentRow.Index;
                DataRow myDataRow = dtCustomer.Rows[r];
                //2. gán lại dữ liệu
                myDataRow["FullName"] = txtFullName.Text;
                myDataRow["Address"] = txtAddress.Text;
                myDataRow["Phone"] = txtPhone.Text;
                myDataRow["Email"] = txtEmail.Text;
                myDataRow["Description"] = txtDescription.Text;
                //3. cập nhật lại dữ liệu trong bảng Custumers của CSDL
                mySqlDataAdapter.Update(dtCustomer);
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
            string sSql = "Select * From tblCustomers where FullName like N'%" + search + "%' or  Phone like N'%" + search + "%' Order By CustomerID";
            mySqlDataAdapter = new SqlDataAdapter(sSql, mySqlConnection);
            //lệnh này quan trong - dùng cho update
            SqlCommandBuilder mySqlCommandBuilder = new SqlCommandBuilder(mySqlDataAdapter);
            //truy vấn vào DataTable
            dtCustomer = new DataTable();
            mySqlDataAdapter.Fill(dtCustomer);
            //Chuyen dữ liệu lên lưới
            dataGridView1.DataSource = dtCustomer;

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
