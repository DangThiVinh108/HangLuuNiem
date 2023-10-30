using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//mở thêm các thư viện
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
namespace DA_DoLuuNiem
{
    class DataServices
    {
        private static SqlConnection mySqlConnection;
        private SqlDataAdapter mySqlDataAdapter;
        public DataServices()
        {
            string severName = Environment.MachineName;

            try
            {
                mySqlConnection = new SqlConnection($"Data Source ={severName};Initial Catalog=DoLuuNiem;Integrated Security=True");
                //conn = new SqlConnection($"Data Source={severName};Initial Catalog=LuuNiem;Integrated Security=False;User ID=UserName;Password=YourPassword;");
                mySqlConnection.Open();

            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Error" + ex.Number.ToString());
            }
        }
        
        public DataTable RunQuery(string sSql)
        {
            DataTable myDataTable = new DataTable();
            try
            {
                mySqlDataAdapter = new SqlDataAdapter(sSql, mySqlConnection);
                SqlCommandBuilder sqlCommandBuilder = new SqlCommandBuilder(mySqlDataAdapter);
                mySqlDataAdapter.Fill(myDataTable);
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Error" + ex.Number.ToString());
                return null;
            }
            return myDataTable;
        }
        public void Update(DataTable myDataTable)
        {
            try
            {
                mySqlDataAdapter.Update(myDataTable);
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Error" + ex.Number.ToString());
            }
        }
        public void ExecuteNoneQuery(string sSql)
        {
            SqlCommand mySqlCommand = new SqlCommand(sSql, mySqlConnection);
            try
            {
                mySqlCommand.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Error" + ex.Number.ToString());
            }
        }
    
    }
}
