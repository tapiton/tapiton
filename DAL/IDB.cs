using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace DAL
{
    interface IDB
    {
        SqlConnection SetConnectionString();
        SqlConnection getConnection();
        void disconnect();
        void Connect();
        DataSet FillDataSet(string txt);
        int ExecuteSqlHelper(string spName, object[] values);
        DataSet ExeSqlHelper(string spName, object[] values);
        DataSet ExeSqlHelper(string spName);
        SqlDataReader ExecuteSqlHelperDR(string spName);
        SqlDataReader ExecuteSqlHelperDR(string spName, object[] values);
    }
}
