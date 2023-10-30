
namespace DA_DoLuuNiem
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mnuCapNhatDuLieu = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuUsers = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuQLHangHoa = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSouvenir = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuQLKhachHang_NhaCC = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCustomers = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSuppliers = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuQLNhap_Mua = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOrders = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuListOrders = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSell = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuCapNhatDuLieu,
            this.mnuQLHangHoa,
            this.mnuQLKhachHang_NhaCC,
            this.mnuQLNhap_Mua});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(877, 28);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // mnuCapNhatDuLieu
            // 
            this.mnuCapNhatDuLieu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuUsers});
            this.mnuCapNhatDuLieu.Name = "mnuCapNhatDuLieu";
            this.mnuCapNhatDuLieu.Size = new System.Drawing.Size(154, 24);
            this.mnuCapNhatDuLieu.Text = "Quản lý người dùng";
            // 
            // mnuUsers
            // 
            this.mnuUsers.Name = "mnuUsers";
            this.mnuUsers.Size = new System.Drawing.Size(224, 26);
            this.mnuUsers.Text = "Người dùng";
            this.mnuUsers.Click += new System.EventHandler(this.mnuUsers_Click);
            // 
            // mnuQLHangHoa
            // 
            this.mnuQLHangHoa.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSouvenir});
            this.mnuQLHangHoa.Name = "mnuQLHangHoa";
            this.mnuQLHangHoa.Size = new System.Drawing.Size(139, 24);
            this.mnuQLHangHoa.Text = "Quản lý hàng hóa";
            // 
            // mnuSouvenir
            // 
            this.mnuSouvenir.Name = "mnuSouvenir";
            this.mnuSouvenir.Size = new System.Drawing.Size(224, 26);
            this.mnuSouvenir.Text = "Hàng hóa";
            this.mnuSouvenir.Click += new System.EventHandler(this.mnuSouvenir_Click);
            // 
            // mnuQLKhachHang_NhaCC
            // 
            this.mnuQLKhachHang_NhaCC.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuCustomers,
            this.toolStripSeparator3,
            this.mnuSuppliers});
            this.mnuQLKhachHang_NhaCC.Name = "mnuQLKhachHang_NhaCC";
            this.mnuQLKhachHang_NhaCC.Size = new System.Drawing.Size(254, 24);
            this.mnuQLKhachHang_NhaCC.Text = "Quản lý khách hàng - nhà cung cấp";
            // 
            // mnuCustomers
            // 
            this.mnuCustomers.Name = "mnuCustomers";
            this.mnuCustomers.Size = new System.Drawing.Size(224, 26);
            this.mnuCustomers.Text = "Khách hàng";
            this.mnuCustomers.Click += new System.EventHandler(this.mnuCustomers_Click);
            // 
            // mnuSuppliers
            // 
            this.mnuSuppliers.Name = "mnuSuppliers";
            this.mnuSuppliers.Size = new System.Drawing.Size(224, 26);
            this.mnuSuppliers.Text = "Nhà cung cấp";
            this.mnuSuppliers.Click += new System.EventHandler(this.mnuSuppliers_Click);
            // 
            // mnuQLNhap_Mua
            // 
            this.mnuQLNhap_Mua.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuOrders,
            this.toolStripSeparator2,
            this.mnuListOrders,
            this.toolStripSeparator1,
            this.mnuSell});
            this.mnuQLNhap_Mua.Name = "mnuQLNhap_Mua";
            this.mnuQLNhap_Mua.Size = new System.Drawing.Size(153, 24);
            this.mnuQLNhap_Mua.Text = "Quản lý nhập - mua";
            // 
            // mnuOrders
            // 
            this.mnuOrders.Name = "mnuOrders";
            this.mnuOrders.Size = new System.Drawing.Size(234, 26);
            this.mnuOrders.Text = "Nhập hàng";
            this.mnuOrders.Click += new System.EventHandler(this.mnuOrders_Click);
            // 
            // mnuListOrders
            // 
            this.mnuListOrders.Name = "mnuListOrders";
            this.mnuListOrders.Size = new System.Drawing.Size(234, 26);
            this.mnuListOrders.Text = "Danh sách nhập hàng";
            this.mnuListOrders.Click += new System.EventHandler(this.mnuListOrders_Click);
            // 
            // mnuSell
            // 
            this.mnuSell.Name = "mnuSell";
            this.mnuSell.Size = new System.Drawing.Size(234, 26);
            this.mnuSell.Text = "Bán hàng";
            this.mnuSell.Click += new System.EventHandler(this.mnuSell_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(231, 6);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(231, 6);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(221, 6);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(877, 486);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.IsMdiContainer = true;
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.Text = "frmMain";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnuCapNhatDuLieu;
        private System.Windows.Forms.ToolStripMenuItem mnuQLHangHoa;
        private System.Windows.Forms.ToolStripMenuItem mnuSouvenir;
        private System.Windows.Forms.ToolStripMenuItem mnuQLKhachHang_NhaCC;
        private System.Windows.Forms.ToolStripMenuItem mnuCustomers;
        private System.Windows.Forms.ToolStripMenuItem mnuSuppliers;
        private System.Windows.Forms.ToolStripMenuItem mnuQLNhap_Mua;
        private System.Windows.Forms.ToolStripMenuItem mnuOrders;
        private System.Windows.Forms.ToolStripMenuItem mnuSell;
        private System.Windows.Forms.ToolStripMenuItem mnuUsers;
        private System.Windows.Forms.ToolStripMenuItem mnuListOrders;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}

