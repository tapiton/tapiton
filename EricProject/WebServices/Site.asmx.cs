using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using BAL;
using DAL;
using BusinessObject;
using System.Data.SqlClient;

namespace EricProject.WebServices
{
    /// <summary>
    /// Summary description for Site
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
     [System.Web.Script.Services.ScriptService]
    public class Site : System.Web.Services.WebService
    {
        [WebMethod]
        public string  SiteFAQ(string Search)
        {
            return "hello";
            
        }
        [WebMethod(enableSession: true)]
        public IList<SiteFAQ.SiteFAQLoad> BindSiteFAQLoad()
        {
            _Site obj = new _Site();
            obj.ID = 0;
            DAL.Site sqlobj = new DAL.Site();
            SqlDataReader DR = sqlobj.BindSiteFAQCategoryName(obj);
            IList<SiteFAQ.SiteFAQLoad> grid = new List<SiteFAQ.SiteFAQLoad>();
            while (DR.Read())
            {
                SiteFAQ.SiteFAQLoad ddc = new SiteFAQ.SiteFAQLoad(DR["Category_Name"].ToString());
                grid.Add(ddc);
            }
            var objConn = DBAccess.InstanceCreation();
            objConn.disconnect();
            return grid;
        }

    }
}
