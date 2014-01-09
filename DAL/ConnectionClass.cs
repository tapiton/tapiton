using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Web;
using Microsoft.ApplicationBlocks.Data;

namespace DAL
{
    public class MSSQL : IDB
    {
        SqlCommand com;
        SqlConnection Con;
        SqlDataAdapter adp;
        string strConnection = String.Empty;
        string strErr = string.Empty;
        DataSet ds;
        
        public SqlConnection SetConnectionString()
        {
            if (HttpContext.Current.Request.IsLocal)
                //strConnection = @"Data Source=100.100.7.7; Initial Catalog=EricReferral;  User Id=sa; Password=Flexsinsa123;Max Pool Size=2000";
                strConnection = ConfigurationSettings.AppSettings["conStr"].ToString();
            else
                //strConnection = @"Data Source=WIN-YHA38JQ60DD\SQL2008; Initial Catalog=socialreferral;  User Id=flexsin; Password=Flex$$#456$;Max Pool Size=2000";
                strConnection = ConfigurationSettings.AppSettings["conStr"].ToString();
            Con = new SqlConnection(strConnection);
            return Con;
        }
        public SqlConnection getConnection()
        {
            Con = SetConnectionString();
            return Con;
        }
        public void disconnect()
        {
            if (Con.State == ConnectionState.Open)
                Con.Close();
        }
        public void Connect()
        {
            if (Con.State == ConnectionState.Closed)
                Con.Open();
        }
        public DataSet FillDataSet(string txt)
        {
            com = new SqlCommand(txt, SetConnectionString());
            Connect();
            adp = new SqlDataAdapter(com);
            ds = new DataSet();
            adp.Fill(ds);
            disconnect();
            Con.Close();
            return ds;
        }
        public int ExecuteSqlHelper(string spName, object[] values)
        {
            int i = SqlHelper.ExecuteNonQuery(getConnection(), spName, values);
            disconnect();
            Con.Close();
            return (i);
        }
        public DataSet ExeSqlHelper(string spName, object[] values)
        {
            DataSet ds = SqlHelper.ExecuteDataset(getConnection(), spName, values);
            disconnect();
            Con.Close();
            return (ds);
        }
        public DataSet ExeSqlHelper(string spName)
        {
            DataSet ds = SqlHelper.ExecuteDataset(getConnection(), CommandType.StoredProcedure, spName);
            disconnect();
            Con.Close();
            return (ds);
        }

        public SqlDataReader ExecuteSqlHelperDR(string spName)
        {
            SqlDataReader DR = SqlHelper.ExecuteReader(getConnection(), CommandType.StoredProcedure, spName);
            //disconnect();
            //Con.Close();
            return (DR);
        }
        public SqlDataReader ExecuteSqlHelperDR(string spName, object[] values)
        {
            SqlDataReader DR = SqlHelper.ExecuteReader(getConnection(), spName, values);
            //disconnect();
            //Con.Close();
            return (DR);
        }

    }
    class MYSQL
    {
    }
}
