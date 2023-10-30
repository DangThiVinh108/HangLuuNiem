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
    public partial class frmListOrders : Form
    {
        private string conStr = @"Data Source = DESKTOP-GP47KE0;Initial Catalog = DoLuuNiem; Integrated Security = True";

        //khai bao bien de ket noi toi CSDL
        private SqlConnection mySqlConnection;

        //Khai bao bien de truy van va cap nhat du lieu
        private SqlDataAdapter mySqlDataAdapter;
        //Khai báo biến để lưu bảng dữ liệu Customer trong CSDL
        private DataTable dtListOrder;
        //private SqlConnection mySqlConnection;
        //private SqlCommand sqlCommand;
        //Khai báo biến để kiểm tra đã chọn nút <Thêm mới> hay <Sửa>
        private bool modeNew;
        //Khai báo biên lưu lại Phone
        private string _User;
        public frmListOrders()
        {
            InitializeComponent();
        }

        
        private void frmListOrders_Load(object sender, EventArgs e)
        {
            mySqlConnection = new SqlConnection(conStr);
            mySqlConnection.Open();
            //truy vấn dữ liệu lên lưới
            Display();
        }
        private void Display()
        {
            //truy vấn dữ liệu
            string sSql = "Select OrderID,UserID, From tblOrders Order By OrderID";
            mySqlDataAdapter = new SqlDataAdapter(sSql, mySqlConnection);
            //lệnh này quan trong - dùng cho update
            SqlCommandBuilder mySqlCommandBuilder = new SqlCommandBuilder(mySqlDataAdapter);
            //truy vấn vào DataTable
            dtListOrder = new DataTable();
            mySqlDataAdapter.Fill(dtListOrder);
            //Chuyen dữ liệu lên lưới
            dataGridView1.DataSource = dtListOrder;
        }
       
    }
}
