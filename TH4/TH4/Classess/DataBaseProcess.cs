using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TH4.Classess
{
    internal class DataBaseProcess
    {
        // khai bao bien toan cuc
        string strConnect = "Data Source=DESKTOP-L11EF0H\\MYLINH;" +
            "Initial Catalog=BanHang;Integrated Security=True";
        SqlConnection sqlConnect = null;

        //phuong thuc mo ket noi

        void OpenConnect()
        {
            sqlConnect = new SqlConnection(strConnect);
            if (sqlConnect.State != ConnectionState.Open)
            {
                sqlConnect.Open();
            }
        }

        // phuong thuc dong ket noi

        void CloseConnect()
        {
            if (sqlConnect.State != ConnectionState.Closed)
            {
                sqlConnect.Close();
                sqlConnect.Dispose();
            }
        }

        // phuong thuc thuc thi cau lenh Select tra ve 1 data table
        public DataTable DataReader(string sql)
        {
            DataTable tblData = new DataTable();
            OpenConnect();
            SqlDataAdapter sqlData = new SqlDataAdapter(sql, sqlConnect);
            sqlData.Fill(tblData);
            CloseConnect();
            return tblData;
        }

        // phuong thuc thuc hien cau lenh dang insert, update, delete
        public void DataChange(string sql)
        {
            OpenConnect();
            SqlCommand sqlcomma = new SqlCommand();
            sqlcomma.Connection = sqlConnect;
            sqlcomma.CommandText = sql;
            sqlcomma.ExecuteNonQuery();
            CloseConnect();
        }
    }
}
