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
    public partial class frmLogin : Form
    {
        //khai báo biến lớp
        private DataServices myDataServices;
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            DataServices myDataServices = new DataServices();
            

            //Kiểm tra username và password trong bảng User
            string sSql = "Select * from tblUsers Where (UserName = N'" + txtUserName.Text + "') and (Password = N'" + txtPassword.Text + "')";
            //truy vấn dữ liệu
            DataTable dtUser = myDataServices.RunQuery(sSql);
            if (dtUser.Rows.Count == 0)
            {
                MessageBox.Show("Không đúng tên hoặc mật khẩu đăng nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtUserName.Focus();
                return;
            }

            //Nếu đúng thì gọi hàm main
            frmMain frmMain = new frmMain();
            frmMain.ShowDialog();
            this.Hide();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
            
        }

        private void txtUserName_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyValue == 13) || (e.KeyValue == 40)) txtPassword.Focus();
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyValue == 13) || (e.KeyValue == 40)) btnLogin.Focus();
            if (e.KeyValue == 38) txtUserName.Focus();
        }
    }
}
