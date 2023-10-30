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
    public partial class frmUsers : Form
    {
        private string conStr = @"Data Source = DESKTOP-GP47KE0;Initial Catalog = DoLuuNiem; Integrated Security = True";

        //khai bao bien de ket noi toi CSDL
        private SqlConnection mySqlConnection;

        //Khai bao bien de truy van va cap nhat du lieu
        private SqlCommand mySqlCommand;

        //Khai báo biến để kiểm tra đã chọn nút <Thêm mới> hay <Sửa>
        private bool modeNew;

        //Khai báo biên lưu lại PublisherName
        private string _UserName;
        public frmUsers()
        {
            InitializeComponent();
        }

        private void frmUsers_Load(object sender, EventArgs e)
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
            // Gọi thủ tục truy vấn dữ liệu
            string sSql = "EXECUTE DisplayUser";
            mySqlCommand = new SqlCommand(sSql, mySqlConnection);
            SqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();

            //Hiển thị dữ liệu đã truy vấn lên lưới
            DataTable dt = new DataTable();
            dt.Load(mySqlDataReader);
            dataGridView1.DataSource = dt;

            //Đóng SqlDataReader
            mySqlDataReader.Close();
        }
        private void SetControls(bool edit)
        {
            txtFullName.Enabled = edit;
            txtUserName.Enabled = edit;
            txtPassword.Enabled = edit;
            txtPhone.Enabled = edit;
            dtBirthday.Enabled = edit;
            txtAddress.Enabled = edit;
            txtEmail.Enabled = edit;
            rdoHoatDong.Enabled = edit;
            rdoKhongHD.Enabled = edit;
            txtDescription.Enabled = edit;

            btnNew.Enabled = !edit;
            btnEdit.Enabled = !edit;
            btnDelete.Enabled = !edit;

            btnSave.Enabled = edit;
            btnCancel.Enabled = edit;
        }

        private void dataGirdView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            //lấy dòng hiện thời
            int r = e.RowIndex;
            txtFullName.Text = dataGridView1.Rows[r].Cells[1].Value.ToString();//gán giá trị ở dòng thứ r ở cột 1 lên ô textFullname
            txtUserName.Text = dataGridView1.Rows[r].Cells[2].Value.ToString();
            txtPassword.Text = dataGridView1.Rows[r].Cells[3].Value.ToString();
            txtPhone.Text = dataGridView1.Rows[r].Cells[4].Value.ToString();
            dtBirthday.Text = dataGridView1.Rows[r].Cells[8].Value.ToString();
            txtAddress.Text = dataGridView1.Rows[r].Cells[9].Value.ToString();
            txtEmail.Text = dataGridView1.Rows[r].Cells[5].Value.ToString();
            txtDescription.Text = dataGridView1.Rows[r].Cells[7].Value.ToString();
            rdoHoatDong.Checked = dataGridView1.Rows[r].Cells[6].Value.ToString() == "1";
            rdoKhongHD.Checked = dataGridView1.Rows[r].Cells[6].Value.ToString() == "0";
            //Lưu lại UserName
            _UserName = txtUserName.Text;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            modeNew = true;
            SetControls(true);

            //Xóa trắng các textBox
            txtFullName.Clear();
            txtUserName.Clear();
            txtPassword.Clear();
            txtPhone.Clear();
            txtAddress.Clear();
            txtEmail.Clear();
            rdoHoatDong.Checked = true;
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
            dr = MessageBox.Show("Chắc chắn xoá dữ liệu không. Nếu xóa thì người này không truy nhập được hệ thống?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.No) return;

            //Lấy dòng dữ liệu hiện thời đã chọn trên lưới
            int r = dataGridView1.CurrentRow.Index;
            //Lấy mã 
            string UserID = dataGridView1.Rows[r].Cells[0].Value.ToString();
            string sSql1 = "Select * from tblOrders Where UserID = '" + UserID + "'";
            SqlDataAdapter mySqlDataAdapter1 = new SqlDataAdapter(sSql1, mySqlConnection);
            DataTable dtSearch = new DataTable();
            mySqlDataAdapter1.Fill(dtSearch);
            //dataGridView1.DataSource = dtSearch;
            if (dtSearch.Rows.Count > 0)
            {
                MessageBox.Show("Người dùng đã tồn tại trong bảng tblOrders!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                return;
            }
            //Gọi thủ tục xóa dữ liệu
            string sSql = "EXECUTE DeleteUser @UserID";
            //Chạy lệnh Sql
            mySqlCommand = new SqlCommand(sSql, mySqlConnection);
            //định nghĩa và gán giá trị cho tham số
            mySqlCommand.Parameters.Add("@UserID", SqlDbType.Int, 4).Value = UserID;
            //Thực hiện lệnh xóa
            mySqlCommand.ExecuteNonQuery();

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
            if (txtUserName.Text.Trim() == "")
            {
                MessageBox.Show("Đề nghị nhập tên tài khoản của người dùng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtUserName.Focus();
                return;
            }
            //
            string sSql;
            //Kiểm tra dữ liệu trùng khi <thêm mới> hoặc <sửa> tên nhà xuất bản
            if ((modeNew == true) || ((modeNew == false) && (txtUserName.Text != _UserName)))
            {
                //truy vấn dữ liệu và kiểm tra trùng
                sSql = "Select UserName from tblUsers Where UserName = @UserName";
                mySqlCommand = new SqlCommand(sSql, mySqlConnection);
                //định nghĩa và gán giá trị cho tham số
                mySqlCommand.Parameters.Add("@UserName", SqlDbType.NVarChar, 20).Value = txtUserName.Text;
                //thực hiện lệnh truy vấn dữ liệu
                SqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();
                if (mySqlDataReader.HasRows == true)
                {
                    MessageBox.Show("Đã trùng tên người dùng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtUserName.Focus();
                    //Đóng SqlDataReader
                    mySqlDataReader.Close();
                    return;
                }
                mySqlDataReader.Close();
            }

            var trangthai = "1";
            if (rdoKhongHD.Checked)
            {
                trangthai = "0";
            }

            if (modeNew == true)
            {
               
                //Thêm mới dữ liệu
                sSql = "EXECUTE AddUser @FullName,@UserName,@Password,@Phone,@Birthday,@Address,@Email,@Status,@Description";
                mySqlCommand = new SqlCommand(sSql, mySqlConnection);
                //Định nghĩa các tham số
                mySqlCommand.Parameters.Add("@FullName", SqlDbType.NVarChar, 35).Value = txtFullName.Text;
                mySqlCommand.Parameters.Add("@UserName", SqlDbType.NVarChar, 20).Value = txtUserName.Text;
                mySqlCommand.Parameters.Add("@Password", SqlDbType.NVarChar, 30).Value = txtPassword.Text;
                mySqlCommand.Parameters.Add("@Phone", SqlDbType.NVarChar, 11).Value = txtPhone.Text;
                mySqlCommand.Parameters.Add("@Birthday", SqlDbType.Date, 10).Value = dtBirthday.Value;
                mySqlCommand.Parameters.Add("@Address", SqlDbType.NVarChar, 200).Value = txtAddress.Text;
                mySqlCommand.Parameters.Add("@Email", SqlDbType.NVarChar, 30).Value = txtEmail.Text;
                mySqlCommand.Parameters.Add("@Status", SqlDbType.NVarChar, 4).Value = trangthai;
                mySqlCommand.Parameters.Add("@Description", SqlDbType.NVarChar, 200).Value = txtDescription.Text;

                mySqlCommand.ExecuteNonQuery();
            }
            else
            {
                //Sửa dữ liệu
                //1. Lấy dòng hiện thời cần sửa
                int r = dataGridView1.CurrentRow.Index;
                //2. lấy mã cần sửa
                string UserID = dataGridView1.Rows[r].Cells[0].Value.ToString();
                //3. Định nghĩa câu SQL
                sSql = "EXECUTE UpdateUser @UserID, @FullName,@UserName,@Password,@Phone,@Birthday,@Address,@Email,@Status,@Description";
                //4. Chạy lệnh SQL
                mySqlCommand = new SqlCommand(sSql, mySqlConnection);
                //định nghĩa tham số
                mySqlCommand.Parameters.Add("@UserID", SqlDbType.Int, 4).Value = UserID;
                mySqlCommand.Parameters.Add("@FullName", SqlDbType.NVarChar, 35).Value = txtFullName.Text;
                mySqlCommand.Parameters.Add("@UserName", SqlDbType.NVarChar, 20).Value = txtUserName.Text;
                mySqlCommand.Parameters.Add("@Password", SqlDbType.NVarChar, 30).Value = txtPassword.Text;
                mySqlCommand.Parameters.Add("@Phone", SqlDbType.NVarChar, 11).Value = txtPhone.Text;
                mySqlCommand.Parameters.Add("@Birthday", SqlDbType.Date, 10).Value = dtBirthday.Value;
                mySqlCommand.Parameters.Add("@Address", SqlDbType.NVarChar, 200).Value = txtAddress.Text;
                mySqlCommand.Parameters.Add("@Email", SqlDbType.NVarChar, 30).Value = txtEmail.Text;
                mySqlCommand.Parameters.Add("@Status", SqlDbType.NVarChar, 30).Value = trangthai;
                mySqlCommand.Parameters.Add("@Description", SqlDbType.NVarChar, 250).Value = txtDescription.Text;

                //thực hiện lệnh
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
            //truy vấn dữ liệu

           string sSql = "EXECUTE SearchUser @s_search";
            //4. Chạy lệnh SQL
            mySqlCommand = new SqlCommand(sSql, mySqlConnection);
            //định nghĩa tham số
            mySqlCommand.Parameters.Add("@s_search", SqlDbType.NVarChar, 50).Value = search;
            
            //Hiển thị lại dữ liệu sau khi thêm mới hoặc sửa
            SqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader();

            //Hiển thị dữ liệu đã truy vấn lên lưới
            DataTable dt = new DataTable();
            dt.Load(mySqlDataReader);
            dataGridView1.DataSource = dt;
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
