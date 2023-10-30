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
    public partial class frmSouvenir : Form
    {
        private string conStr = @"Data Source = DESKTOP-GP47KE0;Initial Catalog = DoLuuNiem; Integrated Security = True";

        //khai bao bien de ket noi toi CSDL
        private SqlConnection mySqlConnection;

        //Khai bao bien de truy van va cap nhat du lieu
        private SqlCommand mySqlCommand;

        //Khai báo biến để kiểm tra đã chọn nút <Thêm mới> hay <Sửa>
        private bool modeNew;

        //Khai báo biên lưu lại CategoryName
        private string _Souvenir;
        public frmSouvenir()
        {
            InitializeComponent();
        }

        private void frmSouvenir_Load(object sender, EventArgs e)
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
            // Truy vấn dữ liệu
            string sSql = "Select * from tblSouvenir Order By SouvenirID";
            mySqlCommand = new SqlCommand(sSql, mySqlConnection);
            SqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();

            //Hiển thị dữ liệu đã truy vấn lên lưới
            DataTable dt = new DataTable();
            dt.Load(mySqlDataReader);
            dataGridView1.DataSource = dt;

            //Đóng SqlDataReader
            mySqlDataReader.Close();
        }
        //Hàm điều khiển trạng thái Enable của các điều khiển
        private void SetControls(bool edit)
        {
            txtSouvenirName.Enabled = edit;
            txtDescription.Enabled = edit;

            btnNew.Enabled = !edit;
            btnEdit.Enabled = !edit;
            btnDelete.Enabled = !edit;

            btnSave.Enabled = edit;
            btnCancel.Enabled = edit;
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            //Lấy dòng hiện thời
            int r = e.RowIndex;
            txtSouvenirName.Text = dataGridView1.Rows[r].Cells[1].Value.ToString();
            txtDescription.Text = dataGridView1.Rows[r].Cells[2].Value.ToString();

            //lưu lại CategoryName trước khi sửa
            _Souvenir = txtSouvenirName.Text;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            modeNew = true;
            SetControls(true);
            //Xóa trắng các textBox
            txtSouvenirName.Clear();
            txtDescription.Clear();
            //Chuyển con trỏ về txtCategory để nhập
            txtSouvenirName.Focus();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            modeNew = false;
            SetControls(true);
            //Chuyển con trỏ về txtCategory để nhập
            txtSouvenirName.Focus();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //Hiển thị hộp thoại xác nhận chắc chắn xóa không?
            DialogResult dr;
            dr = MessageBox.Show("Chắc chắn xoá dữ liệu không. Nếu xóa thì tất cả dữ liệu liên quan đến đồ lưu niệm sẽ xóa hết?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.No) return;

            //Lấy dòng dữ liệu hiện thời đã chọn trên lưới
            int r = dataGridView1.CurrentRow.Index;
            //Lấy mã 
            string SouvenirID = dataGridView1.Rows[r].Cells[0].Value.ToString();

            string sSql = "Delete From tblSouvenir Where SouvenirID = " + SouvenirID;
            //Chạy lệnh Sql
            mySqlCommand = new SqlCommand(sSql, mySqlConnection);
            mySqlCommand.ExecuteNonQuery();

            //Hiển thị lại dữ liệu đã xóa
            Display();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtSouvenirName.Text.Trim() == "")
            {
                MessageBox.Show("Đề nghị nhập tên đồ lưu niệm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSouvenirName.Focus();
                return;
            }
            string sSql;
            //Kiểm tra dữ liệu trùng khi <thêm mới> hoặc <sửa> tên loại sách
            if ((modeNew == true) || ((modeNew == false) && (txtSouvenirName.Text != _Souvenir)))
            {
                //truy vấn dữ liệu và kiểm tra trùng
                sSql = "Select SouvenirName from tblSouvenir Where SouvenirName = N'" + txtSouvenirName.Text + "'";
                mySqlCommand = new SqlCommand(sSql, mySqlConnection);
                SqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
                if (mySqlDataReader.HasRows == true)
                {
                    MessageBox.Show("Đã trùng tên đồ lưu niệm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtSouvenirName.Focus();
                    //Đóng SqlDataReader
                    mySqlDataReader.Close();
                    return;
                }
                mySqlDataReader.Close();
            }


            if (modeNew == true)
            {
                //Thêm mới dữ liệu
                sSql = "Insert Into tblSouvenir (SouvenirName,Description) Values (N'" + txtSouvenirName.Text + "',N'" + txtDescription.Text + "')";
                mySqlCommand = new SqlCommand(sSql, mySqlConnection);
                mySqlCommand.ExecuteNonQuery();
            }
            else
            {
                //Sửa dữ liệu
                //1. Lấy dòng hiện thời cần sửa
                int r = dataGridView1.CurrentRow.Index;
                //2. lấy mã cần sửa
                string SouvenirID = dataGridView1.Rows[r].Cells[0].Value.ToString();
                //3. Định nghĩa câu SQL
                sSql = "Update tblSouvenir SET SouvenirName = N'" + txtSouvenirName.Text + "',";
                sSql = sSql + "Description = N'" + txtDescription.Text + "' "; //có 1 dấu cách
                sSql = sSql + "Where SouvenirID = " + SouvenirID;
                //4. Chạy lệnh SQL
                mySqlCommand = new SqlCommand(sSql, mySqlConnection);
                mySqlCommand.ExecuteNonQuery();
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
            // Truy vấn dữ liệu
            string sSql = "Select * from tblSouvenir where SouvenirName like N'%" + search + "%' Order By SouvenirID";
            mySqlCommand = new SqlCommand(sSql, mySqlConnection);
            SqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();

            //Hiển thị dữ liệu đã truy vấn lên lưới
            DataTable dt = new DataTable();
            dt.Load(mySqlDataReader);
            dataGridView1.DataSource = dt;

            //Đóng SqlDataReader
            mySqlDataReader.Close();
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
