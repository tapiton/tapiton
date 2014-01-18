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
using System.Web.Configuration;

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
            try
            {
                if (Con.State == ConnectionState.Closed)
                    Con.Open();
            }
            catch (Exception)
            {
                HttpContext.Current.Response.Redirect(WebConfigurationManager.AppSettings["pageURL"].ToString() + "Site/SMTP_ErrorHandling.aspx");
            }
        }
        public DataSet FillDataSet(string txt)
        {
            try
            {
                com = new SqlCommand(txt, SetConnectionString());
                Connect();
                adp = new SqlDataAdapter(com);
                ds = new DataSet();
                adp.Fill(ds);
                disconnect();
                Con.Close();
                
            }
            catch (Exception)
            {
                HttpContext.Current.Response.Redirect(WebConfigurationManager.AppSettings["pageURL"].ToString() + "Site/SMTP_ErrorHandling.aspx");
            }
            return ds;
        }
        public int ExecuteSqlHelper(string spName, object[] values)
        {
            int i = 0;
            try
            {
                i = SqlHelper.ExecuteNonQuery(getConnection(), spName, values);
                disconnect();
                Con.Close();

            }
            catch (Exception)
            {
                HttpContext.Current.Response.Redirect(WebConfigurationManager.AppSettings["pageURL"].ToString() + "Site/SMTP_ErrorHandling.aspx");
            }
            return (i);
        }
        public DataSet ExeSqlHelper(string spName, object[] values)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = SqlHelper.ExecuteDataset(getConnection(), spName, values);
                disconnect();
                Con.Close();

            }
            catch (Exception)
            {
                HttpContext.Current.Response.Redirect(WebConfigurationManager.AppSettings["pageURL"].ToString() + "Site/SMTP_ErrorHandling.aspx");
            }
            return (ds);
        }
        public DataSet ExeSqlHelper(string spName)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = SqlHelper.ExecuteDataset(getConnection(), CommandType.StoredProcedure, spName);
                disconnect();
                Con.Close();

            }
            catch (Exception)
            {
                HttpContext.Current.Response.Redirect(WebConfigurationManager.AppSettings["pageURL"].ToString() + "Site/SMTP_ErrorHandling.aspx");
            }
            return (ds);
        }

        public SqlDataReader ExecuteSqlHelperDR(string spName)
        {
            SqlDataReader DR = null;
            try
            {
                DR = SqlHelper.ExecuteReader(getConnection(), CommandType.StoredProcedure, spName);
            }
            //disconnect();
            //Con.Close();
            catch (Exception)
            {
                HttpContext.Current.Response.Redirect(WebConfigurationManager.AppSettings["pageURL"].ToString() + "Site/SMTP_ErrorHandling.aspx");
            }
            return (DR);
        }
        public SqlDataReader ExecuteSqlHelperDR(string spName, object[] values)
        {
            SqlDataReader DR = null;
            try
            {
                DR = SqlHelper.ExecuteReader(getConnection(), spName, values);
            }
            catch (Exception)
            {
                HttpContext.Current.Response.Redirect(WebConfigurationManager.AppSettings["pageURL"].ToString() + "ServiceNotAvailable.html");
            }
            //disconnect();
            //Con.Close();
            return (DR);
        }

    }
    class MYSQL
    {
    }
}
