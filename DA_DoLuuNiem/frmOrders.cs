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
    public partial class frmOrders : Form
    {
        private string conStr = @"Data Source = DESKTOP-GP47KE0;Initial Catalog = DoLuuNiem; Integrated Security = True";
        private DataServices dsOrders;
        private DataTable dtOrders;
        //Khai bao bien de truy van va cap nhat du lieu
        private SqlCommand mySqlCommand;
        //khai bao bien de ket noi toi CSDL
        private SqlConnection mySqlConnection;
        //Khai báo biến để kiểm tra đã chọn nút <Thêm mới> hay <Sửa>
        private bool modeNew;
        public frmOrders()
        {
            InitializeComponent();
        }

        private void frmOrders_Load(object sender, EventArgs e)
        {
            //Kết nối tới CSDL
            mySqlConnection = new SqlConnection(conStr);
            mySqlConnection.Open();
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
            sSql = "Select * from tblSuppliers  Order By CompanyName";
            DataServices dsSupplier = new DataServices();
            DataTable dtSupplier = dsSupplier.RunQuery(sSql);
            //Đưa vào cbotblSuppliers
            cboSupplier.DataSource = dtSupplier;
            cboSupplier.DisplayMember = "CompanyName";
            cboSupplier.ValueMember = "SupplierID";
            //Gọi hàm hiển thị lên lưới
            // Display();
            SetControls(true);
        }
        private void Display()
        {
            dsOrders = new DataServices();
            string sSql = "Select * From tblOrders Order BY OrderDate";
            dtOrders = dsOrders.RunQuery(sSql);
            dataGridView1.DataSource = dtOrders;
        }
        private void SetControls(bool edit)
        {
            //các textbox và combobox
            cboUsers.Enabled = edit;
            cboSupplier.Enabled = edit;
            dtOrderDate.Enabled = edit;
            txtDescription.Enabled = edit;
            cboSouvenirs.Enabled = edit;
            cboSouvenirs.SelectedIndex = -1;
            cboSupplier.SelectedIndex = -1;
            cboUsers.SelectedIndex = -1;
            txtQuantity.Enabled = edit;
            txtPrice.Enabled = edit;
            txtSumPrice.Enabled = edit;

            //các nút ấn
            btnNew.Enabled = edit;
            btnSave.Enabled = edit;

        }



        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            cboUsers.SelectedValue = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            cboSupplier.SelectedValue = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            dtOrderDate.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtDescription.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            //hiển thị danh sách nhập đồ lưu niệm
            string sSql = "Select FullName from tblUser,tblSuppliers,tblOrders ";
            sSql = sSql + "Where (tblUsers.UserID = tblOrders.UserID) and (tblSuppliers.SupplierID = tblOrders.SupplierID) and (tblOrders.OrderDate = N'" + dtOrderDate.Text + "')";

        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            //1. Kiểm tra dữ liệu
            if (cboUsers.Text.Trim() == "")
            {
                MessageBox.Show("Đề nghị chọn nhân viên nhập", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cboUsers.Focus();
                return;
            }
            //kiểm tra tiếp 
            if (cboSupplier.Text.Trim() == "")
            {
                MessageBox.Show("Đề nghị chọn nhà cung cấp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cboSupplier.Focus();
                return;
            }

            //2. Thêm dữ liệu vào bảng Orders
            string sSql1 = "EXECUTE AddOrder @UserID,@SupplierID,@OrderDate,@Description,@Total";
            mySqlCommand = new SqlCommand(sSql1, mySqlConnection);
            //Định nghĩa các tham số
            mySqlCommand.Parameters.Add("@UserID", SqlDbType.Int, 4).Value = cboUsers.SelectedValue;
            mySqlCommand.Parameters.Add("@SupplierID", SqlDbType.Int, 4).Value = cboSupplier.SelectedValue;
            mySqlCommand.Parameters.Add("@OrderDate", SqlDbType.Date, 10).Value = dtOrderDate.Value;
            mySqlCommand.Parameters.Add("@Description", SqlDbType.NVarChar, 200).Value = txtDescription.Text;
            mySqlCommand.Parameters.Add("@Total", SqlDbType.Int, 50).Value = lblTongTien.Text;

            mySqlCommand.ExecuteNonQuery();
            
            //4.1. Truy vấn lại bảng Order để lấy mã Nhập hàng
            string sSql = "Select top 1 OrderID from tblOrders order by OrderID desc ";
            DataServices dsSearch = new DataServices();
            DataTable dtSearch = dsSearch.RunQuery(sSql);
            string OrderID = dtSearch.Rows[0]["OrderID"].ToString();

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.IsNewRow==false)
                {
                    var mahang = row.Cells[0].Value;
                    var soluong = row.Cells[2].Value;
                    var dongia = row.Cells[3].Value;
                    var thanhtien = row.Cells[4].Value;
                    string sSql2 = "EXECUTE AddOrderDetails @OrderID,@SouvenirID,@Quantity,@Price,@SumPrice";
                    mySqlCommand = new SqlCommand(sSql2, mySqlConnection);
                    //Định nghĩa các tham số
                    mySqlCommand.Parameters.Add("@OrderID", SqlDbType.Int,4).Value = OrderID;
                    mySqlCommand.Parameters.Add("@SouvenirID", SqlDbType.Int, 4).Value = mahang;
                    mySqlCommand.Parameters.Add("@Quantity", SqlDbType.Int, 30).Value = soluong;
                    mySqlCommand.Parameters.Add("@Price", SqlDbType.Int, 50).Value = dongia;
                    mySqlCommand.Parameters.Add("@SumPrice", SqlDbType.Int, 50).Value = thanhtien;

                    mySqlCommand.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Đã lưu thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            string mahang = cboSouvenirs.SelectedValue.ToString();
            string tenhang = cboSouvenirs.Text;
            string soluong = txtQuantity.Text;
            string thanhtien = txtSumPrice.Text;
            string gianhap = txtPrice.Text;

            dataGridView1.ColumnCount = 5;
            dataGridView1.Columns[0].Name = "MaHang";
            dataGridView1.Columns[1].Name = "TenHang";
            dataGridView1.Columns[2].Name = "SoLuong";
            dataGridView1.Columns[3].Name = "GiaNhap";
            dataGridView1.Columns[4].Name = "ThanhTien";

            string[] row = new string[] { mahang, tenhang, soluong, gianhap, thanhtien };
            dataGridView1.Rows.Add(row);
            TongTien();
        }

        private void TinhTien()
        {
            double soluong = 1, dongia = 0;
            // kiểm tra ô số lượng có bị rỗng không, nếu không rỗng thì false ngược lại true
            if (string.IsNullOrEmpty(txtQuantity.Text) == false)
            {
                soluong = Convert.ToDouble(txtQuantity.Text);
            }
            if (string.IsNullOrEmpty(txtPrice.Text) == false)
            {
                dongia = Convert.ToDouble(txtPrice.Text);
            }
            var thanhtien = soluong * dongia;
            txtSumPrice.Text = thanhtien.ToString();
        }
        private void TongTien()
        {
            var tongtien = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.IsNewRow == false)
                {
                    
                    var thanhtien = Convert.ToInt32(row.Cells[4].Value);

                    tongtien = tongtien + thanhtien;
                    
                }

            }
            lblTongTien.Text = tongtien.ToString();
        }
        private void txtQuantity_KeyUp(object sender, KeyEventArgs e)
        {
            TinhTien();
        }

        private void txtSellingPrice_KeyUp(object sender, KeyEventArgs e)
        {
            TinhTien();
        }
        private int rowIndex = 0;
        private void dataGridView1_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                this.dataGridView1.Rows[e.RowIndex].Selected = true;//rownIndex: vị trí dòng
                this.rowIndex = e.RowIndex;
                //this.dataGridView1.CurrentCell= this.dataGridView1.Rows[e.RowIndex].Cells[1];
                this.contextMenuStrip1.Show(this.dataGridView1, e.Location);
                contextMenuStrip1.Show(Cursor.Position);
            }
        }

        private void contextMenuStripDelete_Click(object sender, EventArgs e)
        {
            if (!this.dataGridView1.Rows[this.rowIndex].IsNewRow)
            {
                this.dataGridView1.Rows.RemoveAt(this.rowIndex);
            }
        }
    }
}
